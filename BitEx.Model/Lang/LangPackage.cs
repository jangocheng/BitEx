using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;
using System.Collections.Generic;

namespace Coin.Model.Lang
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class LangPackage
    {
        [BsonId]
        public string Id { get; set; }
        public string Key { get; set; }
        public string Group { get; set; }
        public List<LangText> TextList { get; set; }
    }
}
