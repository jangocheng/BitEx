using BitEx.IGrain.States;

namespace BitEx.IGrain.Entity
{
    public class CapitalAccountDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public byte AccountType { get; set; }
        public string BankName { get; set; }
        public string Subbranch { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsDefault { get; set; }
        public string Status { get; set; }
    }
}
