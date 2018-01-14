using BitEx.IGrain.Actors;
using BitEx.IGrain.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Ray.MongoES;
using Ray.RabbitMQ;
using BitEx.IGrain;
using BitEx.Core;
using BitEx.Core.Result;
using BitEx.IGrain.Entity.User;
using BitEx.IGrain.Entity.User.DTO;
using Orleans;
using BitEx.Model.User;
using BitEx.IRepository.User;
using BitEx.Core.Security;
using BitEx.Framework.Notice;
using BitEx.IGrain.Events.User;
using BitEx.Core.Jwt;
using Coin.Core.Lib;
using BitEx.Framework.Errors;
using BitEx.Core.Utils;
using BitEx.IRepository.langue;
using BitEx.IRepository.Repository;
using BitEx.Framework.LangKeys;
using Ray.Core.EventSourcing;
using Ray.Grain.EventHandles;

namespace BitEx.Grain
{
    [MongoStorage("Coin_Core", "User")]
    [RabbitPub(QueueCount = 6)]
    public class User : MongoESGrain<string, UserState, MessageInfo>, IUser
    {
        IUserRepository userRepository;
        ILangContainer langContainer;
        IServiceProvider serviceProvider;
        public User(IUserRepository uRepository, ILangContainer langContainer, IServiceProvider svProvider)
        {
            this.userRepository = uRepository;
            this.langContainer = langContainer;
            this.serviceProvider = svProvider;
        }
        protected override string GrainId
        {
            get
            {
                return this.GetPrimaryKeyString();
            }
        }
        static IEventHandle _eventHandle = new UserEventHandle();
        protected override IEventHandle EventHandle => _eventHandle;
        //当前活跃的市场列表
        protected override async Task InitState()
        {
            await base.InitState();
            var user = await userRepository.GetById(this.State.StateId);
            this.State.Lang = (Lang)user.LangType;
            this.State.Email = user.Email;
            this.State.CountryCode = user.CountryCode;
            this.State.PhoneNumber = user.Phone;
            this.State.NeedSecondVerify = !string.IsNullOrEmpty(user.Phone);
            this.State.OtpSecretKey = user.OtpSecretKey;
            this.State.IsBindOtp = !string.IsNullOrEmpty(user.OtpSecretKey);
            this.State.Status = (UserStatus)user.Status;
            if (!this.State.NeedSecondVerify)
            {
                if (!string.IsNullOrEmpty(user.OtpSecretKey))
                {
                    this.State.NeedSecondVerify = true;
                }
            }
            this.State.Password = user.Password;
            this.State.Salt = user.Salt;
            this.State.Points = user.Points;
            if (user.VipLevel == 0) user.VipLevel = 1;
            this.State.VipLevel = (UserVipLevel)user.VipLevel;
            this.State.Promoter = user.Promoter;
            this.State.PromoterDividendEndTime = user.PromoterDividendEndTime;
            this.State.PromoteLevel = (PromoteLevel)user.PromoteLevel;
            this.State.Points = user.Points;
            this.State.RegisterTime = user.CreateTime;
            if (!string.IsNullOrEmpty(user.TradePassword))
            {
                this.State.TradePassword = user.TradePassword;
                if (user.TradePasswordType == 0)
                    this.State.TradePasswordType = TradePasswordType.PerLogin;
                else
                    this.State.TradePasswordType = (TradePasswordType)user.TradePasswordType;
            }
            else
            {
                this.State.TradePasswordType = TradePasswordType.None;
            }
            if (!string.IsNullOrEmpty(user.IdNo))
            {
                this.State.RealName = user.RealName;
                this.State.IDNo = user.IdNo;
            }
            //初始化用户配置
            this.State.UserConfigList.AddRange(new List<UserConfig>() {
                new UserConfig { Key= UserConfigEnum.LoginEmailNotice ,Value="OFF"},
                new UserConfig { Key= UserConfigEnum.TopUpNotice ,Value="ON"},
                new UserConfig { Key= UserConfigEnum.WithrawalNotice ,Value="ON"},
                new UserConfig { Key= UserConfigEnum.NeedSecondaryAuth,Value="ON"},
                new UserConfig { Key=UserConfigEnum.MarketOrderPriceLimit,Value=""}
            });
            await SaveSnapshotAsync();
        }
        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            var nowTime = DateTime.UtcNow.AddMinutes(-30);
            EmailCaptchaTime = nowTime;

