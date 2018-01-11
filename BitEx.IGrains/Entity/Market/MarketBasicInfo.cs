using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitEx.IGrain.Entity.Market
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MarketBasicInfo
    {
        public bool BasicIsVirtualCoin { get; set; }
        /// <summary>
        /// 价格精度限制
        /// </summary>
        public int PricePrecision { get; set; }
        /// <summary>
        /// 数量精度限制
        /// </summary>
        public int VolumePrecision { get; set; }
        /// <summary>
        /// 订单最小金额
        /// </summary>
        public decimal OrderMinAmount { get; set; }
        /// <summary>
        /// 市场深度数量精度
        /// </summary>
        public int DepthVolumePrecision { get; set; }
        /// <summary>
        /// 基币展示精度
        /// </summary>
        public int BasicShowPrecision { get; set; }
        /// <summary>
        /// 目标币展示精度
        /// </summary>
        public int TargetShowPrecision { get; set; }
    }
}