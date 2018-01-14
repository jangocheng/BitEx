using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitEx.Dapper.Core;

namespace BitEx.Model.Certification
{
    [Table("Coin_CertificationPhone", autoIncrement: true)]
    public class CertificationPhone
    {
        [Key]
        public int Id { get; set; }
        public string Idcard { get; set; }
        public string RealName { get; set; }
        public string Phone { get; set; }
        public bool IsOk { get; set; }
    }
}
