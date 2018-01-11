namespace BitEx.IGrain.Entity
{
    public class CoinTransferDto
    {
        public string Id { get; set; }
        public string CurrencyId { get; set; }
        public byte TransferType { get; set; }
        public string FromAddress { get; set; }
        public string TargetAddress { get; set; }
        public string Amount { get; set; }
        public string TxNo { get; set; }
        public string Remark { get; set; }
        public string CreatedAt { get; set; }
    }
}
