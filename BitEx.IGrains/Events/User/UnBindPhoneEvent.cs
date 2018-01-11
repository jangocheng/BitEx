using System;
using System.Collections.Generic;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Coin.Core;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class UnBindPhoneEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(UnBindPhoneEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string Remark { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public UnBindPhoneEvent(string remark)
        {
            this.Remark = remark;
        }
        public UnBindPhoneEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.PhoneNumber = null;
                if (!modelState.IsBindOtp)
                    modelState.NeedSecondVerify = true;
                modelState.Points -= 1000;
            }
        }
    }
}
