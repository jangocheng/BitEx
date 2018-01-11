using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class DepositCodeDestroyedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UsedAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DestroyedAt { get; set; }
        public int OperatorId { get; set; }
        public DepositCodeStatus Status { get; set; }
        private static string _TypeCode = typeof(DepositCodeDestroyedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public DepositCodeDestroyedEvent()
        {
        }
        public DepositCodeDestroyedEvent(string commandId, string usedAccountId, decimal amount, DateTime destroyedAt, int operatorId)
        {
            this.CommandId = commandId;
            this.UsedAccountId = usedAccountId;
            this.Amount = amount;
            this.DestroyedAt = destroyedAt;
            this.OperatorId = operatorId;
            this.Status = DepositCodeStatus.Destroyed;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as DepositCodeState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.DestroyedAt = this.DestroyedAt;
                modelState.OperatorId = this.OperatorId;
                modelState.Status = this.Status;
            }
        }
    }
}
