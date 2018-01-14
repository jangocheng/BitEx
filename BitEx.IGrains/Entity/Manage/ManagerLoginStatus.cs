using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.Manage.Manager
{
    public enum ManagerLoginStatus
    {
        None=0,
        Success = 1,
        AccountNotExist = 2,
        PasswordError = 4,
        OtpError = 8,
        IsLock = 16,
        ErrorMoreThanMost = 32
    }
}
