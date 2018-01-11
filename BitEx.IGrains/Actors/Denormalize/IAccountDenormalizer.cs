using Orleans;
using Coin.Framework.EventSourcing;
using System.Threading.Tasks;
using Coin.Core;

namespace BitEx.IGrain.Actors.Denormalize
{
    public interface IAccountDenormalizer : IDenormalize, IGrainWithStringKey
    {
    }
}
