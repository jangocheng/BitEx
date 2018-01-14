using System;

namespace BitEx.IGrain.Entity.User
{
    [Flags]
    public enum LoginStatus:byte
    {
        None = 0,
        Success = 1,
        PasswordError = 2,
        IsLock = 4,
        ErrorMoreThanMost = 8,
        AccountNotExist = 16
    }
}
