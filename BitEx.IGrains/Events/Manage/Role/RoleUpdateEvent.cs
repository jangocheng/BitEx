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
    public class RoleUpdateEvent : IEventBase<int>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public int StateId { get; set; }
        private static string _TypeCode = typeof(RoleUpdateEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public bool IsUsable { get; set; }
        public int ManagerId { get; set; }
        public string RoleName { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public RoleUpdateEvent(string roleName, bool isUsable, int managerId)
        {
            this.RoleName = roleName;
            this.IsUsable = isUsable;
            this.ManagerId = managerId;
        }
        public RoleUpdateEvent() { }
        public void Apply(IState<int> state)
        {
            var modelState = state as RoleState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.IsUsable = IsUsable;
                modelState.Name = RoleName;
            }
        }
    }
}
