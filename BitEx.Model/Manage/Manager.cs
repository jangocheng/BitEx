using System;
using BitEx.Dapper.Core;
namespace Coin.Model.Manage
{
    [Table("Coin_Manager", autoIncrement: true)]
    public class Manager
    {
        [Key]
        public int Id { get; set; }
        public string Account { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string OtpSecretKey { get; set; }
        public string Roles { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime LockedTime { get; set; }
        public int LockedBy { get; set; }
        public decimal DepositAmount { get; set; }
        public bool IsLocked { get; set; }
    }
}
