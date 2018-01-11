using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinAccountUnLockedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public CoinAccountStatus Status { get; set; }
        public AccountLockReason LockReason { get; set; }
        public string LockParameter { get; set; }
        private static string _TypeCode = typeof(CoinAccountUnLockedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinAccountUnLockedEvent()
        {
        }
        public CoinAccountUnLockedEvent(string commandId)
        {
            this.CommandId = commandId;
            this.Status = CoinAccountStatus.Actived;
            this.LockReason = AccountLockReason.None;
            this.LockParameter = null;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
                modelState.LockReason = this.LockReason;
                modelState.LockParameter = this.LockParameter;
            }
        }
    }
}
