using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using System.Collections.Generic;


namespace BitEx.IGrain.States.Manage
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class RoleState : IState<int>
    {
        public RoleState()
        {
            ModuleLimits = new Dictionary<string, int[]>();
        }
        public int StateId
        {
            get;
            set;
        }
        public string Name { get; set; }
        public Dictionary<string, int[]> ModuleLimits { get; set; }
        public bool IsUsable { get; set; }
        public uint Version
        {
            get;
            set;
        }
    }
}
