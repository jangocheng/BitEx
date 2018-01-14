using System;
using ProtoBuf;
using Orleans.Concurrency;
using Ray.Core.EventSourcing;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class BindOtpEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(BindOtpEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string SecretKey { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public BindOtpEvent(string secretKey)
        {
            this.SecretKey = secretKey;
        }
        public BindOtpEvent() { }
    }
}
