using BitEx.Dapper.Core;
using BitEx.IGrain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface IStatisticsRepository
    {
        Task CreateDailyBookAsync();
        Task CreateLedgerBookAsync();
        Task<Page<DailyBookDto>> GetDailyListAsync(DateTime period, long pageIndex, long pageSize);
        Task<Page<LedgerBookDto>> GetLedgerListAsync(long pageIndex, long pageSize, DateTime? beginDate = null, DateTime? endDate = null, string currencyId = null);
        Task<List<LedgerBookLineDto>> GetLedgerLineListAsync(string currencyId, int limit);
        Task<Page<TurnoverDto>> GetTurnoverListAsync(string marketId, int frequency, DateTime beginDate, DateTime endDate, long pageIndex, long pageSize);
    }
}
