using BitEx.Model.User;
using Orleans.Concurrency;
using ProtoBuf;

namespace BitEx.IGrain.Entity.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class UserRealtimeInfo
    {
        /// <summary>
        /// 是否完成实名认证
        /// </summary>
        public bool Certed { get; set; }
        /// <summary>
        /// 是否需要二次身份验证
        /// </summary>
        public bool NeedSVerify { get; set; }
        /// <summary>
        /// 是否绑定安全工具
        /// </summary>
        public bool BindOtp { get; set; }
        /// <summary>
        /// 交易密码类型
        /// </summary>
        public TradePasswordType TradePwdType { get; set; }
        /// <summary>
        /// 是否需要交易密码
        /// </summary>
        public bool NeedTradePwd { get; set; }
    }
}
