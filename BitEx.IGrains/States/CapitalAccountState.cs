using Coin.Core.EventSourcing;
using ProtoBuf;
using System;
using Coin.Framework.ThirdParty;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CapitalAccountState : IState<string>
    {
        public CapitalAccountState()
        {
        }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public BankType AccountType { get; set; }
        public string Subbranch { get; set; }
        public string OwnerName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        //此字段标志着该账号为用户银行转账方式，默认显示的银行卡
        public bool IsDefault { get; set; }
        public AccountStatus Status { get; set; }
        public string Result { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
