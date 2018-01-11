using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coin.Framework.ThirdParty;

namespace BitEx.IGrain.Repository
{
    public interface IBankRepository
    {
        Task<IEnumerable<BankDto>> GetBankListAsync();
        Task<BankDto> GetBankAsync(BankType accountType);
    }
}
