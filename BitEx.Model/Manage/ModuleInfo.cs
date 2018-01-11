using System;
using BitEx.Dapper.Core;
using ProtoBuf;

namespace Coin.Model.Manage
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Table("Coin_Module", autoIncrement: true)]
    public class ModuleInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ModuleKey { get; set; }
        public int ParentId { get; set; }
        public string ModuleUrl { get; set; }
        public string Icon { get; set; }
        public int SortIndex { get; set; }
        public bool IsUsable { get; set; }
    }
}
