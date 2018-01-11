using BitEx.Dapper.Core;
using ProtoBuf;
using System;

namespace Coin.Model.Market
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Table("coin_market", autoIncrement: false)]
    public class MarketInfo
    {
        [Key]
        public string Id { get; set; }
        public string BasicId { get; set; }
        public string TargetId { get; set; }
        /// <summary>
        /// 挂单手续费
        /// </summary>
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// 吃蛋手续费
        /// </summary>
        public decimal TakerFeeRate { get; set; }
        public decimal Minorderamount { get; set; }
        /// <summary>
        /// 小数点后面的精度位数
        /// </summary>
        public int PricePrecision { get; set; }
        /// <summary>
        /// 订单数量小数位精度位数
        /// </summary>
        public int VolumePrecision { get; set; }
        /// <summary>
        /// 市场深度数量精度
        /// </summary>
        public int DepthVolumePrecision { get; set; }
        /// <summary>
        /// 价格浮动百分比
        /// </summary>
        public decimal PriceLimitPercent { get; set; }
        /// <summary>
        /// 区域（eg:A,B,C）
        /// </summary>
        public short Area { get; set; }
        public short Status { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
