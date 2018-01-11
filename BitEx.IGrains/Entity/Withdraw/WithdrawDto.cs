using System;

namespace BitEx.IGrain.Entity
{
    public class WithdrawDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string BankName { get; set; }
        public byte PayWay { get; set; }
        public string PayWayName { get; set; }
        public string Subbranch { get; set; }
        public string AccountNumber { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Fee { get; set; }
        public decimal? PayAmount { get; set; }
        public decimal? TxFee { get; set; }
        public string CapitalUserName { get; set; }
        public string CapitalBankName { get; set; }
        public string CapitalSubbranch { get; set; }
        public string CapitalAccountNumber { get; set; }
        public byte Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string OperatorName { get; set; }
        public string Result { get; set; }
    }
}
