using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using Coin.Core;
using BitEx.IGrain.States;
using Coin.Core.Message;
using Coin.Framework.ThirdParty;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawWithholdingFailedMsg : IActorOwnMessage<string>
    {
        public string MsgId { get; set; }
        public string StateId { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public BankType BankType { get; set; }
        public string BranchInfo { get; set; }
        public string AcceptAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string CauseCode { get; set; }
        public DateTime CreatedAt { get; set; }
        private static string _TypeCode = typeof(WithdrawWithholdingFailedMsg).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public WithdrawWithholdingFailedMsg()
        {
        }
        public WithdrawWithholdingFailedMsg(string msgId, string userId, string currencyId, BankType bankType, string branchInfo, string acceptAccountNumber, decimal amount, int causeCode, DateTime createdAt)
        {
            this.MsgId = msgId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.BankType = bankType;
            this.BranchInfo = branchInfo;
            this.AcceptAccountNumber = acceptAccountNumber;
            this.Amount = amount;
            this.CauseCode = causeCode.ToString();
            this.CreatedAt = createdAt;
        }
    }
}
