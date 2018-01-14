using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitEx.Model.Notice;
using BitEx.Dapper.Core;

namespace BitEx.IGrain.Repository.Notice
{
    public interface ISmsRepository
    {
        Task Add(SmsInfo data);
        Task<SmsInfo> GetById(string id);
        Task<List<SmsInfo>> GetPageList(string key = null, short langType = -1, string userId = null, int page = 1, int pageSize = 20);
    }
}
