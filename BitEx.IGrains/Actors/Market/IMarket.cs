using Coin.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.Entity.Market;
using BitEx.IGrain.Events;
using BitEx.IGrain.States;
using Orleans;
using Orleans.Concurrency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors
{
    public interface IMarket : IGrainWithStringKey
    {
        Task Modify(decimal makerFeeRate, decimal takerFeeRate, int pricePrecision, int volumePrecision, int depthVolumePrecision, decimal minOrderAmount, decimal priceLimitPercent, MarketArea area);
        [AlwaysInterleave]
        Task<bool> IsOpening();
        [AlwaysInterleave]
        Task<decimal> GetMarketPrice();
        /// <summary>
        /// 币种互换
        /// </summary>
        /// <param name="targetCurrencyId">当前币种Id</param>
        /// <param name="amount">金额</param>
        /// <returns></returns>
        [AlwaysInterleave]
        Task<decimal> Exchange(string targetCurrencyId, decimal amount);
        Task Lock(string remark, int operatorId);
        Task Unlock(string remark, int operatorId);
        Task Close(string remark, int operatorId);
        Task Open(string remark, int operatorId);
        Task AddOrder(AccountOrderCreatedEventV1 orderCreatedEvent);
        Task AddPlanOrder(AccountPlanOrderCreatedEventV1 planOrderCreatedEvent);
        Task<bool> CancelOrder(OrderCategory orderCategory, string userId, string orderId, OrderType orderType);
        /// <summary>
        /// 批量撤销订单
        /// </summary>
        /// <param name="orderCategory"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CancelAllOrder(OrderCategory orderCategory, string userId);
        /// <summary>
        /// 撤销市场的所有订单
        /// </summary>
        /// <returns></returns>
        Task CancelAllOrder();
        /// <summary>
        /// 获取当前市场的费率
        /// </summary>
        /// <returns></returns>
        Task<TradeFeeRateDto> GetTradeFeeRate();
        /// <summary>
        /// 获取基币成交总额
        /// </summary>
        /// <returns></returns>
        [AlwaysInterleave]
        Task<decimal> GetBasicTurnover();
        /// <summary>
        /// 获取目标币成交总额
        /// </summary>
        /// <returns></returns>
        [AlwaysInterleave]
        Task<decimal> GetTargetTurnover();
        /// <summary>
        /// 获取市场价格精度
        /// </summary>
        /// <returns></returns>
        [AlwaysInterleave]
        Task<MarketPrecision> GetPrecision();
        /// <summary>
        /// 订单校验
        /// </summary>
        /// <param name="prices">价格列表</param>
        /// <param name="amounts">成交额列表</param>
        /// <returns></returns>
        [AlwaysInterleave]
        Task<Result> OrderVerify(decimal[] prices, decimal[] amounts, decimal[] volumes = null);
        /// <summary>
        /// 计划订单校验
        /// </summary>
        /// <param name="orderType"></param>
        /// <param name="highTriggerPrice"></param>
        /// <param name="lowTriggerPrice"></param>
        /// <param name="hightPrice"></param>
        /// <param name="lowPrice"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [AlwaysInterleave]
        Task<Result> PlanOrderVerify(OrderType orderType, decimal highTriggerPrice, decimal lowTriggerPrice, decimal hightPrice, decimal lowPrice, decimal amount);
    }
}
