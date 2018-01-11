using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CurrencyLockedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public bool IsLocked { get; set; }
        public int OperatorId { get; set; }
        private static string _TypeCode = typeof(CurrencyLockedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CurrencyLockedEvent()
        {
        }
        public CurrencyLockedEvent(string commandId, int operatorId)
        {
            this.CommandId = commandId;
            this.OperatorId = operatorId;
            this.IsLocked = true;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CurrencyState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.IsLocked = this.IsLocked;
            }
        }
    }
}
