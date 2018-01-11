using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.User
{
    [Flags]
    public enum LoginStatus
    {
        None=0,
        Success = 1,
        PasswordError = 2,
        NeedSecurityValid = 4,
        IsLock = 8,
        ErrorMoreThanMost = 16,
        AccountNotExist = 32
    }
}
