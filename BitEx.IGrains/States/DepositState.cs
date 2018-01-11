using Coin.Core.EventSourcing;
using ProtoBuf;
using System;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class DepositState : IState<string>
    {
        public DepositState()
        {
        }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public DepositWay DepositWay { get; set; }
        public string CapitalAccountId { get; set; }
        public string FundSourceId { get; set; }
        public decimal Amount { get; set; }
        public decimal TxAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal TxFee { get; set; }
        public string Remark { get; set; }
        public int OperatorId { get; set; }
        public string Result { get; set; }
        public DateTime DoneAt { get; set; }
        public DepositStatus Status { get; set; }
        public int Identify { get; set; }
    }
}
