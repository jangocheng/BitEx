using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class OrderCancelBackEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        private static string _TypeCode = typeof(OrderCancelBackEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public string MarketId { get; set; }
        public OrderCategory OrderCategory { get; set; }
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public decimal Locked { get; set; }
        public decimal Balance { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
        public OrderCancelBackEvent()
        {
        }
        public OrderCancelBackEvent(string commandId, string marketId, OrderCategory orderCategory, string orderId, decimal amount, decimal balance, decimal locked)
        {
            this.CommandId = commandId;
            Amount = amount;
            Balance = balance;
            Locked = locked;
            MarketId = marketId;
            OrderId = orderId;
            OrderCategory = orderCategory;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as AccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Locked = Locked;
                modelState.Balance = Balance;
            }
        }
    }
}
