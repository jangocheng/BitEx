using Coin.Core.EventSourcing;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class AccountState : IState<string>
    {
        public AccountState()
        {
        }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public decimal Locked { get; set; }
        /// <summary>
        /// 抵押到账金额
        /// </summary>
        public decimal Mortgaged { get; set; }
        public decimal LastTotalAmount { get; set; }
        public DateTime LastStatisticsTotalAmountTime { get; set; } = DateTime.Today;
        public AccountStatus Status { get; set; }
        public int EventTableVersion { get; set; }
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
