namespace BitEx.IGrain.Entity
{
    public class TransferDto
    {
        public string Id { get; set; }
        public string CurrencyId { get; set; }
        public byte TransferType { get; set; }
        public string BankName { get; set; }
        public string OwnerName { get; set; }
        public string Subbranch { get; set; }
        public string AccountNumber { get; set; }
        public string TxBankName { get; set; }
        public string TxOwnerName { get; set; }
        public string TxSubbranch { get; set; }
        public string TxAccountNumber { get; set; }
        public string Amount { get; set; }
        public string TxNo { get; set; }
        public string Remark { get; set; }
        public string CreatedAt { get; set; }
    }
}
