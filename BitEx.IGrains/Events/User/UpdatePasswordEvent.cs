using System;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;
using Ray.Core.EventSourcing;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class UpdatePasswordEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(UpdatePasswordEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string NewPassword { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public UpdatePasswordEvent(string newPassword)
        {
            this.NewPassword = newPassword;
        }
        public UpdatePasswordEvent() { }
    }
}
