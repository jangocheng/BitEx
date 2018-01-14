using System;
using BitEx.Dapper.Core;

namespace BitEx.Model.User
{
    [Table("Coin_Points", autoIncrement: true)]
    public class MyPoints
    {
        [Key]
        public int Id { get; set; }
        public string UKey { get; set; }
        public string UserId { get; set; }
        public decimal Points { get; set; }
        public DateTime Createdat { get; set; }
        public string Remark { get; set; }
    }
}
