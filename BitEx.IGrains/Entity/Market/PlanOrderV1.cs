using BitEx.IGrain.Entity.User;
using BitEx.IGrain.States;
using ProtoBuf;
using System;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class PlanOrderInfoV1
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public UserVipLevel VipLevel { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public OrderType OrderType { get; set; }
        public decimal HighTriggerPrice { get; set; }
        public decimal LowTriggerPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal TradeAmount { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
