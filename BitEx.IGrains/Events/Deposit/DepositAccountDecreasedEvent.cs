using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class RollbackDepositDecreasedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string DepositId { get; set; }
        public decimal Balance { get; set; }
        public decimal Volume { get; set; }
        public decimal LastTotalAmount { get; set; }
        public string TxNo { get; set; }
        public CoinDepositStatus Status { get; set; }
        private static string _TypeCode = typeof(RollbackDepositDecreasedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public RollbackDepositDecreasedEvent()
        {
        }
        public RollbackDepositDecreasedEvent(string commandId, string userId, string currencyId, string depositId, decimal balance, decimal volume)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.DepositId = depositId;
            this.Balance = balance;
            this.Volume = volume;
            this.Status = CoinDepositStatus.Canceled;
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
            }
        }
    }
}
