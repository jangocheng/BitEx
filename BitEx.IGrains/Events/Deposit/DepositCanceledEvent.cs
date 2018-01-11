using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Coin.Core;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class DepositRepealedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string FundSourceId { get; set; }
        public string AccountId { get; set; }
        public string CapitalAccountId { get; set; }
        public decimal Amount { get; set; }
        public int OperatorId { get; set; }
        public string Result { get; set; }
        public DateTime DoneAt { get; set; }
        public DepositStatus Status { get; set; }
        private static string _TypeCode = typeof(DepositRepealedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public DepositRepealedEvent()
        {
        }
        public DepositRepealedEvent(string commandId, string fundSourceId, string accountId, string capitalAccountId, decimal amount, int operatorId, TResult result)
        {
            this.CommandId = commandId;
            this.FundSourceId = fundSourceId;
            this.AccountId = accountId;
            this.CapitalAccountId = capitalAccountId;
            this.Amount = amount;
            this.OperatorId = operatorId;
            this.Result = result.ToString();
            this.DoneAt = DateTime.MinValue;
            this.Status = DepositStatus.Canceled;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as DepositState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.DoneAt = this.DoneAt;
                modelState.OperatorId = this.OperatorId;
                modelState.Status = this.Status;
            }
        }
    }
}
