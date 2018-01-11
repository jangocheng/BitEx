using BitEx.IGrain.States;
using ProtoBuf;
using System;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class OrderInfo
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public OrderType OrderType { get; set; }
        public decimal Price { get; set; }
        /// <summary>
        /// 交易量
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// 已交易交易量
        /// </summary>
        public decimal TxVolume { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 已成交金额
        /// </summary>
        public decimal TxAmount { get; set; }
        /// <summary>
        /// 手续费
        /// </summary>
        public decimal Fee { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
