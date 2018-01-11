using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositGuaranteeFailedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string DepositCurrencyId { get; set; }
        public decimal Volume { get; set; }
        public CoinDepositStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinDepositGuaranteeFailedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositGuaranteeFailedEvent()
        {
        }
        public CoinDepositGuaranteeFailedEvent(string commandId, string userId, string depositCurrencyId, decimal volume, CoinDepositStatus status)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.DepositCurrencyId = depositCurrencyId;
            this.Volume = volume;
            this.Status = status | CoinDepositStatus.GuaranteeFailed;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinDepositState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
            }
        }
    }
}
