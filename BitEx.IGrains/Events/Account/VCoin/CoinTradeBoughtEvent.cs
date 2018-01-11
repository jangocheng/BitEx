using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;
using System.Collections.Generic;
using BitEx.IGrain.Entity;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CoinTradeBoughtEvent : IEventBase<string>
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
        public decimal Locked { get; set; }
        public decimal Balance { get; set; }
        public decimal ReturnAmount { get; set; }
        public decimal Amount { get; set; }
        public AccountStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinTradeBoughtEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinTradeBoughtEvent()
        {
        }
        public CoinTradeBoughtEvent(string commandId, string userId, string currencyId, string marketId, int tradeId, string orderId, decimal balance, decimal locked, decimal returnAmount, decimal amount)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.MarketId = marketId;
            this.TradeId = tradeId;
            this.OrderId = orderId;
            this.Balance = balance;
            this.Locked = locked;
            this.ReturnAmount = returnAmount;
            this.Amount = amount;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Locked = this.Locked;
                modelState.Balance = this.Balance;
            }
        }
    }
}
