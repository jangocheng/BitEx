using BitEx.IGrain.States;
using System;

namespace BitEx.IGrain.Entity
{
    public class DepositDto
    {
        public string Id { get; set; }
        public string UserAccount { get; set; }
        public string UserName { get; set; }
        public string PayWay { get; set; }
        public string Email { get; set; }
        public int Identify { get; set; }
        /// <summary>
        /// 扣掉手续费的金额
        /// </summary>
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public decimal TxFee { get; set; }
        public string CapitalUserName { get; set; }
        public string CapitalBankName { get; set; }
        public string CapitalSubbranch { get; set; }
        public string CapitalAccountNumber { get; set; }
        public string Remark { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
