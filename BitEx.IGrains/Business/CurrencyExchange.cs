using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coin.Model.Withdrawl;
using BitEx.IGrain.Repository;
using Coin.Core.Ioc;
using Autofac;
using Orleans;
using BitEx.IGrain.Actors;

namespace BitEx.IGrain.Business
{
    public class CurrencyExchange
    {
        static ConcurrentDictionary<string, ExchangeMapper> mapperDict = new ConcurrentDictionary<string, ExchangeMapper>();
        /// <summary>
        /// 不同币种之间进行金额转换
        /// </summary>
        /// <param name="marketActorFac">通过market的ID生成Actor对象的委托方法</param>
        /// <param name="basicCurrencyId">基本币种(需要转换币种)</param>
        /// <param name="targetCurrencyId">目标币种(转换后的目标币种)</param>
        /// <param name="amount">转换的金额</param>
        /// <returns></returns>
        public static async Task<Tuple<bool, decimal>> Exchange(Func<string, IMarket> marketActorFac, string basicCurrencyId, string targetCurrencyId, decimal amount)
        {
            if (amount == 0)
                return new Tuple<bool, decimal>(true, 0);
            if (!mapperDict.TryGetValue(basicCurrencyId + "_" + targetCurrencyId, out var Mapper))
            {
                if (!mapperDict.TryGetValue(targetCurrencyId + "_" + basicCurrencyId, out Mapper))
                {
                    Mapper = await GetMapper(basicCurrencyId, targetCurrencyId);
                    if (Mapper != null)
                    {
                        mapperDict.TryAdd(Mapper.BasicId + "_" + Mapper.TargetId, Mapper);
                    }
                }
            }
            if (Mapper != null)
            {
                if (Mapper.TargetId == targetCurrencyId)
                {
                    var result = await MarketExchange(marketActorFac, Mapper.FirstMarketId, Mapper.BasicId, amount);
                    if (!string.IsNullOrEmpty(Mapper.SecondMarketId))
                    {
                        result = await MarketExchange(marketActorFac, Mapper.SecondMarketId, result.Item1, result.Item2);
                    }
                    return new Tuple<bool, decimal>(true, result.Item2);
                }
                else if (Mapper.BasicId == targetCurrencyId)
                {
                    Tuple<string, decimal> result = new Tuple<string, decimal>(Mapper.TargetId, amount);
                    if (!string.IsNullOrEmpty(Mapper.SecondMarketId))
                    {
                        result = await MarketExchange(marketActorFac, Mapper.FirstMarketId, result.Item1, result.Item2);
                    }
                    result = await MarketExchange(marketActorFac, Mapper.FirstMarketId, result.Item1, result.Item2);
                    return new Tuple<bool, decimal>(true, result.Item2);
                }
            }
            return new Tuple<bool, decimal>(false, 0);
        }
        public static async Task<ExchangeMapper> GetMapper(string basicId, string targetId)
        {
            var repository = IocManage.Container.Resolve<ICurrencyMarketMapRepository>();
            var mapper = await repository.GetMapper(basicId, targetId);
            if (mapper == null)
            {
                mapper = await repository.GetMapper(targetId, basicId);
            }
            if (mapper == null)
            {
                var marketRepository = IocManage.Container.Resolve<IMarketRepository>();
                var id = basicId + "_" + targetId;
                if (await marketRepository.ExistsAsync(id))
                {
                    mapper = new ExchangeMapper()
                    {
                        BasicId = basicId,
                        TargetId = targetId,
                        FirstMarketId = id
                    };
                }
                else
                {
                    id = targetId + "_" + basicId;
                    if (await marketRepository.ExistsAsync(id))
                    {
                        mapper = new ExchangeMapper()
                        {
                            BasicId = targetId,
                            TargetId = basicId,
                            FirstMarketId = id
                        };
                    }
                }
                if (mapper == null)
                {
                    var marketList = await marketRepository.GetAllList();
                    var basicList = marketList.Where(m => m.BasicId == basicId || m.TargetId == basicId);
                    var targetList = marketList.Where(m => m.BasicId == targetId || m.TargetId == targetId);
                    foreach (var firstMarket in basicList)
                    {
                        var secondMarket = targetList.Where(t => t.BasicId == firstMarket.BasicId || t.BasicId == firstMarket.TargetId || t.TargetId == firstMarket.BasicId || t.TargetId == firstMarket.TargetId).FirstOrDefault();
                        if (secondMarket != null)
                        {
                            mapper = new ExchangeMapper()
                            {
                                BasicId = basicId,
                                TargetId = targetId,
                                FirstMarketId = firstMarket.Id,
                                SecondMarketId = secondMarket.Id
                            };
                            break;
                        }
                    }
                }
            }
            return mapper;
        }
        private static async Task<Tuple<string, decimal>> MarketExchange(Func<string, IMarket> marketActorFac, string marketId, string currencyId, decimal amount)
        {
            var market = marketActorFac(marketId);
            currencyId = marketId.Split('_').Single(c => c != currencyId);
            amount = await market.Exchange(currencyId, amount);
            return new Tuple<string, decimal>(currencyId, amount);
        }
    }
}
