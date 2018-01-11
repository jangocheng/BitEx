using Coin.Framework.EventSourcing;
using Orleans;

namespace BitEx.IGrain.Actors.Denormalize
{
    public interface IMarketDenormalizer : IDenormalize, IGrainWithStringKey
    {
    }
}
