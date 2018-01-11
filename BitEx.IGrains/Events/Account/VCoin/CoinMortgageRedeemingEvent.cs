using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinMortgageRedeemingEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string DepositId { get; set; }
        public string GuaranteeId { get; set; }
        public bool IsMortgagedVirtualCoin { get; set; }
        public string MortgagedAccountId { get; set; }
        public decimal MortgagedAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal DeopositAmount { get; set; }
        public decimal LastTotalAmount { get; set; }
        private static string _TypeCode = typeof(CoinMortgageRedeemingEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinMortgageRedeemingEvent()
        {
        }
        public CoinMortgageRedeemingEvent(string commandId, string userId, string currencyId, string depositId, string guaranteeId, bool isMortgagedVirtualCoin, string mortgagedAccountId, decimal mortgagedAmount, decimal balance, decimal deopositAmount)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.DepositId = depositId;
            this.GuaranteeId = guaranteeId;
            this.IsMortgagedVirtualCoin = isMortgagedVirtualCoin;
            this.MortgagedAccountId = mortgagedAccountId;
            this.MortgagedAmount = mortgagedAmount;
            this.Balance = balance;
            this.DeopositAmount = deopositAmount;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);

                if (this.Timestamp.Date.Subtract(modelState.LastStatisticsTotalAmountTime).TotalDays >= 1)
                {
                    modelState.LastTotalAmount = modelState.Balance + modelState.Locked + modelState.Mortgaged;
                    modelState.LastStatisticsTotalAmountTime = this.Timestamp.Date;
                }
                this.LastTotalAmount = modelState.LastTotalAmount;
                modelState.Balance = this.Balance;
            }
        }
    }
}
