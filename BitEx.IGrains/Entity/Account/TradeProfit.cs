using Orleans.Concurrency;
using ProtoBuf;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class TradeProfit
    {
        public decimal AskAvgPrice { get; set; }
        public decimal AskVolume { get; set; }
        public decimal AskAmount { get; set; }
        public decimal BidAvgPrice { get; set; }
        public decimal BidVolume { get; set; }
        public decimal BidAmount { get; set; }
        /// <summary>
        /// 持仓均价
        /// </summary>
        public decimal HeldAvgPrice { get; set; }
    }
}
