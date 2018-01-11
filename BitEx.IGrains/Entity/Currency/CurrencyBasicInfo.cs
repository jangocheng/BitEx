using System;

namespace BitEx.IGrain.Entity
{
    public class CurrencyBasicInfo
    {
        public string Id { get; set; }
        public decimal HotBalance { get; set; }
        public decimal Difficulty { get; set; }
        public int PeerCount { get; set; }
        public long Height { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
