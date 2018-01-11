using Coin.Core.EventSourcing;
using ProtoBuf;
using System;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositState : IState<string>
    {
        public CoinDepositState()
        {
        }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
        public string Address { get; set; }
        public decimal Volume { get; set; }
        public decimal TxVolume { get; set; }
        public int Confirmation { get; set; }
        public int TxConfirmation { get; set; }
        public decimal Fee { get; set; }
        public decimal TxFee { get; set; }
        public string TxNo { get; set; }
        public DateTime ConfirmedAt { get; set; }
        public DateTime SafeAt { get; set; }
        public bool IsGuarantee { get; set; }
        public string Result { get; set; }
        public CoinDepositStatus Status { get; set; }
        public int EventTableVersion { get; set; }
        public string Memo { get; set; }
        public CoinDepositSpecialStatus SpecialStatus { get; set; }
    }
}
