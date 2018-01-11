using Coin.Core;
using System;
using System.Threading.Tasks;
using Coin.Model.User;
using BitEx.Dapper.Core;
using BitEx.IGrain.Entity.User.DTO;
using BitEx.IGrain.Entity.User;
using Coin.Model.User.Dto;

namespace BitEx.IGrain.Repository
{
    public interface IUserRepository
    {
        Task<bool> Exists(string userId);
        Task<bool> EmailIsUsed(string email);
        Task<bool> PhoneIsUsed(string phone);
        Task<bool> NickNameIsUsed(string nickName);
        Task UpdateNickName(string userId, string nickName);
        Task UpdateLangType(string userId, short langType);
        /// <summary>
        /// 更新用户的登陆信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="lastLoginIp">最后登陆ip地址</param>
        /// <param name="lastLoginArea">最后登陆区域</param>
        /// <returns></returns>
        Task UpdateLoginInfo(string userId, string lastLoginIp, string lastLoginArea);
        Task<TResult<string>> RegisterByPhone(string countryCode, string phone, string password, short langType, string promoter = null);
        Task<TResult<string>> RegisterByEmail(string countryCode, string email, string password, short langType, string promoter = null);
        Task<string> GetIdByAccount(string account);
        Task<UserInfo> GetById(string id);
        Task<NoEventData> GetDynamicInfo(string userId);
        #region 账号安全
        Task BindEmail(string userId, string email);
        Task UnBindEmail(string userId);
        Task BindPhone(string userId, string phone, string countryCode);
        Task UnBindPhone(string userId);
        Task BindOtp(string userId, string otpSecretKey);
        Task UnBindOtp(string userId);
        Task UpdatePassword(string userId, string password);
        Task UpdateStatus(string userId, short status, short lockType = 0);
        Task UpdateCerInfo(string userId, string realName, short idType, string idNo, bool? isAdvanced = null);
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
        Task UpdatePoints(string userId, decimal points);
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
