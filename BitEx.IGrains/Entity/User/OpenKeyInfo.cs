using Orleans.Concurrency;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace BitEx.IGrain.Entity.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class OpenKeyInfo
    {
        public string Label { get; set; }
        public string Id { get; set; }
        public string Secretkey { get; set; }
        public int[] Limits { get; set; }
        public string[] IPs { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
