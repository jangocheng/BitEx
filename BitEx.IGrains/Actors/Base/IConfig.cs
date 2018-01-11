using System.Threading.Tasks;
using System.Collections.Generic;
using Orleans;
using BitEx.IGrain.Entity;
using Coin.Core;

namespace BitEx.IGrain.Actors
{
    public interface IConfig : IGrainWithStringKey
    {
        Task<string> Get(string key);
        Task Set(ConfigInfo config);
        Task<List<ConfigInfo>> GetList();
    }
}
