using System.Threading.Tasks;
using System.Collections.Generic;
using BitEx.IRepository.User;
using BitEx.Model.User;
using Coin.Core;
using BitEx.Repository.Db;
using Dapper;

namespace BitEx.Repository.User
{
    public class UserVipRepository : IUserVipRepository
    {
        static LocalCache<Dictionary<int, UserVipFee>> tickerCache = new LocalCache<Dictionary<int, UserVipFee>>();
        public async Task<UserVipFee> GetAsync(int id)
        {
            var data = await tickerCache.GetAsync(6000, async () =>
              {
                  var dict = new Dictionary<int, UserVipFee>();
                  var list = await this.GetListAsync();
                  foreach (var item in list)
                  {
                      dict.Add(item.Id, item);
                  }
                  return dict;
              });
            return data[id];
        }
        public async Task<IEnumerable<UserVipFee>> GetListAsync()
        {
            using (var conn = PSQLDbBase.GetCoreConnection().Base)
            {
                string sql = @"SELECT * FROM Coin_VipFee";
                return await conn.QueryAsync<UserVipFee>(sql);
            }
        }
        public async Task<bool> AddAsync(int id, string vipName, decimal withdrawRate, decimal depositRate, decimal makerRate, decimal takerRate)
        {
            using (var conn = PSQLDbBase.GetCoreConnection().Base)
            {
                string sql = @"INSERT INTO Coin_VipFee(Id,VipName,WithdrawRate,DepositRate,MakerRate,TakerRate,CreatedAt,UpdatedAt) VALUES(@Id,@VipName,@WithdrawRate,@DepositRate,@MakerRate,@TakerRate,NOW(),NOW())";
                return await conn.ExecuteAsync(sql, new { Id = id, VipName = vipName, WithdrawRate = withdrawRate, DepositRate = depositRate, MakerRate = makerRate, TakerRate = takerRate }) > 0;
            }
        }
        public async Task<bool> UpdateAsync(int id, string vipName, decimal withdrawRate, decimal depositRate, decimal makerRate, decimal takerRate)
        {
            using (var conn = PSQLDbBase.GetCoreConnection().Base)
            {
                string sql = @"UPDATE Coin_VipFee SET VipName=@VipName,WithdrawRate=@WithdrawRate,DepositRate=@DepositRate,MakerRate=@MakerRate,TakerRate=@TakerRate,UpdatedAt=NOW() WHERE Id=@Id";
                return await conn.ExecuteAsync(sql, new { Id = id, VipName = vipName, WithdrawRate = withdrawRate, DepositRate = depositRate, MakerRate = makerRate, TakerRate = takerRate }) > 0;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using (var conn = PSQLDbBase.GetCoreConnection().Base)
            {
                string sql = @"DELETE FROM Coin_VipFee WHERE Id=@Id";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }
    }
}
