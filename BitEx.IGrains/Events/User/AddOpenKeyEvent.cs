using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using Coin.Core.Lib;
using System.Collections.Generic;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class AddOpenKeyEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(AddOpenKeyEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string OpenId { get; set; }
        public string Secretkey { get; set; }
        public int[] Limits { get; set; }
        public string[] IPs { get; set; }
        public string Label { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public AddOpenKeyEvent(string id, string label, string secretkey, int[] limits, string[] ips)
        {
            this.OpenId = id;
            this.Label = label;
            this.Secretkey = secretkey;
            this.Limits = limits;
            this.IPs = ips;
        }
        public AddOpenKeyEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.OpenList.Add(new OpenKeyInfo() { Id = OpenId, Label = Label, Secretkey = Secretkey, Limits = Limits, IPs = IPs, CreateTime = Timestamp });
            }
        }
    }
}
