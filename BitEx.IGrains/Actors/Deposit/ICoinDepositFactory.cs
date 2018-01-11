using BitEx.IGrain.States;
using Orleans;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors
{
    public interface ICoinDepositFactory : IGrainWithStringKey
    {
        Task<bool> AddDeposit(string userId, string accountId, string address, string depositId, decimal volume, string txNo);
        Task ConfirmAsync(string depositId, string txNo, int confirmation);
        Task CancelAsync(string txNo);
        Task CancelGuaranteeAsync(string txNo);
        Task ActivateGuaranteeDepositAsync(string depositId, string guaranteeId, bool mortgagedIsVirtualCoin, string mortgagedAccountId, decimal mortgagedAmount, GuaranteeStatus status);
    }
}
