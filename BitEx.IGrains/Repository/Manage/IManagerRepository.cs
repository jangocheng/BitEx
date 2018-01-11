using BitEx.Dapper.Core;
using BitEx.IGrain.Entity.Manage.Manager;
using BitEx.IGrain.Entity.Manage.Manager.DTO;
using Coin.Model.Manage;
using System;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository.Manage
{
    public interface IManagerRepository
    {
        Task<int> Add(Manager data);
        Task<Manager> GetById(int id);
        Task BindOtp(int id, string otpSecretKey);
        Task UpdatePassword(int id, string password);
        Task Lock(int id, bool isLocked, DateTime lockedTime, int lockedBy);
        Task<int> GetIdByAccount(string account);
        Task UpdateDepositAmount(int id, decimal depositAmount);
        Task<int> SetRoles(int id, string roles);
        Task<Page<ManagerDto>> GetPageList(long page, long pageSize);
        Task UpdateLastLoginInfo(int id, string lastLoginIp, DateTime lastLoginTime);
        Task<LastLoginInfo> GetLastLoginInfo(int id);
    }
}
