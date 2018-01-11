using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class UpdateTradePasswordTypeEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(UpdateTradePasswordTypeEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public TradePasswordType PasswordType { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public UpdateTradePasswordTypeEvent(TradePasswordType type)
        {
            this.PasswordType = type;
        }
        public UpdateTradePasswordTypeEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.TradePasswordType = this.PasswordType;
            }
        }
    }
}
