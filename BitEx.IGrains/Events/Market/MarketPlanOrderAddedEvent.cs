using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MarketPlanOrderAddedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public PlanOrderInfo Order { get; set; }
        private static string _TypeCode = typeof(MarketPlanOrderAddedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MarketPlanOrderAddedEvent()
        {
        }
        public MarketPlanOrderAddedEvent(string commandId, PlanOrderInfo order)
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
                var V1Order = new PlanOrderInfoV1()
                {
                    OrderId = Order.OrderId,
                    UserId = Order.UserId,
                    VipLevel = Entity.User.UserVipLevel.Vip0,
                    CurrencyId = Order.CurrencyId,
                    AccountId = Order.AccountId,
                    OrderType = Order.OrderType,
                    HighTriggerPrice = Order.HighTriggerPrice,
                    LowTriggerPrice = Order.LowTriggerPrice,
                    HighPrice = Order.HighPrice,
                    LowPrice = Order.LowPrice,
                    TradeAmount = Order.TradeAmount,
                    Price = Order.Price,
                    CreatedAt = Order.CreatedAt
                };
                modelState.PlanOrderList.Add(V1Order);
            }
        }
    }
}
