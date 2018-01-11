using BitEx.IGrain.Entity.User;
using System.Collections.Generic;

namespace BitEx.IGrain.Entity
{
    public class CoinWithdrawValidApplyDto
    {
        public string AccountId { get; set; }
        public int DefaultVerifyWay { get; set; }
        public List<WithdrawAddress> AddressList { get; set; }
    }
}
