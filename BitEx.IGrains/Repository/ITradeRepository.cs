using BitEx.Dapper.Core;
using BitEx.IGrain.States;
using BitEx.Model.Trade.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Repository
{
    public interface ITradeRepository
    {
        Task<Page<SimpleTrade>> GetPageList(string marketId, string orderId, OrderType orderType, int page = 1, int pageSize = 20);
    }
}
