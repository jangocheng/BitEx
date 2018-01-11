using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawAssignedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public WithdrawStatus Status { get; set; }
        public int OperatorId { get; set; }
        private static string _TypeCode = typeof(WithdrawAssignedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public WithdrawAssignedEvent()
        {
        }
        public WithdrawAssignedEvent(string commandId, int operatorId)
        {
            this.CommandId = commandId;
            this.OperatorId = operatorId;
            this.Status = WithdrawStatus.Assigned;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as WithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
                modelState.OperatorId = this.OperatorId;
            }
        }
    }
}
