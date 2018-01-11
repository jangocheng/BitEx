namespace BitEx.IGrain.Entity
{
    public class TurnoverDto
    {
        public string MarketId { get; set; }
        public string Period { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
    }
}
