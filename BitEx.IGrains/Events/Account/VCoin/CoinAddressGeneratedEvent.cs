using System;
using Coin.Core.EventSourcing;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinAddressGeneratedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string AccountId { get; set; }
        public string CurrencyId { get; set; }
        private static string _TypeCode = typeof(CoinAddressGeneratedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinAddressGeneratedEvent()
        {
        }
        public CoinAddressGeneratedEvent(string commandId, string accountId, string currencyId)
        {
            this.CommandId = commandId;
            this.AccountId = accountId;
            this.CurrencyId = currencyId;
        }
        public void Apply(IState<string> state)
        {
        }
    }
}

