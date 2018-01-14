namespace BitEx.Framework.Errors
{
    public enum UserError
    {
        /// <summary>
        /// 账号已被使用
        /// </summary>
        EmailUsed = 1,
        /// <summary>
        /// 邮件验证码一分钟只能发送一次
        /// </summary>
        EmailCaptchaSendLimit = 2,
        /// <summary>
        /// 邮箱验证码错误
        /// </summary>
        EmailCaptchaError = 3,
        /// <summary>
        /// 邮箱验证码超时
        /// </summary>
        EmailCaptchaTimeout = 4,
        /// <summary>
        /// 实名认证重复提交
        /// </summary>
        CertificationRepeatSubmit = 5,
        /// <summary>
        /// otp密钥过期
        /// </summary>
        OtpSecretTimeout = 6,
        /// <summary>
        /// otp验证码错误
        /// </summary>
        OtpCodeError = 7,
        /// <summary>
        /// 身份号码验证错误
        /// </summary>
        IDNoVerifyError = 8,
        /// <summary>
        /// 交易密码错误
        /// </summary>
        TradePasswordError = 9,
        /// <summary>
        /// 登陆密码错误
        /// </summary>
        LoginPasswordError = 10,
        /// <summary>
        /// 不能正常解锁
        /// </summary>
        CanNotNormalUnlock = 11,
        /// <summary>
        /// 昵称已被使用
        /// </summary>
        NickNameIsUsed = 12,
        /// <summary>
        /// 交易密码超过最大错误次数
        /// </summary>
        TradePwdMoreThanMaxErros = 13,
        /// <summary>
        /// 账号被锁定
        /// </summary>
        IsLocked = 14,
        /// <summary>
        /// 修改交易密码后24H内禁止提现
        /// </summary>
        UpTradePwdWithdrawalLocked24H = 15,
        /// <summary>
        /// 提现超过日限额
        /// </summary>
        WithdrawalOverDailyLimit = 16,
        /// <summary>
        /// 体现超过月限额
        /// </summary>
        WithdrawalOverMonthlyLimit = 17,
        /// <summary>
        /// 银行卡已经存在
        /// </summary>
        BankCardExists = 18,
        /// <summary>
        /// 身份Token错误
        /// </summary>
        JwtTokenError=19
    }
}
