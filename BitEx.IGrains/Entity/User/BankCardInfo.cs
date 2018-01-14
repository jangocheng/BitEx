using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Entity.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class BankCardInfo
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string Bank { get; set; }
        public string CardNumber { get; set; }
        public string NoteInfo { get; set; }
    }
}
