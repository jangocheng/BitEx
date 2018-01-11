using Coin.Core.EventSourcing;
using BitEx.IGrain.Entity;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class ConfigState : IState<string>
    {
        public string StateId
        {
            get;
            set;
        }
        public string Name { get; set; }
        public Dictionary<string, ConfigInfo> Settings { get; set; }
        public bool IsUsable { get; set; }
        public uint Version
        {
            get;
            set;
        }
    }
}
