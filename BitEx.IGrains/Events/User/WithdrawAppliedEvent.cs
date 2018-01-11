using System;
using Coin.Core.EventSourcing;
using Coin.Framework.ThirdParty;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawAppliedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public BankType BankType { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string BranchInfo { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public UserVipLevel VipLevel { get; set; }
        public WithdrawValidResult ValidResult { get; set; }
        private static string _TypeCode = typeof(WithdrawAppliedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; }
        public WithdrawAppliedEvent(string commandId, UserVipLevel vipLevel, string currencyId, string accountId, BankType bankType, string branchInfo, string province, string city, string cardNumber, decimal amount, WithdrawValidResult validResult)
        {
            this.Province = province;
            this.City = city;
            this.CommandId = commandId;
            this.CurrencyId = currencyId;
            this.CardNumber = cardNumber;
            this.Amount = amount;
            this.ValidResult = validResult;
            this.AccountId = accountId;
            this.BankType = bankType;
            this.BranchInfo = branchInfo;
            this.VipLevel = vipLevel;
        }
        public WithdrawAppliedEvent() { }
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
                modelState.WithdrawalDayLimit += this.Amount;
                modelState.WithdrawalMonthLimit += this.Amount;
            }
        }
    }
}
