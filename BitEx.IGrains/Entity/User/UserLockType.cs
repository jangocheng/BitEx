namespace BitEx.IGrain.Entity.User
{
    /// <summary>
    /// 锁定类型
    /// </summary>
    public enum UserLockType
    {
        None = 0,
        Admin = 1,
        AutoVerifyUser = 4,
        UserSelf = 8,
        /// <summary>
        /// 双花攻击锁定
        /// </summary>
        DepositTerminated=16
    }
}
