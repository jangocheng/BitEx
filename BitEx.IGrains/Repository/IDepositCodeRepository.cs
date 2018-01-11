using BitEx.Dapper.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using System;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface IDepositCodeRepository
    {
        Task<DepositCodeState> GetAsync(string id);
        Task<bool> ExistAsync(string code);
        Task<bool> CreateAsync(string id, string currencyId, string fundSourceId, decimal amount, string code, string password, int operatorId, DepositCodeStatus status, DateTime createdAt);
        Task<bool> UseAsync(string id, string usedBy, DateTime usedAt, DepositCodeStatus status);
        Task<bool> DestroyAsync(string id, DateTime destroyedAt, DepositCodeStatus status, int operatorId);
        Task<Page<DepositCodeDto>> GetListAsync(DepositCodeStatus status, int? loginUserId, long pageIndex, long pageSize);
        Task<string> GetId(string code);
    }
}
