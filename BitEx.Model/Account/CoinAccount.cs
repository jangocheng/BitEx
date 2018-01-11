using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitEx.Dapper.Core;

namespace Coin.Model.Account
{
    [Table("coin_coinaccount", autoIncrement: false)]
    public class CoinAccount
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string Address { get; set; }
        public decimal? Balance { get; set; }
        public decimal? Locked { get; set; }
        public decimal? Mortgaged { get; set; }
        public decimal? TotalAmount { get; set; }
        public short? Status { get; set; }
        public int? Version { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal? LastTotalAmount { get; set; }
    }
}
