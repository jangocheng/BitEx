using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.States;
using Coin.Core;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositRedeemingFailedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string DepositCurrencyId { get; set; }
        public string GuaranteeId { get; set; }
        public decimal Volume { get; set; }
        public CoinDepositStatus Status { get; set; }
        public GuaranteeStatus GuaranteeStatus { get; set; }
        public string Result { get; set; }
        private static string _TypeCode = typeof(CoinDepositRedeemingFailedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositRedeemingFailedEvent()
        {
        }
        public CoinDepositRedeemingFailedEvent(string commandId, string userId, string depositCurrencyId, decimal volume, string guaranteeId, string result)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.DepositCurrencyId = depositCurrencyId;
            this.GuaranteeId = guaranteeId;
            this.Volume = volume;
            this.Result = result;
            this.Status = CoinDepositStatus.Increased | CoinDepositStatus.GuaranteeSuccessed;
            this.GuaranteeStatus = GuaranteeStatus.Failed;
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
