namespace BitEx.IGrain.Entity
{
    public class PendingDividend
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public decimal HeldVolume { get; set; }
        public decimal Amount { get; set; }
    }
}
