using Coin.Framework.EventSourcing;
using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface IMarketSnaptshotRepository
    {
        Task<List<PlanOrderInfoV1>> GetPlanOrderList(MongoStorageAttribute mongoInfo, string stateId, UInt32 version);
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="mongoInfo"></param>
        /// <param name="stateId"></param>
        /// <param name="version"></param>
        /// <returns>item1为askOrderList,item2为bidOrderList</returns>
        Task<Tuple<List<OrderInfoV1>, List<OrderInfoV1>>> GetOrderList(MongoStorageAttribute mongoInfo, string stateId, UInt32 version);
        Task SaveOrderList(MongoStorageAttribute mongoInfo, MarketState state);
        Task DeleteSnapshotData(MongoStorageAttribute mongoInfo, string stateId, UInt32 version);
    }
}
