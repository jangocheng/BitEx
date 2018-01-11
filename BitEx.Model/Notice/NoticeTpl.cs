using BitEx.Dapper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coin.Model.Notice
{
    [Table("Coin_NoticeTpl", autoIncrement: true)]
    public class NoticeTpl
    {
        [Key]
        public int Id { get; set; }
        public int TriggerId { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Sms { get; set; }
        public string App { get; set; }
        public short LangType { get; set; }
        public bool IsUsable { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public int UpdateBy { get; set; }
    }
}
