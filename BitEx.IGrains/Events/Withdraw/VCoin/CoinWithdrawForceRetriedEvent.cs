using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawForceRetriedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string CurrencyId { get; set; }
        public string Address { get; set; }
        public decimal Volume { get; set; }
        public CoinWithdrawStatus Status { get; set; }
        public int OperatorId { get; set; }
        public string Memo { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawForceRetriedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinWithdrawForceRetriedEvent()
        {
        }
        public CoinWithdrawForceRetriedEvent(string commandId, string currencyId, string address, decimal volume, int operatorId, string memo)
        {
            this.CommandId = commandId;
            this.CurrencyId = currencyId;
            this.Address = address;
            this.Volume = volume;
            this.OperatorId = operatorId;
            this.Status = CoinWithdrawStatus.Processing;
            this.Memo = memo;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinWithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.OperatorId = this.OperatorId;
            }
        }
    }
}
