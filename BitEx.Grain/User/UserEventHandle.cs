using BitEx.IGrain.Entity.User;
using BitEx.IGrain.Events.User;
using BitEx.IGrain.States;
using BitEx.Model.User;
using Newtonsoft.Json;
using Ray.Core.EventSourcing;
using System;
using System.Linq;

namespace Ray.Grain.EventHandles
{
    public class UserEventHandle : IEventHandle
    {
        public void Apply(object state, IEvent evt)
        {
            if (state is UserState actorState)
            {
                switch (evt)
                {
                    case AddOrderMarket value: AddOrderMarket_Handle(actorState, value); break;
                    case CertificationUpdateEvent value: CertificationUpdateEvent_handle(actorState, value); break;
                    case CertificationApplyEvent value: CertificationApplyEvent_handle(actorState, value); break;
                    case CertificationAuditEvent value: CertificationAuditEvent_handle(actorState, value); break;
                    case BindOtpEvent value: BindOtpEvent_handle(actorState, value); break;
                    case UnBindOtpEvent value: UnBindOtpEvent_handle(actorState, value); break;
                    case UpdatePasswordEvent value: UpdatePasswordEvent_handle(actorState, value); break;
                    case UpdateTradePasswordEvent value: UpdateTradePasswordEvent_handle(actorState, value); break;
                    case SetTradePasswordEvent value: SetTradePasswordEvent_handle(actorState, value); break;
                    case UpdateTradePasswordTypeEvent value: UpdateTradePasswordTypeEvent_handle(actorState, value); break;
                    case UserLockEvent value: UserLockEvent_handle(actorState, value); break;
                    case UserUnlockEvent value: UserUnlockEvent_handle(actorState, value); break;
                    case SetVipLevelEvent value: SetVipLevelEvent_handle(actorState, value); break;
                    case CoinWithdrawAppliedEvent value: CoinWithdrawAppliedEvent_handle(actorState, value); break;
                    case SetPromoteLevelEvent value: SetPromoteLevelEvent_handle(actorState, value); break;
                    case DeleteBankCardEvent value: DeleteBankCardEvent_handle(actorState, value); break;
                    case AddBankCardEvent value: AddBankCardEvent_handle(actorState, value); break;
                    case SetUserConfigEvent value: SetUserConfigEvent_handle(actorState, value); break;
                    case AddPointsEvent value: AddPointsEvent_handle(actorState, value); break;
                    default: break;
                }
            }
        }
        private void AddPointsEvent_handle(UserState state, AddPointsEvent value)
        {
            state.Points = value.TotalPoints;
            state.LiveLoginPointTime = value.Timestamp;
            state.VipLevel = UserVipLeverService.GetVipLevel(state.VipLevel, state.Points);//增加积分
            if (value.Unique != null && !state.UniquePointsKeyList.Contains(value.Unique))
            {
                state.UniquePointsKeyList.Add(value.Unique);
            }
        }
        private void SetUserConfigEvent_handle(UserState state, SetUserConfigEvent value)
        {
            state.UserConfigList.First(u => u.Key == value.Key).Value = value.Value;
        }
        private void AddBankCardEvent_handle(UserState state, AddBankCardEvent value)
        {
            var bankCard = new BankCardInfo()
            {
                Id = value.Id,
                Bank = value.Bank,
                CardNumber = value.CardNumber,
                NoteInfo = value.NoteInfo
            };
            state.BankCardList.Add(bankCard);
        }
        private void DeleteBankCardEvent_handle(UserState state, DeleteBankCardEvent value)
        {
            var card = state.BankCardList.FirstOrDefault(b => b.Id.Equals(value.CardId));
            if (card != null)
            {
                state.BankCardList.Remove(card);
            }
        }
        private void SetPromoteLevelEvent_handle(UserState state, SetPromoteLevelEvent value)
        {
            state.PromoteLevel = value.Level;
        }
        private void CoinWithdrawAppliedEvent_handle(UserState state, CoinWithdrawAppliedEvent value)
        {
            if ((value.Timestamp - state.WithdrawalDayTime).Days > 0)
            {
                state.WithdrawalDayTime = value.Timestamp;
                state.WithdrawalDayLimit = 0;
            }
            if (DateTime.UtcNow.Year > state.WithdrawalMonthTime.Year || DateTime.UtcNow.Month > state.WithdrawalMonthTime.Month)
            {
                state.WithdrawalMonthTime = value.Timestamp;
                state.WithdrawalMonthLimit = 0;
            }
            state.WithdrawalDayLimit += value.limitExchangeAmount;
            state.WithdrawalMonthLimit += value.limitExchangeAmount;
        }
        private void SetVipLevelEvent_handle(UserState state, SetVipLevelEvent value)
        {
            state.VipLevel = value.VipLevel;
        }
        private void UserUnlockEvent_handle(UserState state, UserUnlockEvent value)
        {
            state.LockType = UserLockType.None;
            state.Status = value.Status;
        }
        private void UserLockEvent_handle(UserState state, UserLockEvent value)
        {
            state.LockType = value.LockType;
            state.Status = value.Status;
        }
        private void UpdateTradePasswordTypeEvent_handle(UserState state, UpdateTradePasswordTypeEvent value)
        {
            state.TradePasswordType = value.PasswordType;
        }
        private void SetTradePasswordEvent_handle(UserState state, SetTradePasswordEvent value)
        {
            if (state.TradePasswordType == TradePasswordType.None && string.IsNullOrEmpty(state.TradePassword))
            {
                state.Points += 1000;
            }
            state.TradePassword = value.Password;
            state.TradePasswordType = value.Type;
        }
        private void UpdateTradePasswordEvent_handle(UserState state, UpdateTradePasswordEvent value)
        {
            state.TradePassword = value.Password;
            if (value.IsForget)
                state.ForgotTradePasswordTime = value.Timestamp;
        }
        private void UpdatePasswordEvent_handle(UserState state, UpdatePasswordEvent value)
        {
            state.Password = value.NewPassword;
        }
        private void UnBindOtpEvent_handle(UserState state, UnBindOtpEvent value)
        {
            state.OtpSecretKey = null;
            state.IsBindOtp = false;
            state.NeedSecondVerify = false;
        }
        private void BindOtpEvent_handle(UserState state, BindOtpEvent value)
        {
            state.OtpSecretKey = value.SecretKey;
            state.IsBindOtp = true;
            state.NeedSecondVerify = true;
        }
        private void CertificationAuditEvent_handle(UserState state, CertificationAuditEvent value)
        {
            var cerInfo = state.Certification;
            cerInfo.AuditTime = value.Timestamp;
            cerInfo.AuditManagerId = value.AdminId;
            cerInfo.AuditRemark = value.Remark;
            cerInfo.Status = value.Status;
            if (value.Status == CertificationStatus.AuditSucess)
            {
                var cerData = JsonConvert.DeserializeObject<CerDataInfo>(cerInfo.Data);
                if (cerData != null)
                {
                    state.RealName = cerData.Name;
                    state.IDNo = cerData.CardNumber;
                }
                state.CompleteCertification = true;
                //认证积分处理
                if (!state.UniquePointsKeyList.Contains("Certification"))
                {
                    state.Points += 2000;
                    state.UniquePointsKeyList.Add("Certification");
                }
            }
        }
        private void CertificationApplyEvent_handle(UserState state, CertificationApplyEvent value)
        {
            state.Certification = new CertificationInfo
            {
                AuditTime = value.Timestamp,
                CerType = value.CerType,
                Images = value.Images,
                CreateTime = value.Timestamp,
                Data = value.Data
            };

            if (!value.NeedAudit)
            {
                var cerData = JsonConvert.DeserializeObject<CertificationData86>(value.Data);
                if (cerData != null)
                {
                    state.RealName = cerData.Name;
                    state.IDNo = cerData.CardNumber;
                }
                state.Certification.Status = CertificationStatus.AuditSucess;
                state.CompleteCertification = true;
                //验证成功后，银行卡添加到银行卡列表
                bool isAddBankList = state.Certification.CerType == CertificationType.Bankcard;
                if (isAddBankList)
                {
                    if ((cerData != null) && !state.BankCardList.Exists(b => b.CardNumber.Equals(cerData.BankcardNumber)))
                    {
                        var bankCard = new BankCardInfo()
                        {
                            Id = value.Id,
                            BankType = (BankType)int.Parse(cerData.BankType),
                            Province = cerData.Province,
                            City = cerData.CityName,
                            CardNumber = cerData.BankcardNumber,
                            Bank = cerData.Bank,
                            BranchBank = cerData.BranchBank
                        };
                        state.BankCardList.Add(bankCard);
                    }
                }
            }
            else
            {
                var cerData = JsonConvert.DeserializeObject<CerDataInfo>(value.Data);
                if (cerData != null)
                {
                    state.RealName = cerData.Name;
                    state.IDNo = cerData.CardNumber;
                }
                state.Certification.Status = CertificationStatus.Apply;
            }
        }
        private void CertificationUpdateEvent_handle(UserState state, CertificationUpdateEvent value)
        {
            var cerInfo = state.Certification;
            cerInfo.AuditTime = value.Timestamp;
            cerInfo.Type = value.CerType;
            cerInfo.Images = value.Images;
            if (!value.NeedAudit)
            {
                var cerData = JsonConvert.DeserializeObject<CertificationData86>(value.Data);
                if (cerData != null)
                {
                    state.RealName = cerData.Name;
                    state.IDNo = cerData.CardNumber;
                }
                cerInfo.Status = CertificationStatus.AuditSucess;
                state.CompleteCertification = true;
                #region
                //验证成功后，银行卡添加到银行卡列表
                bool isAddBankList = cerInfo.CerType == CertificationType.Bankcard;
                if (isAddBankList)
                {
                    if ((cerData != null) && !state.BankCardList.Exists(b => b.CardNumber.Equals(cerData.BankcardNumber)))
                    {
                        var bankCard = new BankCardInfo()
                        {
                            Id = value.Id,
                            BankType = (BankType)int.Parse(cerData.BankType),
                            Province = cerData.Province,
                            City = cerData.CityName,
                            CardNumber = cerData.BankcardNumber,
                            Bank = cerData.Bank,
                            BranchBank = cerData.BranchBank
                        };
                        state.BankCardList.Add(bankCard);
                    }
                }
                #endregion
            }
            else
            {
                var cerData = JsonConvert.DeserializeObject<CerDataInfo>(value.Data);
                if (cerData != null)
                {
                    state.RealName = cerData.Name;
                    state.IDNo = cerData.CardNumber;
                }
                cerInfo.Status = CertificationStatus.Apply;
            }
            cerInfo.AuditManagerId = 0;
            cerInfo.Data = value.Data;
        }
        private void AddOrderMarket_Handle(UserState state, AddOrderMarket evt)
        {
            if (state.ActiveMarketList.IndexOf(evt.MarketId) == -1)
                state.ActiveMarketList.Add(evt.MarketId);
        }
    }
}
