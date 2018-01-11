using ProtoBuf;
using System;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class KPoint
    {
        public string FrequencyKey { get; set; }
        public int Id { get; set; }
        public int TradeId { get; set; }
        public DateTime OpenTime { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal HighPrice { get; set; }
        public DateTime ClosedTime { get; set; }
        public decimal ClosedPrice { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
        public UInt32 Version { get; set; }
        [ProtoIgnore]
        public bool NeedSave { get; set; } = true;
    }
}
