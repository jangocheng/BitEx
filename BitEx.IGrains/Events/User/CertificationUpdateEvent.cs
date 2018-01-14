using System;
using System.Linq;
using System.Collections.Generic;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using Newtonsoft.Json;
using BitEx.Model.User;
using Ray.Core.EventSourcing;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CertificationUpdateEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(CertificationUpdateEvent).FullName;
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
        public bool IsAdvanced { get; set; }
        public bool NeedAudit { get; set; }
        public DateTime Timestamp { get; set; }
        public UInt32 Version { get; set; }
        public CertificationUpdateEvent(CertificationType cerType, List<string> images, string data, bool isAdvanced, bool needAudit = true)
        {
            this.CerType = cerType;
            this.Images = images;
            this.Data = data;
            this.IsAdvanced = isAdvanced;
            this.NeedAudit = needAudit;
        }
        public CertificationUpdateEvent() { }
    }
}
