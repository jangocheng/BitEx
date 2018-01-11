using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class MarketStatusChangeEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public MarketStatus Status { get; set; }
        public string Remark { get; set; }
        public int OperatorId { get; set; }
        private static string _TypeCode = typeof(MarketStatusChangeEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MarketStatusChangeEvent()
        {
        }
        public MarketStatusChangeEvent(MarketStatus status, string remark, int operatorId)
        {
            this.Remark = remark;
            this.Status = status;
            this.OperatorId = operatorId;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as MarketState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
            }
        }
    }
}
