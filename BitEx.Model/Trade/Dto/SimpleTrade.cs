using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.Model.Trade.Dto
{
    public class SimpleTrade
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
