using Orleans.Concurrency;
using ProtoBuf;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class MarketDepth
    {
        [ProtoMember(1)]
        public double Price { get; set; }
        [ProtoMember(2)]
        public double Total { get; set; }
        [ProtoMember(3)]
        public double Amount { get; set; }
    }
}
