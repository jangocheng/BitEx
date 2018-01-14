using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Coin.Core;
using Ray.Core.EventSourcing;
using BitEx.Core;
using BitEx.Model.User;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class UserState : IState<string>
    {
        public string StateId { get; set; }
        public uint DoingVersion { get; set; }
        public DateTime VersionTime { get; set; }
        public Lang Lang { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public decimal Points { get; set; }
        /// <summary>
        /// 真名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 推广人
        /// </summary>
        public string Promoter { get; set; }
        /// <summary>
        /// 给推广人分红的截至日期
        /// </summary>
        public DateTime PromoterDividendEndTime { get; set; }
        /// <summary>
        /// 推广专员等级
        /// </summary>
        public PromoteLevel PromoteLevel { get; set; }
        public UserVipLevel VipLevel { get; set; }
        public UserLockType LockType { get; set; }
        public DateTime RegisterTime { get; set; }
        //账号安全部分
        public string Password { get; set; }
        /// <summary>
        /// MD5加密密钥
        /// </summary>
        public string Salt { get; set; }
        public UserStatus Status { get; set; }
        /// <summary>
        /// 登陆是否二次验证
        /// </summary>
        public bool NeedSecondVerify { get; set; }
        public string OtpSecretKey { get; set; }
        public bool IsBindOtp { get; set; }
        public string TradePassword { get; set; }
        public TradePasswordType TradePasswordType { get; set; }
        public DateTime LiveLoginPointTime { get; set; }
        public DateTime ForgotTradePasswordTime { get; set; }
        //账号安全结束
        //实名认证信息
        public CertificationInfo Certification { get; set; }
        public bool IsComplateCertification { get; set; }
        public bool CompleteCertification { get; set; }
        //实名结束
        #region 提现
        public List<BankCardInfo> BankCardList { get; set; } = new List<BankCardInfo>();
        public decimal WithdrawalDayLimit { get; set; }
        public decimal WithdrawalMonthLimit { get; set; }
        public DateTime WithdrawalMonthTime { get; set; }
        public DateTime WithdrawalDayTime { get; set; }
        #endregion
        /// <summary>
        /// 个人用户配置信息
        /// </summary>
        public List<UserConfig> UserConfigList { get; set; } = new List<UserConfig>();
        /// <summary>
        /// 有订单市场列表
        /// </summary>
        public List<string> ActiveMarketList = new List<string>();
        /// <summary>
        /// 已经获取的不可重复的积分key列表
        /// </summary>
        public List<string> UniquePointsKeyList = new List<string>();
    }
}
