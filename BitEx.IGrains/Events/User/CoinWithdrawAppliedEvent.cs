using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.States;
using BitEx.IGrain.Entity.User;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawAppliedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string Address { get; set; }
        public string Memo { get; set; }
        public decimal Volume { get; set; }
        public UserVipLevel VipLevel { get; set; }
        public decimal RmbAmount { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public WithdrawValidResult ValidResult { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawAppliedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinWithdrawAppliedEvent(string commandId, string currencyId, string accountId, decimal volume, UserVipLevel vipLevel, decimal rmbAmount, WithdrawValidResult validResult, string address, string memo = null)
        {
            this.CommandId = commandId;
            this.Address = address;
            this.Memo = memo;
            this.Volume = volume;
            this.ValidResult = validResult;
            this.VipLevel = vipLevel;
            this.RmbAmount = rmbAmount;
            this.CurrencyId = currencyId;
            this.AccountId = accountId;
        }
        public CoinWithdrawAppliedEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                if ((Timestamp - modelState.WithdrawalDayTime).Days > 0)
                {
                    modelState.WithdrawalDayTime = Timestamp;
                    modelState.WithdrawalDayLimit = 0;
                }
                if (DateTime.Now.Year > modelState.WithdrawalMonthTime.Year || DateTime.Now.Month > modelState.WithdrawalMonthTime.Month)
                {
                    modelState.WithdrawalMonthTime = Timestamp;
                    modelState.WithdrawalMonthLimit = 0;
                }
                modelState.WithdrawalDayLimit += this.RmbAmount;
                modelState.WithdrawalMonthLimit += this.RmbAmount;
            }
        }
    }
}
