using BitEx.Dapper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coin.Model.Account
{
    [Table("Coin_AmountUpdateRecord", autoIncrement: false)]
    public class AmountUpdateRecord
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        /// <summary>
        /// 增加的金额
        /// </summary>
        public decimal AddBalance { get; set; }
        /// <summary>
        /// 增加后的余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 增加的锁定金额
        /// </summary>
        public decimal AddLocked { get; set; }
        /// <summary>
        /// 增加锁定金额之后的金额
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// 增加的抵押金额
        /// </summary>
        public decimal AddMortgaged { get; set; }
        /// <summary>
        /// 增加后的抵押金额
        /// </summary>
        public decimal Mortgaged { get; set; }
        /// <summary>
        /// 操作者ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
