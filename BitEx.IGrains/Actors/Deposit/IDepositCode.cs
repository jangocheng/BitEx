using Coin.Core;
using Orleans;
using System;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors
{
    public interface IDepositCode : IGrainWithStringKey
    {
        Task CreateAsync(string currencyId, string fundSourceId, decimal amount, string code, string password, int operatorId);
        Task<TResult> UseAsync(string password, string usedBy);
        Task<TResult> DestroyAsync(DateTime destroyedAt, int operatorId);
    }
}
