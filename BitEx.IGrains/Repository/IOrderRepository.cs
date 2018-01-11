using Coin.Model.Order;
using BitEx.IGrain.States;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitEx.Dapper.Core;

namespace BitEx.IGrain.Repository
{
    public interface IOrderRepository
    {
        Task CreateAsync(OrderInfo orderInfo);
        Task<bool> TradeAsync(string id, string marketId, decimal txVolume, decimal txAmount, decimal returnAmount, decimal orderFee, OrderStatus status);
        Task<bool> ChangeStatus(string marketId, string id, OrderStatus status);
        Task<int> GetCount(string marketId, string userId, OrderType type, OrderStatus status);
        Task<IEnumerable<OrderInfo>> GetAllList(string marketId, string userId, OrderType type, OrderStatus status);
        Task<Page<OrderInfo>> GetList(string marketId, string userId, OrderType type, OrderStatus status, int page, int pageSize);
        Task<int> GetPlanCount(string marketId, string userId, OrderType type, OrderStatus status);
        Task<IEnumerable<PlanOrderInfo>> GetAllPlanList(string marketId, string userId, OrderType type, OrderStatus status);
        Task<Page<PlanOrderInfo>> GetPlanList(string marketId, string userId, OrderType type, OrderStatus status, int page, int pageSize);
    }
}
