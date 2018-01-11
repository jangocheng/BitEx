using System.Threading.Tasks;
using Orleans;
using BitEx.IGrain.Entity.User;
using BitEx.IGrain.States;
using Coin.Core;
using System.Collections.Generic;
using Orleans.Concurrency;
using System;
using BitEx.IGrain.Entity.User.DTO;
using Coin.Core.Notice;
using Coin.Framework.ThirdParty;
using BitEx.IGrain.Entity.Notice;

namespace BitEx.IGrain.Actors
{
    public interface IUser : IGrainWithStringKey
    {
        #region 注册登陆
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="source">来源(手机or web)</param>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <param name="needSecondVerify">是否需要二次身份验证</param>
        /// <param name="bindIp">是否绑定IP</param>
        /// <returns>如果登陆成功，item1里含有加密串，请保存到Cookie</returns>
        Task<Tuple<LoginStatus, string>> Login(Sources source, string password, string ip, bool needSecondVerify = true, bool bindIp = false);
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="loginKey">登陆的加密串，保存在Cookie里</param>
        /// <returns></returns>
        Task<LoginVerifyStatus> LoginVerify(Sources source, string loginKey, string ip);
        /// <summary>
        /// 二次登陆验证
        /// </summary>
        /// <param name="source">请求来源</param>
        /// <param name="isOtp">是否是otp验证</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        Task<TResult> SecondVerify(Sources source, bool isOtp, string code);
        Task<LoginVerifyType> GetLoginVerify();
        Task Loginout(Sources source);
        #endregion

        #region 账号基本信息操作
        /// <summary>
        /// 设置会员等级
        /// </summary>
        /// <param name="level">登记</param>
        /// <param name="operatorId">操作人Id</param>
        /// <param name="remark">操作备注</param>
        /// <returns></returns>
        Task SetVipLevel(UserVipLevel level, int operatorId, string remark);
        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task SetLangType(LangType type);
        /// <summary>
        /// 获取当前用户的语言设置
        /// </summary>
        /// <returns></returns>
        Task<LangType> GetLangType();
        /// <summary>
        /// 获取登陆信息
        /// </summary>
        /// <returns></returns>
        Task<UserLoginInfo> GetLoginInfo();
        /// <summary>
        /// 设置用户昵称
        /// </summary>
        /// <param name="nickName">用户昵称</param>
        /// <returns></returns>
        Task<TResult> SetNickName(string nickName);
        #endregion

        #region 实名认证
        /// <summary>
        /// 申请身份认证
        /// </summary>
        /// <param name="type">认证类型</param>
        /// <param name="images">认证图片列表</param>
        /// <param name="data">认证数据</param>
        /// <param name="needAudit">是否需要审核</param>
        /// <returns></returns>
        Task<TResult> ApplyCertification(CertificationType type, List<string> images, string data, bool needAudit = true);
        /// <summary>
        /// 审核身份认证信息
        /// </summary>
        /// <param name="type">认证类别</param>
        /// <param name="result">认证结果(通过|不通过)</param>
        /// <param name="managerId">审核管理员Id</param>
        /// <param name="auditRemark">审核备注</param>
        /// <returns></returns>
        Task<TResult> AuditCertificationInfo(CertificationType type, bool result, int managerId, string auditRemark);
        /// <summary>
        /// 获取实名认证信息列表
        /// </summary>
        /// <returns></returns>
        Task<List<CertificationInfo>> GetCertificationList();
        /// <summary>
        /// 根据认证类型获取身份认证信息
        /// </summary>
        /// <param name="type">认证类型</param>
        /// <returns></returns>
        Task<CertificationInfo> GetCertification(int type);
        #endregion
        #region 账号安全
        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="forgotType">忘记密码方式</param>
        /// <param name="idNo">身份证号码</param>
        /// <param name="lang"></param>
        /// <returns>0:手机,1:邮箱</returns>
        Task<TResult> SendForgotPasswordCaptcha(BindType forgotType, string idNo = null, LangType lang = LangType.ZH, bool isVoice = false);
        Task<BindType> GetAuthType();
        Task<string> GetCountryCode();
        /// <summary>
        /// 判断用户是否绑定了身份证
        /// </summary>
        /// <returns></returns>
        Task<bool> IsBindIDNo();
        Task<TResult> ForgotPasswordVerify(BindType type, string code, string idNo = null);
        /// <summary>
        /// 强制修改密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        Task UpdatePassword(string password);
        /// <summary>
        /// 通过旧密码修改密码
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<TResult> UpdatePassword(string oldPassword, string newPassword);
        Task<TResult> SetTradePwd(string password, TradePasswordType auditType);
        Task<TResult> SetTradePwdType(string password, TradePasswordType auditType);
        Task<TResult> UpdateTradePwd(string oldPassword, string newPassword);
        Task<TResult> SendForgotTradePasswordCaptcha(bool isVoice = false);
        Task<TResult> ForgotTradePassword(string password, string code);
        Task<TResult> ForgotTradePasswordByOtp(string password, string otpCode);
        Task<TResult> VerifyTradePassword(string password);
        Task<string> GetOtpSecretKey();
        Task<TResult> BindOtp(Sources source, string code, string captcha);
        Task UnBindOtp(string remark);
        Task<bool> IsBindOtp();
        Task<bool> VerifyOtp(string code);
        Task<bool> IsPhoneRegistered();
        Task<TResult> EmailActiveApply(string email);
        Task<TResult> BindEmail(string captcha);
        Task<TResult> UnBindEmail(string remark);
        Task<TResult> SendEmailCaptcha();
        Task<TResult> VerifyEmailCaptcha(string code);
        /// <summary>
        /// 激活手机申请
        /// </summary>
        /// <returns></returns>
        Task<TResult> PhoneActiveApply(string phone, bool isVoice = false);
        Task<TResult> BindPhone(Sources source, string code);
        Task<TResult> UnBindPhone(string remark);
        /// <summary>
        /// 发送普通手机验证码
        /// </summary>
        /// <returns></returns>
        Task<TResult> SendPhoneCaptcha(bool isVoice = false);
        /// <summary>
        /// 验证码普通手机验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<TResult> VerifyPhoneCaptcha(string code);
        Task Lock(UserLockType type, string remark);
        Task<TResult> UnLock(string remark, bool mandatory);
        Task<UserLockType> GetLockType();
        Task<TradePasswordType> GetTradePasswordType();
        #endregion

