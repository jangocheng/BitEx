using BitEx.IGrain.Entity.Manage.Manager.DTO;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors.Manage
{
    public interface IRole : IGrainWithIntegerKey
    {
        Task SetModuleLimit(List<ModuleLimits> limits);
        Task<int[]> GetLimitsByModuleKey(string moduleKey);
        Task<Dictionary<string, int[]>> GetModuleLimits();
        Task<List<string>> GetModules();
        Task Update(string name, bool isUsable, int managerId);
    }
}
