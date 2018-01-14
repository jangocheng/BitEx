using BitEx.Core;
using ProtoBuf;
using System.Collections.Generic;

namespace BitEx.Framework.Notice
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class NoticeInfo
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string TargetId { get; set; }
        public string EmailAddress { get; set; }
        public Lang LangType { get; set; }
        public Dictionary<string, string> Values { get; set; }
    }
}
