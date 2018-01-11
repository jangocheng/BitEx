using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.User
{
    [Flags]
    public enum UserStatus
    {
        NoActive = 1,
        Active = 2,
        Lock = 4
    }
}
