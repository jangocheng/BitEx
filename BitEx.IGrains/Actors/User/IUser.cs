using System.Threading.Tasks;
using Orleans;
using BitEx.IGrain.Entity.User;
using BitEx.IGrain.States;
using System.Collections.Generic;
using System;
using BitEx.IGrain.Entity.User.DTO;
using BitEx.Core;
using BitEx.Core.Result;
using BitEx.Core.Jwt;
using BitEx.Model.User;
using BitEx.Framework.Notice;

namespace BitEx.IGrain.Actors
{
    public interface IUser : IGrainWithStringKey
    {
        #region 注册登陆
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="aud">token的使用场景(web,app,open api)</param>
        /// <param name="exp">过期时间</param>
        /// <param name="ipLim">ip绑定</param>
        /// <returns></returns>
        Task<string> CreateJwtToken(Audience aud, long exp, string ipLim = null);
        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="head"></param>
        /// <param name="payload"></param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        Task<Result<int>> VerifyJwt(Lang lang, Header head, Payload payload, string sign);
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns>如果登陆成功，item1里含有jwt token，请保存到Cookie或每次请求的时候携带</returns>
        Task<Tuple<LoginStatus, string>> Login(Lang lang, Audience source, string password, string ip, bool bindIp = false);
        /// <summary>
        /// 二次登陆验证
        /// </summary>
        /// <param name="source">请求来源</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        Task<Result<string>> SecondVerify(Lang lang, Audience source, string code);
        /// <summary>
        /// 获取token的安全信息
        /// </summary>
        /// <param name="aud"></param>
        /// <returns></returns>
        Task<UserRealtimeInfo> GetRealtimeData(Audience aud);
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
        Task SetPromoteLevel(PromoteLevel level, int operatorId, string remark);
        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        Task SetLangType(Lang lang);
        /// <summary>
        /// 获取基本信息
        /// </summary>
        /// <returns></returns>
        Task<UserBaseInfo> GetBaseInfo();
        /// <summary>
        /// 设置用户昵称
        /// </summary>
        /// <param name="nickName">用户昵称</param>
        /// <returns></returns>
        Task<Result<string>> SetNickName(Lang lang, string nickName);
        #endregion
        #region 消息通知
        Task<Result<string>> VerifyEmailCaptcha(string code, Lang lang);
        Task<Result<string>> SendEmailCaptcha(Lang lang);
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
        Task<Result<string>> ApplyCertification(CertificationType type, List<string> images, string data, Lang lang, bool needAudit = true);
        /// <summary>
        /// 审核身份认证信息
        /// </summary>
        /// <param name="type">认证类别</param>
        /// <param name="result">认证结果(通过|不通过)</param>
        /// <param name="managerId">审核管理员Id</param>
        /// <param name="auditRemark">审核备注</param>
        /// <returns></returns>
        Task<Result<string>> AuditCertificationInfo(bool result, int managerId, string auditRemark);
        Task<CertificationInfo> GetCertification();
        #endregion
        #region 账号安全
        Task<string> GetOtpSecretKey();
        Task<Result<string>> BindOtp(Audience source, string code, string captcha, Lang lang);
        Task UnBindOtp(string remark);
        Task<bool> VerifyOtp(string code);
        /// <summary>
        /// 解除锁定
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="remark">备注</param>
        /// <param name="mandatory">是否强制解锁</param>
        /// <returns></returns>
        Task<Result<string>> UnLock(Lang lang, string remark, bool mandatory);
        Task<Result<int>> VerifyTradePassword(Lang lang, string password);
        Task<Result<string>> VerifyTradePasswordAndOtp(Lang lang, string tradePassword, string code);
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
        Task<Result<string>> UpdatePassword(Lang lang, string oldPassword, string newPassword);
        Task<Result<string>> SetTradePwd(string password, TradePasswordType auditType);
        Task<Result<string>> SetTradePwdType(Lang lang, string password, TradePasswordType auditType);
        Task<Result<string>> UpdateTradePwd(Lang lang, string oldPassword, string newPassword);
        Task<Result<string>> ForgotTradePasswordByOtp(Lang lang, string password, string otpCode);

        Task Lock(UserLockType type, string remark);
        #endregion

        #region 交易部分
        Task<Result<string>> CoinWithdrawalApply(Lang lang, string password, string validCode, string currencyId, decimal volume, decimal limitExchangeAmount, string address, string memo = null);
        /// <summary>
        /// 获取推广人ID
        /// </summary>
        /// <returns></returns>
        Task<string> GetPromoter();
        Task<List<BankCardInfo>> GetBankCardList();
        Task DeleteBankCard(string id);
        Task<Result<string>> AddBankCard(Lang lang, string country, string bank, string cardNumber, string noteInfo);
        Task<UserWithdrawalLimit> GetWithdrawLimit();
        /// <summary>
        /// 订单价格校验
        /// </summary>
        /// <param name="marketId">市场编号</param>
        /// <param name="type">订单类型</param>
        /// <param name="price">订单价格</param>
        /// <returns></returns>
        Task<bool> OrderPriceVerify(string marketId, OrderType type, decimal price);
        #endregion

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
        /// <returns></returns>
        Task Notice(NoticeTplKey key, Dictionary<string, string> values, Lang lang = Lang.Default);
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
    }
}
