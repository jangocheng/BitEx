namespace BitEx.IGrain.Entity
{
    public class WithdrawInfo
    {
        public decimal WithdrawFeeRate { get; set; }
        public decimal WithdrawFixedFee { get; set; }
        public decimal WithdrawOnceMin { get; set; }
        public decimal WithdrawOnceLimit { get; set; }
        public decimal WithdrawVerifyLine { get; set; }
        public int Precision { get; set; }
    }
}
