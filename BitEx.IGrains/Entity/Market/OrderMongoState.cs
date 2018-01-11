using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BitEx.IGrain.Entity.Market
{
    public class OrderMongoState
    {
        [BsonId]
        public string Id { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public byte[] Data { get; set; }
    }
}
