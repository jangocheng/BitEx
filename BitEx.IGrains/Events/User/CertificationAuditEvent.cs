using System;
using System.Linq;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using Newtonsoft.Json;

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
        public CertificationType CerType { get; set; }
        public CertificationStatus Status { get; set; }
        public string Data { get; set; }
        public string Remark { get; set; }

        public UInt32 Version { get; set; }
        public CertificationAuditEvent(CertificationType type, string data, bool result, int adminId, string remark)
        {
            Data = data;
            CerType = type;
            Status = result ? CertificationStatus.AuditSucess : CertificationStatus.AuditFail;
            AdminId = adminId;
            Remark = remark;
        }
        public CertificationAuditEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                var cerInfo = modelState.CertificationList.First(c => c.Type == CerType);
                cerInfo.AuditTime = Timestamp;
                cerInfo.AuditManagerId = AdminId;
                cerInfo.AuditRemark = Remark;
                cerInfo.Status = Status;
                if (Status == CertificationStatus.AuditSucess)
                {
                    var cerData = JsonConvert.DeserializeObject<CerDataInfo>(this.Data);
                    if (cerData != null)
                    {
                        modelState.RealName = cerData.Name;
                        modelState.IDNo = cerData.CardNumber;
                    }
                    modelState.VerifyLevel = CertificationTypeService.GetCertificationLevel(modelState.CertificationList);
                    //认证积分处理
                    if (!modelState.ReceivedPointsCerList.Exists(c => c == cerInfo.Type))
                    {
                        if ((cerInfo.Type == CertificationType.AdvancedCard || cerInfo.Type == CertificationType.AdvancedBankcard))
                        {
                            modelState.Points += 2000;
                        }
                        else
                        {
                            modelState.Points += 1000;
                        }
                        modelState.ReceivedPointsCerList.Add(cerInfo.Type);
                    }
                }
            }
        }
    }
}
