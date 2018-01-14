using System;
using System.Collections.Generic;
using ProtoBuf;
using Orleans.Concurrency;
using BitEx.Model.User;
using Ray.Core.EventSourcing;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CertificationApplyEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(CertificationApplyEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public CertificationType CerType { get; set; }
        public List<string> Images { get; set; }
        public string Data { get; set; }
        public bool NeedAudit { get; set; }
        public bool IsAdvanced { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }

        public CertificationApplyEvent(CertificationType cerType, List<string> images, string data, bool isAdvanced, bool needAudit = true)
        {
            this.CerType = cerType;
            this.Images = images;
            this.Data = data;
            this.IsAdvanced = isAdvanced;
            NeedAudit = needAudit;
        }
        public CertificationApplyEvent() { }
    }
}
