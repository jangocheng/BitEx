using System;
using ProtoBuf;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class PlanOrderInfoDto
    {
        [ProtoMember(1)]
        public string Id { get; set; }
        [ProtoMember(2)]
        public string UserId { get; set; }
        [ProtoMember(3)]
        public string CurrencyId { get; set; }
        [ProtoMember(4)]
        public string AccountId { get; set; }
        [ProtoMember(5)]
        public string MarketId { get; set; }
        [ProtoMember(6)]
        public Int32 OrderType { get; set; }
        [ProtoMember(7)]
        public double HighTriggerPrice { get; set; }
        [ProtoMember(8)]
        public double LowTriggerPrice { get; set; }
        [ProtoMember(9)]
        public double HighPrice { get; set; }
        [ProtoMember(10)]
        public double LowPrice { get; set; }
        [ProtoMember(11)]
        public double Price { get; set; }
        [ProtoMember(12)]
        public double Amount { get; set; }
        [ProtoMember(13)]
        public Int32 Status { get; set; }
        [ProtoMember(14)]
        public string CreateTime { get; set; }
        [ProtoMember(15)]
        public string UpdateTime { get; set; }
    }
}
