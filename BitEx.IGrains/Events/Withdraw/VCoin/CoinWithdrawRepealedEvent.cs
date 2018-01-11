using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Coin.Core;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawRepealedEvent : IEventBase<string>
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
        /// <summary>
        /// 折合人民币的金额
        /// </summary>
        public decimal CnyAmount { get; set; }
        public decimal Fee { get; set; }
        public TResult Result { get; set; }
        public CoinWithdrawStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawRepealedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinWithdrawRepealedEvent()
        {
        }
        public CoinWithdrawRepealedEvent(string commandId, string userId, string currencyId, string accountId, string address, decimal volume, decimal cnyAmount, decimal fee, TResult result)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.AccountId = accountId;
            this.Address = address;
            this.Volume = volume;
            this.CnyAmount = cnyAmount;
            this.Fee = fee;
            this.Result = result;
            this.Status = CoinWithdrawStatus.Canceled;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinWithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Result = this.Result.ToString();
                modelState.Status = this.Status;
            }
        }
    }
}
