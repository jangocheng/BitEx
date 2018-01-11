using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class UserUnlockEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(UserUnlockEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string Remark { get; set; }
        public UserStatus Status { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public UserUnlockEvent(UserStatus status, string remark)
        {
            this.Remark = remark;
            this.Status = status;
        }
        public UserUnlockEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.LockType = UserLockType.None;
                modelState.Status = Status;
            }
        }
    }
}
