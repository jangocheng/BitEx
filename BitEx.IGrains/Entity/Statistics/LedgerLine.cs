using System;

namespace BitEx.IGrain.Entity
{
    public class LedgerLine
    {
        public decimal PlatBalance { get; set; }
        public decimal BookBalance { get; set; }
        public DateTime Period { get; set; }
    }
}
