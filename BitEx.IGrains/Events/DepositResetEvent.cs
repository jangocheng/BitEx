using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class DepositResetEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public int OperatorId { get; set; }
        public DepositStatus Status { get; set; }
        private static string _TypeCode = typeof(DepositResetEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public DepositResetEvent()
        {
        }
        public DepositResetEvent(string commandId, int operatorId)
        {
            this.CommandId = commandId;
            this.OperatorId = operatorId;
            this.Status = DepositStatus.Started;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as DepositState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);

                modelState.OperatorId = this.OperatorId;
                modelState.Status = this.Status;
            }
        }
    }
}
