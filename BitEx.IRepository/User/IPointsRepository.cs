using System.Collections.Generic;
using System.Threading.Tasks;
using BitEx.Model.User;

namespace BitEx.IRepository.User
{
    public interface IPointsRepository
    {
        Task<bool> CreateAsync(MyPoints point);
        Task<List<MyPoints>> GetTopListAsync(string userid);
    }
}
