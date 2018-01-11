using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity;
using System.Linq;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MarketOrderChangedEventV1 : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public OrderInfoV1 Order { get; set; }
        private static string _TypeCode = typeof(MarketOrderChangedEventV1).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MarketOrderChangedEventV1()
        {
        }
        public MarketOrderChangedEventV1(string commandId, OrderInfoV1 order)
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
                if (this.Order.OrderType == OrderType.Ask)
                {
                    modelState.AskList.Add(this.Order);
                }
                else
                {
                    modelState.BidList.Add(this.Order);
                }
            }
        }
    }
}
