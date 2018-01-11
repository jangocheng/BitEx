using BitEx.Dapper.Core;
using Coin.Model.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository.Manage
{
    public interface ILimitRepository
    {
        Task<int> Add(LimitInfo data);
        Task<int> Update(LimitInfo data);
        Task<bool> Delete(int id);
        Task<LimitInfo> GetById(int id);
        Task<Page<LimitInfo>> GetPageList(int moduleId,long page, long pageSize);
    }
}
