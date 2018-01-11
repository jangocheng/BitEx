using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawManualCompletedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string TxNo { get; set; }
        public decimal TxFee { get; set; }
        public int OperatorId { get; set; }
        public DateTime DoneAt { get; set; } = DateTime.Now;
        public CoinWithdrawStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawManualCompletedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinWithdrawManualCompletedEvent()
        {
        }
        public CoinWithdrawManualCompletedEvent(string commandId, string txNo, decimal txFee, int operatorId)
        {
            this.CommandId = commandId;
            this.TxNo = txNo;
            this.TxFee = txFee;
            this.OperatorId = operatorId;
            this.Status = CoinWithdrawStatus.Completed;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinWithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.TxNo = this.TxNo;
                modelState.TxFee = this.TxFee;
                modelState.DoneAt = this.DoneAt;
                modelState.Status = this.Status;
                modelState.OperatorId = this.OperatorId;
            }
        }
    }
}
