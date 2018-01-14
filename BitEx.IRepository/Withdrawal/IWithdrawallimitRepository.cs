using System.Threading.Tasks;
using BitEx.Model.User;
using BitEx.Dapper.Core;

namespace BitEx.IRepository.Repository
{

    public interface IWithdrawallimitRepository
    {
        Task<int> Add(Withdrawallimit data);
        Task<int> Update(Withdrawallimit data);
        Task<bool> Delete(int id);
        Task<Withdrawallimit> GetById(int id);
        Task<Page<Withdrawallimit>> GetPageList(long page, long pageSize);
        Task<Withdrawallimit> GetLimit(short level);
    }
}
