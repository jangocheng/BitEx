using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class PromoterDividendAllottedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public decimal Balance { get; set; }
        public decimal DividendAmount { get; set; }
        public DateTime DividendTime { get; set; }
        public decimal LastTotalAmount { get; set; }
        private static string _TypeCode = typeof(PromoterDividendAllottedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public PromoterDividendAllottedEvent()
        {
        }
        public PromoterDividendAllottedEvent(string commandId, decimal balance, decimal dividendAmount, DateTime dividendTime)
        {
            this.CommandId = commandId;
            this.Balance = balance;
            this.DividendAmount = dividendAmount;
            this.DividendTime = dividendTime;
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
                modelState.DividendTotalAmount += DividendAmount;
            }
        }
    }
}
