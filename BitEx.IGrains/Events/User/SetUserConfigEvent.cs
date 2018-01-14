using System;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using Ray.Core.EventSourcing;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class SetUserConfigEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(SetUserConfigEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public UserConfigEnum Key { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public SetUserConfigEvent(UserConfigEnum key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public SetUserConfigEvent() { }
    }
}
