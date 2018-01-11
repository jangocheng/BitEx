using BitEx.IGrain.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.Market
{
    public class DiscussInfo
    {
        /// <summary>
        /// 成交数量
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// 成交价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 成交金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 卖家应退数量(小于最小交易量时退款)
        /// </summary>
        public decimal AskReturnAmount { get; set; }
        /// <summary>
        /// 买家应退金额
        /// </summary>
        public decimal BidReturnAmount { get; set; }
        /// <summary>
        /// 卖家订单状态
        /// </summary>
        public OrderStatus AskStatus { get; set; }
        /// <summary>
        /// 买家订单状态
        /// </summary>
        public OrderStatus BidStatus { get; set; }
    }
}
