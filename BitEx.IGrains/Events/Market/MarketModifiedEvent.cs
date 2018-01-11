using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class MarketModifiedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public decimal MakerFeeRate { get; set; }
        public decimal TakerFeeRate { get; set; }
        public int PricePrecision { get; set; }
        public int VolumePrecision { get; set; }
        public int DepthVolumePrecision { get; set; }
        public decimal MinOrderAmount { get; set; }
        public decimal PriceLimitPercent { get; set; }
        public MarketArea Area { get; set; }
        public MarketStatus Status { get; set; }
        private static string _TypeCode = typeof(MarketModifiedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MarketModifiedEvent()
        {
        }
        public MarketModifiedEvent(string commandId, decimal makerFeeRate, decimal takerFeeRate, int pricePrecision, int volumePrecision, int depthVolumePrecision, decimal minOrderAmount, decimal priceLimitPercent, MarketArea area, MarketStatus status)
        {
            this.CommandId = commandId;
            this.MakerFeeRate = makerFeeRate;
            this.TakerFeeRate = takerFeeRate;
            this.PricePrecision = pricePrecision;
            this.VolumePrecision = volumePrecision;
            this.DepthVolumePrecision = depthVolumePrecision;
            this.MinOrderAmount = minOrderAmount;
            this.PriceLimitPercent = priceLimitPercent;
            this.Area = area;
            this.Status = status;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as MarketState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.MakerFeeRate = this.MakerFeeRate;
                modelState.TakerFeeRate = this.TakerFeeRate;
                modelState.Precision.VolumePrecision = this.VolumePrecision;
                modelState.Precision.PricePrecision = this.PricePrecision;
                modelState.Precision.MinOrderAmount = this.MinOrderAmount;
                modelState.Precision.DepthVolumePrecision = this.DepthVolumePrecision;
                modelState.PriceLimitPercent = PriceLimitPercent;
                modelState.Area = this.Area;
                modelState.Status = this.Status;
            }
        }
    }
}
