using BitEx.Dapper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coin.Model.Notice
{
    [Table("Coin_NoticeTrigger", autoIncrement: true)]
    public class NoticeTrigger
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Props { get; set; }
        public bool IsUsable { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public int UpdateBy { get; set; }
    }
}
