using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.Currency
{
    public class CurrencyServiceFeeDto
    {
        public string Id { get; set; }
        public decimal DepositFixedFee { get; set; }
        public decimal DepositFeeRate { get; set; }
        public decimal WithdrawFeeRate { get; set; }
        public decimal WithdrawFixedFee { get; set; }
    }
}
