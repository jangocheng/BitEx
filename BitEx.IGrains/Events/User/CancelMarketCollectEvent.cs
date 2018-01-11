using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using System.Linq;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CancelMarketCollectEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(CancelMarketCollectEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string MarketId { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public CancelMarketCollectEvent(string marketId)
        {
            this.MarketId = marketId;
        }
        public CancelMarketCollectEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserCollectState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.CollectList.Remove(MarketId);
            }
        }
    }
}
