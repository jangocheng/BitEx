using System;
using ProtoBuf;
using Orleans.Concurrency;
using Ray.Core.EventSourcing;
using BitEx.Model.User;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class SetPromoteLevelEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(SetPromoteLevelEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public PromoteLevel Level { get; set; }
        public int OperatorId { get; set; }
        public string Remark { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public SetPromoteLevelEvent(PromoteLevel level, int operatorId, string remark)
        {
            this.Level = level;
            this.OperatorId = operatorId;
            this.Remark = remark;
        }
        public SetPromoteLevelEvent() { }
    }
}
