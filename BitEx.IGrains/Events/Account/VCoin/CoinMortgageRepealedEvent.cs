using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinMortgageRepealedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string GuaranteeId { get; set; }
        public decimal Balance { get; set; }
        public decimal Mortgaged { get; set; }
        public decimal Volume { get; set; }
        private static string _TypeCode = typeof(CoinMortgageRepealedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinMortgageRepealedEvent()
        {
        }
        public CoinMortgageRepealedEvent(string commandId, string userId, string currencyId, string guaranteeId, decimal balance, decimal mortgaged, decimal volume)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.GuaranteeId = guaranteeId;
            this.Balance = balance;
            this.Mortgaged = mortgaged;
            this.Volume = volume;
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
