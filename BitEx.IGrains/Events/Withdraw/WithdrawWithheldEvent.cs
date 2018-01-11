using System;
using Coin.Core.EventSourcing;
using Coin.Framework.ThirdParty;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawWithheldEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public BankType AccountType { get; set; }
        public string Subbranch { get; set; }
        public string AcceptAccountNumber { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public decimal TxAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal LastTotalAmount { get; set; }
        private static string _TypeCode = typeof(WithdrawWithheldEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public WithdrawWithheldEvent()
        {
        }
        public WithdrawWithheldEvent(string commandId, string userId, string currencyId, BankType accountType, string Subbranch, string acceptAccountNumber, string province, string city, decimal balance, decimal amount, decimal txAmount, decimal fee)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.AccountType = accountType;
            this.Subbranch = Subbranch;
            this.AcceptAccountNumber = acceptAccountNumber;
            this.Province = province;
            this.City = city;
            this.Balance = balance;
            this.Amount = amount;
            this.TxAmount = txAmount;
            this.Fee = fee;
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
