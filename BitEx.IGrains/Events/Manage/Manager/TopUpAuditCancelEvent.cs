using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using BitEx.IGrain.States.Manage;

namespace BitEx.IGrain.Events.Manage
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class TopUpAuditCancelEvent : IEventBase<int>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public int StateId { get; set; }
        private static string _TypeCode = typeof(TopUpAuditCancelEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public decimal Amount { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public TopUpAuditCancelEvent(decimal amount, decimal depositAmount)
        {
            this.Amount = amount;
            this.DepositAmount = depositAmount;
        }
        public TopUpAuditCancelEvent() { }
        public void Apply(IState<int> state)
        {
            var modelState = state as ManagerState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.DepositAmount = this.DepositAmount;
            }
        }
    }
}
