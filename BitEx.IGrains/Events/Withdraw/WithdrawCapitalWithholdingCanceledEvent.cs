using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Coin.Core;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawCapitalWithholdingCanceledEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string WithdrawId { get; set; }
        public decimal Balance { get; set; }
        public string Result { get; set; }
        private static string _TypeCode = typeof(WithdrawCapitalWithholdingCanceledEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public WithdrawCapitalWithholdingCanceledEvent()
        {
        }
        public WithdrawCapitalWithholdingCanceledEvent(string commandId, string withdrawId, decimal balance, string result)
        {
            this.CommandId = commandId;
            this.WithdrawId = withdrawId;
            this.Balance = balance;
            this.Result = result;
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
