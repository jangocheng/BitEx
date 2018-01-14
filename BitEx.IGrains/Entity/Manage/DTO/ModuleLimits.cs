using Orleans.Concurrency;
using ProtoBuf;

namespace BitEx.IGrain.Entity.Manage.Manager.DTO
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class ModuleLimits
    {
        public string ModuleKey { get; set; }
        public int[] Limits { get; set; }
    }
}
