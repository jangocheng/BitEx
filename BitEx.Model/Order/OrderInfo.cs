using System;
using BitEx.Dapper.Core;

namespace Coin.Model.Order
{
    [Table("Coin_Order", autoIncrement: false)]
    public class OrderInfo
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public string MarketId { get; set; }
        public short OrderType { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal TxVolume { get; set; }
        public decimal Amount { get; set; }
        public decimal TxAmount { get; set; }
        public decimal ReturnAmount { get; set; }
        public short Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 交易手续费
        /// </summary>
        public decimal Fee { get; set; }
    }
}
