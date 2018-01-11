using Coin.Framework.ThirdParty;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Entity
{
    public class FundSource
    {
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public string FundSourceId { get; set; }
        public BankType AccountType { get; set; }
        public string CapitalAccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal TxAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal TxFee { get; set; }
        public string Remark { get; set; }
    }
}
