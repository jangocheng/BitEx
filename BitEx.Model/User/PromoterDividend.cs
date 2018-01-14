using BitEx.Dapper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.Model.User
{
    [Table("coin_promoterdividend", autoIncrement: false)]
    public class PromoterDividend
    {
        [Key]
        public string Id { get; set; }
        public string PromoterId { get; set; }
        public string CurrencyId { get; set; }
        public string MarketId { get; set; }
        public string TxOrderId { get; set; }
        public string TxUserId { get; set; }
        /// <summary>
        /// 交易手续费
        /// </summary>
        public decimal Tradefee { get; set; }
        /// <summary>
        /// 分成金额
        /// </summary>
        public decimal DividendAmount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 分配时间
        /// </summary>
        public DateTime AllottedTime { get; set; }
        /// <summary>
        /// 是否完成
        /// </summary>
        public PromoterDividendStatus Status { get; set; }
    }
    /// <summary>
    /// 推广分红状态
    /// </summary>
    public enum PromoterDividendStatus : byte
    {
        /// <summary>
        /// 未开始处理
        /// </summary>
        None = 0,
        /// <summary>
        /// 开始处理
        /// </summary>
        Process = 1,
        /// <summary>
        /// 处理成功
        /// </summary>
        Success = 2
    }
}
