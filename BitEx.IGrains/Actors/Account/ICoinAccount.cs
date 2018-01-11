using Coin.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.Entity.Account;
using BitEx.IGrain.Events;
using BitEx.IGrain.Events.User;
using BitEx.IGrain.States;
using Orleans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors
{
    public interface ICoinAccount : IGrainWithStringKey
    {
        Task GenerateAddress();
        Task<TResult> ChangeAddressAsync(string commandId, string address);
        Task<decimal> GetBalance();
        Task<decimal> GetLockedAmount();
        Task<decimal> GetMortgagedAmount();
        Task<string> GetAddress();
        Task<string> GetDepositAddress();
        Task LockAsync(AccountLockReason reason, string lockParameter);
        Task UnLockAsync();
        Task WithdrawAsync(CoinWithdrawAppliedEvent @event);
        Task RollbackWithdrawAsync(CoinWithdrawCanceledEvent @event);
        Task RepealWithdrawAsync(CoinWithdrawRepealedEvent @event);
        Task DepositAsync(string messageId, string depositId, decimal volume);
        Task DepositByManagerAsync(string messageId, string depositId, decimal volume);
        Task RollbackDepositAsync(string depositId, decimal volume);
        Task<TResult> MortgageAsync(string messageId, string depositId, string depositCurrencyId, decimal depositAmount);
        Task<TResult> BalanceAsync(string depositId, string depositCurrencyId, decimal depositAmount);
        Task RepealMortgageAsync(string commandId, string messageId, string guaranteeId, decimal volume);
        Task<TResult> RedeemMortgageAsync(string messageId, string depositId, string guaranteeId, bool isMortgagedVirtualCoin, string mortgagedAccountId, decimal mortgagedAmount, decimal depositAmount);
        Task RedeemMortgageIncreaseAsync(string commandId, string messageId, string depositId, string guaranteeId, decimal amount);
        Task RedeemMortgageDecreaseAsync(string commandId, string messageId, string depositId, string guaranteeId, decimal amount, TResult result);
        Task<TResult> CreateBidOrderAsync(string marketId, OrderSource orderSource, decimal price, decimal volume);
        Task<TResult> CreateAskOrderAsync(string marketId, OrderSource orderSource, decimal price, decimal volume);
        Task<TResult> CreateBidPlanOrderAsync(string marketId, OrderSource orderSource, decimal highTriggerPrice, decimal lowTriggerPrice, decimal highPrice, decimal lowPrice, decimal amount);
        Task<TResult> CreateAskPlanOrderAsync(string marketId, OrderSource orderSource, decimal highTriggerPrice, decimal lowTriggerPrice, decimal highPrice, decimal lowPrice, decimal volume);
        Task<TResult> CreateBidBatchOrderAsync(string marketId, OrderSource orderSource, BatchOrderType batchType, decimal highPrice, decimal lowPrice, decimal volume);
        Task<TResult> CreateAskBatchOrderAsync(string marketId, OrderSource orderSource, BatchOrderType batchType, decimal highPrice, decimal lowPrice, decimal volume);
        Task CancelOrderAsync(MarketOrderCanceledEvent @event);
        Task TradeBuyAsync(string commandId, string messageId, string marketId, int tradeId, string orderId, decimal amount, decimal returnAmount);
        Task TradeSellAsync(string commandId, string messageId, string marketId, int tradeId, string orderId, decimal volume, decimal price, decimal returnVolume);
        Task TradeChangeAchievementAsync(string commandId, string messageId, string marketId, int tradeId, OrderType orderType, string orderId, decimal price, decimal volume, decimal fee);
        /// <summary>
        /// 推广分红到账
        /// </summary>
        /// <param name="dividendVolume"></param>
        /// <param name="dividendTime"></param>
        /// <returns></returns>
        Task AllotPromoterDividendAsync(decimal dividendVolume, DateTime dividendTime);
        Task AddWithdrawAddress(string address, string tag, bool isVerified, string memo = null);
        Task CompleteAddWithdrawAddress(string address, string memo, string tag, bool isVerified, bool isValid);
        /// <summary>
        /// 用户账户差额补齐
        /// </summary>
        /// <param name="addBalance"></param>
        /// <param name="addLocked"></param>
        /// <param name="addMortgaged"></param>
        /// <param name="operatorId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<TResult> UpdateAmount(decimal addBalance, decimal addLocked, decimal addMortgaged, string operatorId, string remark);
        Task RemoveWithdrawAddress(string addressId);
        Task<List<WithdrawAddress>> GetWithdrawAddressList();
        Task<TradeProfit> GetTradeProfit(string marketId);
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
