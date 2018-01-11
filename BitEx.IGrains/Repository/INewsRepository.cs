using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coin.Model.Cms;
using BitEx.Dapper.Core;

namespace BitEx.IGrain.Repository
{
    public interface INewsRepository
    {
        Task<NewsInfo> GetById(int id);
        Task<NewsInfo> GetByTag(string tag, short langType);
        Task<int> Add(NewsInfo data);
        Task<bool> Delete(int id);
        Task<int> Update(NewsInfo data);
        Task<Page<NewsInfo>> GetPageList(long page, long pageSize, string tag = null, short newsType = -1, short langType = -1);
    }
}
