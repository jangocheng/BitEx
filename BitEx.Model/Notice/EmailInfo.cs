using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.Model.Notice
{
    public class EmailInfo
    {
        [BsonId]
        public string Id { get; set; }
        public string Key { get; set; }
        public int LangType { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Body { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
