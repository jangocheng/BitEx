using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MarketOrderCanceledEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public OrderCategory OrderCategory { get; set; }
        public OrderType OrderType { get; set; }
        public string MarketId { get; set; }
        public string CurrencyId { get; set; }
        public string OrderId { get; set; }
        public decimal SurplusAmount { get; set; }
        public OrderStatus Status { get; set; }
        private static string _TypeCode = typeof(MarketOrderCanceledEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MarketOrderCanceledEvent()
        {
        }
        public MarketOrderCanceledEvent(string commandId, string marketId, string currencyId, string userId, OrderCategory orderCategory, OrderType orderType, string orderId, decimal surplusAmount)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.OrderCategory = orderCategory;
            this.OrderType = orderType;
            this.OrderId = orderId;
            this.MarketId = marketId;
            this.CurrencyId = currencyId;
            this.SurplusAmount = surplusAmount;
            this.Status = OrderStatus.Canceled;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as MarketState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                if (OrderCategory == OrderCategory.PlanOrder)
                {
                    var planOrder = modelState.PlanOrderList.Find(o => o.OrderId == OrderId);
                    if (planOrder != null)
                    {
                        modelState.PlanOrderList.Remove(planOrder);
                    }
                }
                else
                {
                    if (this.OrderType == OrderType.Ask)
                    {
                        var order = modelState.AskList.Find(f => f.OrderId == this.OrderId);
                        if (order != null)
                        {
                            modelState.AskList.Remove(order);
                        }
                    }
                    else
                    {
                        var order = modelState.BidList.Find(f => f.OrderId == this.OrderId);
                        if (order != null)
                        {
                            modelState.BidList.Remove(order);
                        }
                    }
                }
            }
        }
    }
}
