using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.User
{
    /// <summary>
    /// 会员等级
    /// </summary>
    public enum UserVipLevel
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
}
