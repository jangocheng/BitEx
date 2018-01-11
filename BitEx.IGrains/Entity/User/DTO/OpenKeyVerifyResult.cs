using Orleans.Concurrency;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.User.DTO
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class OpenKeyVerifyResult
    {
        public string Secretkey { get; set; }
        public int InvokeCount { get; set; }
        public int[] Limits { get; set; }
        public bool IPIslegal { get; set; }
    }
}
