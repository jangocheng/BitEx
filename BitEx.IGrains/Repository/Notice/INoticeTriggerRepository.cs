using System.Threading.Tasks;
using Coin.Model.Notice;
using BitEx.Dapper.Core;

namespace BitEx.IGrain.Repository.Notice
{
    public interface INoticeTriggerRepository
    {
        Task<int> Add(NoticeTrigger data);
        Task<int> Update(NoticeTrigger data);
        Task<bool> Delete(int id);
        Task<NoticeTrigger> GetById(int id);
        Task<Page<NoticeTrigger>> GetPageList(long page, long pageSize);
    }
}
