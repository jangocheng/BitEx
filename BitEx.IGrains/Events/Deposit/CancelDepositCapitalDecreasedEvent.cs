using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CancelDepositCapitalDecreasedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string DepositId { get; set; }
        public string FundSourceId { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        private static string _TypeCode = typeof(CancelDepositCapitalDecreasedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CancelDepositCapitalDecreasedEvent()
        {
        }
        public CancelDepositCapitalDecreasedEvent(string commandId, string depositId, string fundSourceId, decimal balance, decimal amount)
        {
            this.CommandId = commandId;
            this.DepositId = depositId;
            this.FundSourceId = fundSourceId;
            this.Balance = balance;
            this.Amount = amount;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CapitalAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Balance = this.Balance;
            }
        }
    }
}
