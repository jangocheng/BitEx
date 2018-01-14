using System.Threading.Tasks;
using Orleans;
using Coin.Core;
using BitEx.IGrain.Events.User;
using BitEx.IGrain.States;
using BitEx.IGrain.Entity.Account;
using BitEx.IGrain.Events;
using System;

namespace BitEx.IGrain.Actors
{
    public interface IAccount : IGrainWithStringKey
    {
        Task<decimal> GetBalance();
        Task<decimal> GetLockedAmount();
        Task<decimal> GetMortgagedAmount();
        Task LockAsync(AccountLockReason reason, int operatorId);
        Task UnLockAsync();
        Task WithdrawAsync(WithdrawAppliedEvent @event);
        Task CancelWithdrawAsync(string messageId, decimal amount, string result);
        Task<Result> MortgageAsync(string messageId, string depositId, string depositCurrencyId, decimal depositAmount);
        /// <summary>
        /// 用户账户差额补齐
        /// </summary>
        /// <param name="addBalance"></param>
        /// <param name="addLocked"></param>
        /// <param name="addMortgaged"></param>
        /// <param name="operatorId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<Result> UpdateAmount(decimal addBalance, decimal addLocked, decimal addMortgaged, string operatorId, string remark);
        Task<Result> BalanceAsync(string depositId, string depositCurrencyId, decimal depositAmount);
        Task RepealMortgageAsync(string commandId, string messageId, string guaranteeId, decimal amount);
        Task<Result> RedeemMortgageAsync(string depositId, string guaranteeId, bool isMortgagedVirtualCoin, string mortgagedAccountId, decimal mortgagedAmount, decimal depositAmount);
        Task RedeemMortgageIncreaseAsync(string commandId, string messageId, string depositId, string guaranteeId, decimal amount);
        Task DepositIncreaseAsync(string commandId, string messageId, string depositId, decimal amount);
        Task RollbackDepositDecreaseAsync(string messageId, string depositId, decimal amount);
        Task<Result> CreateBidOrderAsync(string marketId, OrderSource orderSource, decimal price, decimal volume);
        /// <summary>
        /// 批量买入
        /// </summary>
        /// <param name="marketId"></param>
        /// <param name="orderSource"></param>
        /// <param name="batchType"></param>
        /// <param name="highPrice"></param>
        /// <param name="lowPrice"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        Task<Result> CreateBidBatchOrderAsync(string marketId, OrderSource orderSource, BatchOrderType batchType, decimal highPrice, decimal lowPrice, decimal volume);
        Task<Result> CreateBidPlanOrderAsync(string marketId, OrderSource orderSource, decimal highTriggerPrice, decimal lowTriggerPrice, decimal highPrice, decimal lowPrice, decimal amount);
        Task CancelOrderAsync(MarketOrderCanceledEvent @event);
        Task TradeBuyAsync(string commandId, string messageId, string marketId, int tradeId, string orderId, decimal amount, decimal returnAmount);
        Task TradeChangeAchievementAsync(string commandId, string messageId, string marketId, int tradeId, string orderId, decimal amount, decimal fee);
        /// <summary>
        /// 推广分红到账
        /// </summary>
        /// <param name="dividendAmount"></param>
        /// <param name="dividendTime"></param>
        /// <returns></returns>
        Task AllotPromoterDividendAsync(decimal dividendAmount, DateTime dividendTime);
        /// <summary>
        /// 发送交易事件到Account队列，方便修改订单状态
        /// </summary>
        /// <param name="tradeEvent"></param>
        /// <returns></returns>
        Task OrderTrade(MarketTradedEvent tradeEvent);
        /// <summary>
        /// 发送计划订单被触发事件到Account队列，方便修改订单状态
        /// </summary>
        /// <param name="triggerEvent"></param>
        /// <returns></returns>
        Task PlanOrderTrigger(MarketPlanOrderChangedEvent triggerEvent);
    }
}
