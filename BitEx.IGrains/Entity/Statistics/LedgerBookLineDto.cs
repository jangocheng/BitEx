using System.Collections.Generic;

namespace BitEx.IGrain.Entity
{
    public class LedgerBookLineDto
    {
        public string Period { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal IncomeAmount { get; set; }
        public decimal LedgerAmount { get; set; }
    }
}
