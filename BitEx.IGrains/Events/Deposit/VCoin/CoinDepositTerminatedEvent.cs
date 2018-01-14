using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.States;
using Coin.Core;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositTerminatedEvent : IEventBase<string>
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
        public bool IsDeposited { get; set; }
        public int Confirmation { get; set; }
        public int TxConfirmation { get; set; }
        public string TxNo { get; set; }
        public Result Result { get; set; }
        public CoinDepositStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinDepositTerminatedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinDepositTerminatedEvent()
        {
        }
        public CoinDepositTerminatedEvent(string commandId, string userId, string currencyId, string accountId, string address, decimal volume, string txNo, bool isDeposited, int confirmation, int txConfirmation, Result result)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.AccountId = accountId;
            this.Address = address;
            this.Volume = volume;
            this.IsDeposited = isDeposited;
            this.TxNo = txNo;
            this.Confirmation = confirmation;
            this.TxConfirmation = txConfirmation;
            this.Status = CoinDepositStatus.Canceled;
            this.Result = result;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinDepositState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Confirmation = this.Confirmation;
                modelState.TxConfirmation = this.TxConfirmation;
                modelState.Status = this.Status;
                modelState.Result = this.Result.ToString();
            }
        }
    }
}
