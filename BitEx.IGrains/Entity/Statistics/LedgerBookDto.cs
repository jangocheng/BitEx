using System;
using System.Collections.Generic;

namespace BitEx.IGrain.Entity
{
    public class LedgerBookDto
    {
        //币种
        public string CurrencyId { get; set; }
        //统计时间
        public DateTime Period { get; set; }
        //系统总金额
        public decimal SysTotalAmount { get; set; }
        //用户总金额
        public decimal UserTotalAmount { get; set; }
        //账面金额
        public decimal CarryingAmount { get; set; }
        //利润
        public decimal IncomeAmount { get; set; }
        //已提现总金额
        public decimal WithdrawAmount { get; set; }
        //提现中总金额
        public decimal WithdrawingAmount { get; set; }
        //用户可用总金额
        public decimal BalanceAmount { get; set; }
        //用户挂单总金额
        public decimal LockedAmount { get; set; }
        //数据版本
        public UInt32 Version { get; set; }
    }
}
