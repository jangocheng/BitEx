using ProtoBuf;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawAddress
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Memo { get; set; }
        public bool IsVerified { get; set; }
        public string Tag { get; set; }
        public bool IsValid { get; set; }
    }
}
