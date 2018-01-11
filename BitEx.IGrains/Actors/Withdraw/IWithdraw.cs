using System.Threading.Tasks;
using Orleans;
using Coin.Core;
using BitEx.IGrain.Entity.Withdraw;

namespace BitEx.IGrain.Actors
{
    public interface IWithdraw : IGrainWithStringKey
    {
       // Task CreateAsync(string commandId, string msgId, string userId, string currencyId, string accountId, BankType accountType, string subbranch, string accountNumber, string province, string city, decimal amount, decimal txAmount, decimal fee);
        Task<TResult> AssignAsync(int operatorId);
        Task<TResult> HandleAsync(int operatorId);
        Task<TResult> ResetHandleAsync(int operatorId);
        Task<TResult> CompleteAsync(string capitalAccountId, string txNo, decimal txFee, int operatorId);
        Task<TResult> RollbackAsync(string reason, int operatorId);
        Task<TResult> RepealAsync();
        Task<WithdrawCompleteVerifyDto> GetWithdrawCompleteVerifyInfo();
        Task<decimal> GetWithdrawAmount();
        Task<string> GetCurrencyId();
        Task<string> GetUserId();
    }
}
