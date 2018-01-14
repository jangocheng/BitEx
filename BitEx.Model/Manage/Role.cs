using System;
using BitEx.Dapper.Core;

namespace BitEx.Model.Manage
{
    [Table("Coin_Role", autoIncrement: true)]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsUsable { get; set; }
    }
}
