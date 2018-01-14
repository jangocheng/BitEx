using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitEx.Dapper;
using BitEx.IRepository.User;
using BitEx.Model.User;
using BitEx.Repository.Db;
using Dapper;

namespace BitEx.Repository.User
{
    public class PointsRepository : IPointsRepository
    {

        public async Task<bool> CreateAsync(MyPoints point)
        {
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                var result = await conn.InsertAsync(point);
                return Convert.ToInt32(result) > 0;
            }
        }
        public async Task<bool> Exists(string userId, string unique)
        {
            const string sql = "SELECT count(*) FROM coin_points where userid=@UserId and ukey=@UKey";
            using (var conn = PSQLDbBase.GetCoreConnection())
            {
                return (await conn.Base.ExecuteScalarAsync<int>(sql, new { UserId = userId, UKey = unique })) > 0;
            }
        }

        public async Task<List<MyPoints>> GetTopListAsync(string userid)
        {
            using (var conn = PSQLDbBase.GetCoreConnection().Base)
            {
                const string sql = "SELECT Id,UserId,Points,Createdat,Remark FROM Coin_Points WHERE Userid=@Userid ORDER BY Createdat DESC LIMIT 10;";
                var data = await conn.QueryAsync<MyPoints>(sql, new { Userid = userid });
                return data.ToList();
            }
        }
    }
}
