using BitEx.IGrain.States;
using ProtoBuf;
using System;

namespace BitEx.IGrain.Entity
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class TradeWsDto
    {
        [ProtoMember(1)]
        public string MarketId { get; set; }
        [ProtoMember(2)]
        public int Id { get; set; }
        [ProtoMember(3)]
        public double Price { get; set; }
        [ProtoMember(4)]
        public double Volume { get; set; }
        [ProtoMember(5)]
        public double Amount { get; set; }
        [ProtoMember(6)]
        public bool IsAsk { get; set; }
        [ProtoMember(7)]
        public string AskUserId { get; set; }
        [ProtoMember(8)]
        public string AskOrderId { get; set; }
        [ProtoMember(9)]
        public decimal AskTxVolume { get; set; }
        [ProtoMember(10)]
        public decimal AskTxAmount { get; set; }
        [ProtoMember(11)]
        public string BidUserId { get; set; }
        [ProtoMember(12)]
        public string BidOrderId { get; set; }
        [ProtoMember(13)]
        public decimal BidTxVolume { get; set; }
        [ProtoMember(14)]
        public decimal BidTxAmount { get; set; }
        [ProtoMember(15)]
        public DateTime CreateTime { get; set; }
    }
}
