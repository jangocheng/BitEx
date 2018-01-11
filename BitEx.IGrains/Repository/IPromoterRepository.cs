using BitEx.Dapper.Core;
using Coin.Model.User;
using Coin.Model.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface IPromoterRepository
    {
        /// <summary>
        /// 插入推广收入记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AllotPromoterDividend(PromoterDividend model);
        /// <summary>
        /// 统计推广人的分红信息
        /// </summary>
        /// <param name="promoterId"></param>
        /// <returns></returns>
        Task<IEnumerable<PromoterStatistics>> GetPromoterStatistics(string promoterId);
        /// <summary>
        /// 获取用户推广分红分页数据
        /// </summary>
        /// <param name="promoterId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<Page<PromoterDividend>> GetPageAsync(string promoterId, long page, long pageSize);
        /// <summary>
        /// 标记需要处理的分红
        /// </summary>
        /// <param name="endTime">分红结束时间</param>
        /// <returns></returns>
        Task StartRemark(DateTime endTime);
        /// <summary>
        /// 获取需要分红到账的统计数据分页列表
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IEnumerable<PromoterDividendStatistics>> GetNeedDividendPageList(DateTime endTime, long offset, long limit);
        /// <summary>
        /// 分红完成
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="promoterId"></param>
        /// <param name="currencyid"></param>
        /// <returns></returns>
        Task Complate(DateTime endTime, string promoterId, string currencyid);
    }
}
