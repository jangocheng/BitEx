using Coin.Core.EventSourcing;
using ProtoBuf;
using System;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawState : IState<string>
    {
        public CoinWithdrawState()
        {
        }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        public decimal Volume { get; set; }
        public decimal TxVolume { get; set; }
        public decimal CnyAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal TxFee { get; set; }
        public string TxNo { get; set; }
        public DateTime DoneAt { get; set; }
        public int OperatorId { get; set; }
        public WithdrawValidResult ValidResult { get; set; }
        public string Result { get; set; }
        public CoinWithdrawStatus Status { get; set; }
    }
}