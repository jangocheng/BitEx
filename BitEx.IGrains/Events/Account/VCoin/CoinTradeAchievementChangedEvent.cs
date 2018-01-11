using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;
using BitEx.IGrain.Entity;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CoinTradeAchievementChangedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string MarketId { get; set; }
        public OrderType OrderType { get; set; }
        public int TradeId { get; set; }
        public string OrderId { get; set; }
        public decimal Balance { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        public decimal Fee { get; set; }
        public decimal LastTotalAmount { get; set; }
        private static string _TypeCode = typeof(CoinTradeAchievementChangedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; }
        public CoinTradeAchievementChangedEvent()
        {
        }
        public CoinTradeAchievementChangedEvent(string commandId, string userId, string currencyId, string marketId, OrderType orderType, int tradeId, string orderId, decimal balance, decimal volume, decimal price, decimal fee)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.MarketId = marketId;
            this.OrderType = orderType;
            this.TradeId = tradeId;
            this.OrderId = orderId;
            this.Balance = balance;
            this.Volume = volume;
            this.Price = price;
            this.Fee = fee;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                if (this.Timestamp.Date.Subtract(modelState.LastStatisticsTotalAmountTime).TotalDays >= 1)
                {
                    modelState.LastTotalAmount = modelState.Balance + modelState.Locked + modelState.Mortgaged;
                    modelState.LastStatisticsTotalAmountTime = this.Timestamp.Date;
                }

                if (OrderType == OrderType.Bid)
                {
                    if (!modelState.Profits.TryGetValue(MarketId, out var tradeProfit))
                    {
                        tradeProfit = new TradeProfit();
                        modelState.Profits.Add(MarketId, tradeProfit);
                    }
                    //计算统计结果
                    decimal amount = Volume * Price;
                    tradeProfit.BidAmount += amount;
                    tradeProfit.BidVolume += Volume;
                    tradeProfit.BidAvgPrice = Math.Round(tradeProfit.BidAmount / tradeProfit.BidVolume, 8);

                    tradeProfit.HeldAvgPrice = tradeProfit.HeldAvgPrice > 0 ? Math.Round((tradeProfit.HeldAvgPrice * modelState.Balance + amount), 8) / this.Balance : Price;
                }
                this.LastTotalAmount = modelState.LastTotalAmount;
                modelState.Balance = this.Balance;
            }
        }
    }
}
