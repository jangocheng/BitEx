using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.Withdraw
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawNoticeDto
    {
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public decimal Amount { get; set; }
    }
}
