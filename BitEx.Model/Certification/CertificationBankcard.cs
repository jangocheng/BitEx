using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitEx.Dapper.Core;

namespace BitEx.Model.Certification
{
    [Table("Coin_CertificationBankcard", autoIncrement: true)]
    public class CertificationBankcard
    {
        [Key]
        public int Id { get; set; }
        public string Idcard { get; set; }
        public string RealName { get; set; }
        public string BankcardNumber { get; set; }
        public string Src { get; set; }
        public bool IsOk { get; set; }
    }
}
