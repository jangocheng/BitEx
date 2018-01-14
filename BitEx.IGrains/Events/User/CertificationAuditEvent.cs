using System;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using Ray.Core.EventSourcing;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CertificationAuditEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(CertificationAuditEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public int AdminId { get; set; }
        public DateTime Timestamp { get; set; }
        public CertificationStatus Status { get; set; }
        public string Remark { get; set; }

        public UInt32 Version { get; set; }
        public CertificationAuditEvent(bool result, int adminId, string remark)
        {
            Status = result ? CertificationStatus.AuditSucess : CertificationStatus.AuditFail;
            AdminId = adminId;
            Remark = remark;
        }
        public CertificationAuditEvent() { }
    }
}
