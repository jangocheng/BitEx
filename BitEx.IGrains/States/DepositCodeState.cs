using Coin.Core.EventSourcing;
using ProtoBuf;
using System;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class DepositCodeState : IState<string>
    {
        public DepositCodeState()
        {
        }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string CurrencyId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public string Password { get; set; }
        public string FundSourceId { get; set; }
        public int OperatorId { get; set; }
        public string UsedBy { get; set; }
        public DateTime UsedAt { get; set; }
        public DateTime DestroyedAt { get; set; }
        public DepositCodeStatus Status { get; set; }
        public int CreatedBy { get; set; }
    }
}
