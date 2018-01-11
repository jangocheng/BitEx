using System.Collections.Generic;

namespace BitEx.IGrain.Entity
{
    public class KLine
    {
        public int Frequency { get; set; }
        public List<KPoint> Points { get; set; }
    }
}
