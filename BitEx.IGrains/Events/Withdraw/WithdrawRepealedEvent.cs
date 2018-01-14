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
    public class WithdrawRepealedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public string Result { get; set; }
        public int OperatorId { get; set; }
        public WithdrawStatus Status { get; set; }
        private static string _TypeCode = typeof(WithdrawRepealedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public WithdrawRepealedEvent()
        {
        }
        public WithdrawRepealedEvent(string commandId, string userId, string currencyId, decimal amount, Result result)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.Amount = amount;
            this.Result = result.ToString();
            this.Status = WithdrawStatus.Repealed;
            this.OperatorId = 0;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as WithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
                modelState.Result = this.Result;
            }
        }
    }
}
