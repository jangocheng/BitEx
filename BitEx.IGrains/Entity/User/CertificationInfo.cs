using BitEx.Model.User;
using Orleans.Concurrency;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace BitEx.IGrain.Entity.User
{
    /// <summary>
    /// 身份认证记录
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CertificationInfo
    {
        public CertificationType CerType { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 认证的图片
        /// </summary>
        public List<string> Images { get; set; }
        /// <summary>
        /// 认证的json数据
        /// </summary>
        public string Data { get; set; }
        public string AuditRemark { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditTime { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public int AuditManagerId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public CertificationStatus Status { get; set; }
    }
    /// <summary>
    /// 实名认证状态
    /// </summary>
    public enum CertificationStatus
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None=0,
        /// <summary>
        /// 申请待审核
        /// </summary>
        Apply=1,
        /// <summary>
        /// 审核通过
        /// </summary>
        AuditSucess=2,
        /// <summary>
        /// 审核失败
        /// </summary>
        AuditFail=3
    }
}
