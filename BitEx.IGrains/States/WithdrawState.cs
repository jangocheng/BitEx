using Coin.Core.EventSourcing;
using ProtoBuf;
using System;
using Coin.Framework.ThirdParty;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawState : IState<string>
    {
        public WithdrawState()
        {
        }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public BankType AccountType { get; set; }
        public string Subbranch { get; set; }
        public string AccountNumber { get; set; }
        public string CapitalAccountId { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public decimal Amount { get; set; }
        public decimal TxAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal TxFee { get; set; }
        public string TxNo { get; set; }
        public int OperatorId { get; set; }
        public string Result { get; set; }
        public WithdrawStatus Status { get; set; }
    }
}