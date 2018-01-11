using System;
using System.Linq;
using System.Collections.Generic;
using Coin.Core.EventSourcing;
using Coin.Framework.ThirdParty;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using Newtonsoft.Json;

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
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                var cerInfo = modelState.CertificationList.First(c => c.Type == CerType);
                cerInfo.AuditTime = Timestamp;
                cerInfo.Type = CerType;
                cerInfo.Images = Images;
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
                    #region
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
                    #endregion
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
                cerInfo.AuditManagerId = 0;
                cerInfo.Data = Data;
            }
        }
    }
}
