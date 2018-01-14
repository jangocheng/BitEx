using BitEx.Dapper.Core;
using System;

namespace BitEx.Model.User
{
    [Table("Coin_Certification", autoIncrement: false)]
    public class UserVipFee
    {
        public int Id { get; set; }
        public string VipName { get; set; }
        public decimal WithdrawRate { get; set; }
        public decimal DepositRate { get; set; }
        public decimal MakerRate { get; set; }
        public decimal TakerRate { get; set; }
        public DateTime Createdat { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
