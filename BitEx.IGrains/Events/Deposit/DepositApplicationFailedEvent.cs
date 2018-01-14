using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using Coin.Core;
using BitEx.IGrain.States;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class DepositApplicationFailedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string AccountId { get; set; }
        public string CurrencyId { get; set; }
        public string CapitalAccountId { get; set; }
        public DepositWay DepositWay { get; set; }
        public decimal Amount { get; set; }
        public Result Result { get; set; }
        private static string _TypeCode = typeof(DepositApplicationFailedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public DepositApplicationFailedEvent()
        {
        }
        public DepositApplicationFailedEvent(string commandId, string userId, string accountId, string currencyId, string capitalAccountId, DepositWay depositWay, decimal amount, Result result)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.AccountId = accountId;
            this.CurrencyId = currencyId;
            this.CapitalAccountId = capitalAccountId;
            this.DepositWay = depositWay;
            this.Amount = amount;
            this.Result = result;
        }
        public void Apply(IState<string> state)
        {
        }
    }
}
