using System;
using MongoDB.Bson.Serialization.Attributes;
using Orleans.Concurrency;

namespace BitEx.IGrain.Entity.Manage.Manager
{
    [Immutable]
    public class ManagerLog
    {
        [BsonId]
        public string Id { get; set; }
        public int ManagerId { get; set; }
        public ManagerLogType Type { get; set; }
        public string Log { get; set; }
        public DateTime Time { get; set; }
    }
    public enum ManagerLogType
    {
        /// <summary>
        /// 系统
        /// </summary>
        Common = 0,
        /// <summary>
        /// 充值授权
        /// </summary>
        Deposit = 1,
        /// <summary>
        /// 登陆
        /// </summary>
        Login = 2,
        /// <summary>
        /// 充值
        /// </summary>
        TopUp = 3,
        /// <summary>
        /// 提现
        /// </summary>
        Withdrawal = 4
    }
}
