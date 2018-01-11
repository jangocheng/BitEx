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
    public class SetTradePasswordEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(SetTradePasswordEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string Password { get; set; }
        public TradePasswordType Type { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public SetTradePasswordEvent(string password, TradePasswordType type)
        {
            this.Password = password;
            Type = type;
        }
        public SetTradePasswordEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                if (modelState.TradePasswordType == TradePasswordType.None && string.IsNullOrEmpty(modelState.TradePassword))
                {
                    modelState.Points += 1000;
                }
                modelState.TradePassword = this.Password;
                modelState.TradePasswordType = Type;
            }
        }
    }
}
