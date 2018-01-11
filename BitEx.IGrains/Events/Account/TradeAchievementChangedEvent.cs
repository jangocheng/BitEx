using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class TradeAchievementChangedEvent : IEventBase<string>
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
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public decimal LastTotalAmount { get; set; }
        private static string _TypeCode = typeof(TradeAchievementChangedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public TradeAchievementChangedEvent()
        {
        }
        public TradeAchievementChangedEvent(string commandId, string userId, string currencyId, string marketId, int tradeId,
             string orderId, decimal balance, decimal amount, decimal fee)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.MarketId = marketId;
            this.TradeId = tradeId;
            this.OrderId = orderId;
            this.Balance = balance;
            this.Amount = amount;
            this.Fee = fee;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as AccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                if (this.Timestamp.Date.Subtract(modelState.LastStatisticsTotalAmountTime).TotalDays >= 1)
                {
                    modelState.LastTotalAmount = modelState.Balance + modelState.Locked + modelState.Mortgaged;
                    modelState.LastStatisticsTotalAmountTime = this.Timestamp.Date;
                }
                this.LastTotalAmount = modelState.LastTotalAmount;
                modelState.Balance = this.Balance;
            }
        }
    }
}
