using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitEx.IGrain.Entity.Market
{
    public class UserMarketInfo
    {
        public decimal BasicBalance { get; set; }
        public decimal BasicLockedAmount { get; set; }
        public decimal TargetBalance { get; set; }
        public decimal TargetLockedAmount { get; set; }
        public int LoginStatus { get; set; }
        public string OrderPriceLimit { get; set; }
    }
}