using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Coin.Core;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinMortgageRedeemingFailedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string DepositId { get; set; }
        public string GuaranteeId { get; set; }
        public Result Result { get; set; }
        public decimal LastTotalAmount { get; set; }
        private static string _TypeCode = typeof(CoinMortgageRedeemingFailedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinMortgageRedeemingFailedEvent()
        {
        }
        public CoinMortgageRedeemingFailedEvent(string commandId, string userId, string currencyId, string depositId, string guaranteeId, Result result)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.DepositId = depositId;
            this.GuaranteeId = guaranteeId;
            this.Result = result;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
            }
        }
    }
}
