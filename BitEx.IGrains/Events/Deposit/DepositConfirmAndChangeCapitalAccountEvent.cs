using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class DepositConfirmAndChangeCapitalAccountEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string FundSourceId { get; set; }
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal TxAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal TxFee { get; set; }
        public string CapitalAccountId { get; set; }
        public int OperatorId { get; set; }
        public DateTime DoneAt { get; set; }
        public DepositStatus Status { get; set; }
        private static string _TypeCode = typeof(DepositConfirmAndChangeCapitalAccountEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public DepositConfirmAndChangeCapitalAccountEvent()
        {
        }
        public DepositConfirmAndChangeCapitalAccountEvent(string commandId, string fundSourceId, string accountId, string capitalAccuntId, decimal amount, decimal txAmount, decimal fee, decimal txFee, int operatorId)
        {
            this.CommandId = commandId;
            this.FundSourceId = fundSourceId;
            this.AccountId = accountId;
            this.CapitalAccountId = capitalAccuntId;
            this.Amount = amount;
            this.TxAmount = txAmount;
            this.Fee = fee;
            this.TxFee = txFee;
            this.OperatorId = operatorId;
            this.Status = DepositStatus.Completed;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as DepositState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.FundSourceId = this.FundSourceId;
                modelState.TxAmount = this.TxAmount;
                modelState.TxFee = this.TxFee;
                modelState.Fee = this.Fee;
                modelState.CapitalAccountId = this.CapitalAccountId;
                modelState.DoneAt = this.DoneAt;
                modelState.OperatorId = this.OperatorId;
                modelState.Status = this.Status;
            }
        }
    }
}
