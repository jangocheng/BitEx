using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface IDepositGuaranteeRepository
    {
        Task<GuaranteeInfo> GetAsync(string depositId);
        Task<bool> CreateAsync(string id, string userId, string mortgagedCurrencyId, decimal mortgagedAmount, string depositId, string depositCurrencyId, decimal depositAmount, GuaranteeStatus status);
        Task<bool> ChangeStatus(string id, GuaranteeStatus status);
        Task<Dictionary<string, GuaranteeInfo>> GetUnConfirmedListAsync(string depositCurrencyId);
    }
}
