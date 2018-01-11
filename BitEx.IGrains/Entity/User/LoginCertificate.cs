using ProtoBuf;
using System;

namespace BitEx.IGrain.Entity.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class LoginCertificate
    {
        /// <summary>
        /// 登陆key
        /// </summary>
        public string LoginKey { get; set; } = string.Empty;
        /// <summary>
        /// 是否进行了二次省份验证
        /// </summary>
        public bool IsSecondVerified { get; set; }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime LastOperateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 登陆IP
        /// </summary>
        public string LoginIP { get; set; } = string.Empty;
        /// <summary>
        /// 是否绑定IP
        /// </summary>
        public bool IsBindIP { get; set; }
    }
}
