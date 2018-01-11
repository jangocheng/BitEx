using System;
using Coin.Core.EventSourcing;
using Coin.Framework.ThirdParty;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CapitalAccountCreatedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string OwnerName { get; set; }
        public BankType AccountType { get; set; }
        public string Subbranch { get; set; }
        public string AccountNumber { get; set; }
        public bool IsDefault { get; set; }
        public AccountStatus Status { get; set; }
        private static string _TypeCode = typeof(CapitalAccountCreatedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CapitalAccountCreatedEvent()
        {
        }
        public CapitalAccountCreatedEvent(string commandId, string userId, string currencyId, string ownerName, BankType accountType, string subbranch, string accountNumber, bool isDefault)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.OwnerName = ownerName;
            this.AccountType = accountType;
            this.Subbranch = subbranch;
            this.AccountNumber = accountNumber;
            this.IsDefault = isDefault;
            this.Status = AccountStatus.Actived;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CapitalAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.UserId = this.UserId;
                modelState.CurrencyId = this.CurrencyId;
                modelState.OwnerName = this.OwnerName;
                modelState.AccountType = this.AccountType;
                modelState.Subbranch = this.Subbranch;
                modelState.AccountNumber = this.AccountNumber;
                modelState.IsDefault = this.IsDefault;
                modelState.Status = this.Status;
                modelState.Balance = 0;
            }
        }
    }
}
