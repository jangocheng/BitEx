using System;

namespace BitEx.IGrain.Entity
{
    public class StatisticsPeriod
    {
        public string CurrencyId { get; set; }
        public bool IsVirtualCoin { get; set; }
        public DateTime LastPeriod { get; set; }
    }
}
