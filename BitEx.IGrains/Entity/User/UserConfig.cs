using ProtoBuf;

namespace BitEx.IGrain.Entity.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class UserConfig
    {
        public UserConfigEnum Key { get; set; }
        public string Value { get; set; }
    }
}
