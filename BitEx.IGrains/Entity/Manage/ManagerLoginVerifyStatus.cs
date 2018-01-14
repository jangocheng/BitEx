using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.Manage.Manager
{
    public enum ManagerLoginVerifyStatus
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
        /// 账号锁定
        /// </summary>
        IsLock = 16
    }
}
