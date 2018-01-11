using BitEx.Dapper.Core;
using BitEx.IGrain.Entity;
using BitEx.IGrain.States;
using Coin.Model.Market;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface IMarketRepository
    {
        Task<MarketInfo> GetAsync(string id);
        Task<bool> ExistsAsync(string id);
        Task<List<MarketInfo>> GetAllList();
        Task<IEnumerable<string>> GetListAsync(string currencyId, bool? isPrefix);
        Task<Page<MarketInfo>> GetListAsync(string queryText, long pageIndex, long pageSize);
        Task<bool> CreateAsync(string id, string basicId, string targetId, decimal makerFeeRate, decimal takerFeeRate, int pricePrecision, int volumePrecision, int depthVolumePrecision, decimal minOrderAmount, decimal priceLimitPercent, MarketArea area, MarketStatus status);
        Task<bool> ModifyAsync(string id, decimal makerFeeRate, decimal takerFeeRate, int pricePrecision, int volumePrecision, int depthVolumePrecision, decimal minOrderAmount, decimal priceLimitPercent, MarketArea area, MarketStatus status);

        Task<bool> StatusChange(string id, MarketStatus status);
        Task<bool> TradeAsync(string marketId, int tradeId, bool isAsk, string askUserId, string bidUserId, string askOrderId, string bidOrderId, decimal price, decimal volume, decimal amount, decimal askFee, decimal bidFee, PriceTrend trend, DateTime createdAt);
        Task<IEnumerable<T>> GetListAsync<T>(string marketId, string batchSql);
        Task<bool> UpdateKLineAsync(string marketId, string batchSql);
        Task<Page<TradeDto>> GetTradeListAsync(string marketId, DateTime? beginDate, DateTime? endDate, long pageIndex, long pageSize);
        /// <summary>
        /// 通过用户Id获取用户交易列表
        /// </summary>
        /// <param name="marketId">市场编号(eg:cny_btc)</param>
        /// <param name="userId">用户编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页数据量</param>
        /// <returns></returns>
        Task<Page<TradeDto>> GetTradeListByUser(string marketId, string userId, long pageIndex, long pageSize);
    }
}
