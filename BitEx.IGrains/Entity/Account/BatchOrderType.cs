using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.Account
{
    public enum BatchOrderType
    {
        /// <summary>
        /// 平均分布批量订单
        /// </summary>
        Avg = 0,
        /// <summary>
        /// 正分布批量订单(价格越高数量越多)
        /// </summary>
        Asc = 1,
        /// <summary>
        /// 倒分布批量订单(价格越高数量越少)
        /// </summary>
        Des = 2
    }
}
