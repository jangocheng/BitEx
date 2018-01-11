using Coin.Core;
using BitEx.IGrain.Entity.Manage.Manager.DTO;
using BitEx.IGrain.Entity.Manage.Manager;
using BitEx.IGrain.Events;
using Orleans;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Coin.Model.Manage;
using BitEx.IGrain.Entity;

namespace BitEx.IGrain.Actors.Manage
{
    public interface IManager : IGrainWithIntegerKey
    {
        #region 注册登陆
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <returns>如果登陆成功，item1里含有加密串，请保存到Cookie</returns>
        Task<Tuple<ManagerLoginStatus, string>> Login(string password, string ip, string otpCode);
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="loginKey">登陆的加密串，保存在Cookie里</param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task<ManagerLoginVerifyStatus> LoginVerify(string loginKey, string ip);
        Task Loginout();
        #endregion
        Task<TResult> UpdatePassword(string password, string code);
        Task<TResult> UpdatePassword(string password);
        Task<OtpInfo> GetOtpSecretKey();
        Task<int[]> GetModuleLimits(string moduleKey);
        Task<ManagerDto> GetInfo();
        Task<decimal> AddDepositAmount(decimal amount, int operatorId);
        Task<bool> TopUpAudit(decimal amount);
        Task TopUpAuditCancel(decimal amount);
        Task UpdateLockStatus(bool isLock, int managerId);
        Task WriteLog(ManagerLogType type, string log, DateTime time);
        Task<List<ManagerLog>> GetLogs(int page, int pageSize);
        Task<List<ModuleInfo>> GetModules();
        Task<bool> BindOtp(string code);
        Task UnBindOtp();
        Task SetRoles(string roles, int managerId);
    }
}
