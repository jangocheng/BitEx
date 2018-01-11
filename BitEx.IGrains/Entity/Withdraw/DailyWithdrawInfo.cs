namespace BitEx.IGrain.Entity
{
    public class DailyWithdrawInfo
    {
        /// <summary>
        /// 为安全管控增加的日提现总金额
        /// </summary>
        public decimal DailyWithdrawAmount { get; set; }
        /// <summary>
        /// 提现日期
        /// </summary>
        public int DailyTimestamp { get; set; }
    }
}
