using BitEx.IGrain.Entity.User;
using BitEx.IGrain.States;
using Coin.Model.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface IUserVipRepository
    {
        Task<IEnumerable<UserVipFee>> GetListAsync();
        Task<UserVipFee> GetAsync(UserVipLevel id);
        Task<bool> AddAsync(UserVipLevel id, string vipName, decimal withdrawRate, decimal depositRate, decimal makerRate, decimal takerRate);
        Task<bool> UpdateAsync(UserVipLevel id, string vipName, decimal withdrawRate, decimal depositRate, decimal makerRate, decimal takerRate);
        Task<bool> DeleteAsync(UserVipLevel id);
    }
}
