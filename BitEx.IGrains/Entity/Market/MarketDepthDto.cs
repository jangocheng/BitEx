using ProtoBuf;
using System.Collections.Generic;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MarketDepthDto
    {
        [ProtoMember(1)]
        public string MarketId { get; set; }
        [ProtoMember(2)]
        public int Precision { get; set; }
        [ProtoMember(3)]
        public List<MarketDepth> AskList { get; set; }
        [ProtoMember(4)]
        public List<MarketDepth> BidList { get; set; }
    }
}
