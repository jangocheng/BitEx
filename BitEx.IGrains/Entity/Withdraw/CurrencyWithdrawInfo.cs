namespace BitEx.IGrain.Entity
{
    public class CurrencyWithdrawInfo
    {
        public decimal WithdrawFeeRate { get; set; }
        public decimal WithdrawFixedFee { get; set; }
        public decimal WithdrawOnceMin { get; set; }
        public decimal WithdrawOnceLimit { get; set; }
    }
}
