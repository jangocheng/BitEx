using Coin.Core;
using BitEx.Dapper.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using System;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface IDepositRepository
    {
        Task<DepositState> GetAsync(string id);
        Task<DepositState> GetAsync(int identity, decimal amount);
        Task<bool> CreateAsync(string id, string userId, string currencyId, string accountId, DepositWay depositWay, string capitalAccountId, string fundSourceId, decimal amount, string remark, DepositStatus status, int identify, DateTime createdAt);
        Task<bool> CreateFailAsync(string id, string userId, string accountId, string currencyId, string capitalAccountId, string fundSourceId, DepositWay depositWay, decimal amount, DepositStatus status, Result result);
        Task<bool> ConfirmAsync(string id, string fundSourceId, decimal txAmount, decimal fee, decimal txFee, int operatorId, DepositStatus status, DateTime doneAt);
        Task<bool> ChangeStatusAsync(string id, DepositStatus status, int operatorId);
        Task<bool> ClearInvalid(int operatorId);
        Task<Page<DepositDto>> GetListAsync(string queryText, DepositStatus status, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        Task<Page<DepositDto>> GetListAsync(string queryText, DepositWay depositWay, DepositStatus status, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        Task<Page<DepositDto>> GetListAsync(DepositStatus status, string userId, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        Task<DepositConfirmationInfo> GetInfoAsync(string id);
        Task<bool> CreateOrderAsync(string order, decimal money, string user, DateTime time, string remark, string type, string keymd5);
        Task<bool> CreateYSBOrderAsync(string order, decimal money, string userId, string accountId, string remark, string bankCode, int identify);
        Task<bool> UpdateYSBOrderAsync(string order, string returnCode, string returnMessage);
    }
}
