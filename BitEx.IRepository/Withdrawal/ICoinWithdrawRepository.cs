using Coin.Core;
using BitEx.Dapper.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using System;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface ICoinWithdrawRepository
    {
        Task<CoinWithdrawState> GetAsync(string id);
        Task<bool> CreateAsync(string id, string userId, string currencyId, string accountId, string address, decimal volume, decimal txVolume, decimal cnyAmount, decimal fee, WithdrawValidResult validResult, CoinWithdrawStatus status, DateTime createdAt, string memo = null);
        Task<bool> CreateFailAsync(string id, string userId, string currencyId, string accountId, string address, decimal volume, decimal cnyAmount, string result, CoinWithdrawStatus status, DateTime createdAt, string memo = null);
        Task<bool> HandleAsync(string id, int operatorId, CoinWithdrawStatus status);
        Task<bool> HandleFailAsync(string id, CoinWithdrawStatus status);
        Task<bool> CompleteAsync(string id, string txNo, decimal txFee, DateTime doneAt, CoinWithdrawStatus status);
        Task<bool> CancelAsync(string id, string result, int operatorId, CoinWithdrawStatus status);
        Task<bool> ChangeStatusAsync(string id, CoinWithdrawStatus status);
        Task<Page<CoinWithdrawDto>> GetListAsync(string currencyId, CoinWithdrawStatus status, string queryText, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        Task<Page<CoinWithdrawDto>> GetListAsync(string currencyId, string userId, CoinWithdrawStatus status, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
    }
}
