using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CurrencyTransferedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public CoinTransferType TransferType { get; set; }
        public string TargetAddress { get; set; }
        public string FromAddress { get; set; }
        public decimal ColdBalance { get; set; }
        public decimal Volume { get; set; }
        public decimal Fee { get; set; }
        public string TxNo { get; set; }
        public string Remark { get; set; }
        public int OperatorId { get; set; }
        private static string _TypeCode = typeof(CurrencyTransferedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CurrencyTransferedEvent()
        {
        }
        public CurrencyTransferedEvent(string commandId, CoinTransferType txType, string fromAddress, string targetAddress, decimal coldBalance, decimal volume, decimal fee, string txNo, string remark, int operatorId)
        {
            this.CommandId = commandId;
            this.TransferType = txType;
            this.FromAddress = fromAddress;
            this.TargetAddress = targetAddress;
            this.ColdBalance = coldBalance;
            this.Volume = volume;
            this.Fee = fee;
            this.TxNo = txNo;
            this.Remark = remark;
            this.OperatorId = operatorId;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CurrencyState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.ColdBalance = this.ColdBalance;
            }
        }
    }
}
