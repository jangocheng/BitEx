using System;
using System.Collections.Generic;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Coin.Core;
using Coin.Framework.ThirdParty;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using Newtonsoft.Json;

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
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                CertificationInfo cerInfo = new CertificationInfo();
                cerInfo.AuditTime = Timestamp;
                cerInfo.Type = CerType;
                cerInfo.Images = Images;
                cerInfo.CreateTime = Timestamp;
                cerInfo.Data = Data;
                if (modelState.CertificationList == null) modelState.CertificationList = new List<CertificationInfo>();
                modelState.CertificationList.Add(cerInfo);

                if (!NeedAudit)
                {
                    var cerData = JsonConvert.DeserializeObject<CertificationData86>(this.Data);
                    if (cerData != null)
                    {
                        modelState.RealName = cerData.Name;
                        modelState.IDNo = cerData.CardNumber;
                    }
                    cerInfo.Status = CertificationStatus.AuditSucess;
                    modelState.VerifyLevel = CertificationTypeService.GetCertificationLevel(modelState.CertificationList);
                    //验证成功后，银行卡添加到银行卡列表
                    bool isAddBankList = cerInfo.Type == CertificationType.Bankcard || cerInfo.Type == CertificationType.AdvancedBankcard;
                    if (isAddBankList)
                    {
                        if ((cerData != null) && !modelState.BankCardList.Exists(b => b.CardNumber.Equals(cerData.BankcardNumber)))
                        {
                            var bankCard = new BankCardInfo()
                            {
                                Id = this.Id,
                                BankType = (BankType)int.Parse(cerData.BankType),
                                Province = cerData.Province,
                                City = cerData.CityName,
                                CardNumber = cerData.BankcardNumber,
                                Bank = cerData.Bank,
                                BranchBank = cerData.BranchBank
                            };
                            modelState.BankCardList.Add(bankCard);
                        }
                    }
                }
                else
                {
                    var cerData = JsonConvert.DeserializeObject<CerDataInfo>(this.Data);
                    if (cerData != null)
                    {
                        modelState.RealName = cerData.Name;
                        modelState.IDNo = cerData.CardNumber;
                    }
                    cerInfo.Status = CertificationStatus.Apply;
                }
            }
        }
    }
}
