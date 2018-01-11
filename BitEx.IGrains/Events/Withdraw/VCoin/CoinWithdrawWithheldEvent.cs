using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawWithheldEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public decimal Volume { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        public decimal TxVolume { get; set; }
        /// <summary>
        /// 折合人民币的金额
        /// </summary>
        public decimal CnyAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal LastTotalAmount { get; set; }
        public WithdrawValidResult ValidResult { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawWithheldEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; }
        public CoinWithdrawWithheldEvent()
        {
        }
        public CoinWithdrawWithheldEvent(string commandId, string userId, string currencyId, string address, decimal balance, decimal volume, decimal txVolume, decimal cnyAmount, decimal fee, WithdrawValidResult validResult, DateTime createdAt, string memo = null)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.Address = address;
            this.Balance = balance;
            this.Volume = volume;
            this.TxVolume = txVolume;
            this.CnyAmount = cnyAmount;
            this.Fee = fee;
            this.ValidResult = validResult;
            this.Timestamp = createdAt;
            this.Memo = memo;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                if (this.Timestamp.Date.Subtract(modelState.LastStatisticsTotalAmountTime).TotalDays >= 1)
                {
                    modelState.LastTotalAmount = modelState.Balance + modelState.Locked + modelState.Mortgaged;
                    modelState.LastStatisticsTotalAmountTime = this.Timestamp.Date;
                }
                this.LastTotalAmount = modelState.LastTotalAmount;
                modelState.Balance = this.Balance;
            }
        }
    }
}
