using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using Orleans.Concurrency;
using ProtoBuf;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class MarketListCollectEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public UInt32 Version { get; set; }
        public string CommandId { get; set; }
        public DateTime Timestamp { get; set; }
        public string StateId { get; set; }
        public List<string> MarketIdList { get; set; }
        private static string _TypeCode = typeof(MarketCollectEvent).FullName;
        [ProtoIgnore]
        public string TypeCode => _TypeCode;
        public MarketListCollectEvent(List<string> marketIdList)
        {
            this.MarketIdList = marketIdList;
        }

        public void Apply(IState<string> state)
        {
            var modelState = state as UserCollectState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.CollectList = MarketIdList;
            }
        }
    }
}
