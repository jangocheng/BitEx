using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BitEx.IGrain.Entity.User
{
    /// <summary>
    /// 认证类型
    /// </summary>
    public enum CertificationType
    {
        None = 0,
        /// <summary>
        /// 证件认证
        /// </summary>
        [Description("证件认证")]
        Card = 1,
        /// <summary>
        /// 证件高级验证
        /// </summary>
        [Description("证件高级验证")]
        AdvancedCard = 2,
        /// <summary>
        /// 银行卡初级认证
        /// </summary>
        [Description("银行卡认证")]
        Bankcard = 3,
        /// <summary>
        /// 银行卡高级认证
        /// </summary>
        [Description("银行卡高级认证")]
        AdvancedBankcard = 4

    }
    public static class CertificationTypeService
    {
        public static int GetCertificationLevel(List<CertificationInfo> cerList)
        {
            int level = 0;
            if (cerList != null)
            {
                if (cerList.Any(p => p.Type == CertificationType.Card && p.Status == CertificationStatus.AuditSucess) ||
                    cerList.Any(p => p.Type == CertificationType.Bankcard && p.Status == CertificationStatus.AuditSucess))
                    level = 1;

                if (cerList.Any(p => p.Type == CertificationType.AdvancedCard && p.Status == CertificationStatus.AuditSucess) ||
                    cerList.Any(p => p.Type == CertificationType.AdvancedBankcard && p.Status == CertificationStatus.AuditSucess))
                    level = 2;
            }
            return level;
        }
    }
}
