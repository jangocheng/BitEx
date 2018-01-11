using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinAccountCreatedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public CoinAccountStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinAccountCreatedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinAccountCreatedEvent()
        {
        }
        public CoinAccountCreatedEvent(string commandId, string userId, string currencyId)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.Status = CoinAccountStatus.Actived;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.UserId = this.UserId;
                modelState.CurrencyId = this.CurrencyId;
                modelState.Status = this.Status;
            }
        }
    }
}

