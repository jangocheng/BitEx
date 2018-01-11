namespace BitEx.IGrain.Entity
{
    public class AvailableAsset
    {
        public string CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public decimal LockedAmount { get; set; }
    }
}
