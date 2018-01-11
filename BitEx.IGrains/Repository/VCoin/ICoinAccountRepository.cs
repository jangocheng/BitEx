using BitEx.Dapper.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface ICoinAccountRepository
    {
        Task<CoinAccountState> GetAsync(string id);
        Task<bool> CreateAsync(string id, string userId, string currencyId, CoinAccountStatus status);
        Task<bool> LockAsync(string id, CoinAccountStatus status);
        Task<bool> ChangeAddress(string id, string address);
        Task<bool> ChangeAmountAsync(string id, decimal balance, decimal lastTotalAmount);
        Task<bool> LockAmountAsync(string id, decimal balance, decimal locked);
        Task<bool> AmountUpdateRecord(string id, decimal balance, decimal locked, decimal mortgaged, DateTime updateTime);
        Task<bool> MortgageAmountAsync(string id, decimal balance, decimal mortgaged);
        Task<bool> AddWithdrawAddress(string id, string userId, string currencyId, string accountId, string address, string memo, string tag, bool isVerified, bool isValid);
        Task<bool> RemoveWithdrawAddress(string id);
        Task<bool> AllotPromoterDividend(string id, decimal balance, decimal lastTotalAmount);
        Task<CoinAccountInfo> GetAccountInfoByAddressAsync(string currencyId, string address);
        Task<CoinAccountInfo> GetAccountInfoByUserIdAsync(string currencyId, string userId);
        Task<Page<AccountBalanceDto>> GetListAsync(string queryText, string currencyId, long pageIndex, long pageSize);
        Task<List<WithdrawAddress>> GetWidthrawAddressListAsync(string id);
    }
}
