using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors
{
    public interface IUserCollect : IGrainWithStringKey
    {
        Task MarketCollect(string marketId);
        Task MarketListCollect(List<string> marketIdList);
        Task CancelMarketCollect(string marketId);
        Task<List<string>> GetMarketCollect();
    }
}
