using System.Threading.Tasks;
using Orleans;
using Coin.Core;
using BitEx.IGrain.Entity.Withdraw;

namespace BitEx.IGrain.Actors
{
    public interface IWithdraw : IGrainWithStringKey
    {
       // Task CreateAsync(string commandId, string msgId, string userId, string currencyId, string accountId, BankType accountType, string subbranch, string accountNumber, string province, string city, decimal amount, decimal txAmount, decimal fee);
        Task<Result> AssignAsync(int operatorId);
        Task<Result> HandleAsync(int operatorId);
        Task<Result> ResetHandleAsync(int operatorId);
        Task<Result> CompleteAsync(string capitalAccountId, string txNo, decimal txFee, int operatorId);
        Task<Result> RollbackAsync(string reason, int operatorId);
        Task<Result> RepealAsync();
        Task<WithdrawCompleteVerifyDto> GetWithdrawCompleteVerifyInfo();
        Task<decimal> GetWithdrawAmount();
        Task<string> GetCurrencyId();
        Task<string> GetUserId();
    }
}
