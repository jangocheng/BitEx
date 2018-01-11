namespace BitEx.IGrain.Entity
{
    public class DepositInfo
    {
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyId { get; set; }
        public string TxNo { get; set; }
        public int Confirmation { get; set; }
    }
}
