using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coin.Model.User.Dto
{
    public class PromoterDividendDto
    {
        /// <summary>
        /// 推广人ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 币种ID
        /// </summary>
        public string CurrencyId { get; set; }
        /// <summary>
        /// 分红金额
        /// </summary>
        public decimal DividendAmount { get; set; }
        /// <summary>
        /// 分红日期
        /// </summary>
        public DateTime DividendTime { get; set; }
    }
}
