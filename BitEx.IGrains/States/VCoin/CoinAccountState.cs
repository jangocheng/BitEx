using Coin.Core.EventSourcing;
using BitEx.IGrain.Entity;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinAccountState : IState<string>
    {
        public CoinAccountState()
        {
        }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public decimal Locked { get; set; }
        public decimal Mortgaged { get; set; }
        public AccountLockReason LockReason { get; set; }
        public string LockParameter { get; set; }
        public CoinAccountStatus Status { get; set; }
        /// <summary>
        /// 前一天账户结余余额
        /// </summary>
        public decimal LastTotalAmount { get; set; }
        public DateTime LastStatisticsTotalAmountTime { get; set; } = DateTime.Today;
        public int EventTableVersion { get; set; }
        public List<WithdrawAddress> WithdrawAddressList { get; set; } = new List<WithdrawAddress>();
        public Dictionary<string, TradeProfit> Profits { get; set; } = new Dictionary<string, TradeProfit>();
        /// <summary>
        /// 已激活市场列表
        /// </summary>
        public List<string> ActivatedMarketList = new List<string>();
        /// <summary>
        /// 是否首次充值
        /// </summary>
        public bool IsFirstDeposit { get; set; } = true;
        /// <summary>
        /// 推广总收入
        /// </summary>
        public decimal DividendTotalAmount { get; set; }
    }
}
