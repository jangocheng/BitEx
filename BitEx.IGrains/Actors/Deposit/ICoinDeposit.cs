using System.Threading.Tasks;
using Orleans;
using BitEx.IGrain.States;
using Coin.Core;
using BitEx.IGrain.Entity;

namespace BitEx.IGrain.Actors
{
    public interface ICoinDeposit : IGrainWithStringKey
    {
        Task CreateAsync(string userId, string currencyId, string accountId, string address, decimal volume, decimal txVolume, decimal fee, decimal txFee, string txNo, string memo = null, CoinDepositSpecialStatus specialStatus = CoinDepositSpecialStatus.None);
        Task ChangeConfirmationAsync(string messageId, int confirmation);
        Task AutoConfirmAsync(string messageId, int confirmation, CoinDepositStatus status);
        Task AutoSafeAsync(string messageId, int confirmation, CoinDepositStatus status);
        Task AutoGuaranteeConfirmAsync(string messageId, int confirmation, string guaranteeId, bool isMortgagedVirtualCoin, string mortgagedAccountId, decimal mortgagedAmount, CoinDepositStatus status);
        Task<Result> ManualConfirmAsync(CoinDepositStatus status);
        Task TerminateAsync(string messageId, int confirmation, int txConfirmation, Result result);
        Task ActivateGuaranteeAsync(string commandId, string messageId, string guaranteeId, string mortgagedCurrencyId, decimal mortgagedAmount, bool isMortgagedVirtualCoin);
        Task RepealAsync(string reason, int operatorId);
        Task<Result> RedeemAsync(string commandId, string messageId, string guaranteeId, bool isMortgagedVirtualCoin, string mortgagedAccountId, decimal mortgagedAmount);
        Task<DepositInfo> GetDepositInfo();
        Task<Result> SpecialConfirmAsync(CoinDepositSpecialStatus status, string userId, string accountId);
        Task<Result> SpecialRepealAsync(string reason);
        Task ChangeCoinDepositAsync(string messageId, string userId, string accountId, CoinDepositSpecialStatus specialStatus);
        Task<CoinDepositStatus> GetCoinDepositStatus();
        Task<Result> MortgageAsync(string currencyId);
        Task<Result> RedeemMortgageAsync(string guaranteeId, bool isVirtualCoin, string depositAccountId, string mortgagedAccountId, decimal morgagedAmount, decimal depositAmount);
        Task MortgageFailedAsync();
        Task RedeemingMortgageFailedAsync(string guaranteeId, string result);
    }
}
