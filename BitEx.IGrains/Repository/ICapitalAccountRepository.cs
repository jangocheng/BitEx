using BitEx.Dapper.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coin.Framework.ThirdParty;

namespace BitEx.IGrain.Repository
{
    public interface ICapitalAccountRepository
    {
        Task<CapitalAccountState> GetAsync(string id);
        Task<bool> Create(string id, string userId, string currencyId, string ownerName, BankType accountType, string subbranch, string accountNumber);
        Task<bool> ExistsAsync(BankType accountType, string accountNumber);
        Task<bool> ChangeStatus(string id, AccountStatus stuats);
        Task<bool> SetDefault(string id, bool isDefault);
        Task<bool> ChangeAmountAsync(string id, decimal balance);
        Task<bool> TransferAsync(string id, string currencyId, TransferType txType, string txOwnerName, BankType txAccountType, string txSubbranch, string txAccountNumber, decimal balance, decimal amount, string txNo, string remark,int operatorId, DateTime createdAt);
        Task<IEnumerable<string>> GetIdListAsync(string id, BankType accountType);
        Task<IEnumerable<CapitalAccountItemDto>> GetListAsync();
        Task<Page<CapitalAccountDto>> GetListAsync(string queryText, long pageIndex, long pageSize);
        Task<Page<TransferDto>> GetTransferListAsync(string capitalAccountId, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        Task<DepositInfoDto> GetDepositInfoAsync(PayWay payWay);
    }
}
