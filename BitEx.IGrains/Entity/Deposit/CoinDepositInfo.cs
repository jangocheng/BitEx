namespace BitEx.IGrain.Entity
{
    public class CoinDepositInfo
    {
        public decimal FixedFee { get; set; }
        public decimal FeeRate { get; set; }
        public decimal NotifyLine { get; set; }
        public int Precision { get; set; }
    }
}
