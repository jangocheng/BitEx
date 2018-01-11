using ProtoBuf;

namespace BitEx.IGrain.Entity.Market
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MarketPrecision
    {
        /// <summary>
        /// 价格精度
        /// </summary>
        public int PricePrecision { get; set; }
        /// <summary>
        /// 数量精度
        /// </summary>
        public int VolumePrecision { get; set; }
        /// <summary>
        /// 市场深度数量精度
        /// </summary>
        public int DepthVolumePrecision { get; set; }
        /// <summary>
        /// 最小订单金额
        /// </summary>
        public decimal MinOrderAmount { get; set; }
    }
}
