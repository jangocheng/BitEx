using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.Model.Notice
{
    public class SmsInfo
    {
        [BsonId]
        public string Id { get; set; }
        public string Key { get; set; }
        public string ServiceProviders { get; set; }
        public int LangType { get; set; }
        public string UserId { get; set; }
        public string Mobile { get; set; }
        public string Body { get; set; }
        public bool IsVoice { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
