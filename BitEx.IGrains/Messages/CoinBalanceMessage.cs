using Coin.Core;
using Coin.Core.Message;
using ProtoBuf;
using System;

namespace Coin.Messages
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinBasicInfoRequest
    {
        public string CurrencyId { get; set; }
        public string TypeCode { get; set; } = "Coin.Messages.CoinBasicInfoResponse";
        public string Exchange { get; set; } = "Coin.Grain";
        public string RoutingKey { get; set; } = "CoinBalance";
        public CoinBasicInfoRequest()
        {
        }
        public CoinBasicInfoRequest(string currencyId)
        {
            this.CurrencyId = currencyId;
        }
    }
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinBasicInfoResponse : IMessage
    {
        public string CurrencyId { get; set; }
        public decimal Volume { get; set; }
        public decimal Difficulty { get; set; }
        public int PeerCount { get; set; }
        public long Height { get; set; }
        public DateTime DoneAt { get; set; } = DateTime.Now;
        public string TypeCode { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
        public CoinBasicInfoResponse()
        {
        }
        public CoinBasicInfoResponse(string message)
        {
            this.Result = false;
            this.Message = message;
        }
        public CoinBasicInfoResponse(string currencyId, decimal volume)
        {
            this.CurrencyId = currencyId;
            this.Volume = volume;
            this.Result = true;
        }
    }
}
