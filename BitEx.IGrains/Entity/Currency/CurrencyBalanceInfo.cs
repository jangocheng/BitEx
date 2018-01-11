namespace BitEx.IGrain.Entity
{
    public class CurrencyBalanceInfo
    {
        public decimal HotBalance { get; set; }
        public decimal ColdBalance { get; set; }
        public CurrencyBalanceInfo(decimal hotBalance, decimal coldBalance)
        {
            this.HotBalance = hotBalance;
            this.ColdBalance = coldBalance;
        }
    }
}
