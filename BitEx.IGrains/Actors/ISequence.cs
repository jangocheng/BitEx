using System.Threading.Tasks;
using Orleans;

namespace BitEx.IGrain.Actors
{
    public interface ISequence : IGrainWithStringKey
    {
        Task<int> Next(int minValue);
    }
}