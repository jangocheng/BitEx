using System.Threading.Tasks;
using Orleans;
using Coin.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Actors
{
    public interface IDeposit : IGrainWithStringKey
    {
        Task CreateAsync(string userId, string currencyId, string accountId, DepositWay depositWay, string capitalAccountId, decimal amount, string remark);
        Task<bool> CreateYSBAsync(string userId, string currencyId, string accountId, DepositWay depositWay, string capitalAccountId, decimal amount, string remark,string bankCode);
        Task CreateAsync(string userId, string currencyId, string accountId, string fundSourceId, decimal amount);
        Task ConfimAsync(string fundSourceId, int operatorId);
        Task YSBConfimAsync(string fundSourceId, int operatorId);
        Task ConfimAsync(string fundSourceId, string capitalAccountId, int operatorId);
        Task<TResult> ResetAsync(int operatorId);
        Task<TResult> RepealAsync(string reason, int operatorId);
        Task<FundInfo> GetFundInfo();
        Task<int> GetIdentify();
    }
}
