using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawHandledEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public string Address { get; set; }
        public decimal Volume { get; set; }
        public CoinWithdrawStatus Status { get; set; }
        public int OperatorId { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawHandledEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinWithdrawHandledEvent()
        {
        }
        public CoinWithdrawHandledEvent(string commandId, string currencyId, string accountId, string address, decimal volume, int operatorId)
        {
            this.CommandId = commandId;
            this.CurrencyId = currencyId;
            this.AccountId = accountId;
            this.Address = address;
            this.Volume = volume;
            this.OperatorId = operatorId;
            this.Status = CoinWithdrawStatus.Processing;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinWithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
                modelState.OperatorId = this.OperatorId;
            }
        }
    }
}
