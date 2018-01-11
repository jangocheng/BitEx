using Orleans.Concurrency;

namespace BitEx.IGrain.Entity.Market
{
    [Immutable]
    public class TradeFeeRateDto
    {
        public decimal MakerFeeRate { get; set; }
        public decimal TakerFeeRate { get; set; }
    }
}
