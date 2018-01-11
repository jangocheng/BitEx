using BitEx.Dapper.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface ICoinDepositRepository
    {
        Task<CoinDepositState> GetAsync(string id);
        Task<bool> CreateAsync(string id, string userId, string currencyId, string accountId, string address, decimal volume, decimal txVolume, decimal fee, decimal txFee, string txNo, DateTime createdAt, CoinDepositStatus status, string memo, CoinDepositSpecialStatus specialStatus);
        Task<bool> ChangeConfirmationAsync(string id, int confirmation);
        Task<bool> ConfirmAsync(string id, int confirmation, DateTime confirmAt, CoinDepositStatus status);
        Task<bool> ConfirmAsync(string id, int confirmation, DateTime confirmAt, GuaranteeStatus guaranteeStatus, CoinDepositStatus status);
        Task<bool> TerminateAsync(string id, int confirmation, int txConfirmation, CoinDepositStatus status, string result);
        Task<bool> ChangeStatusAsync(string id, CoinDepositStatus status, string result);
        Task<bool> ChangeGuaranteeAsync(string id, bool isGuarantee, CoinDepositStatus status);
        Task<Dictionary<string, CoinDepositConfirmation>> GetUnConfirmedListAsync(string currencyId);
        Task<Page<CoinDepositDto>> GetListAsync(string queryText, string currencyId, byte status, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        Task<Page<CoinDepositDto>> GetListAsync(string currencyId, byte status, string userId, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        Task<bool> ChangeInfoAsync(string id, string userId, string accountId, string result, CoinDepositSpecialStatus specialStatus);
        Task<Page<CoinDepositDto>> GetSpecialListAsync(string queryText, string currencyId, CoinDepositSpecialStatus status, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
    }
}
