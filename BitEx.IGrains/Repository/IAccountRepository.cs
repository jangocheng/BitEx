using BitEx.Dapper.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface IAccountRepository
    {
        Task<AccountState> GetAsync(string id);
        Task<bool> CreateAsync(string id, string userId, string currencyId, AccountStatus status);
        Task<bool> LockAsync(string id, AccountStatus status);
        Task<bool> ChangeAmountAsync(string id, decimal balance, decimal lastTotalAmount);
        Task<bool> LockAmountAsync(string id, decimal balance, decimal locked);
        Task<bool> AmountUpdateRecord(string id, decimal balance, decimal locked, decimal mortgaged, DateTime updateTime);
        Task<bool> LockAmountAsync(string id, decimal locked);
        Task<bool> MortgageAmountAsync(string id, decimal balance, decimal mortgaged);
        Task<bool> AllotPromoterDividend(string id, decimal balance, decimal lastTotalAmount);
        Task<Page<AccountBalanceDto>> GetListAsync(string queryText, string currencyId, long pageIndex, long pageSize);
        Task<Page<AccountBalanceDto>> GetAllListAsync(string queryText, long pageIndex, long pageSize);
        Task<IEnumerable<MyAssetInfo>> GetMyBalanceAsync(string userId);
        Task<IEnumerable<AccountAssetInfo>> GetTotalAssetListAsync(string userId);
    }
}
