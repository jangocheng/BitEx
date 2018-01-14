using System.Threading.Tasks;
using BitEx.IRepository.User;
using BitEx.Model.User;
using BitEx.Repository.Db;
using Dapper;
using System;
using BitEx.Model.User.Dto;
using BitEx.Dapper.Core;
using BitEx.Dapper;
using BitEx.Core;
using BitEx.Core.Result;
using BitEx.Core.Utils;
using BitEx.Core.Security;
using BitEx.Framework.Errors;

namespace BitEx.Repository.User
{
    public class UserRepository : IUserRepository
    {
        public async Task<bool> Exists(string userId)
        {
            var sql = "select count(0) from coin_user where id=@Id";
            using (var conn = PSQLDbBase.GetCoreConnection().Base)
            {
                return await conn.ExecuteScalarAsync<int>(sql, new { Id = userId }) > 0;
            }
        }
        #region 注册登陆
        public async Task<bool> EmailIsUsed(string email)
        {
            const string sql = "email=@Email";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                return await conn.ExistsAsync<UserInfo>(sql, new { Email = email });
            }
        }
        public async Task<bool> NickNameIsUsed(string nickName)
        {
            const string sql = "nickname=@NickName";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                return await conn.ExistsAsync<UserInfo>(sql, new { NickName = nickName });
            }
        }
        public async Task UpdateNickName(string userId, string nickName)
        {
            const string sql = "UPDATE coin_user SET nickname=@NickName where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { NickName = nickName, UserId = userId });
            }
        }
        public async Task UpdateLangType(string userId, Lang lang)
        {
            const string sql = "UPDATE coin_user SET langtype=@LangType where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { LangType = lang, UserId = userId });
            }
        }
        public async Task UpdateLoginInfo(string userId, string lastLoginIp)
        {
            const string sql = "UPDATE coin_user SET lastloginip=@LastLoginIp where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { LastLoginIp = lastLoginIp, UserId = userId });
            }
        }
        public async Task<UserInfo> GetById(string id)
        {
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                return await conn.SingleOrDefaultAsync<UserInfo>(id);
            }
        }

        public async Task<string> GetIdByAccount(string account)
        {
            const string sql = "SELECT id from coin_user where email=@Account or (phone=@Account and IsPhoneRegistered=true)";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                return await conn.Base.ExecuteScalarAsync<string>(sql, new { Account = account });
            }
        }

        public async Task<Result<string>> RegisterByEmail(string countryCode, string email, string password, short langType, string promoter = null, DateTime promoterdividendendtime = default(DateTime))
        {
            var registered = await EmailIsUsed(email);
            if (registered)
            {
                return await Task.FromResult(Result<string>.Error((int)UserError.EmailUsed, null));
            }
            if (!string.IsNullOrEmpty(promoter))
            {
                if (!await Exists(promoter))
                {
                    promoter = null;
                }
            }
            var user = new UserInfo();
            user.CountryCode = countryCode;
            user.Email = email;
            var salt = RandomHelper.CreateString(8);
            user.Id = OGuid.GenerateNewId().ToString();
            user.Salt = salt;
            user.Password = PasswordManager.Encrypt(password, salt);
            user.CreateBy = "System";
            user.CreateTime = DateTime.UtcNow;
            user.UpdateBy = "System";
            user.UpdateTime = DateTime.UtcNow;
            user.VipLevel = 1;
            user.Status = 2;
            user.VerifyLevel = 0;
            user.Points = 0;
            user.IsPhoneRegistered = false;
            user.LangType = langType;
            user.Promoter = promoter;
            user.PromoterDividendEndTime = promoterdividendendtime;
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.InsertAsync(user);
            }
            return await Task.FromResult(Result<string>.COk(user.Id));
        }

        #endregion
        #region 账号安全
        public async Task BindOtp(string userId, string otpSecretKey)
        {
            const string sql = "UPDATE coin_user SET otpsecretkey=@SecretKey where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { SecretKey = otpSecretKey, UserId = userId });
            }
        }
        public async Task UnBindOtp(string userId)
        {
            const string sql = "UPDATE coin_user SET otpsecretkey='' where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { UserId = userId });
            }
        }
        public async Task UpdatePassword(string userId, string password)
        {
            const string sql = "UPDATE coin_user SET password=@Password where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { Password = password, UserId = userId });
            }
        }
        public async Task UpdateStatus(string userId, short status, short lockType = 0)
        {
            const string sql = "UPDATE coin_user SET status=@Status,locktype=@LockType where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { Status = status, LockType = lockType, UserId = userId });
            }
        }
        public async Task UpdateCerInfo(string userId, string realName, short idType, string idNo, int verifylevel)
        {
            const string sql = "UPDATE coin_user SET idno=@IdNo,idtype=@Idtype,realname=@Realname,verifylevel=@Verifylevel where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { IdNo = idNo, Idtype = idType, Realname = realName, Verifylevel = verifylevel, UserId = userId });
            }
        }
        #endregion
        #region 交易
        public async Task UpdateTradePassword(string userId, string tradePassword)
        {
            const string sql = "UPDATE coin_user SET tradepassword=@Tradepassword where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { Tradepassword = tradePassword, UserId = userId });
            }
        }
        public async Task SetTradePassword(string userId, string password, short passwordType)
        {
            const string sql = "UPDATE coin_user SET tradepassword=@Tradepassword,tradepasswordtype=@Type where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { Tradepassword = password, Type = passwordType, UserId = userId });
            }
        }
        public async Task UpdateTradePasswordType(string userId, short passwordType)
        {
            const string sql = "UPDATE coin_user SET tradepasswordtype=@Type where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { Type = passwordType, UserId = userId });
            }
        }
        public async Task UpdatePriceLimit(string userId, float priceLimit)
        {
            const string sql = "UPDATE coin_user SET pricelimit=@Pricelimit where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { Pricelimit = priceLimit, UserId = userId });
            }
        }

        public async Task<Page<UserDto>> GetPageList(string email, string realName, string phone, string id, long page, long pageSize)
        {
            var builder = new SqlBuilder();
            var template = builder.AddTemplate("SELECT * FROM coin_user /**where**/ ORDER BY id ASC");
            if (!string.IsNullOrEmpty(realName))
                builder.Where("realname like @RealName", new { RealName = "%" + realName + "%" });
            if (!string.IsNullOrEmpty(email))
                builder.Where("email like @Email", new { Email = "%" + email + "%" });
            if (!string.IsNullOrEmpty(phone))
                builder.Where("phone like @Phone", new { Phone = "%" + phone + "%" });
            if (!string.IsNullOrEmpty(id))
                builder.Where("id=@Id", new { Id = id });
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                return await conn.QueryPageAsync<UserDto>(page, pageSize, template.RawSql, template.Parameters);
            }
        }

        public async Task<DynamicUserInfo> GetDynamicInfo(string userId)
        {
            const string sql = "SELECT nickname,points,langtype,lastloginip,lastloginarea,liveloginpointtime from coin_user where id=@Id";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                return await conn.Base.QuerySingleOrDefaultAsync<DynamicUserInfo>(sql, new { Id = userId });
            }
        }
        #endregion
        public async Task UpdatePoints(string userId, decimal points, DateTime? liveloginpointtime = null)
        {
            const string sql = "UPDATE coin_user SET points=@Points where id=@UserId";
            const string sqlOne = "UPDATE coin_user SET points=@Points,liveloginpointtime=@Time where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                if (liveloginpointtime == null)
                    await conn.Base.ExecuteAsync(sql, new { Points = points, UserId = userId });
                else
                    await conn.Base.ExecuteAsync(sqlOne, new { Points = points, Time = liveloginpointtime.Value, UserId = userId });
            }
        }
        public async Task<Page<SimpleUserInfo>> GetPageByPromoter(long page, long pageSize, string promoterId)
        {
            const string sql = "SELECT id,Email,Phone,isphoneregistered,CreateTime from coin_user where promoter=@PromoterId order by createtime DESC";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                return await conn.QueryPageAsync<SimpleUserInfo>(page, pageSize, sql, new { PromoterId = promoterId });
            }
        }

        public async Task UpdateVipLevel(UserVipLevel level, string userId)
        {
            const string sql = "UPDATE coin_user SET viplevel=@Viplevel where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { Viplevel = (short)level, UserId = userId });
            }
        }

        public async Task UpdatePromoteLevel(PromoteLevel level, string userId)
        {
            const string sql = "UPDATE coin_user SET promotelevel=@Promotelevel where id=@UserId";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                await conn.Base.ExecuteAsync(sql, new { Promotelevel = (short)level, UserId = userId });
            }
        }
    }
}
