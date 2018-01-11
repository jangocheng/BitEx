using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.User.DTO
{
    public class CertificationDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RealName { get; set; }
        public short Type { get; set; }
        public DateTime CreateTime { get; set; }
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public DateTime AuditTime { get; set; }
        public int Status { get; set; }
    }
}
