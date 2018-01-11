using BitEx.IGrain.States;
using System;
using System.Threading.Tasks;
using Coin.Framework.ThirdParty;

namespace BitEx.IGrain.Repository
{
    public interface IFundSourceRepository
    {
        Task<bool> Create(string id, string capitalAccountId, decimal amount, int operatorId);
        Task<bool> Create(string id, BankType accountType, string accountNumber, string subbranch, string capitalAccountId, decimal amount, string remark, string txNo, int operatorId);
        Task<string> GetCapitalAccountId(string id);
        Task<bool> ChangeCapitalAccount(string id, string capitalAccountId);
        Task<bool> Delete(string id);
        Task<decimal> GetUsedDeopsitLimit(int managerId, DateTime date);
    }
}
