using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using Coin.Core.Lib;
using System.Linq;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class RemoveOpenKeyEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(RemoveOpenKeyEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string OpenId { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public RemoveOpenKeyEvent(string openId)
        {
            this.OpenId = openId;
        }
        public RemoveOpenKeyEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                var index = modelState.OpenList.FindIndex(o => o.Id == this.OpenId);
                if (index != -1)
                {
                    modelState.OpenList.RemoveAt(index);
                }
            }
        }
    }
}
