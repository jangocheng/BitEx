using BitEx.Dapper.Core;
using BitEx.Model.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository.Manage
{
    public interface IModuleRepository
    {
        Task<int> Add(ModuleInfo data);
        Task<int> Update(ModuleInfo data);
        Task<bool> Delete(int id);
        Task<ModuleInfo> GetByKey(string key);
        Task<ModuleInfo> GetById(int id);
        Task<IEnumerable<ModuleInfo>> GetListByKeys(string[] keys);
        Task<Page<ModuleInfo>> GetPageList(long page, long pageSize);
    }
}
