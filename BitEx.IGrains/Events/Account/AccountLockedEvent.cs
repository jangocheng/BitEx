using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class AccountLockedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public AccountLockReason LockReason { get; set; }
        public string LockParameter { get; set; }
        public AccountStatus Status { get; set; }
        public int OperatorId { get; set; }
        private static string _TypeCode = typeof(AccountLockedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public AccountLockedEvent()
        {
        }
        public AccountLockedEvent(string commandId, AccountLockReason reason, int operatorId)
        {
            this.CommandId = commandId;
            this.LockReason = reason;
            this.Status = AccountStatus.Locked;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as AccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
            }
        }
    }
}
