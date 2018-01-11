using ProtoBuf;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class KLineAggrDto
    {
        public string MarketId { get; set; }     
        public KPoint Point { get; set; }
    }
}
