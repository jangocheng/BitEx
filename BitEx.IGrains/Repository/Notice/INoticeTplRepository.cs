using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitEx.Model.Notice;
using BitEx.Dapper.Core;

namespace BitEx.IGrain.Repository.Notice
{
    public interface INoticeTplRepository
    {
        Task<int> Add(NoticeTpl data);
        Task<int> Update(NoticeTpl data);
        Task<bool> Delete(int id);
        Task<NoticeTpl> GetById(int id);
        Task<Page<NoticeTpl>> GetPageList(int triggerId,long page, long pageSize);
        Task<NoticeTpl> GetByTrigger(string key,short langType);
    }
}
