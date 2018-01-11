using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.Notice
{
    public enum NoticeKey
    {
        /// <summary>
        /// 验证码(邮箱和短信验证码)
        /// </summary>
        CommonCaptcha,
        /// <summary>
        /// 登陆二次验证
        /// </summary>
        LoginValid,
        /// <summary>
        /// 充值超过提醒线，就发邮件通知出来
        /// </summary>
        DepositNotifyLine,
        /// <summary>
        /// 币种监控器余额低于提醒线，就发邮件通知出来
        /// </summary>
        BalanceNotifyLine,
        /// <summary>
        /// 如果确认次数变少，说明发生了双花攻击，锁定账户和用户
        /// </summary>
        ConfirmationTerminatedNotify,
        /// <summary>
        /// 充值回退，用户账户扣款失败
        /// </summary>
        RepealDepositFailure,
        /// <summary>
        /// 虚拟币充值确认到账
        /// </summary>
        CoinDepositConfirm,
        /// <summary>
        /// 人民币充值确认到账
        /// </summary>
        RmbDepositConfirm,
        /// <summary>
        /// 抵押到账已自动赎回
        /// </summary>
        AutoRedeemMortgage,
        /// <summary>
        /// 登陆邮件通知
        /// </summary>
        LoginEmailNotice,
        /// <summary>
        /// 异地登陆邮件通知
        /// </summary>
        DistanceLoginNotice,
        /// <summary>
        /// 提现处理完成通知
        /// </summary>
        WithdrawComplete
    }
}
