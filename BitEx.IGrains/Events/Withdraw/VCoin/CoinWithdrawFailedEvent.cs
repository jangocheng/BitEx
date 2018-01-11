using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using Coin.Core;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawFailedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string Address { get; set; }
        public decimal Volume { get; set; }
        public TResult Result { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawFailedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinWithdrawFailedEvent()
        {
        }
        public CoinWithdrawFailedEvent(string commandId, string userId, string currencyId, string address, decimal volume, TResult result)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.Address = address;
            this.Volume = volume;
            this.Result = result;
        }
        public void Apply(IState<string> state)
        {
        }
    }
}
