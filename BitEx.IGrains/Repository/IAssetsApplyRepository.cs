using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitEx.Model.Cms;
using BitEx.Dapper.Core;
using BitEx.IGrain.Entity.Cms;

namespace BitEx.IGrain.Repository
{
    public interface IAssetsApplyRepository
    {
        Task<AssetsApply> GetById(int id);
        Task<int> Add(AssetsApply data);
        Task<bool> Delete(int id);
        Task<int> Update(AssetsApply data);
        Task Audit(int id, int managerId, AssetsApplyStatus status, string remark);
        Task Praise(int id);
        Task Belittle(int id);
        Task<Page<AssetsApply>> GetPageList(long page, long pageSize, string userId, AssetsApplyStatus status);
    }
}
