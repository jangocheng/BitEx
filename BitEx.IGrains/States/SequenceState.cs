using Coin.Core.EventSourcing;
using ProtoBuf;
using System;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SequenceState : IState<string>
    {
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public int MinValue { get; set; }
        public int Identity { get; set; }
    }
}
