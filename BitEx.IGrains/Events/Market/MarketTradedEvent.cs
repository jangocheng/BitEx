using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;
using System.Linq;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class MarketTradedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public int TradeId { get; set; }
        public bool IsAsk { get; set; }
        public string AskUserId { get; set; }
        public string AskCurrencyId { get; set; }
        public string AskOrderId { get; set; }
        public decimal AskTxVolume { get; set; }
        public decimal AskTxAmount { get; set; }
        public decimal AskFee { get; set; }
        public decimal AskReturnAmount { get; set; }
        public OrderStatus AskStatus { get; set; }
        public string BidUserId { get; set; }
        public string BidCurrencyId { get; set; }
        public string BidOrderId { get; set; }
        public decimal BidTxVolume { get; set; }
        public decimal BidTxAmount { get; set; }
        public decimal BidFee { get; set; }
        public decimal BidReturnAmount { get; set; }
        public OrderStatus BidStatus { get; set; }
        public decimal Price { get; set; }
        public PriceTrend Trend { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
        public decimal AskOrderFee { get; set; }
        public decimal BidOrderFee { get; set; }
        private static string _TypeCode = typeof(MarketTradedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MarketTradedEvent()
        {
        }
        public MarketTradedEvent(string commandId, int tradeId, bool isAsk, string askUserId, string askCurrencyId, string askOrderId, string bidUserId, string bidCurrencyId,
            string bidOrderId, decimal askTxVolume, decimal askTxAmount, decimal askFee, OrderStatus askStatus, decimal bidTxVolume,
            decimal bidTxAmount, decimal bidFee, decimal bidReturnAmount, OrderStatus bidStatus, decimal txPrice, PriceTrend trend,
            decimal txVolume, decimal txAmount, decimal askReturnAmount, decimal askOrderFee, decimal bidOrderFee)
        {
            if (askCurrencyId == bidCurrencyId)
                throw new Exception("交易出现两个currency一样的BUG");
            this.CommandId = commandId;
            this.TradeId = tradeId;
            this.IsAsk = isAsk;
            this.AskUserId = askUserId;
            this.AskCurrencyId = askCurrencyId;
            this.AskOrderId = askOrderId;
            this.BidUserId = bidUserId;
            this.BidCurrencyId = bidCurrencyId;
            this.BidOrderId = bidOrderId;
            this.AskTxVolume = askTxVolume;
            this.AskTxAmount = askTxAmount;
            this.AskFee = askFee;
            this.AskStatus = askStatus;
            this.BidTxVolume = bidTxVolume;
            this.BidTxAmount = bidTxAmount;
            this.BidFee = bidFee;
            this.BidReturnAmount = bidReturnAmount;
            this.BidStatus = bidStatus;
            this.Price = txPrice;
            this.Trend = trend;
            this.Volume = txVolume;
            this.Amount = txAmount;
            this.AskReturnAmount = askReturnAmount;
            this.AskOrderFee = askOrderFee;
            this.BidOrderFee = bidOrderFee;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as MarketState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);

                //更新买家信息
                var bidOrder = modelState.BidList.Find(f => f.OrderId == this.BidOrderId);
                if (this.BidStatus != OrderStatus.Done)
                {
                    //买家都买完了，则删除，否则更新交易量和扣款
                    bidOrder.TxVolume = this.BidTxVolume;
                    bidOrder.TxAmount = this.BidTxAmount;
                    bidOrder.Fee = this.BidOrderFee;
                    bidOrder.Status = this.BidStatus;

                }
                else
                {
                    modelState.BidList.Remove(bidOrder);
                }
                //更新卖家信息
                var askOrder = modelState.AskList.Find(f => f.OrderId == this.AskOrderId);
                if (this.AskStatus != OrderStatus.Done)
                {
                    //卖家都卖完了，则删除，否则更新交易量
                    askOrder.TxVolume = this.AskTxVolume;
                    askOrder.TxAmount = this.AskTxAmount;
                    askOrder.Fee = this.AskOrderFee;
                    askOrder.Status = this.AskStatus;
                }
                else
                {
                    modelState.AskList.Remove(askOrder);
                }

                //更新市场价
                modelState.Price = this.Price;
                modelState.TradeId = this.TradeId;
                //更新成交量数据
                modelState.BasicTurnover += Amount;
                modelState.TargetTurnover += Volume;
                //更新开盘信息
                if ((this.Timestamp >= this.Timestamp.Date.AddHours(8) && this.Timestamp.Date > modelState.OpentTime.Date) || modelState.OpenPrice == 0)
                {
                    modelState.OpenPrice = this.Price;
                    modelState.OpentTime = this.Timestamp.Date.AddHours(8);
                }
            }
        }
    }
}
