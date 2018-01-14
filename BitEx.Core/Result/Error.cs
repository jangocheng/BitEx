using Orleans.Concurrency;
using ProtoBuf;

namespace BitEx.Core.Result
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public struct Error
    {
        public int Code { get; set; };
        public string Message { get; set; }
    }
}
