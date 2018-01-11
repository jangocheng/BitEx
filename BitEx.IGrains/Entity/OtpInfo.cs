using Orleans.Concurrency;
using ProtoBuf;
using System;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class OtpInfo
    {
        public bool IsBind { get; set; }
        public string SecretKey { get; set; }
        public OtpInfo(bool isBind, string secretKey)
        {
            this.IsBind = isBind;
            this.SecretKey = secretKey;
        }
    }
}
