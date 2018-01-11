using Orleans;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors
{
    public interface ICoinBalanceFactory : IGrainWithIntegerKey
    {
        Task Active();
        Task CreateDailyBook();
        Task CreateLedgerBook();
    }
}
