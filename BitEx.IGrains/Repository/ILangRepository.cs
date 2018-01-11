using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coin.Model.Lang;
using Coin.Core;

namespace BitEx.IGrain.Repository
{
    public interface ILangRepository
    {
        Task<LangPackage> GetById(string id);
        Task<LangPackage> GetByKey(string key);
        Task<string> Add(LangPackage data);
        Task<bool> Delete(string id);
        Task<bool> Update(LangPackage data);
        Task<List<LangPackage>> GetPageList(string group, string key, int page, int pageSize);
    }
}
