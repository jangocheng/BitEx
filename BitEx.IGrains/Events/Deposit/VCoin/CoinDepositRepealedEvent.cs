using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CoinDepositRepealedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public bool IsDeposited { get; set; }
        public decimal Volume { get; set; }
        public string Reason { get; set; }
        public int OperatorId { get; set; }
        public CoinDepositStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinDepositRepealedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositRepealedEvent()
        {
        }
        public CoinDepositRepealedEvent(string commandId, string userId, string currencyId, bool isDeposited, decimal volume, string reason, int operatorId)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.Volume = volume;
            this.IsDeposited = isDeposited;
            this.Reason = reason;
            this.OperatorId = operatorId;
            this.Status = CoinDepositStatus.Canceled;
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
