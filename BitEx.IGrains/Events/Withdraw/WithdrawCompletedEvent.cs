using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawCompletedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string CapitalAccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string TxNo { get; set; }
        public decimal TxFee { get; set; }

        public WithdrawStatus Status { get; set; }
        private static string _TypeCode = typeof(WithdrawCompletedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public WithdrawCompletedEvent()
        {
        }
        public WithdrawCompletedEvent(string commandId, string capitalAccountId, decimal amount, decimal fee, decimal txFee, string txNo)
        {
            this.CommandId = commandId;
            this.CapitalAccountId = capitalAccountId;
            this.Amount = amount;
            this.Fee = fee;
            this.TxFee = txFee;
            this.TxNo = txNo;
            this.Status = WithdrawStatus.Completed;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as WithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);

                modelState.CapitalAccountId = this.CapitalAccountId;
                modelState.TxNo = this.TxNo;
                modelState.TxFee = this.TxFee;
                modelState.Status = this.Status;
            }
        }
    }
}
