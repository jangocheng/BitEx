using Coin.Core.Message;
using ProtoBuf;
using System;

namespace Coin.Messages
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositResponse : IMessage
    {
        public string TxNo { get; set; }
        public int TxIndex { get; set; }
        public string CurrencyId { get; set; }
        public string Address { get; set; }
        public decimal Volume { get; set; }
        public string Memo { get; set; }
        public DateTime DoneAt { get; set; } = DateTime.Now;
        public string TypeCode { get; set; } = "Coin.Messages.CoinDepositResponse";
        public CoinDepositResponse()
        {
        }
        public CoinDepositResponse(string txNo,int txIndex, string currencyId, string address, decimal volume, string memo)
        {
            this.TxNo = txNo;
            this.CurrencyId = currencyId;
            this.Address = address;
            this.Volume = volume;
            this.Memo = memo;
            this.TxIndex = txIndex;
        }
    }
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinDepositConfirmationRequest
    {
        public string TxNo { get; set; }
        public string DepositId { get; set; }
        public string CurrencyId { get; set; }
        public string TypeCode { get; set; } = "Coin.Messages.CoinConfirmationResponse";
        public string Exchange { get; set; } = "Coin.Grain";
        public string RoutingKey { get; set; } = "CoinDeposit";
        public CoinDepositConfirmationRequest(string depositId, string txNo, string currencyId)
        {
            this.DepositId = depositId;
            this.TxNo = txNo;
            this.CurrencyId = currencyId;
        }
    }
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinConfirmationResponse : IMessage
    {
        public string TxNo { get; set; }
        public string DepositId { get; set; }
        public string CurrencyId { get; set; }
        public int Confirmation { get; set; }
        public string TypeCode { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
