using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CurrencyBalanceNotifyLineChangedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public decimal Amount { get; set; }
        public int OperatorId { get; set; }
        private static string _TypeCode = typeof(CurrencyBalanceNotifyLineChangedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CurrencyBalanceNotifyLineChangedEvent()
        {
        }
        public CurrencyBalanceNotifyLineChangedEvent(string commandId, decimal amount, int operatorId)
        {
            this.CommandId = commandId;
            this.Amount = amount;
            this.OperatorId = operatorId;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CurrencyState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.BalanceNotifyLine = this.Amount;
            }
        }
    }
}
