using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositSpecialConfirmedEvent : IEventBase<string>
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
        public int Confirmation { get; set; } = 0;
        public string TxNo { get; set; }
        public CoinDepositSpecialStatus SpecialStatus { get; set; }
        private static string _TypeCode = typeof(CoinDepositSpecialConfirmedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositSpecialConfirmedEvent()
        {
        }
        public CoinDepositSpecialConfirmedEvent(string commandId, string userId, string currencyId, string accountId, string address, decimal volume, string txNo, CoinDepositSpecialStatus status)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.AccountId = accountId;
            this.Address = address;
            this.Volume = volume;
            this.TxNo = txNo;
            this.SpecialStatus = status;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinDepositState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.SpecialStatus = this.SpecialStatus;
                modelState.UserId = this.UserId;
                modelState.AccountId = this.AccountId;
            }
        }
    }
}
