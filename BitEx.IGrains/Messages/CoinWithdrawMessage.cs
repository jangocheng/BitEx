using Coin.Core.Message;
using ProtoBuf;
using System;

namespace Coin.Messages
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawRequest
    {
        public string CommandId { get; set; }
        public string CurrencyId { get; set; }
        public string WithdrawId { get; set; }
        public string Address { get; set; }
        public decimal Volume { get; set; }
        public string Memo { get; set; }
        public bool ForceRetry { get; set; }
        public DateTime UtcTimestamp { get; set; } = DateTime.Now;
        public string TypeCode { get; set; } = "Coin.Messages.CoinWithdrawResponse";
        public string Exchange { get; set; } = "Coin.Grain";
        public string RoutingKey { get; set; } = "CoinWithdraw";
        public CoinWithdrawRequest()
        {
        }
        public CoinWithdrawRequest(string commandId, string currencyId, string withdrawId, string address, decimal volume, bool isRetry=false, string memo = null)
        {
            this.CommandId = commandId;
            this.CurrencyId = currencyId;
            this.WithdrawId = withdrawId;
            this.Address = address;
            this.Volume = volume;
            this.ForceRetry = isRetry;
            this.Memo = memo;
        }
    }
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawResponse : IMessage
    {
        public string CommandId { get; set; }
        public string CurrencyId { get; set; }
        public string WithdrawId { get; set; }
        public string TxNo { get; set; }
        public decimal TxFee { get; set; }
        public DateTime DoneAt { get; set; } = DateTime.Now;
        public string TypeCode { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
        public CoinWithdrawResponse()
        {
        }
        public CoinWithdrawResponse(string message)
        {
            this.Result = false;
            this.Message = message;
        }
        public CoinWithdrawResponse(string commandId, string currencyId, string withdrawId, string txNo, decimal txFee, DateTime doneAt)
        {
            this.CommandId = commandId;
            this.CurrencyId = currencyId;
            this.WithdrawId = withdrawId;
            this.TxNo = txNo;
            this.TxFee = txFee;
            this.DoneAt = doneAt;
            this.Result = true;
        }
    }
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawConfirmRequest
    {
        public string CommandId { get; set; }
        public string CurrencyId { get; set; }
        public string WithdrawId { get; set; }
        public string Address { get; set; }
        public decimal Volume { get; set; }
        public DateTime UtcTimestamp { get; set; } = DateTime.Now;
        public string TypeCode { get; set; } = "Coin.Messages.CoinWithdrawConfirmResponse";
        public string Exchange { get; set; } = "Coin.Grain";
        public string RoutingKey { get; set; } = "CoinWithdraw";
        public CoinWithdrawConfirmRequest()
        {
        }
        public CoinWithdrawConfirmRequest(string commandId, string currencyId, string withdrawId, string address, decimal volume)
        {
            this.CommandId = commandId;
            this.CurrencyId = currencyId;
            this.WithdrawId = withdrawId;
            this.Address = address;
            this.Volume = volume;
        }
    }
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawConfirmResponse : IMessage
    {
        public string CommandId { get; set; }
        public string CurrencyId { get; set; }
        public string WithdrawId { get; set; }
        public string TxNo { get; set; }
        public decimal TxFee { get; set; }
        public DateTime DoneAt { get; set; } = DateTime.Now;
        public string TypeCode { get; set; }
        public ConfirmResponseResult Result { get; set; }
        public string Message { get; set; }
        public CoinWithdrawConfirmResponse()
        {
        }
        public CoinWithdrawConfirmResponse(ConfirmResponseResult result, string message)
        {
            this.Result = result;
            this.Message = message;
        }
        public CoinWithdrawConfirmResponse(string commandId, string currencyId, string withdrawId, string txNo, decimal txFee, DateTime doneAt)
        {
            this.CommandId = commandId;
            this.CurrencyId = currencyId;
            this.WithdrawId = withdrawId;
            this.TxNo = txNo;
            this.TxFee = txFee;
            this.DoneAt = doneAt;
            this.Result = ConfirmResponseResult.Success;
        }
    }
    public enum ConfirmResponseResult
    {
        Success = 0,
        Failed = 1,
        Unknown = 2
    }
}
