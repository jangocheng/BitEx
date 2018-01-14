using System;
using ProtoBuf;
using Orleans.Concurrency;
using Ray.Core.EventSourcing;
using BitEx.Model.User;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class UserLockEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(UserLockEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public UserLockType LockType { get; set; }
        public UserStatus Status { get; set; }
        public string Remark { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public UserLockEvent(UserLockType type, UserStatus status, string remark)
        {
            this.LockType = type;
            this.Remark = remark;
            this.Status = status;
        }
        public UserLockEvent() { }
    }
}
