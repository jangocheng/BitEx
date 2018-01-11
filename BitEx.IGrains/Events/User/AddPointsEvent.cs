using Coin.Core.EventSourcing;
using BitEx.IGrain.Entity.User;
using BitEx.IGrain.States;
using Orleans.Concurrency;
using ProtoBuf;
using System;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class AddPointsEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        private static string _TypeCode = typeof(AddPointsEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string Remark { get; set; }
        public string Unique { get; set; }
        public decimal Points { get; set; }
        public decimal TotalPoints { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public AddPointsEvent(decimal points, decimal total, string remark, string unique = null)
        {
            this.Unique = unique;
            this.Points = points;
            this.TotalPoints = total;
            this.Remark = remark;
        }
        public AddPointsEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Points = TotalPoints;
                modelState.LiveLoginPointTime = Timestamp;
                modelState.VipLevel = UserVipLeverService.GetVipLevel(modelState.VipLevel, modelState.Points);//增加积分
                if (Unique != null && !modelState.UniquePointsKeyList.Contains(Unique))
                {
                    modelState.UniquePointsKeyList.Add(Unique);
                }
            }
        }
    }
}
