using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using System.Linq;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class BindOpenKeyIPEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(BindOpenKeyIPEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string OpenId { get; set; }
        public string[] IPs { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public BindOpenKeyIPEvent(string id, string[] ips)
        {
            this.OpenId = id;
            this.IPs = ips;
        }
        public BindOpenKeyIPEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                var open= modelState.OpenList.Single(o => o.Id == OpenId);
                open.IPs = IPs;
            }
        }
    }
}
