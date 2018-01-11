using Coin.Core;
using BitEx.Dapper.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coin.Framework.ThirdParty;

namespace BitEx.IGrain.Repository
{
    public interface IWithdrawRepository
    {
        Task<bool> CreateAsync(string id, string userId, string currencyId, string accountId, BankType accountType, string subbranch, string accountNumber, string province, string city, decimal amount, decimal txAmount, decimal fee, WithdrawStatus status, DateTime createdAt);
        Task<bool> CreateFailAsync(string id, string userId, string currencyId, string accountId, BankType accountType, string subbranch, string accountNumber, decimal amount, string result, DateTime createdAt, WithdrawStatus status);
        Task<WithdrawState> GetAsync(string id);
        Task<bool> ChangeStatusAsync(string id, WithdrawStatus status, int operatorId);
        Task<bool> ResetAsync(string id, WithdrawStatus status, int operatorId);
        Task<bool> CompleteAsync(string id, string capitalAccountId, string txNo, decimal txFee, WithdrawStatus status, DateTime doneAt);
        Task<bool> RollbackAsync(string id, string result, int operatorId, WithdrawStatus status);
        Task<Page<WithdrawDto>> GetPendingListAsync(string queryText, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        Task<Page<WithdrawDto>> GetListAsync(WithdrawStatus status, int? operatorId, string queryText, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        Task<Page<WithdrawDto>> GetListAsync(WithdrawStatus status, string userId, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        Task<IEnumerable<WithdrawExportDto>> GetExportListAsync(WithdrawStatus status, int operatorId, string queryText, DateTime? beginDate, DateTime? endDate, byte amountType);
        Task<IEnumerable<WithdrawDto>> GetBatchWithdrawListAsync(WithdrawStatus status, int operatorId);
        Task<bool> CreateYSBOrderAsync(string orderId, string userName, string account, string amount);
        Task<bool> UpdateYSBOrderAsync(string order, string returnCode, string returnMessage);
    }
}
