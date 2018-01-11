using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using Orleans.Concurrency;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class AddOrderMarket : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(AddOrderMarket).FullName;
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
        public AddOrderMarket(string marketId)
        {
            this.MarketId = marketId;
        }
        public AddOrderMarket() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                if (modelState.ActiveMarketList.IndexOf(MarketId) == -1)
                    modelState.ActiveMarketList.Add(MarketId);
            }
        }
    }
}
