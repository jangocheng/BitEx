using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositCreatedEvent : IEventBase<string>
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
        public decimal TxVolume { get; set; }
        public decimal Fee { get; set; }
        public decimal TxFee { get; set; }
        public string TxNo { get; set; }
        public CoinDepositStatus Status { get; set; }
        public string Memo { get; set; }
        public CoinDepositSpecialStatus SpecialStatus { get; set; }

        private static string _TypeCode = typeof(CoinDepositCreatedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositCreatedEvent()
        {
        }
        public CoinDepositCreatedEvent(string commandId, string userId, string currencyId, string accountId, string address, decimal volume, decimal txVolume, decimal fee, decimal txFee, string txNo,string memo,CoinDepositSpecialStatus specialStatus)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.AccountId = accountId;
            this.Address = address;
            this.Volume = volume;
            this.TxVolume = txVolume;
            this.Fee = fee;
            this.TxFee = txFee;
            this.TxNo = txNo;
            this.Status = CoinDepositStatus.Increased;
            this.Memo = memo;
            this.SpecialStatus = specialStatus;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinDepositState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.UserId = this.UserId;
                modelState.CurrencyId = this.CurrencyId;
                modelState.AccountId = this.AccountId;
                modelState.Address = this.Address;
                modelState.Volume = this.Volume;
                modelState.TxVolume = this.TxVolume;
                modelState.Fee = this.Fee;
                modelState.TxFee = this.TxFee;
                modelState.TxNo = this.TxNo;
                modelState.Status = this.Status;
                modelState.Memo = this.Memo;
                modelState.SpecialStatus = this.SpecialStatus;
            }
        }
    }
}
