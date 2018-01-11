using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinMortgagedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string DepositId { get; set; }
        public string DepositCurrencyId { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal Mortgaged { get; set; }
        /// <summary>
        /// 本次抵押金额
        /// </summary>
        public decimal MortgagedAmount { get; set; }
        private static string _TypeCode = typeof(CoinMortgagedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinMortgagedEvent()
        {
        }
        public CoinMortgagedEvent(string commandId, string userId, string currencyId, string depositId, string depositCurrencyId, decimal balance, decimal mortgaged, decimal depositAmount, decimal mortgagedVolume)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.DepositId = depositId;
            this.DepositCurrencyId = depositCurrencyId;
            this.Balance = balance;
            this.Mortgaged = mortgaged;
            this.DepositAmount = depositAmount;
            this.MortgagedAmount = mortgagedVolume;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Balance = this.Balance;
                modelState.Mortgaged = this.Mortgaged;
            }
        }
    }
}
