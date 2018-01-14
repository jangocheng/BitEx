using ProtoBuf;

namespace BitEx.IGrain.Entity.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class UserConfig
    {
        public UserConfigEnum Key { get; set; }
        public string Value { get; set; }
    }
    public enum UserConfigEnum : byte
    {
        /// <summary>
        /// 登陆邮件通知
        /// </summary>
        LoginEmailNotice = 0,
        /// <summary>
        /// 充值到账邮件通知
        /// </summary>
        TopUpNotice = 1,
        /// <summary>
        /// 提现处理邮件通知
        /// </summary>
        WithrawalNotice = 2,
        /// <summary>
        /// 是否启用二次登陆验证
        /// </summary>
        NeedSecondaryAuth = 3,
        /// <summary>
        /// 市场订单价格超过市场价太多提醒
        /// </summary>
        MarketOrderPriceLimit = 4
    }
}
