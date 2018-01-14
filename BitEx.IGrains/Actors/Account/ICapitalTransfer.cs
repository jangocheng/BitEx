using Coin.Core;
using Orleans;
using System;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors
{
    /// <summary>
    /// 资金账户额外收支
    /// </summary>
    public interface ICapitalTransfer : IGrainWithStringKey
    {
        Task<Result> CreateAsync(string commandId, string fromAccountId, string toAccountId, string currencyId, decimal amount, string txNo, decimal txFee, string remark, int operatorId);
    }
}
