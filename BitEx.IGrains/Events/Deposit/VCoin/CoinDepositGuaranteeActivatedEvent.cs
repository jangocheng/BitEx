using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositGuaranteeActivatedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public decimal DepositAmount { get; set; }
        public bool IsGuarantee { get; set; }
        public string GuaranteeId { get; set; }
        public string MortgagedCurrencyId { get; set; }
        public decimal MortgagedAmount { get; set; }
        public CoinDepositStatus Status { get; set; }
        public GuaranteeStatus GuaranteeStatus { get; set; }
        private static string _TypeCode = typeof(CoinDepositGuaranteeActivatedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositGuaranteeActivatedEvent()
        {
        }
        public CoinDepositGuaranteeActivatedEvent(string commandId, string userId, string currencyId, decimal depositAmount, string guaranteeId, string mortgagedCurrencyId, decimal mortgagedAmount)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.GuaranteeId = guaranteeId;
            this.DepositAmount = depositAmount;
            this.MortgagedCurrencyId = mortgagedCurrencyId;
            this.MortgagedAmount = mortgagedAmount;
            this.Status = CoinDepositStatus.Increased | CoinDepositStatus.GuaranteeSuccessed;
            this.GuaranteeStatus = GuaranteeStatus.Successed;
            this.IsGuarantee = true;
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
