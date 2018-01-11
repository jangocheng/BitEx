namespace BitEx.IGrain.Entity
{
    public class DepositInfoDto
    {
        public string Id { get; set; }
        public string OwnerName { get; set; }
        public string BankName { get; set; }
        public string Subbranch { get; set; }
        public string AccountNumber { get; set; }
        public string CurrencyId { get; set; }
        public int Identify { get; set; }
        public decimal Amount { get; set; }
    }
}
