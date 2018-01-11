using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class DepositAccountIncreasedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string DepositId { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public decimal LastTotalAmount { get; set; }
        private static string _TypeCode = typeof(DepositAccountIncreasedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public DepositAccountIncreasedEvent()
        {
        }
        public DepositAccountIncreasedEvent(string commandId, string depositId, decimal balance, decimal amount)
        {
            this.CommandId = commandId;
            this.DepositId = depositId;
            this.Balance = balance;
            this.Amount = amount;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as AccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);

                if (this.Timestamp.Date.Subtract(modelState.LastStatisticsTotalAmountTime).TotalDays >= 1)
                {
                    modelState.LastTotalAmount = modelState.Balance + modelState.Locked + modelState.Mortgaged;
                    modelState.LastStatisticsTotalAmountTime = this.Timestamp.Date;
                }
                this.LastTotalAmount = modelState.LastTotalAmount;
                modelState.Balance = this.Balance;
                modelState.IsFirstDeposit = false;
            }
        }
    }
}
