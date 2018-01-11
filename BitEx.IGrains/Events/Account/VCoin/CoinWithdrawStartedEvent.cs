using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawStartedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public decimal Volume { get; set; }
        public decimal TxVolume { get; set; }
        /// <summary>
        /// 折合人民币的金额
        /// </summary>
        public decimal CnyAmount { get; set; }
        public decimal Fee { get; set; }
        public WithdrawValidResult ValidResult { get; set; }
        public CoinWithdrawStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawStartedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinWithdrawStartedEvent()
        {
        }
        public CoinWithdrawStartedEvent(string commandId, string userId, string currencyId, string accountId, string address, decimal balance, decimal volume, decimal txVolume, decimal cnyAmount, decimal fee, WithdrawValidResult validResult, CoinWithdrawStatus status, DateTime createdAt)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.AccountId = accountId;
            this.Address = address;
            this.Balance = balance;
            this.Volume = volume;
            this.TxVolume = txVolume;
            this.CnyAmount = cnyAmount;
            this.Fee = fee;
            this.ValidResult = validResult;
            this.Status = status;
            this.CreatedAt = createdAt;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinWithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.UserId = this.UserId;
                modelState.AccountId = this.AccountId;
                modelState.CurrencyId = this.CurrencyId;
                modelState.Address = this.Address;
                modelState.Volume = this.Volume;
                modelState.TxVolume = TxVolume;
                modelState.CnyAmount = this.CnyAmount;
                modelState.Fee = this.Fee;
                modelState.ValidResult = this.ValidResult;
                modelState.Status = this.Status;
            }
        }
    }
}
