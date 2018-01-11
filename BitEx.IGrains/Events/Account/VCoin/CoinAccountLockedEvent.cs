using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinAccountLockedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public AccountLockReason LockReason { get; set; }
        public string LockParameter { get; set; }
        public CoinAccountStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinAccountLockedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinAccountLockedEvent()
        {
        }
        public CoinAccountLockedEvent(string commandId, AccountLockReason reason, string lockParameter)
        {
            this.CommandId = commandId;
            this.LockReason = reason;
            this.LockParameter = lockParameter;
            this.Status = CoinAccountStatus.Locked;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.LockReason = this.LockReason;
                modelState.LockParameter = this.LockParameter;
                modelState.Status = this.Status;
            }
        }
    }
}
