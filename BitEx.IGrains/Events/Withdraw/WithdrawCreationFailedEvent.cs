using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Coin.Core;
using Coin.Framework.ThirdParty;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawCreationFailedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string AccountId { get; set; }
        public BankType BankType { get; set; }
        public string BranchInfo { get; set; }
        public string AcceptAccountNumber { get; set; }
        public string CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public string Result { get; set; }
        public WithdrawStatus Status { get; set; }
        private static string _TypeCode = typeof(WithdrawCreationFailedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public WithdrawCreationFailedEvent()
        {
        }
        public WithdrawCreationFailedEvent(string commandId, string userId, string accountId, string currencyId, BankType bankType, string branchInfo, string acceptAccountNumber, decimal amount, string result)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.AccountId = accountId;
            this.CurrencyId = currencyId;
            this.BankType = bankType;
            this.BranchInfo = branchInfo;
            this.AcceptAccountNumber = acceptAccountNumber;
            this.Amount = amount;
            this.Result = result;
            this.Status = WithdrawStatus.Failed;
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
                modelState.AccountType = this.BankType;
                modelState.Subbranch = this.BranchInfo;
                modelState.AccountNumber = this.AcceptAccountNumber;
                modelState.Amount = this.Amount;
                modelState.Result = this.Result.ToString();
                modelState.Status = this.Status;
            }
        }
    }
}
