using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitEx.Dapper.Core;

namespace Coin.Model.User
{
    [Table("coin_withdrawallimit", autoIncrement: true)]
    public class Withdrawallimit
    {
        [Key]
        public int Id { get; set; }
        public short VerifyLevel { get; set; }
        public decimal DayAmount { get; set; }
        public decimal MonthAount { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public int UpdateBy { get; set; }
    }
}
