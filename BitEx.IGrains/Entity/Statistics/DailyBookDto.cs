namespace BitEx.IGrain.Entity
{
    public class DailyBookDto
    {
        public string CurrencyId { get; set; }
        public string Period { get; set; }
        public decimal BeginBalance { get; set; }
        public decimal Deposit { get; set; }
        public decimal DepositFee { get; set; }
        public decimal DepositTxFee { get; set; }
        public decimal Depositing { get; set; }
        public decimal DepositingFee { get; set; }
        public decimal Withdraw { get; set; }
        public decimal WithdrawFee { get; set; }
        public decimal WithdrawTxFee { get; set; }
        public decimal Withdrawing { get; set; }
        public decimal WithdrawingFee { get; set; }
        public decimal TradeFee { get; set; }
        public decimal CoinDividend { get; set; }
        public decimal PromoterDividend { get; set; }
        public decimal TransferIn { get; set; }
        public decimal TransferInvest { get; set; }
        public decimal TransferHard { get; set; }
        public decimal EndBalance { get; set; }
        public decimal CapitalBalance { get; set; }
        public decimal Hotbalance { get; set; }
        public decimal ColdBalance { get; set; }
    }
}

