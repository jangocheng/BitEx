using System;
using System.Linq;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity;
using Coin.Core.Lib;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class MarketPlanOrderChangedEventV1 : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public decimal Price { get; set; }
        public string OrderId { get; set; }
        public string AccountId { get; set; }
        private static string _TypeCode = typeof(MarketPlanOrderChangedEventV1).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MarketPlanOrderChangedEventV1()
        {
        }
        public MarketPlanOrderChangedEventV1(string commandId, decimal price, string orderId, string accountId)
        {
            this.CommandId = commandId;
            this.Price = price;
            this.OrderId = orderId;
            this.AccountId = accountId;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as MarketState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                var Order = modelState.PlanOrderList.SingleOrDefault(o => o.OrderId == OrderId);
                var order = new OrderInfoV1
                {
                    UserId = Order.UserId,
                    CurrencyId = Order.CurrencyId,
                    AccountId = Order.AccountId,
                    OrderId = Order.OrderId,
                    OrderType = Order.OrderType,
                    Price = Price,
                    Volume = Order.OrderType == OrderType.Ask ? Order.TradeAmount : SelfMath.ToFixed(Order.TradeAmount / Price, modelState.Precision.VolumePrecision),
                    Amount = Order.OrderType == OrderType.Bid ? Order.TradeAmount : 0,
                    TxVolume = 0,
                    TxAmount = 0,
                    Status = OrderStatus.Open,
                    CreatedAt = Order.CreatedAt,
                    VipLevel = Order.VipLevel
                };
                //计划订单也需要加入到列表中
                if (order.OrderType == OrderType.Ask)
                {
                    modelState.AskList.Add(order);
                }
                else
                {
                    modelState.BidList.Add(order);
                }
                modelState.PlanOrderList.Remove(Order);
            }
        }
    }
}
