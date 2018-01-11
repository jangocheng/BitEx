using Coin.Framework.ThirdParty;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Entity
{
    public  class CapitalAccountDefaultInfo
    {
        public bool IsDefault { get; set; }
        public BankType AccountType { get; set; }
    }
}
