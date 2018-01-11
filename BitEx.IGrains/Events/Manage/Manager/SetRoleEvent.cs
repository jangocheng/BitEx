using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using BitEx.IGrain.States.Manage;
using System.Linq;

namespace BitEx.IGrain.Events.Manage
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class SetRoleEvent : IEventBase<int>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public int StateId { get; set; }
        private static string _TypeCode = typeof(SetRoleEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public int OperatorId { get; set; }
        public string Roles { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public SetRoleEvent(string roles, int operatorId)
        {
            this.Roles = roles;
            this.OperatorId = operatorId;
        }
        public SetRoleEvent() { }
        public void Apply(IState<int> state)
        {
            var modelState = state as ManagerState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Roles.Clear();
                if (!string.IsNullOrEmpty(Roles))
                {
                    var roles = Roles.Split(',');
                    foreach (var item in roles)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            modelState.Roles.Add(int.Parse(item));
                        }
                    }
                }
            }
        }
    }
}
