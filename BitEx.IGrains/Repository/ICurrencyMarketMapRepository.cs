using BitEx.Model.Withdrawl;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface ICurrencyMarketMapRepository
    {
        Task<ExchangeMapper> GetMapper(string basicId, string targetId);
    }
}
