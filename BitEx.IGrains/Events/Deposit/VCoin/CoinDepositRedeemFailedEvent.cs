using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using Orleans.Concurrency;
using Coin.Core;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CoinDepositRedeemFailedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string GuaranteeId { get; set; }
        public decimal DepositAmount { get; set; }
        public TResult Result { get; set; }
        private static string _TypeCode = typeof(CoinDepositRedeemFailedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositRedeemFailedEvent()
        {
        }
        public CoinDepositRedeemFailedEvent(string commandId, string userId, string currencyId, string guaranteeId, decimal depositAmount, TResult result)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.GuaranteeId = guaranteeId;
            this.DepositAmount = depositAmount;
            this.Result = result;
        }
        public void Apply(IState<string> state)
        {
        }
    }
}
