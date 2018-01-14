using Coin.Core;
using Orleans;
using System;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors
{
    public interface IDepositCode : IGrainWithStringKey
    {
        Task CreateAsync(string currencyId, string fundSourceId, decimal amount, string code, string password, int operatorId);
        Task<Result> UseAsync(string password, string usedBy);
        Task<Result> DestroyAsync(DateTime destroyedAt, int operatorId);
    }
}
