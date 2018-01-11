using Coin.Framework.EventSourcing;
using BitEx.IGrain.Entity;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors.Replicated
{
    public interface IMarketReplicated : IReplicaGrain, IGrainWithStringKey
    {
        Task<List<MarketDepth>> GetAskDepth(int precision, int limit);
        Task<List<MarketDepth>> GetBidDepth(int precision, int limit);
        /// <summary>
        /// 发送市场深度数据到消息队列
        /// </summary>
        /// <returns></returns>
        Task SendDepthToMq();
    }
}
