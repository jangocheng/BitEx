using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.User
{
    [Flags]
    public enum BindType
    {
        None = 0,
        Email = 1,
        Phone = 2,
        Otp = 4
    }
}
