using Coin.Core.EventSourcing;
using BitEx.IGrain.Entity.Cms;
using BitEx.IGrain.States;
using Orleans.Concurrency;
using ProtoBuf;
using System;

namespace BitEx.IGrain.Events.Cms
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class AssetsApplyNoteEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(AssetsApplyNoteEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public int ApplyId { get; set; }
        public VoteItem Item { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public AssetsApplyNoteEvent(int applyId, VoteItem item)
        {
            this.ApplyId = applyId;
            this.Item = item;
        }
        public AssetsApplyNoteEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as AssetsApplyVoteState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.VoteList.Add(new AssetsApplyVote() { ApplyId = this.ApplyId, Item = this.Item });
            }
        }
    }
}
