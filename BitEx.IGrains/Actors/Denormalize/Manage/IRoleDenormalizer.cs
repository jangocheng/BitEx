using Coin.Framework.EventSourcing;
using Orleans;

namespace BitEx.IGrain.Actors.Denormalize.Manage
{
    public interface IRoleDenormalizer : IDenormalize, IGrainWithIntegerKey
    {
    }
}
