using Coin.Core.EventSourcing;
using ProtoBuf;
using System;
using BitEx.IGrain.Entity.Cms;
using System.Collections.Generic;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class AssetsApplyVoteState : IState<string>
    {
        public AssetsApplyVoteState() { }
        public string StateId { get; set; }
        public List<AssetsApplyVote> VoteList { get; set; }
        public UInt32 Version { get; set; }
    }
}
