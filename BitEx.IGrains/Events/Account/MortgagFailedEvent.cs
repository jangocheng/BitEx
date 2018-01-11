using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MortgagFailedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string DepositId { get; set; }
        public string DepositCurrencyId { get; set; }
        private static string _TypeCode = typeof(MortgagFailedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MortgagFailedEvent()
        {
        }
        public MortgagFailedEvent(string commandId, string userId, string currencyId, string depositId, string depositCurrencyId)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.DepositId = depositId;
            this.DepositCurrencyId = depositCurrencyId;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as IState<string>;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
            }
        }
    }
}
