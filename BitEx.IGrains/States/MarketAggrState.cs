using BitEx.IGrain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitEx.IGrain.States
{
    public class MarketAggrState
    {
        public DateTime LastSentTime { get; set; }
        public Dictionary<string, KPoint> KLines { get; set; } = new Dictionary<string, KPoint>();
        public StringBuilder InsertSqlBuilder { get; set; } = new StringBuilder();
    }
}
