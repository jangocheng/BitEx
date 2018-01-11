using Coin.Core.EventSourcing;
using ProtoBuf;
using System;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CurrencyState : IState<string>
    {
        public CurrencyState()
        {
        }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string Name { get; set; }
        public bool IsVirtualCoin { get; set; }
        public bool IsBasicCoin { get; set; }
        public string AssetId { get; set; }
        public decimal TotalCirculation { get; set; }
        public decimal DepositFixedFee { get; set; } = 0;
        public decimal DepositFeeRate { get; set; } = 0;
        /// <summary>
        /// 虚拟币充值超过此线，将发送邮件通知我们，如果此值为0，则不通知
        /// </summary>
        public decimal DepositNotifyLine { get; set; }
        public decimal WithdrawOnceMin { get; set; }
        public decimal WithdrawOnceLimit { get; set; }
        public decimal WithdrawFixedFee { get; set; }
        public decimal WithdrawFeeRate { get; set; }
        public decimal WithdrawVerifyLine { get; set; }
        /// <summary>
        /// 虚拟币币种监控器的余额，低于此线将发邮件通知我们
        /// </summary>
        public decimal BalanceNotifyLine { get; set; }
        /// <summary>
        /// 提现中转账消耗的币种
        /// </summary>
        public string TxCurrencyId { get; set; }
        /// <summary>
        /// 小数点后最大精度
        /// </summary>
        public int WithdrawPrecision { get; set; }
        public int ShowPrecision { get; set; }
        public decimal HotBalance { get; set; }
        public decimal ColdBalance { get; set; }
        public int Required { get; set; }
        public int Safe { get; set; }
        public bool IsLocked { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ExplorerUrl { get; set; }
        /// <summary>
        /// 抵押到账折扣
        /// </summary>
        public float MgDiscount { get; set; }

        public decimal Difficulty { get; set; }
        /// <summary>
        /// 节点数量
        /// </summary>
        public int PeerCount { get; set; }
        /// <summary>
        /// 区块高度
        /// </summary>
        public long Height { get; set; }
        /// <summary>
        /// 充值积分奖励比例
        /// </summary>
        public float DepositPointsRate { get; set; }
        /// <summary>
        /// 交易积分奖励比例
        /// </summary>
        public float TradePointsRate { get; set; }
    }
}
