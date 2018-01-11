using ProtoBuf;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class PlanOrderTrigger
    {
        [ProtoMember(1)]
        public string MarketId { get; set; }
        [ProtoMember(2)]
        public string Id { get; set; }
        [ProtoMember(3)]
        public double Price { get; set; }
        [ProtoMember(4)]
        public string UserId { get; set; }
    }
}
