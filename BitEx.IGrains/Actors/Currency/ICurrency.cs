using System.Threading.Tasks;
using Orleans;
using BitEx.IGrain.Entity;
using Coin.Core;
using BitEx.IGrain.States;
using Orleans.Concurrency;

namespace BitEx.IGrain.Actors
{
    public interface ICurrency : IGrainWithStringKey
    {
        Task Modify(string name, bool isVirtualCoin, bool isBasicCoin, string assetId, decimal totalCirculation, decimal depositFixedFee, decimal depositFeeRate, decimal depositNotifyLine, decimal withdrawOnceMin, decimal withdrawOnceLimit, decimal withdrawFixedFee, decimal withdrawFeeRate, decimal withdrawVerifyLine, decimal balanceNotifyLine, string txCurrencyId, int withdrawPrecision, int showPrecision, int required, int safe, string explorerUrl, float mgDiscount, float depositPointsRate, float tradePointsRate);
        Task Lock(int operatorId);
        Task Unlock(int operatorId);
        Task ModifyBalanceNotifyLine(decimal amount, int operatorId);
        Task ModifyDepositNotifyLine(decimal amount, int operatorId);
        Task ModifyHotBalanceAsync(decimal balance);
        Task ModifyCurrencyInfoAsync(decimal balance, decimal difficulty, int peerCount, long height);
        Task<Result> TransferToAsync(CoinTransferType txType, string fromAddress, string targetAddress, decimal volume, decimal fee, string txNo, string remark, int operatorId);
        [AlwaysInterleave]
        Task<bool> IsVirtualCoin();
        [AlwaysInterleave]
        Task<bool> IsBasicCoin();
        [AlwaysInterleave]
        Task<bool> Exist();
        [AlwaysInterleave]
        Task<WithdrawInfo> GetWidthdrawInfoAsync();
        [AlwaysInterleave]
        Task<ConfirmatonInfo> GetConfirmationInfoAsync();
        [AlwaysInterleave]
        Task<CoinDepositInfo> GetDepositInfoAsync();
        Task<decimal> GetTotalCirculation();
        Task<decimal> GetBalanceNotifyLineAsync();
        [AlwaysInterleave]
        Task<CurrencyBalanceInfo> GetBalanceInfoAsync();
        /// <summary>
        /// 获取抵押折扣
        /// </summary>
        /// <returns></returns>
        [AlwaysInterleave]
        Task<float> GetMgDiscount();
        /// <summary>
        /// 获取币种精度信息
        /// </summary>
        /// <returns></returns>
        [AlwaysInterleave]
        Task<PrecisionDto> GetPrecision();
        /// <summary>
        /// 获取充值积分比率
        /// </summary>
        /// <returns></returns>
        [AlwaysInterleave]
        Task<float> GetDepositPointsRate();
        /// <summary>
        /// 获取交易积分比例
        /// </summary>
        /// <returns></returns>
        [AlwaysInterleave]
        Task<float> GetTradePointsRate();
    }
}
