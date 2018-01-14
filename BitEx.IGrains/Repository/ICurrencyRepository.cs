using BitEx.Dapper.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.Entity.Currency;
using BitEx.IGrain.States;
using BitEx.Model.Currency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface ICurrencyRepository
    {
        Task<List<CurrencyNameDto>> GetNameList();
        /// <summary>
        /// 获取币种的费率信息列表
        /// </summary>
        /// <returns></returns>
        Task<List<CurrencyFeeRate>> GetFeeRateList();
        /// <summary>
        /// 获取基币种列表
        /// </summary>
        /// <returns></returns>
        Task<List<CurrencyNameDto>> GetBasicList();
        Task<CurrencyState> GetAsync(string id);
        Task<bool> ExistsAsync(string id);
        Task<bool> CreateAsync(string id, string name, bool isVirtualCoin, bool isBasicCoin, string assetId, decimal totalCirculation, decimal depositFixedFee, decimal depositFeeRate, decimal depositNotifyLine, decimal withdrawOnceMin, decimal withdrawOnceLimit, decimal withdrawFixedFee, decimal withdrawFeeRate, decimal withdrawVerifyLine, decimal balanceNotifyLine, string txCurrencyId, int withdrawPrecision, int showPrecision, int required, int safe, string explorerUrl, float mgDiscount, float depositPointsRate, float tradePointsRate);
        Task<bool> UpdateAsync(string id, string name, bool isVirtualCoin, bool isBasicCoin, string assetId, decimal totalCirculation, decimal depositFixedFee, decimal depositFeeRate, decimal depositNotifyLine, decimal withdrawOnceMin, decimal withdrawOnceLimit, decimal withdrawFixedFee, decimal withdrawFeeRate, decimal withdrawVerifyLine, decimal balanceNotifyLine, string txCurrencyId, int withdrawPrecision, int showPrecision, int required, int safe, string explorerUrl, float mgDiscount, float depositPointsRate, float tradePointsRate);
        Task<bool> ConfigDividend(string id, bool isDividendCoin, decimal totalCirculation);
        Task<bool> Lock(string id, bool isLocked);
        Task<bool> ModifyBalanceNotifyLine(string id, decimal balanceNotifyLine);
        Task<bool> ModifyDepositNotifyLine(string id, decimal depositNotifyLine);
        Task<bool> ModifyHotBalanceAsync(string id, decimal hotBalance);
        Task<bool> ModifyCurrencyInfoAsync(string id, decimal hotBalance, decimal difficulty, int peerCount, long height);
        Task<bool> Transfer(string currencyId, CoinTransferType transferDirection, string targetAddress, string coldAddress, decimal coldBalance, decimal volume, decimal fee, string txNo, string remark, int operatorId);
        Task<IEnumerable<string>> GetListAsync(bool? isVirtualCoin);
        Task<Page<CurrencyDto>> GetListAsync(string queryText, long pageIndex, long pageSize);
        Task<Page<CurrencyBasicInfo>> GetBasicInfoListAsync(string queryText, long pageIndex, long pageSize);
        Task<Page<CoinTransferDto>> GetTransferListAsync(string currencyId, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        Task<Page<CurrencyBalanceInfo>> GetBalanceListAsync(string queryText, long pageIndex, long pageSize);
        Task<List<CurrencyShowPrecisionDto>> GetAllShowPrecisionAsync();
        Task<List<CurrencyServiceFeeDto>> GetServiceFeeAsync();
    }
}
