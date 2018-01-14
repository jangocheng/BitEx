using System;
using ProtoBuf;
using Orleans.Concurrency;
using BitEx.Model.User;
using Ray.Core.EventSourcing;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class SetTradePasswordEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(SetTradePasswordEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string Password { get; set; }
        public TradePasswordType Type { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public SetTradePasswordEvent(string password, TradePasswordType type)
        {
            this.Password = password;
            Type = type;
        }
        public SetTradePasswordEvent() { }
    }
}
