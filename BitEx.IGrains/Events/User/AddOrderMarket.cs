using Orleans.Concurrency;
using ProtoBuf;
using System;
using Ray.Core.EventSourcing;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class AddOrderMarket : IEventBase<string>
    {
        #region base
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
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        #endregion
        public string MarketId { get; set; }
        public AddOrderMarket(string marketId)
        {
            this.MarketId = marketId;
        }
        public AddOrderMarket() { }
    }
}
