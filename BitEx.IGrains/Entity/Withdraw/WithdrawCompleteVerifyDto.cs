using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.Withdraw
{
    public class WithdrawCompleteVerifyDto
    {
        public string Province { get; set; }
        public string City { get; set; }
        public string BankNumber { get; set; }
        public string UserId { get; set; }
    }
}
