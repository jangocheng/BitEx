using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using Coin.Core;
using Coin.Core.Message;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawWithholdingFailedMsg : IActorOwnMessage<string>
    {
        public string MsgId { get; set; }
        public string StateId { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string Address { get; set; }
        public string Memo { get; set; }
        public decimal Volume { get; set; }
        public decimal CnyAmount { get; set; }
        public string CauseCode { get; set; }
        public DateTime CreatedAt { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawWithholdingFailedMsg).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public CoinWithdrawWithholdingFailedMsg()
        {
        }
        public CoinWithdrawWithholdingFailedMsg(string msgId, string userId, string currencyId, string address, decimal volume, decimal amount, int causeCode, DateTime createdAt, string memo)
        {
            this.MsgId = msgId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.Address = address;
            this.Volume = volume;
            this.CnyAmount = amount;
            this.CauseCode = causeCode.ToString();
            this.CreatedAt = createdAt;
            this.Memo = memo;
        }
    }
}
