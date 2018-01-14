using BitEx.Dapper.Core;
using BitEx.Model.Manage;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository.Manage
{
    public interface IRoleRepository
    {
        Task<Role> GetById(int id);
        Task<int> Add(Role data);
        Task<bool> Delete(int id);
        Task<int> Update(Role data);
        Task<Page<Role>> GetPageList(long page, long pageSize);
    }
}
