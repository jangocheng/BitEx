using System;
using ProtoBuf;
using Orleans.Concurrency;
using Ray.Core.EventSourcing;
using BitEx.Model.User;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class UpdateTradePasswordTypeEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(UpdateTradePasswordTypeEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public TradePasswordType PasswordType { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public UpdateTradePasswordTypeEvent(TradePasswordType type)
        {
            this.PasswordType = type;
        }
        public UpdateTradePasswordTypeEvent() { }
    }
}
