using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Coin.Core.EventSourcing;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Coin.Core;

namespace BitEx.IGrain.States
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class UserCollectState : IState<string>
    {
        public string StateId { get; set; }

        public UInt32 Version { get; set; }
        public List<string> CollectList { get; set; }
    }
}
