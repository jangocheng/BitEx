using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using Orleans.Concurrency;
using BitEx.IGrain.States.Manage;
using System.Collections.Generic;
using BitEx.IGrain.Entity.Manage.Manager.DTO;

namespace BitEx.IGrain.Events.Manage
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class RoleModuleLimitsSetEvent : IEventBase<int>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public int StateId { get; set; }
        private static string _TypeCode = typeof(RoleModuleLimitsSetEvent).FullName;
        [ProtoIgnore]
        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public List<ModuleLimits> Limits { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public RoleModuleLimitsSetEvent(List<ModuleLimits> limits)
        {
            this.Limits = limits;
        }
        public RoleModuleLimitsSetEvent() { }
        public void Apply(IState<int> state)
        {
            var modelState = state as RoleState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.ModuleLimits.Clear();
                foreach (var limit in this.Limits)
                {
                    if (modelState.ModuleLimits.ContainsKey(limit.ModuleKey))
                    {
                        modelState.ModuleLimits[limit.ModuleKey] = limit.Limits;
                    }
                    else
                    {
                        modelState.ModuleLimits.Add(limit.ModuleKey, limit.Limits);
                    }
                }
            }
        }
    }
}
