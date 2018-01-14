using Orleans.Concurrency;
using ProtoBuf;
using System;
using Ray.Core.EventSourcing;

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
    }
}
