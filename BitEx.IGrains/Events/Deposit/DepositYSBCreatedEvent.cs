using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class DepositYSBCreatedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public string CapitalAccountId { get; set; }
        public string FundSourceId { get; set; }
        public DepositWay DepositWay { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public string BankCode { get; set; }
        public int Identify { get; set; }
        public DepositStatus Status { get; set; }
        private static string _TypeCode = typeof(DepositYSBCreatedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public DepositYSBCreatedEvent()
        {
        }
        public DepositYSBCreatedEvent(string commandId, string userId, string currencyId, string accountId, DepositWay depositWay, string capitalAccountId, string fundSourceId, decimal amount, string remark, int identify,string bankCode)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.AccountId = accountId;
            this.CapitalAccountId = capitalAccountId;
            this.FundSourceId = fundSourceId;
            this.DepositWay = depositWay;
            this.Amount = amount;
            this.Remark = remark;
            this.Identify = identify;
            this.Status = DepositStatus.Started;
            this.BankCode = bankCode;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as DepositState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.UserId = this.UserId;
                modelState.CurrencyId = this.CurrencyId;
                modelState.AccountId = this.AccountId;
                modelState.CapitalAccountId = this.CapitalAccountId;
                modelState.FundSourceId = this.FundSourceId;
                modelState.DepositWay = this.DepositWay;
                modelState.Amount = this.Amount;
                modelState.Remark = this.Remark;
                modelState.Identify = this.Identify;
                modelState.Status = this.Status;
            }
        }
    }
}
