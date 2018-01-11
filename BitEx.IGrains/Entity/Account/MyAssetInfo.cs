namespace BitEx.IGrain.Entity
{
    public class MyAssetInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string Address { get; set; }
        public bool IsVirtualCoin { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal LockedAmount { get; set; }
        public decimal MortgagedAmount { get; set; }
        public decimal WithdrawOnceMin { get; set; }
        public decimal WithdrawOnceLimit { get; set; }
        public int ShowPrecision { get; set; }
    }
}