            var userInfo = await userRepository.GetDynamicInfo(this.State.StateId);
            if (userInfo != null)
            {
                this.State.NickName = userInfo.NickName;
                this.State.Lang = (Lang)userInfo.LangType;
                this.State.Points = userInfo.Points;
            }
        }
        #region 验证码部分
        public string ForgotPwdCaptcha { get; set; }
        public DateTime ForgotPwdEmailTime { get; set; }
        public DateTime ForgotTradePwdTime { get; set; }
        public string EmailCaptchaCode { get; set; }
        public DateTime EmailCaptchaTime { get; set; }
        public int ErrorCount { get; set; }
        public int TradePasswordErrorCount { get; set; }
        public DateTime LastErrorTime { get; set; }
        public DateTime LastTradePasswordErrorTime { get; set; }
        #endregion
        #region 缓存数据
        const string loginCertCacheKey = "LoginCert_";
        private static TimeSpan ExpireTimeSpan = new TimeSpan(8, 0, 0);
        #endregion
        static Header jwtHead = new Header { Type = "JWT", Alg = "HS256" };
        public Task<string> CreateJwtToken(Audience aud, long exp, string ipLim = null)
        {
            var payload = new Payload
            {
                Id = OGuid.GenerateNewId().ToString(),
                Sub = this.State.StateId,
                Aud = aud,
                Iat = DateTime.UtcNow.Ticks,
                Exp = exp,
                Email = this.State.Email,
                IpLim = string.IsNullOrEmpty(ipLim) ? string.Empty : AESEncrypt.Encrypt(ipLim, this.State.Salt),
                Ro = string.Empty,
                Lim = string.Empty
            };
            var sign = JwtManager.GenerateSignature(jwtHead, payload, this.State.Salt);
            return Task.FromResult($"{jwtHead.ToBase64Json()}.{payload.ToBase64Json()}.{sign}");
        }
        DateTime invokeTimestamp = DateTime.UtcNow; int invokeCount = 0;
        public Task<Result<int>> VerifyJwt(Lang lang, Header head, Payload payload, string sign)
        {
            if (this.State.LockType != UserLockType.None)
            {
                return Task.FromResult(Result<int>.Error((int)UserError.IsLocked, GetErrorMessage(UserError.IsLocked, lang)));
            }
            var nowTime = DateTime.UtcNow;
            if (JwtManager.GenerateSignature(head, payload, this.State.Salt).Equals(sign))
            {
                if (nowTime.Subtract(invokeTimestamp).TotalMinutes > 1)
                {
                    invokeTimestamp = nowTime;
                    invokeCount = 0;
                }
                else
                {
                    invokeCount++;
                }
                return Task.FromResult(Result<int>.Ok(invokeCount));
            }
            else
                return Task.FromResult(Result<int>.Error((int)UserError.JwtTokenError, GetErrorMessage(UserError.JwtTokenError, lang)));
        }
        public Task<UserRealtimeInfo> GetRealtimeData(Audience aud)
        {
            var result = new UserRealtimeInfo();
            if (this.State.NeedSecondVerify)
            {
                var config = this.State.UserConfigList.Single(c => c.Key == UserConfigEnum.NeedSecondaryAuth);
                if (config.Value.Equals("ON"))
                {
                    result.NeedSVerify = true;
                }
                else
                {
                    result.NeedSVerify = false;
                }
            }
            result.BindOtp = !this.State.IsBindOtp;
            result.TradePwdType = this.State.TradePasswordType;
            if (this.State.TradePasswordType == TradePasswordType.None)
            {
                if (!string.IsNullOrEmpty(this.State.TradePassword))
                {
                    this.State.TradePasswordType = TradePasswordType.PerLogin;
                }
            }
            result.TradePwdType = this.State.TradePasswordType;
            result.Certed = this.State.CompleteCertification;
            return Task.FromResult(result);
        }
        private string GetErrorMessage(UserError error, Lang lang)
        {
            return langContainer.GetMessage(lang, nameof(UserError), error.ToString());
        }
        public async Task<Result<string>> SendEmailCaptcha(Lang lang)
        {
            if ((DateTime.UtcNow - EmailCaptchaTime).TotalMinutes < 1)//邮箱验证码1分钟只能发送一次
            {
                return Result<string>.Error((int)UserError.EmailCaptchaSendLimit, GetErrorMessage(UserError.EmailCaptchaSendLimit, lang));
            }
            this.EmailCaptchaCode = RandomHelper.CreateNum(6).ToString();
            var values = new Dictionary<string, string>();
            values.Add("Code", this.EmailCaptchaCode);
            await Notice(NoticeTplKey.CommonCaptcha, values, lang);
            EmailCaptchaTime = DateTime.UtcNow;
            return Result<string>.Ok();
        }
        public Task<Result<string>> VerifyEmailCaptcha(string code, Lang lang)
        {
            if (!string.IsNullOrEmpty(this.EmailCaptchaCode) && this.EmailCaptchaCode.Equals(code))
            {
                if ((DateTime.UtcNow - this.EmailCaptchaTime).TotalMinutes <= 15)
                {
                    return Task.FromResult(Result<string>.Ok());
                }
                else
                {
                    return Task.FromResult(Result<string>.Error((int)UserError.EmailCaptchaTimeout, GetErrorMessage(UserError.EmailCaptchaTimeout, lang)));
                }
            }
            return Task.FromResult(Result<string>.Error((int)UserError.EmailCaptchaError, GetErrorMessage(UserError.EmailCaptchaError, lang)));
        }
        public Task<UserBaseInfo> GetBaseInfo()
        {
            var data = new UserBaseInfo()
            {
                UserId = this.State.StateId,
                RealName = this.State.RealName,
                NickName = this.State.NickName,
                Email = this.State.Email,
                VipLevel = this.State.VipLevel,
                CountryCode = this.State.CountryCode,
                PromoteLevel = this.State.PromoteLevel
            };
            return Task.FromResult(data);
        }

