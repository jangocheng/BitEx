using System.Threading.Tasks;
using BitEx.Model.User;
using BitEx.Dapper.Core;
using BitEx.Model.User.Dto;
using System;
using BitEx.Core.Result;
using BitEx.Core;

namespace BitEx.IRepository.User
{
    public interface IUserRepository
    {
        Task<bool> Exists(string userId);
        Task<bool> EmailIsUsed(string email);
        Task<bool> NickNameIsUsed(string nickName);
        Task UpdateNickName(string userId, string nickName);
        Task UpdateLangType(string userId, Lang lang);
        /// <summary>
        /// 修改VIP等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Task UpdateVipLevel(UserVipLevel level, string userId);
        /// <summary>
        /// 修改推广专员等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Task UpdatePromoteLevel(PromoteLevel level, string userId);
        /// <summary>
        /// 更新用户的登陆信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="lastLoginIp">最后登陆ip地址</param>
        /// <returns></returns>
        Task UpdateLoginInfo(string userId, string lastLoginIp);
        Task<Result<string>> RegisterByEmail(string countryCode, string email, string password, short langType, string promoter = null, DateTime promoterdividendendtime = default(DateTime));
        Task<string> GetIdByAccount(string account);
        Task<UserInfo> GetById(string id);
        Task<DynamicUserInfo> GetDynamicInfo(string userId);
        #region 账号安全
        Task BindOtp(string userId, string otpSecretKey);
        Task UnBindOtp(string userId);
        Task UpdatePassword(string userId, string password);
        Task UpdateStatus(string userId, short status, short lockType = 0);
        Task UpdateCerInfo(string userId, string realName, short idType, string idNo, int verifylevel);
        #endregion
        #region 交易
        Task SetTradePassword(string userId, string password, short passwordType);
        Task UpdateTradePasswordType(string userId, short passwordType);
        Task UpdateTradePassword(string userId, string tradePassword);
        Task UpdatePriceLimit(string userId, float priceLimit);
        #endregion
        #region 列表数据
        Task<Page<UserDto>> GetPageList(string email, string realName, string phone, string id, long page, long pageSize);
        #endregion
        Task UpdatePoints(string userId, decimal points, DateTime? liveloginpointtime = null);
        /// <summary>
        /// 根据推广人ID获取用户列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="promoterId"></param>
        /// <returns></returns>
        Task<Page<SimpleUserInfo>> GetPageByPromoter(long page, long pageSize, string promoterId);
    }
}
