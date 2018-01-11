using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class DepositCodeUsedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string CurrencyId { get; set; }
        public string UsedBy { get; set; }
        public string FundSourceId { get; set; }
        public decimal Amount { get; set; }
        public DepositCodeStatus Status { get; set; }
        private static string _TypeCode = typeof(DepositCodeUsedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public DepositCodeUsedEvent()
        {
        }
        public DepositCodeUsedEvent(string commandId, string currencyId, string fundSourceId, decimal amount, string usedBy)
        {
            this.CommandId = commandId;
            this.CurrencyId = currencyId;
            this.FundSourceId = fundSourceId;
            this.Amount = amount;
            this.UsedBy = usedBy;
            this.Status = DepositCodeStatus.Used;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as DepositCodeState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.UsedAt = this.Timestamp;
                modelState.UsedBy = this.UsedBy;
                modelState.Status = this.Status;
            }
        }
    }
}
