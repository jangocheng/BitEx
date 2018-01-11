using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coin.Model.Notice;
using BitEx.Dapper.Core;

namespace BitEx.IGrain.Repository.Notice
{
    public interface IEmailRepository
    {
        Task Add(EmailInfo data);
        Task<EmailInfo> GetById(string id);
        Task<List<EmailInfo>> GetPageList(string key = null, short langType = -1, string userId = null, int page = 1, int pageSize = 20);
    }
}
