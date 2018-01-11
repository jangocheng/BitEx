using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coin.Model.User.Dto
{
    /// <summary>
    /// 推广人分红统计信息
    /// </summary>
    public class PromoterDividendStatistics
    {
        /// <summary>
        /// 推广人ID
        /// </summary>
        public string PromoterId { get; set; }
        /// <summary>
        /// 币种ID
        /// </summary>
        public string CurrencyId { get; set; }
        /// <summary>
        /// 分红金额
        /// </summary>
        public decimal DividendAmount { get; set; }
    }
}
