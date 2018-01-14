using BitEx.Dapper.Core;
using BitEx.Model.Account;
using BitEx.Model.Withdrawl.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IRepository.Account
{
    public interface ICoinAccountRepository
    {
        Task<CoinAccount> GetAsync(string id);
        Task<bool> CreateAsync(string id, string userId, string currencyId, AccountStatus status);
        Task<bool> LockAsync(string id, AccountStatus status);
        Task<bool> ChangeAddress(string id, string address);
        Task<bool> ChangeAmountAsync(string id, decimal balance, decimal lastTotalAmount);
        Task<bool> LockAmountAsync(string id, decimal balance, decimal locked);
        Task<bool> AmountUpdateRecord(string id, decimal balance, decimal locked, decimal mortgaged, DateTime updateTime);
        Task<bool> MortgageAmountAsync(string id, decimal balance, decimal mortgaged);
        Task<bool> AddWithdrawAddress(string id, string userId, string currencyId, string accountId, string address, string memo, string tag, bool isVerified, bool isValid);
        Task<bool> RemoveWithdrawAddress(string id);
        Task<bool> AllotPromoterDividend(string id, decimal balance, decimal lastTotalAmount);
        Task<UserAccount> GetAccountInfoByAddressAsync(string currencyId, string address);
        Task<UserAccount> GetAccountInfoByUserIdAsync(string currencyId, string userId);
        Task<Page<AccountBalanceDto>> GetListAsync(string queryText, string currencyId, long pageIndex, long pageSize);
        Task<List<WithdrawAddress>> GetWidthrawAddressListAsync(string id);
    }
}
