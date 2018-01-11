using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.User
{
    /// <summary>
    /// 交易密码类型
    /// </summary>
    public enum TradePasswordType
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
}
