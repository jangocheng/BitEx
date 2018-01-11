using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MortgageRepealedEvent : IEventBase<string>
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
        public decimal Amount { get; set; }
        private static string _TypeCode = typeof(MortgageRepealedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MortgageRepealedEvent()
        {
        }
        public MortgageRepealedEvent(string commandId, string userId, string currencyId, string guaranteeId, decimal balance, decimal mortgaged, decimal amount)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.GuaranteeId = guaranteeId;
            this.Balance = balance;
            this.Mortgaged = mortgaged;
            this.Amount = amount;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as AccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Balance = this.Balance;
                modelState.Mortgaged = this.Mortgaged;
            }
        }
    }
}
