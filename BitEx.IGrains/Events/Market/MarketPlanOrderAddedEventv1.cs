using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MarketPlanOrderAddedEventV1 : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public PlanOrderInfoV1 Order { get; set; }
        private static string _TypeCode = typeof(MarketPlanOrderAddedEventV1).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MarketPlanOrderAddedEventV1()
        {
        }
        public MarketPlanOrderAddedEventV1(string commandId, PlanOrderInfoV1 order)
        {
            this.CommandId = commandId;
            this.Order = order;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as MarketState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.PlanOrderList.Add(this.Order);
            }
        }
    }
}
