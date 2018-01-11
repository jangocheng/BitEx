using System;

namespace BitEx.IGrain.Entity
{
    public class CoinDepositDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string AccountId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Volume { get; set; }
        public string TxVolume { get; set; }
        public int Confirmation { get; set; }
        public int TxConfirmation { get; set; }
        public int Safe { get; set; }
        public string Fee { get; set; }
        public string TexFee { get; set; }
        public string TxNo { get; set; }
        public string ExplorerUrl { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Memo { get; set; }
    }
}
