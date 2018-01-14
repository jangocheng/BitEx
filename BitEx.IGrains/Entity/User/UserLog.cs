using BitEx.Core;
using MongoDB.Bson.Serialization.Attributes;
using Orleans.Concurrency;
using System;

namespace BitEx.IGrain.Entity.User
{
    [Immutable]
    public class UserLog
    {
        [BsonId]
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserLogType Type { get; set; }
        public Lang Lang { get; set; }
        public UserLogLevel Level { get; set; }
        public string Log { get; set; }
        public DateTime Time { get; set; }
    }
    public enum UserLogLevel
    {
        /// <summary>
        /// 正常
        /// </summary>
        Common=0,
        /// <summary>
        /// 危险
        /// </summary>
        Dangerous=1,
        /// <summary>
        /// 严重
        /// </summary>
        Serious=2
    }
    public enum UserLogType
    {
        /// <summary>
        /// 系统
        /// </summary>
        System=0,
        /// <summary>
        /// 登陆
        /// </summary>
        Login = 1,
        /// <summary>
        /// 账号
        /// </summary>
        Account = 2,
        /// <summary>
        /// 交易
        /// </summary>
        Trade = 3,
        /// <summary>
        /// 充值
        /// </summary>
        TopUp = 4,
        /// <summary>
        /// 提现
        /// </summary>
        Withdrawal = 5,
        /// <summary>
        /// 积分日志
        /// </summary>
        Points=6
    }
}
