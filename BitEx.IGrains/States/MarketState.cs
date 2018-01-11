using Coin.Core.EventSourcing;
using BitEx.IGrain.Entity;
using BitEx.IGrain.Entity.Market;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MarketState : IState<string>
    {
        public MarketState()
        {
        }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string BasicId { get; set; }
        public string TargetId { get; set; }
        /// <summary>
        /// 挂单手续费
        /// </summary>
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// 吃单手续费
        /// </summary>
        public decimal TakerFeeRate { get; set; }
        public decimal BasicTurnover { get; set; }
        public decimal TargetTurnover { get; set; }
        /// <summary>
        /// 市场精度限制
        /// </summary>
        public MarketPrecision Precision { get; set; }
        public MarketArea Area { get; set; }
        public MarketStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int EventTableVersion { get; set; }
        //交易相关状态
        public int TradeId { get; set; }
        public decimal Price { get; set; }
        public PriceTrend Trend { get; set; }
        /// <summary>
        /// 基本币种是不是虚拟币
        /// </summary>
        public bool BasicIsVirtualCoin { get; set; }
        /// <summary>
        /// 目标币种是不是虚拟币
        /// </summary>
        public bool TargetIsVirtualCoin { get; set; } = true;
        /// <summary>
        /// 买家列表
        /// </summary>
        [ProtoIgnore]
        public List<OrderInfoV1> BidList = new List<OrderInfoV1>();
        /// <summary>
        /// 卖家列表
        /// </summary>
        [ProtoIgnore]
        public List<OrderInfoV1> AskList = new List<OrderInfoV1>();
        /// <summary>
        /// 计划订单列表
        /// </summary>
        [ProtoIgnore]
        public List<PlanOrderInfoV1> PlanOrderList = new List<PlanOrderInfoV1>();
        /// <summary>
        /// 价格浮动百分比
        /// </summary>
        public decimal PriceLimitPercent { get; set; }
        /// <summary>
        /// 市场开盘价(以每天8点的价格为准)
        /// </summary>
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// 开盘时间
        /// </summary>
        public DateTime OpentTime { get; set; }
    }
}
