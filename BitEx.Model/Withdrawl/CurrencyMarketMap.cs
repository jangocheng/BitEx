using BitEx.Dapper.Core;

namespace BitEx.Model.Withdrawl
{
    [Table("Coin_ExchangeMapper")]
    public class ExchangeMapper
    {
        public string BasicId { get; set; }
        public string TargetId { get; set; }
        public string FirstMarketId { get; set; }
        public string SecondMarketId { get; set; }
    }
}
