using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositGuaranteeRecallEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public int Confirmation { get; set; }
        public string TxNo { get; set; }
        public string GuaranteeId { get; set; }
        public bool IsMortgagedVirtualCoin { get; set; }
        public string MortgagedAccountId { get; set; }
        public decimal MortgagedAmount { get; set; }
        public GuaranteeStatus GuaranteeStatus { get; set; }
        public CoinDepositStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinDepositGuaranteeRecallEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositGuaranteeRecallEvent()
        {
        }
        public CoinDepositGuaranteeRecallEvent(string commandId, string userId, string currencyId, string txNo, string guaranteeId, bool isMortgagedVirtualCoin, string mortgagedAccountId, decimal mortgagedAmount, CoinDepositStatus status)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.TxNo = txNo;
            this.GuaranteeId = guaranteeId;
            this.IsMortgagedVirtualCoin = IsMortgagedVirtualCoin;
            this.MortgagedAccountId = mortgagedAccountId;
            this.MortgagedAmount = mortgagedAmount;
            this.GuaranteeStatus = GuaranteeStatus.Refunded;
            this.Status = status | CoinDepositStatus.GuaranteeSuccessed;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinDepositState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
            }
        }
    }
}
