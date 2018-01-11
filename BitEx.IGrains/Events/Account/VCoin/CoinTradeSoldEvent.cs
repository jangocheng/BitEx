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
    public class CoinTradeSoldEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string MarketId { get; set; }
        public int TradeId { get; set; }
        public string OrderId { get; set; }
        public decimal Balance { get; set; }
        public decimal Locked { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        public decimal ReturnAmount { get; set; }
        public AccountStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinTradeSoldEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinTradeSoldEvent()
        {
        }
        public CoinTradeSoldEvent(string commandId, string userId, string currencyId, string marketId, int tradeId, string orderId, decimal balance, decimal locked, decimal volume, decimal price, decimal returnAmount)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.MarketId = marketId;
            this.TradeId = tradeId;
            this.OrderId = orderId;
            this.Balance = balance;
            this.Locked = locked;
            this.Volume = volume;
            this.Price = price;
            this.ReturnAmount = returnAmount;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                if (!modelState.Profits.TryGetValue(MarketId, out var tradeProfit))
                {
                    tradeProfit = new TradeProfit();
                    modelState.Profits.Add(MarketId, tradeProfit);
                }
                //计算统计结果
                decimal amount = Volume * Price;

                tradeProfit.AskAmount += amount;
                tradeProfit.AskVolume += Volume;
                tradeProfit.AskAvgPrice = Math.Round(tradeProfit.AskAmount / tradeProfit.AskVolume, 8);

                modelState.Locked = this.Locked;
                modelState.Balance = this.Balance;
            }
        }
    }
}
