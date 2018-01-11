using System;

namespace BitEx.IGrain.Entity
{
    public class CoinWithdrawDto
    {
        public string Id { get; set; }
        public string CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public decimal TxAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal TxFee { get; set; }
        public string Address { get; set; }
        public string ExplorerUrl { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Result { get; set; }
        public string Memo { get; set; }
        public string TxNo { get; set; }
    }
}
