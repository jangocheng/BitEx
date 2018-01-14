using System;
using BitEx.Dapper.Core;

namespace BitEx.Model.User
{
    [Table("Coin_User", autoIncrement: false)]
    public class UserInfo
    {
        [Key]
        public string Id { get; set; }
        public short LangType { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string TradePassword { get; set; }
        public short TradePasswordType { get; set; }
        public string Salt { get; set; }
        public string OtpSecretKey { get; set; }
        public short VipLevel { get; set; }
        /// <summary>
        /// 推广专员等级
        /// </summary>
        public short PromoteLevel { get; set; }
        public string IdNo { get; set; }
        public short IdType { get; set; }
        public string RealName { get; set; }
        public bool IsPhoneRegistered { get; set; }
        public short VerifyLevel { get; set; }
        public short Status { get; set; }
        /// <summary>
        /// 推广人
        /// </summary>
        public string Promoter { get; set; }
        /// <summary>
        /// 推广分红结束时间
        /// </summary>
        public DateTime PromoterDividendEndTime { get; set; }
        public int Points { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateBy { get; set; }
    }
}
