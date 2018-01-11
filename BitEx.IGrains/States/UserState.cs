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

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class UserState : IState<string>
    {
        public string StateId { get; set; }
        public LangType LangType { get; set; }
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
        /// 身份证号码
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
        public UserVipLevel VipLevel { get; set; }
        public UserLockType LockType { get; set; }
        public DateTime RegisterTime { get; set; }
        //账号安全部分
        public string Password { get; set; }
        /// <summary>
        /// MD5加密密钥
        /// </summary>
        public string Salt { get; set; }
        public bool IsPhoneRegistered { get; set; }
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
        /// <summary>
        /// 最后登陆Ip地址
        /// </summary>
        public string LastLoginIp { get; set; }
        /// <summary>
        /// 最后登陆区域
        /// </summary>
        public string LastLoginArea { get; set; }
        //账号安全结束
        //实名认证信息
        public List<CertificationInfo> CertificationList { get; set; } = new List<CertificationInfo>();
        public bool IsComplateCertification { get; set; }
        public int VerifyLevel { get; set; }
        /// <summary>
        /// 已领取实名认证积分的列表
        /// </summary>
        public List<CertificationType> ReceivedPointsCerList { get; set; } = new List<CertificationType>();
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
        /// 开放接口列表
        /// </summary>
        public List<OpenKeyInfo> OpenList { get; set; } = new List<OpenKeyInfo>();
        /// <summary>
        /// 已经获取的不可重复的积分key列表
        /// </summary>
        public List<string> UniquePointsKeyList = new List<string>();
        public UInt32 Version
        {
            get;
            set;
        }
    }
}
