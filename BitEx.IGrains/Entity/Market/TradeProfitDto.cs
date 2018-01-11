namespace BitEx.IGrain.Entity
{
    public class TradeProfitDto
    {
        public decimal AskAvgPrice { get; set; }
        public decimal AskVolume { get; set; }
        public decimal AskAmount { get; set; }
        public decimal BidAvgPrice { get; set; }
        public decimal BidVolume { get; set; }
        public decimal BidAmount { get; set; }
        public decimal HeldAvgPrice { get; set; }
        public decimal HeldVolume { get; set; }
        public decimal HeldAmount { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal MarketAmount { get; set; }
        public decimal HistoryProfit { get; set; }
        public decimal HistoryProfitRate { get; set; }
        public decimal FloatProfit { get; set; }
        public decimal FloatProfitRate { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TotalProfitRate { get; set; }
    }
}
