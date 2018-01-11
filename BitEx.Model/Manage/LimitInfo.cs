using System;
using BitEx.Dapper.Core;

namespace Coin.Model.Manage
{
    [Table("Coin_Limit", autoIncrement: true)]
    public class LimitInfo
    {
        [Key]
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public short Code { get; set; }
        public string Name { get; set; }
        public bool IsUsable { get; set; }
    }
}