        public async Task<Result<string>> ApplyCertification(CertificationType type, List<string> images, string data, Lang lang, bool needAudit = true)
        {
            if (State.Certification != null)
            {
                if (State.Certification.Status != CertificationStatus.AuditFail)
                {
                    return Result<string>.Error((int)UserError.CertificationRepeatSubmit, GetErrorMessage(UserError.CertificationRepeatSubmit, lang));
                }
                var @event = new CertificationUpdateEvent(type, images, data, needAudit);
                await this.RaiseEvent(@event);
                return Result<string>.Ok();
            }
            else
            {
                var @event = new CertificationApplyEvent(type, images, data, needAudit);
                await this.RaiseEvent(@event);
                return Result<string>.Ok();
            }
        }
        public async Task<Result<string>> AuditCertificationInfo(bool result, int managerId, string auditRemark)
        {
            if (this.State.Certification.Status == CertificationStatus.Apply || (this.State.Certification.Status == CertificationStatus.AuditSucess && !result) || (this.State.Certification.Status == CertificationStatus.AuditFail && result))
            {
                var @event = new CertificationAuditEvent(result, managerId, auditRemark);
                await this.RaiseEvent(@event);
            }
            return Result<string>.Ok();
        }

        public Task<CertificationInfo> GetCertification()
        {
            return Task.FromResult(this.State.Certification);
        }

