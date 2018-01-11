using System;
using Coin.Core.EventSourcing;
using Coin.Framework.ThirdParty;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawCreatedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public BankType AccountType { get; set; }
        public string Subbranch { get; set; }
        public string AccountNumber { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public decimal Amount { get; set; }
        public decimal TxAmount { get; set; }
        public decimal Fee { get; set; }
        public WithdrawStatus Status { get; set; }
        private static string _TypeCode = typeof(WithdrawCreatedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public WithdrawCreatedEvent()
        {
        }
        public WithdrawCreatedEvent(string commandId, string userId, string currencyId, string accountId, BankType accountType, string subbranch, string accountNumber, string province, string city, decimal amount, decimal txAmount, decimal fee)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.AccountId = accountId;
            this.CurrencyId = currencyId;
            this.AccountType = accountType;
            this.Subbranch = subbranch;
            this.AccountNumber = accountNumber;
            this.Province = province;
            this.City = city;
            this.Amount = amount;
            this.TxAmount = txAmount;
            this.Fee = fee;
            this.Status = WithdrawStatus.Started;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as WithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);

                modelState.UserId = this.UserId;
                modelState.AccountId = this.AccountId;
                modelState.CurrencyId = this.CurrencyId;
                modelState.AccountType = this.AccountType;
                modelState.Subbranch = this.Subbranch;
                modelState.AccountNumber = this.AccountNumber;
                modelState.Province = this.Province;
                modelState.City = this.City;
                modelState.Amount = this.Amount;
                modelState.TxAmount = this.TxAmount;
                modelState.Fee = this.Fee;
                modelState.Status = this.Status;
            }
        }
    }
}
