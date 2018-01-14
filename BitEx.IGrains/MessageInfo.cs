﻿using Orleans.Concurrency;
using ProtoBuf;
using Ray.Core;

namespace BitEx.IGrain
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    [Immutable]
    public class MessageInfo: MessageWrapper
    {
        public string TypeCode { get; set; }
        public byte[] BinaryBytes { get; set; }
    }
}
