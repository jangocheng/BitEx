using System;
using System.Text;

namespace BitEx.Core.Jwt
{
    public class Payload
    {
        /// <summary>
        /// TokenId
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Sub { get; set; }
        /// <summary>
        /// 观众(Web,App,Open api)
        /// </summary>
        public Audience Aud { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Ro { get; set; }
        /// <summary>
        /// 权限信息
        /// </summary>
        public string Lim { get; set; }
        /// <summary>
        /// IP限制
        /// </summary>
        public string IpLim { get; set; }
        /// <summary>
        /// 签发时间
        /// </summary>
        public long Iat { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public long Exp { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        public void AppendJson(StringBuilder builder)
        {
            builder.Append("{'Id':'").Append(Id).Append("','Sub':'").Append(Sub.ToString()).Append("','Aud':'").Append(Aud).Append("','Ro':'").Append(Ro).Append("','Lim':'").Append(Lim).Append("','IpLim':'").Append(IpLim).Append("','Iat':'").Append(Iat).Append("','Exp':'").Append(Exp).Append("','Email':'").Append(Email).Append("'}");
        }
        public string ToBase64Json()
        {
            var builder = new StringBuilder();
            AppendJson(builder);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(builder.ToString()));
        }
    }
}
