namespace BitEx.IGrain.Entity
{
    public class AccountAssetInfo
    {
        public string Id { get; set; }
        public string CurrencyId { get; set; }
        public bool IsVirtualCoin { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
