using System;
using System.ComponentModel;

namespace BitEx.Model.User
{
    [Flags]
    public enum UserStatus : byte
    {
        NoActive = 1,
        Active = 2,
        Lock = 4
    }
    /// <summary>
    /// 认证类型
    /// </summary>
    public enum CertificationType : byte
    {
        None = 0,
        /// <summary>
        /// 证件认证
        /// </summary>
        [Description("证件认证")]
        IdCard = 1,
        /// <summary>
        /// 银行卡初级认证
        /// </summary>
        [Description("银行卡认证")]
        Bankcard = 3
    }
    [Flags]
    public enum LoginVerifyStatus : byte
    {
        /// <summary>
        /// 登陆超时
        /// </summary>
        Timeout = 1,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 2,
        /// <summary>
        /// 身份异常
        /// </summary>
        Abnormal = 4,
        /// <summary>
        /// 身份信息错误
        /// </summary>
        Error = 8,
        /// <summary>
        /// 未绑定安全工具(谷歌)
        /// </summary>
        NoBindSecurityTool = 16,
        /// <summary>
        /// 没有进行二次身份验证
        /// </summary>
        NoLoginValid = 32,
        /// <summary>
        /// 账号锁定
        /// </summary>
        IsLock = 64
    }
    /// <summary>
    /// 交易密码类型
    /// </summary>
    public enum TradePasswordType : byte
    {
        /// <summary>
        /// 无交易密码
        /// </summary>
        None = 1,
        /// <summary>
        /// 每次登陆只验证一次
        /// </summary>
        PerLogin = 2,
        /// <summary>
        /// 每次交易都需要验证
        /// </summary>
        PerTrade = 3,
        /// <summary>
        /// 不进行验证
        /// </summary>
        NoValidation = 4
    }
    /// <summary>
    /// 锁定类型
    /// </summary>
    public enum UserLockType : byte
    {
        None = 0,
        Admin = 1,
        AutoVerifyUser = 4,
        UserSelf = 8,
        /// <summary>
        /// 双花攻击锁定
        /// </summary>
        DepositTerminated = 16
    }
    /// <summary>
    /// 会员等级
    /// </summary>
    public enum UserVipLevel : byte
    {
        Vip0 = 0,
        Vip1 = 1,
        Vip2 = 2,
        Vip3 = 3,
        Vip4 = 4,
        Vip5 = 5,
        Vip6 = 6,
        /// <summary>
        ///超级VIP(请慎重设置)
        /// </summary>
        VIP7 = 7
    }
    public class UserVipLeverService
    {
        public static UserVipLevel GetVipLevel(UserVipLevel oldLevel, decimal points)
        {
            if (points >= 800000 && oldLevel < UserVipLevel.Vip6)
                return UserVipLevel.Vip6;
            else if (points >= 300000 && oldLevel < UserVipLevel.Vip5)
                return UserVipLevel.Vip5;
            else if (points >= 100000 && oldLevel < UserVipLevel.Vip4)
                return UserVipLevel.Vip4;
            else if (points >= 40000 && oldLevel < UserVipLevel.Vip3)
                return UserVipLevel.Vip3;
            else if (points >= 10000 && oldLevel < UserVipLevel.Vip2)
                return UserVipLevel.Vip2;
            else if (oldLevel < UserVipLevel.Vip1)
                return UserVipLevel.Vip1;
            return oldLevel;
        }
    }

    /// <summary>
    /// 推广专员等级
    /// </summary>
    public enum PromoteLevel : byte
    {
        /// <summary>
        /// 普通专员
        /// </summary>
        Common = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
        Level6 = 6,
        Level7 = 7,
        Level8 = 8,
        Level9 = 9,
        Level10 = 10
    }
    public static class PromoteLevelExtension
    {
        public static DateTime GetPromoterDividendEndTime(this PromoteLevel level)
        {
            return DateTime.UtcNow.AddMonths(((short)level + 1) * 6);
        }
    }
}
