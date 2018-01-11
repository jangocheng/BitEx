using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositGuaranteeRedeemEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string DepositCurrencyId { get; set; }
        public string MortgageCurrencyId { get; set; }
        public string GuaranteeId { get; set; }
        public bool IsMortgagedVirtualCoin { get; set; }
        public string DepositAccountId { get; set; }
        public string MortgagedAccountId { get; set; }
        public decimal MortgagedAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public GuaranteeStatus GuaranteeStatus { get; set; }
        public CoinDepositStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinDepositGuaranteeRedeemEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositGuaranteeRedeemEvent()
        {
        }
        public CoinDepositGuaranteeRedeemEvent(string commandId, string userId, string depositCurrencyId, string guaranteeId, bool isVirtualCoin, string depositAccountId, string mortgagedAccountId, decimal morgagedAmount, decimal depositAmount)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.DepositCurrencyId = depositCurrencyId;
            this.GuaranteeId = guaranteeId;
            this.DepositAmount = depositAmount;
            this.MortgagedAmount = morgagedAmount;
            this.DepositAccountId = depositAccountId;
            this.MortgagedAccountId = mortgagedAccountId;
            this.IsMortgagedVirtualCoin = isVirtualCoin;
            this.Status = CoinDepositStatus.Increased;
            this.GuaranteeStatus = GuaranteeStatus.Canceled;
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
