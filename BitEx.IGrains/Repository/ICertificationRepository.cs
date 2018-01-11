using BitEx.Dapper.Core;
using BitEx.IGrain.Entity.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface ICertificationRepository
    {
        #region 实名认证
        Task<bool> AddCertification(string id, string userId, short type,bool isAdvanced, short status, DateTime createTime);
        Task<bool> UpdateCertification(string userId, short type, short status);
        Task<bool> TakeOverCertification(int id, int adminId);
        Task<bool> AuditCertification(string userId, short type, int adminId, short status, DateTime auditTime);
        #endregion
        Task<Page<CertificationDto>> GetPageList(string userId, int managerId, short type, short status, long page, long pageSize);
    }
}
