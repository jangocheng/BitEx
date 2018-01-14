using System;
using BitEx.Dapper.Core;

namespace BitEx.Model.Order
{
    [Table("Coin_PlanOrder", autoIncrement: false)]
    public class PlanOrderInfo
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public string MarketId { get; set; }
        public short OrderType { get; set; }
        public decimal HighTriggerPrice { get; set; }
        public decimal LowTriggerPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public short Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
