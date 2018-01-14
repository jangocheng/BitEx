using BitEx.Dapper.Core;
using BitEx.IGrain.States;
using BitEx.Model.Order;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface IPlanOrderRepository
    {
        Task<PlanOrderInfo> GetById(string marketId, string orderId);
        Task CreateAsync(PlanOrderInfo orderInfo);
        Task<bool> ChangeStatus(string marketId, string id, OrderStatus status);
        Task<Page<PlanOrderInfo>> GetPageList(string marketId, string userId, OrderStatus status, int page, int pageSize);
        /// <summary>
        /// 计划订单触发
        /// </summary>
        /// <param name="marketId">市场ID</param>
        /// <param name="id">计划订单ID</param>
        /// <param name="price">触发价格</param>
        /// <returns></returns>
        Task Trigger(string marketId, string id, decimal price);
    }
}
