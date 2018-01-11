using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Coin.Core;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawCapitalWithheldEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string WithdrawId { get; set; }
        public decimal Balance { get; set; }
        private static string _TypeCode = typeof(WithdrawCapitalWithheldEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public WithdrawCapitalWithheldEvent()
        {
        }
        public WithdrawCapitalWithheldEvent(string commandId, string withdrawId, decimal balance)
        {
            this.CommandId = commandId;
            this.WithdrawId = withdrawId;
            this.Balance = balance;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CapitalAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Balance = this.Balance;
            }
        }
    }
}
