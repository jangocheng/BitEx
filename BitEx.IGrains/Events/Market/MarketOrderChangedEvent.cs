using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity;
using System.Linq;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MarketOrderChangedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public OrderInfo Order { get; set; }
        private static string _TypeCode = typeof(MarketOrderChangedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MarketOrderChangedEvent()
        {
        }
        public MarketOrderChangedEvent(string commandId, OrderInfo order)
        {
            this.CommandId = commandId;
            this.Order = order;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as MarketState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                var newOrder = new OrderInfoV1
                {
                    OrderId = Order.OrderId,
                    UserId = Order.UserId,
                    CurrencyId = Order.CurrencyId,
                    AccountId = Order.AccountId,
                    OrderType = Order.OrderType,
                    Price = Order.Price,
                    Volume = Order.Volume,
                    TxVolume = Order.TxVolume,
                    Amount = Order.Amount,
                    TxAmount = Order.TxAmount,
                    Fee = Order.Fee,
                    Status = Order.Status,
                    CreatedAt = Order.CreatedAt,
                    VipLevel = Entity.User.UserVipLevel.Vip0
                };
                if (this.Order.OrderType == OrderType.Ask)
                {
                    modelState.AskList.Add(newOrder);
                }
                else
                {
                    modelState.BidList.Add(newOrder);
                }
            }
        }
    }
}
