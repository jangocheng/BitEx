using Coin.Core.EventSourcing;
using ProtoBuf;
using System;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CapitalTransferState : IState<string>
    {
        public CapitalTransferState()
        {
        }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string FromAccountId { get; set; }
        public string ToAccountId { get; set; }
        public string CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public string TxNo { get; set; }
        public decimal TxFee { get; set; }
        public string Remark { get; set; }
        public int OperatorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
