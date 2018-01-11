namespace BitEx.IGrain.Entity
{
    public class WithdrawExportDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte AccontType { get; set; }
        public string BankName { get; set; }
        public string Subbranch { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public decimal Fee { get; set; }
    }
}
