using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.Cms
{
    public enum AssetsApplyStatus
    {
        None = 0,
        /// <summary>
        /// 申请
        /// </summary>
        Apply = 1,
        /// <summary>
        /// 审核成功
        /// </summary>
        Success = 2,
        /// <summary>
        /// 审核失败
        /// </summary>
        Fail = 4
    }
}