        public Task<string> GetOtpSecretKey()
        {
            var key = string.Empty;
            if (!this.State.IsBindOtp)
            {
                key = OtpManager.CreateSecretKey();
                this.State.OtpSecretKey = key;
            }
            return Task.FromResult(key);
        }
        public async Task<Result<string>> BindOtp(Audience source, string code, string captcha, Lang lang)
        {
            if (!string.IsNullOrEmpty(this.State.OtpSecretKey))
            {
                if (await VerifyOtp(code))
                {
                    var @event = new BindOtpEvent(this.State.OtpSecretKey);
                    await this.RaiseEvent(@event);
                    return Result<string>.Ok();
                }
                else
                {
                    return Result<string>.Error((int)UserError.OtpCodeError, GetErrorMessage(UserError.OtpCodeError, lang));
                }
            }
            else
            {
                return Result<string>.Error((int)UserError.OtpSecretTimeout, GetErrorMessage(UserError.OtpSecretTimeout, lang));
            }
        }
        //管理员使用接口
        public async Task UnBindOtp(string remark)
        {
            if (!string.IsNullOrEmpty(this.State.OtpSecretKey))
            {
                var @event = new UnBindOtpEvent(remark);
                await this.RaiseEvent(@event);
            }
        }
        string lastOtpCode = string.Empty;
        public Task<bool> VerifyOtp(string code)
        {
            var result = false;
            if (!string.IsNullOrEmpty(this.State.OtpSecretKey) && !lastOtpCode.Equals(code))
                result = OtpManager.VerifyOtp(this.State.OtpSecretKey, code);
            if (result)
                lastOtpCode = code;
            return Task.FromResult(result);
        }
        public async Task<Result<string>> SendForgotPasswordCaptcha(Lang lang, string idNo = null)
        {
            if (this.State.IDNo != null && this.State.IDNo != idNo)
                return Result<string>.Error((int)UserError.IDNoVerifyError, GetErrorMessage(UserError.IDNoVerifyError, lang));
            var values = new Dictionary<string, string>();
            if ((DateTime.UtcNow - this.ForgotPwdEmailTime).TotalMinutes < 15)//邮箱15分钟一次
            {
                return Result<string>.Error((int)UserError.EmailCaptchaSendLimit, GetErrorMessage(UserError.EmailCaptchaSendLimit, lang));
            }
            this.ForgotPwdCaptcha = RandomHelper.CreateString(6);
            values.Add("Code", this.ForgotPwdCaptcha);
            await Notice(NoticeTplKey.CommonCaptcha, values, lang);
            this.ForgotPwdEmailTime = DateTime.UtcNow;
            return Result<string>.Ok();
        }
        public async Task<Result<string>> ForgotPasswordVerify(Lang lang, string captcha, string password, bool isOtp = true, string idNo = null)
        {
            if (this.State.IDNo != null && this.State.IDNo != idNo)
                return Result<string>.Error((int)UserError.IDNoVerifyError, GetErrorMessage(UserError.IDNoVerifyError, lang));
            if (isOtp)
            {
                var verifyResult = await VerifyOtp(captcha);
                if (!verifyResult)
                {
                    return Result<string>.Error((int)UserError.OtpCodeError, GetErrorMessage(UserError.OtpCodeError, lang));
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.ForgotPwdCaptcha))
                {
                    if (this.ForgotPwdCaptcha.Equals(captcha))
                    {
                        if ((DateTime.UtcNow - this.ForgotPwdEmailTime).TotalMinutes > 15)
                        {
                            return Result<string>.Error((int)UserError.EmailCaptchaTimeout, GetErrorMessage(UserError.EmailCaptchaTimeout, lang));
                        }
                        this.ForgotPwdCaptcha = null;
                    }
                }
                else
                {
                    return Result<string>.Error((int)UserError.EmailCaptchaTimeout, GetErrorMessage(UserError.EmailCaptchaTimeout, lang));
                }
            }
            await UpdatePassword(password);
            return Result<string>.Ok();
        }
        public async Task UpdatePassword(string password)
        {
            var newPws = PasswordManager.Encrypt(password, this.State.Salt);
            if (newPws != this.State.Password)
            {
                var @event = new UpdatePasswordEvent(newPws);
                await this.RaiseEvent(@event);
            }
            ErrorCount = 0;
        }
        public async Task<Result<string>> ForgotTradePasswordByOtp(Lang lang, string password, string otpCode)
        {
            if (this.State.IsBindOtp && await VerifyOtp(otpCode))
            {
                if (EncryptTradePassword(password) != this.State.Password)
                {
                    var @event = new UpdateTradePasswordEvent(EncryptTradePassword(password), true);
                    await this.RaiseEvent(@event);
                }
                return Result<string>.Ok();
            }
            else
            {
                return Result<string>.Error((int)UserError.OtpCodeError, GetErrorMessage(UserError.OtpCodeError, lang));
            }
        }
        public async Task<Result<string>> SetTradePwd(string password, TradePasswordType auditType)
        {
            if (string.IsNullOrEmpty(this.State.TradePassword))
            {
                if (auditType == 0) auditType = TradePasswordType.PerLogin;
                var @event = new SetTradePasswordEvent(EncryptTradePassword(password), auditType);
                await this.RaiseEvent(@event);
            }
            return Result<string>.Ok();
        }
        public async Task<Result<string>> SetTradePwdType(Lang lang, string password, TradePasswordType auditType)
        {
            if (VerifyTradePwd(password))
            {
                if (auditType == 0) auditType = TradePasswordType.PerLogin;
                if (auditType != this.State.TradePasswordType)
                {
                    var @event = new UpdateTradePasswordTypeEvent(auditType);
                    await RaiseEvent(@event);
                }
                return Result<string>.Ok();
            }
            else
            {
                return Result<string>.Error((int)UserError.TradePasswordError, GetErrorMessage(UserError.TradePasswordError, lang));
            }
        }
        public async Task<Result<string>> UpdateTradePwd(Lang lang, string oldPassword, string newPassword)
        {
            if (VerifyTradePwd(oldPassword))
            {
                var @event = new UpdateTradePasswordEvent(EncryptTradePassword(newPassword));
                await RaiseEvent(@event);
                return Result<string>.Ok();
            }
            else
            {
                return Result<string>.Error((int)UserError.TradePasswordError, GetErrorMessage(UserError.TradePasswordError, lang));
            }
        }
        public async Task<Result<string>> UpdatePassword(Lang lang, string oldPassword, string newPassword)
        {
            if (PasswordManager.Encrypt(oldPassword, this.State.Salt) == this.State.Password)
            {
                var @event = new UpdatePasswordEvent(PasswordManager.Encrypt(newPassword, this.State.Salt));
                await RaiseEvent(@event);
                return Result<string>.Ok();
            }
            else
            {
                return Result<string>.Error((int)UserError.LoginPasswordError, GetErrorMessage(UserError.LoginPasswordError, lang));
            }
        }

        public Task Lock(UserLockType type, string remark)
        {
            if (this.State.LockType != type)
            {
                var @event = new UserLockEvent(type, this.State.Status | UserStatus.Lock, remark);
                return RaiseEvent(@event).AsTask();
            }
            else
                return Task.CompletedTask;
        }
        public async Task<Result<string>> UnLock(Lang lang, string remark, bool mandatory)
        {
            if ((mandatory && this.State.LockType != UserLockType.None) || this.State.LockType == UserLockType.UserSelf)
            {
                var @event = new UserUnlockEvent(this.State.Status ^ UserStatus.Lock, remark);
                await this.RaiseEvent(@event);
                return Result<string>.Ok();
            }
            else
            {
                return Result<string>.Error((int)UserError.CanNotNormalUnlock, GetErrorMessage(UserError.CanNotNormalUnlock, lang));
            }
        }
        public async Task<Tuple<LoginStatus, string>> Login(Lang lang, Audience source, string password, string ip, bool bindIp = false)
        {
            LoginStatus result = LoginStatus.None;
            string key = null;
            if (ErrorCount >= 4)
            {
                if ((DateTime.UtcNow - LastErrorTime).TotalMinutes < 30)
                    result = LoginStatus.ErrorMoreThanMost;
                else
                    ErrorCount = 0;
            }
            if (result != LoginStatus.ErrorMoreThanMost)
            {
                if (this.State.LockType != UserLockType.None)
                {
                    result = LoginStatus.IsLock;
                }
                else
                {
                    if (PasswordManager.Encrypt(password, this.State.Salt) == this.State.Password)
                    {
                        result = LoginStatus.Success;
                        key = await CreateJwtToken(source, DateTime.UtcNow.AddHours(8).Ticks);
                        //每日登陆积分
                        if (DateTime.UtcNow > this.State.LiveLoginPointTime.Date)
                        {
                            await AddLoginPoints();
                        }

                        var loginValues = new Dictionary<string, string>();
                        loginValues.Add("Source", source.ToString());
                        loginValues.Add("Ip", ip);
                        loginValues.Add("Time", DateTime.UtcNow.Ticks.ToString());

                        var loginConfig = this.State.UserConfigList.Single(c => c.Key == UserConfigEnum.NeedSecondaryAuth);
                        var loginNotifyConfig = this.State.UserConfigList.Single(c => c.Key == UserConfigEnum.LoginEmailNotice);
                        if (loginNotifyConfig.Value.Equals("ON"))
                        {
                            await Notice(NoticeTplKey.LoginEmailNotice, loginValues, lang);//登陆邮件通知
                        }
                        //记录最后登陆信息
                        if (this.State.Lang != lang)
                        {
                            await userRepository.UpdateLangType(this.State.StateId, lang);
                            this.State.Lang = lang;
                        }
                    }
                    else
                    {
                        result = LoginStatus.PasswordError;
                        LastErrorTime = DateTime.UtcNow;
                        ErrorCount += 1;
                        key = ErrorCount.ToString();//返回错误次数
                    }
                }
            }
            return new Tuple<LoginStatus, string>(result, key);
        }

        public async Task<Result<string>> SecondVerify(Lang lang, Audience source, string code)
        {
            var result = await VerifyOtp(code);
            if (!result)
            {
                return Result<string>.Error((int)UserError.OtpCodeError, GetErrorMessage(UserError.OtpCodeError, lang));
            }
            return Result<string>.Ok();
        }

        public Task SetVipLevel(UserVipLevel level, int operatorId, string remark)
        {
            if (this.State.VipLevel != level)
            {
                var @event = new SetVipLevelEvent(level, operatorId, remark);
                return RaiseEvent(@event).AsTask();
            }
            else
            {
                return Task.CompletedTask;
            }
        }
        public Task SetLangType(Lang lang)
        {
            return userRepository.UpdateLangType(GrainId, lang);
        }

        public async Task<Result<string>> SetNickName(Lang lang, string nickName)
        {
            var isUsed = await userRepository.NickNameIsUsed(nickName);
            if (!isUsed)
            {
                await userRepository.UpdateNickName(this.State.StateId, nickName);
                this.State.NickName = nickName;
                return Result<string>.Ok();
            }
            else
            {
                return Result<string>.Error((int)UserError.NickNameIsUsed, GetErrorMessage(UserError.NickNameIsUsed, lang));
            }
        }

        private string EncryptTradePassword(string password)
        {
            return PasswordManager.Encrypt(password + this.State.StateId, this.State.Salt);
        }
        private bool VerifyTradePwd(string password)
        {
            return EncryptTradePassword(password) == this.State.TradePassword;
        }
        public Task<Result<int>> VerifyTradePassword(Lang lang, string password)
        {
            if (TradePasswordErrorCount >= 5)
            {
                if ((DateTime.UtcNow - LastTradePasswordErrorTime).TotalMinutes < 60)
                    return Task.FromResult(Result<int>.Error((int)UserError.TradePwdMoreThanMaxErros, GetErrorMessage(UserError.TradePwdMoreThanMaxErros, lang)));
                else
                    TradePasswordErrorCount = 0;
            }
            if (string.IsNullOrEmpty(password) && this.State.TradePasswordType == TradePasswordType.NoValidation)
                return Task.FromResult(Result<int>.Ok());
            if (!VerifyTradePwd(password))
            {
                LastTradePasswordErrorTime = DateTime.UtcNow;
                TradePasswordErrorCount += 1;
                return Task.FromResult(Result<int>.Error((int)UserError.TradePasswordError, GetErrorMessage(UserError.TradePasswordError, lang), TradePasswordErrorCount));
            }
            return Task.FromResult(Result<int>.Ok());
        }
        public async Task<Result<string>> VerifyTradePasswordAndOtp(Lang lang, string tradePassword, string code)
        {
            if (VerifyTradePwd(tradePassword))
            {
                if (await VerifyOtp(code))
                {
                    return Result<string>.Ok();
                }
                else
                {
                    return Result<string>.Error((int)UserError.OtpCodeError, GetErrorMessage(UserError.OtpCodeError, lang));
                }
            }
            else
            {
                return Result<string>.Error((int)UserError.TradePasswordError, GetErrorMessage(UserError.TradePasswordError, lang));
            }
        }

        private async Task<WithdrawValidResult> WithdrawalLimitValid(decimal amount)
        {
            var result = WithdrawValidResult.None;
            var repository = serviceProvider.GetService<IWithdrawallimitRepository>();
            var limit = await repository.GetLimit(this.State.IsComplateCertification ? (short)1 : (short)0);
            if ((DateTime.UtcNow - State.WithdrawalDayTime).Days == 0)
            {
                if ((this.State.WithdrawalDayLimit + amount) > limit.DayAmount)
                {
                    result = WithdrawValidResult.OverDailyLimit;
                }
            }
            else
            {
                if (amount > limit.DayAmount)
                    result = WithdrawValidResult.OverDailyLimit;
            }
            if (result == WithdrawValidResult.None)
            {
                if (DateTime.UtcNow.Year - State.WithdrawalMonthTime.Year == 0 && DateTime.UtcNow.Month - State.WithdrawalMonthTime.Month == 0)
                {
                    if ((this.State.WithdrawalMonthLimit + amount) > limit.MonthAount)
                    {
                        result = WithdrawValidResult.OverMonthlyLimit;
                    }
                }
                else
                {
                    if (amount > limit.MonthAount)
                        result = WithdrawValidResult.OverMonthlyLimit;
                }
            }
            return result;
        }

        public async Task<Result<string>> CoinWithdrawalApply(Lang lang, string password, string validCode, string currencyId, decimal volume, decimal limitExchangeAmount, string address, string memo = null)
        {
            if (DateTime.UtcNow.Subtract(this.State.ForgotTradePasswordTime).TotalHours <= 24)
            {
                return Result<string>.Error((int)UserError.UpTradePwdWithdrawalLocked24H, GetErrorMessage(UserError.UpTradePwdWithdrawalLocked24H, lang));
            }
            var result = await this.VerifyTradePasswordAndOtp(lang, password, validCode);
            if (!result.IsOk)
            {
                return result;
            }
            var validResult = await WithdrawalLimitValid(limitExchangeAmount);
            if (validResult != WithdrawValidResult.None)
            {
                if (validResult == WithdrawValidResult.OverDailyLimit)
                {
                    return Result<string>.Error((int)UserError.WithdrawalOverDailyLimit, GetErrorMessage(UserError.WithdrawalOverDailyLimit, lang));
                }
                else if (validResult == WithdrawValidResult.OverMonthlyLimit)
                {
                    return Result<string>.Error((int)UserError.WithdrawalOverMonthlyLimit, GetErrorMessage(UserError.WithdrawalOverMonthlyLimit, lang));
                }
            }
            //需要从配置记录currencyId对应的转换市场的Id
            var @event = new CoinWithdrawAppliedEvent(OGuid.GenerateNewId().ToString(), currencyId, this.State.StateId + "_" + currencyId, volume, State.VipLevel, limitExchangeAmount, validResult, address, memo);
            await RaiseEvent(@event);
            return Result<string>.Ok();
        }
        public Task SetPromoteLevel(PromoteLevel level, int operatorId, string remark)
        {
            if (this.State.PromoteLevel != level)
            {
                var @event = new SetPromoteLevelEvent(level, operatorId, remark);
                return RaiseEvent(@event).AsTask();
            }
            else
            {
                return Task.CompletedTask;
            }
        }
        public Task<string> GetPromoter()
        {
            if (!string.IsNullOrEmpty(this.State.Promoter) && this.State.PromoterDividendEndTime != default(DateTime) && this.State.PromoterDividendEndTime >= DateTime.UtcNow)
            {
                return Task.FromResult(this.State.Promoter);
            }
            else
            {
                return Task.FromResult(string.Empty);
            }
        }
        #region 提现
        public Task<List<BankCardInfo>> GetBankCardList()
        {
            if (State.BankCardList == null) return Task.FromResult(new List<BankCardInfo>());
            return Task.FromResult(State.BankCardList);
        }
        public Task DeleteBankCard(string id)
        {
            var @event = new DeleteBankCardEvent(id);
            return RaiseEvent(@event).AsTask();
        }
        public async Task<Result<string>> AddBankCard(Lang lang, string country, string bank, string cardNumber, string noteInfo)
        {
            if (State.BankCardList.Exists(b => b.Country.Equals(country) && b.Bank.Equals(bank) && b.CardNumber.Equals(cardNumber)))
            {
                return Result<string>.Error((int)UserError.BankCardExists, GetErrorMessage(UserError.BankCardExists, lang));
            }
            var @event = new AddBankCardEvent(country, bank, cardNumber, noteInfo);
            await RaiseEvent(@event);
            return Result<string>.Ok();
        }
        public Task<UserWithdrawalLimit> GetWithdrawLimit()
        {
            return Task.FromResult(new UserWithdrawalLimit { WithdrawalDayLimit = this.State.WithdrawalDayLimit, WithdrawalMonthLimit = this.State.WithdrawalMonthLimit });
        }
        #endregion

        public async Task SetUserConfig(UserConfigEnum key, string value)
        {
            if (!string.IsNullOrEmpty(value) && this.State.UserConfigList.Exists(u => u.Key == key))
            {
                var @event = new SetUserConfigEvent(key, value);
                await RaiseEvent(@event);
            }
        }
        public Task<string> GetUserConfig(UserConfigEnum key)
        {
            var result = this.State.UserConfigList.SingleOrDefault(u => u.Key == key);
            return Task.FromResult(result?.Value);
        }
        public Task<List<UserConfig>> GetUserConfigList()
        {
            return Task.FromResult(this.State.UserConfigList);
        }

        #region 积分处理
        public async Task AddPoints(decimal points, string remark, string unique = null, string msgId = null)
        {
            if (points > 0)
            {
                if (!string.IsNullOrEmpty(unique))
                {
                    if (!this.State.UniquePointsKeyList.Contains(unique))
                    {
                        var @event = new AddPointsEvent(points, this.State.Points + points, remark, unique);
                        await RaiseEvent(@event, msgId);
                    }
                }
                else
                {
                    this.State.Points += points;
                    await serviceProvider.GetService<IPointsRepository>().CreateAsync(new MyPoints()
                    {
                        UserId = this.State.StateId,
                        Createdat = DateTime.UtcNow,
                        Points = points,
                        Remark = remark,
                        UKey = string.Empty
                    });
                    await userRepository.UpdatePoints(this.State.StateId, this.State.Points);
                }
            }
        }
        private async Task AddLoginPoints()
        {
            this.State.Points += 2;
            this.State.LiveLoginPointTime = DateTime.UtcNow;
            await serviceProvider.GetService<IPointsRepository>().CreateAsync(new MyPoints()
            {
                UserId = this.State.StateId,
                Createdat = DateTime.UtcNow,
                Points = 2,
                Remark = UserKeys.DailyLoginPoints,
                UKey = string.Empty
            });
            await userRepository.UpdatePoints(this.State.StateId, this.State.Points, DateTime.UtcNow);
        }
        public Task<List<string>> GetUniquePointsKeyList()
        {
            return Task.FromResult(this.State.UniquePointsKeyList);
        }
        public Task<decimal> GetPoints()
        {
            return Task.FromResult(this.State.Points);
        }
        #endregion
        public Task Notice(NoticeTplKey key, Dictionary<string, string> values, Lang lang = Lang.Default)
        {
            if (key == NoticeTplKey.CoinDepositConfirm || key == NoticeTplKey.RmbDepositConfirm)
            {
                if (this.State.UserConfigList.Single(c => c.Key == UserConfigEnum.TopUpNotice).Value != "ON")
                    return Task.CompletedTask;
            }
            if (key == NoticeTplKey.WithdrawComplete)
            {
                if (this.State.UserConfigList.Single(c => c.Key == UserConfigEnum.WithrawalNotice).Value != "ON")
                    return Task.CompletedTask;
            }
            if (lang == Lang.Default) lang = this.State.Lang;
            return NoticeHub.Send(key, values, lang, this.State.Email, this.State.StateId);
        }
        public async Task AddActiveMarket(string marketId)
        {
            if (!this.State.ActiveMarketList.Contains(marketId))
            {
                var @event = new AddOrderMarket(marketId);
                await this.RaiseEvent(@event);
            }
        }
        public async Task<bool> OrderPriceVerify(string marketId, OrderType type, decimal price)
        {
            var orderPriceConfig = this.State.UserConfigList.SingleOrDefault(u => u.Key == UserConfigEnum.MarketOrderPriceLimit);

            if (orderPriceConfig != null && !string.IsNullOrEmpty(orderPriceConfig.Value))
            {
                var priceLimits = orderPriceConfig.Value.Split(',');
                if (priceLimits.Count() == 2)
                {
                    if (priceLimits[0].Equals("ON"))
                    {
                        var marketActor = GrainFactory.GetGrain<IMarket>(marketId);
                        var marketPrice = await marketActor.GetMarketPrice();
                        var limitPercent = decimal.Parse(priceLimits[1]);
                        if (type == OrderType.Ask)
                        {
                            if (price < marketPrice * (1 + limitPercent))
                                return false;
                        }
                        else
                        {
                            if (price > marketPrice * (1 + limitPercent))
                                return false;
                        }
                    }
                }
            }
            return true;
        }
        public Task<List<string>> GetActiveMarketList()
        {
            return Task.FromResult(this.State.ActiveMarketList);
        }
    }
}
