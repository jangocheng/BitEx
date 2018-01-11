using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitEx.Dapper.Core;

namespace Coin.Model.User
{
    [Table("Coin_Points", autoIncrement: true)]
    public class MyPoints
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Points { get; set; }
        public DateTime Createdat { get; set; }
        public string Remark { get; set; }
    }
}
