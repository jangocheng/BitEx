using System;
using ProtoBuf;
using BitEx.IGrain.States;
using Ray.Core.EventSourcing;
using BitEx.Model.User;

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
        public decimal limitExchangeAmount { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public WithdrawValidResult ValidResult { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawAppliedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinWithdrawAppliedEvent(string commandId, string currencyId, string accountId, decimal volume, UserVipLevel vipLevel, decimal limitExchagneAmount, WithdrawValidResult validResult, string address, string memo = null)
        {
            this.CommandId = commandId;
            this.Address = address;
            this.Memo = memo;
            this.Volume = volume;
            this.ValidResult = validResult;
            this.VipLevel = vipLevel;
            this.limitExchangeAmount = limitExchagneAmount;
            this.CurrencyId = currencyId;
            this.AccountId = accountId;
        }
        public CoinWithdrawAppliedEvent() { }
    }
}
