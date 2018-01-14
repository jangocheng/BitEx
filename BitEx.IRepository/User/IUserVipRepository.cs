using BitEx.Model.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IRepository.User
{
    public interface IUserVipRepository
    {
        Task<IEnumerable<UserVipFee>> GetListAsync();
        Task<UserVipFee> GetAsync(int id);
        Task<bool> AddAsync(int id, string vipName, decimal withdrawRate, decimal depositRate, decimal makerRate, decimal takerRate);
        Task<bool> UpdateAsync(int id, string vipName, decimal withdrawRate, decimal depositRate, decimal makerRate, decimal takerRate);
        Task<bool> DeleteAsync(int id);
    }
}
