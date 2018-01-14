using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitEx.Dapper.Core;

namespace BitEx.Model.User
{
    [Table("Coin_Certification", autoIncrement: false)]
    public class Certification
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public short Type { get; set; }
        public DateTime CreateTime { get; set; }
        public int ManagerId { get; set; }
        public bool IsAdvanced { get; set; }
        public DateTime AuditTime { get; set; }
        public int Status { get; set; }
    }
}
