using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitEx.Dapper.Core;
using Coin.Model.Account;
using Coin.Model.User.Dto;

namespace BitEx.IGrain.Repository
{
    public interface IDividendRepository
    {
        Task<bool> CreateAsync(string id, string userId, string currencyId, decimal dividendVolume, DateTime dividendTime);
        Task<Page<PromoterDividendDto>> GetListAsync(string userId, long pageIndex, long pageSize);
    }
}
