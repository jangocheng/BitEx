using BitEx.Dapper.Core;
using BitEx.Model.Account;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface IAmountUpdateRecordRepository
    {
        Task Add(AmountUpdateRecord model);
        Task<Page<AmountUpdateRecord>> GetPageByPromoter(long page, long pageSize, string userId = null, string currencyId = null, string operatorId = null);
    }
}
