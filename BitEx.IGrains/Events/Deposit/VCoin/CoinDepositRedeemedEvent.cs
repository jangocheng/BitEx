using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CoinDepositRedeemedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string GuaranteeId { get; set; }
        public bool IsVirtualCoin { get; set; }
        public string MortgagedAccountId { get; set; }
        public decimal MortgagedAmount { get; set; }
        public GuaranteeStatus GuaranteeStatus { get; set; }
        public CoinDepositStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinDepositRedeemedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositRedeemedEvent()
        {
        }
        public CoinDepositRedeemedEvent(string commandId, string userId, string currencyId, string guaranteeId, bool isMortgagedVirtualCoin, string mortgagedAccountId, decimal mortgagedAmount)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.GuaranteeId = guaranteeId;
            this.IsVirtualCoin = isMortgagedVirtualCoin;
            this.MortgagedAccountId = mortgagedAccountId;
            this.MortgagedAmount = mortgagedAmount;
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
