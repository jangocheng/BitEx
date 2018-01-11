namespace BitEx.IGrain.Entity
{
    public class CurrencyDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsVirtualCoin { get; set; }
        public bool IsBasicCoin { get; set; }
        public string AssetId { get; set; }
        public decimal BalanceNotifyLine { get; set; }
        public decimal DepositFixedFee { get; set; }
        public decimal DepositFeeRate { get; set; }
        public decimal DepositNotifyLine { get; set; }
        public decimal WithdrawFeeRate { get; set; }
        public decimal WithdrawFixedFee { get; set; }
        public decimal WithdrawOnceMin { get; set; }
        public decimal WithdrawOnceLimit { get; set; }
        public decimal WithdrawVerifyLine { get; set; }
        public int Required { get; set; }
        public int Safe { get; set; }
        public bool IsLocked { get; set; }
        public string ExplorerUrl { get; set; }   
        /// <summary>
        /// 抵押到账折扣率
        /// </summary>
        public float MgDiscount { get; set; }
        public decimal TotalCirculation { get; set; }
        public decimal HotBalance { get; set; }
        public decimal ColdBalance { get; set; }
        public int WithdrawPrecision { get; set; }
        public int ShowPrecision { get; set; }
        public bool IsDividendCoin { get; set; } = false;
    }
}
