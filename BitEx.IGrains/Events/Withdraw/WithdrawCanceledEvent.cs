using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Coin.Core;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class WithdrawCanceledEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string Result { get; set; }
        public int OperatorId { get; set; }
        public WithdrawStatus Status { get; set; }
        private static string _TypeCode = typeof(WithdrawCanceledEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public WithdrawCanceledEvent()
        {
        }
        public WithdrawCanceledEvent(string commandId, string userId, string capitalAccountId, decimal amount, decimal fee, Result result, int operatorId)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.AccountId = capitalAccountId;
            this.Amount = amount;
            this.Fee = fee;
            this.Result = result.ToString();
            this.OperatorId = operatorId;
            this.Status = WithdrawStatus.Canceled;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as WithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
                modelState.OperatorId = this.OperatorId;
                modelState.Result = this.Result;
            }
        }
    }
}
