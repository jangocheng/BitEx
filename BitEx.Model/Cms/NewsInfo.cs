using System;
using BitEx.Dapper.Core;
using ProtoBuf;

namespace Coin.Model.Cms
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Table("Coin_News", autoIncrement: true)]
    public class NewsInfo
    {
        [Key]
        public int Id { get; set; }
        public short NewsType { get; set; }
        public short LangType { get; set; }
        public string LinkUrl { get; set; }
        public string Tag { get; set; }
        public string Title { get; set; }
        public string KeyWords { get; set; }
        public string Cover { get; set; }
        public string Intro { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime PublishTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public int UpdateBy { get; set; }
        public int SortIndex { get; set; }
    }
}
