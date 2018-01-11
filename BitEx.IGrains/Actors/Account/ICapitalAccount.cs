using System.Threading.Tasks;
using Orleans;
using BitEx.IGrain.Events;
using Coin.Core;
using Coin.Framework.ThirdParty;
using BitEx.IGrain.Events.User;
using BitEx.IGrain.States;
using BitEx.IGrain.Entity;

namespace BitEx.IGrain.Actors
{
    public interface ICapitalAccount : IGrainWithStringKey
    {
        Task CreateAsync(string userId, string currencyId, string ownerName, BankType accountType, string subbranch, string accountNumber);
        Task LockAsync(int operatorId);
        Task UnlockAsync(int operatorId);
        Task SetDefaultAsync(bool isDefault, int operatorId);
        Task<BankType> GetAccountTypeAsync();
        Task WithdrawWithholdAsync(WithdrawCompletedEvent @event);
        Task CancelWithdrawWithholdAsync(string commandId, string messageId, string withdrawId, decimal amount, string result);
        Task DepositIncreaseAsync(string commandId, string messageId, string depositId, string fundSourceId, decimal amount);
        Task CancelDepositDecreaseAsync(string commandId, string messageId, string depositId, string fundSourceId, decimal amount);
        Task<TResult> TransferToAsync(TransferType txType, string txOwnerName, BankType txAccountType, string txSubbranch, string txAccountNumber, decimal amount, string txNo, decimal txFee, string remark, int operatorId);
        Task<CapitalAccountDefaultInfo> GetDefaultInfoAsync();
    }
}
