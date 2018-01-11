using Orleans;
using Coin.Framework.EventSourcing;

namespace BitEx.IGrain.Actors.Denormalize.Cms
{
    public interface IAssetsVoteDenormalizer : IDenormalize, IGrainWithStringKey
    {
    }
}
