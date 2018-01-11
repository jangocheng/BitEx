using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositInfoChangedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public string Address { get; set; }
        public decimal Volume { get; set; }
        public string TxNo { get; set; }
        public string Result { get; set; }
        public CoinDepositSpecialStatus SpecialStatus { get; set; }
        private static string _TypeCode = typeof(CoinDepositInfoChangedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositInfoChangedEvent()
        {
        }
        public CoinDepositInfoChangedEvent(string commandId, string userId, string currencyId, string accountId, string address, decimal volume, string txNo, CoinDepositSpecialStatus specialStatus, string reason = null)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.AccountId = accountId;
            this.Address = address;
            this.Volume = volume;
            this.TxNo = txNo;
            this.SpecialStatus = specialStatus;
            this.Result = reason;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinDepositState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.UserId = this.UserId;
                modelState.AccountId = this.AccountId;
                modelState.SpecialStatus = this.SpecialStatus;
                modelState.Result = this.Result;
            }
        }
    }
}
