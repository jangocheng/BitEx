using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawStatusChangedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public CoinWithdrawStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawStatusChangedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinWithdrawStatusChangedEvent()
        {
        }
        public CoinWithdrawStatusChangedEvent(string commandId, string userId, string currencyId, CoinWithdrawStatus status)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.Status = status;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinWithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
            }
        }
    }
}
