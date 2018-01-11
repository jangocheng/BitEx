using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CurrencyModifiedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string Name { get; set; }
        public bool IsVirtualCoin { get; set; }
        public bool IsBasicCoin { get; set; }
        public string AssetId { get; set; }
        public decimal TotalCirculation { get; set; }
        public decimal DepositFixedFee { get; set; } = 0;
        public decimal DepositFeeRate { get; set; } = 0;
        public decimal DepositNotifyLine { get; set; }
        public decimal WithdrawOnceMin { get; set; }
        public decimal WithdrawOnceLimit { get; set; }
        public decimal WithdrawFixedFee { get; set; }
        public decimal WithdrawFeeRate { get; set; }
        public decimal WithdrawVerifyLine { get; set; }
        public decimal BalanceNotifyLine { get; set; }
        public string TxCurrencyId { get; set; }
        public int WithdrawPrecision { get; set; }
        public int ShowPrecision { get; set; }
        public int Required { get; set; }
        public int Safe { get; set; }
        public bool IsLocked { get; set; }
        public string ExplorerUrl { get; set; }
        public float MgDiscount { get; set; }
        /// <summary>
        /// 充值积分奖励比例
        /// </summary>
        public float DepositPointsRate { get; set; }
        /// <summary>
        /// 交易积分奖励比例
        /// </summary>
        public float TradePointsRate { get; set; }
        private static string _TypeCode = typeof(CurrencyModifiedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CurrencyModifiedEvent()
        {
        }
        public CurrencyModifiedEvent(string commandId, string name, bool isVirtualCoin, bool isBasicCoin, string assetId, decimal totalCirculation, decimal depositFixedFee, decimal depositFeeRate, decimal depositNotifyLine, decimal withdrawOnceMin, decimal withdrawOnceLimit, decimal withdrawFixedFee, decimal withdrawFeeRate, decimal withdrawVerifyLine, decimal balanceNotifyLine, string txCurrencyId, int withdrawPrecision, int showPrecision, int required, int safe, string explorerUrl, float mgDiscount, float depositPointsRate, float tradePointsRate)
        {
            this.CommandId = commandId;
            this.Name = name;
            this.IsVirtualCoin = isVirtualCoin;
            this.IsBasicCoin = isBasicCoin;
            this.AssetId = assetId;
            this.TotalCirculation = totalCirculation;
            this.DepositFixedFee = depositFixedFee;
            this.DepositFeeRate = depositFeeRate;
            this.DepositNotifyLine = depositNotifyLine;
            this.WithdrawOnceMin = withdrawOnceMin;
            this.WithdrawOnceLimit = withdrawOnceLimit;
            this.WithdrawFixedFee = withdrawFixedFee;
            this.WithdrawFeeRate = withdrawFeeRate;
            this.WithdrawVerifyLine = withdrawVerifyLine;
            this.BalanceNotifyLine = balanceNotifyLine;
            this.TxCurrencyId = txCurrencyId;
            this.WithdrawPrecision = withdrawPrecision;
            this.ShowPrecision = showPrecision;
            this.Required = required;
            this.Safe = safe;
            this.ExplorerUrl = explorerUrl;
            this.MgDiscount = mgDiscount;
            this.DepositPointsRate = depositPointsRate;
            this.TradePointsRate = tradePointsRate;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CurrencyState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Name = this.Name;
                modelState.IsVirtualCoin = this.IsVirtualCoin;
                modelState.IsBasicCoin = this.IsBasicCoin;
                modelState.AssetId = this.AssetId;
                modelState.TotalCirculation = this.TotalCirculation;
                modelState.DepositFixedFee = this.DepositFixedFee;
                modelState.DepositFeeRate = this.DepositFeeRate;
                modelState.DepositNotifyLine = this.DepositNotifyLine;
                modelState.WithdrawOnceMin = this.WithdrawOnceMin;
                modelState.WithdrawOnceLimit = this.WithdrawOnceLimit;
                modelState.WithdrawFixedFee = this.WithdrawFixedFee;
                modelState.WithdrawFeeRate = this.WithdrawFeeRate;
                modelState.WithdrawVerifyLine = this.WithdrawVerifyLine;
                modelState.BalanceNotifyLine = this.BalanceNotifyLine;
                modelState.TxCurrencyId = this.TxCurrencyId;
                modelState.WithdrawPrecision = this.WithdrawPrecision;
                modelState.ShowPrecision = this.ShowPrecision;
                modelState.Required = this.Required;
                modelState.Safe = this.Safe;
                modelState.ExplorerUrl = this.ExplorerUrl;
                modelState.MgDiscount = this.MgDiscount;
                modelState.DepositPointsRate = this.DepositPointsRate;
                modelState.TradePointsRate = this.TradePointsRate;
            }
        }
    }
}
