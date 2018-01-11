using System;

namespace BitEx.IGrain.Entity
{
    public class TradeDto
    {
        public string MarketId { get; set; }
        public int Id { get; set; }
        public bool IsAsk { get; set; }
        public string AskUserId { get; set; }
        public string AskOrderId { get; set; }
        public decimal AskFee { get; set; }
        public string BidUserId { get; set; }
        public string BidOrderId { get; set; }
        public decimal BidFee { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
