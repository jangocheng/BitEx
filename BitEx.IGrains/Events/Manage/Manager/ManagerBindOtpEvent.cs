using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using BitEx.IGrain.States.Manage;

namespace BitEx.IGrain.Events.Manage
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class ManagerBindOtpEvent:IEventBase<int>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public int StateId { get; set; }
        private static string _TypeCode = typeof(ManagerBindOtpEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string SecretKey { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public ManagerBindOtpEvent(string secretKey)
        {
            this.SecretKey = secretKey;
        }
        public ManagerBindOtpEvent() { }
        public void Apply(IState<int> state)
        {
            var modelState = state as ManagerState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.OtpSecretKey = SecretKey;
                modelState.IsBindOtp = true;
            }
        }
    }
}
