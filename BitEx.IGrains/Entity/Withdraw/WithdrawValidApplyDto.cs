using BitEx.IGrain.Entity.User;
using System.Collections.Generic;

namespace BitEx.IGrain.Entity
{
    public class WithdrawValidApplyDto
    {
        public string AccountId { get; set; }
        public int DefaultVerifyWay { get; set; }
        public List<BankCardInfo> BankCardList { get; set; }
    }
}
