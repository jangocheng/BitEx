using System;

namespace Coin.Model
{
    public class Deposit
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public string FundSourceId { get; set; }
        public byte DepositWay { get; set; }
        public string CapitalAccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal TxAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal TxFee { get; set; }
        public int OperatorId { get; set; }
        public string Result { get; set; }
        public byte Status { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
