using System.Threading.Tasks;
using Orleans;
using BitEx.IGrain.Events;

namespace BitEx.IGrain.Actors
{
    public interface IMarketAggr : IGrainWithStringKey
    {
        Task AddTrade(MarketTradedEvent @event);
        Task SendKLineToMq(string frequency);
    }
}

