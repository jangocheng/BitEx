using System;
using BitEx.Dapper.Core;

namespace BitEx.Model.Cms
{
    [Table("Coin_AssetsApply", autoIncrement: true)]
    public class AssetsApply
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Icon { get; set; }
        public string Intro { get; set; }
        public string AuthPic { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string Contact { get; set; }
        public string ContactPhoneNumber { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public int ManagerId { get; set; }
        public DateTime AuditTime { get; set; }
        public string AuditRemark { get; set; }
        public int Praise { get; set; }
        public int Belittle { get; set; }
    }
}
