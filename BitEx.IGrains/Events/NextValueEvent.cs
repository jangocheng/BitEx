using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class NextValueEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public int MinValue { get; set; }
        public int Identity { get; set; }
        public UInt32 Version { get; set; }
        private static string _TypeCode = typeof(NextValueEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public NextValueEvent()
        {
        }
        public NextValueEvent(string commandId, int minValue, int identity)
        {
            this.CommandId = commandId;
            this.MinValue = minValue;
            this.Identity = identity;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as SequenceState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);

                if (this.Version == 0)
                {
                    modelState.MinValue = this.MinValue;
                }
                modelState.Identity = this.Identity;
            }
        }
    }
}
