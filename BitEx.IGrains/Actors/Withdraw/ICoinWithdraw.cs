using Coin.Core;
using BitEx.IGrain.Entity.Withdraw;
using BitEx.IGrain.States;
using Orleans;
using System;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors
{
    public interface ICoinWithdraw : IGrainWithStringKey
    {
        Task CreateAsync(string commandId, string userId, string currencyId, string accountId, string address, decimal volume, decimal txVolume, decimal cnyAmount, decimal fee, WithdrawValidResult validResult, DateTime createdAt, string memo);
        Task CreateFailAsync(string commandId, string userId, string currencyId, string accountId, string address, decimal volume, decimal txVolume, decimal cnyAmount, decimal fee, WithdrawValidResult validResult, DateTime createdAt, string memo = null);
        Task<TResult> HandleAsync(int operatorId);
        Task AutoCompleteAsync(string commandId, string txNo, decimal txFee, DateTime doneAt);
        Task ManualCompleteAsync(string txNo, decimal txFee, int operatorId);
        Task HandleFailAsync(string commandId, string message);
        Task<WithdrawNoticeDto> GetNoticeInfo();
        Task<TResult> CancelAsync(string reason, int operatorId);
        Task<TResult> RepealAsync();
        Task<TResult> ForceRetryAsync(int operatorId);
        Task ConfirmResponse();
        Task ChangeStatus(string messageId, CoinWithdrawStatus status);
    }
}