        #region 用户操作日志
        [AlwaysInterleave]
        Task WriteUserLog(string logTplKey, Dictionary<string, string> values, UserLogType type, UserLogLevel level);
        Task<List<UserLog>> GetUserLogs(UserLogType type, int page, int pageSize);
        Task<long> GetUserLogsTotal(UserLogType type);
        #endregion

        #region 交易部分
        Task<TResult> WithdrawalApplyByOtp(string currencyId, string bankId, decimal amount, string password, string validCode);
        Task<TResult> WithdrawalApplyByPhone(string currencyId, string bankId, decimal amount, string password, string validCode);
        Task WithdrawalCancel(string messageId, decimal amount, DateTime withdrawlTime);
        Task<TResult> CoinWithdrawalApply(string currencyId, decimal volume, string address, string memo = null);
        /// <summary>
        /// 获取推广人ID
        /// </summary>
        /// <returns></returns>
        Task<string> GetPromoter();
        Task<List<BankCardInfo>> GetBankCardList();
        Task DeleteBankCard(string id);
        Task<TResult> AddBankCard(BankType bankType, string bank, string province, string city, string branchBank, string cardNumber);
        Task<decimal> GetWithdrawLimit();
        /// <summary>
        /// 订单价格校验
        /// </summary>
        /// <param name="marketId">市场编号</param>
        /// <param name="type">订单类型</param>
        /// <param name="price">订单价格</param>
        /// <returns></returns>
        Task<bool> OrderPriceVerify(string marketId, OrderType type, decimal price);
        #endregion

        #region 用户信息获取
        /// <summary>
        /// 获取vip等级
        /// </summary>
        /// <returns></returns>
        Task<UserVipLevel> GetVipLevel();
        /// <summary>
        /// 获取用户认证等级
        /// </summary>
        /// <returns></returns>
        Task<int> GetVerifyLevel();
        /// <summary>
        /// 获取用户手机号码
        /// </summary>
        /// <returns></returns>
        Task<string> GetPhone();

        #endregion
        Task<TResult> VerifyTradePasswordAndPhoneCaptcha(string tradePassword, string code);
        Task<TResult> VerifyTradePasswordAndOtp(string tradePassword, string code);

        Task SetUserConfig(UserConfigEnum key, string value);
        Task<string> GetUserConfig(UserConfigEnum key);
        Task<List<UserConfig>> GetUserConfigList();
        Task<List<string>> GetUniquePointsKeyList();
        Task AddPoints(decimal points, string remark, string unique = null, string msgId = null);
        Task<decimal> GetPoints();
        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="key">模板key</param>
        /// <param name="values">模板值</param>
        /// <param name="noticeType">通知类型</param>
        /// <param name="isVoice">是否是语音通知</param>
        /// <returns></returns>
        Task Notice(NoticeKey key, Dictionary<string, string> values, NoticeType noticeType = NoticeType.All, string emailAddress = null, string phoneNumber = null, bool isVoice = false);
        /// <summary>
        /// 添加用户活跃市场
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        Task AddActiveMarket(string marketId);
        /// <summary>
        /// 获取所有还在活跃的市场列表
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetActiveMarketList();
        /// <summary>
        /// 获取用户的Api开放key列表
        /// </summary>
        /// <returns></returns>
        Task<List<OpenKeyInfo>> GetOpenList();
        /// <summary>
        /// 获取用户的open key信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OpenKeyInfo> GetOpenKey(string id);
        /// <summary>
        /// 创建Open key
        /// </summary>
        /// <param name="lable">标签</param>
        /// <param name="limits">key的权限</param>
        /// <param name="ips">绑定的ip</param>
        /// <returns></returns>
        Task<TResult<OpenKeyInfo>> CreateOpenKey(string lable, int[] limits, string[] ips);
        /// <summary>
        /// 删除openKey信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveOpenKey(string id);
        /// <summary>
        /// 绑定IP
        /// </summary>
        /// <param name="id">openkey的id</param>
        /// <param name="ips">ip地址列表</param>
        /// <returns></returns>
        Task BindOpenKeyIP(string id, string[] ips);
        /// <summary>
        /// 开放API验证
        /// </summary>
        /// <param name="apiPath">API路径</param>
        /// <param name="id"></param>
        /// <param name="ip">请求的IP地址</param>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        Task<OpenKeyVerifyResult> VerifyOpenKey(string apiPath, string id, string ip, long timestamp = 0);
    }
}
