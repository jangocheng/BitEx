using Coin.Framework.ThirdParty;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Entity
{
    public class DepositConfirmationInfo
    {
        public BankType AccountType { get; set; }
        public PayWay PayWay { get; set; }
        public string CapitalAccountId { get; set; }
    }
}
