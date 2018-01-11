using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class ConfigSetEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(ConfigSetEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public ConfigInfo Config { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public ConfigSetEvent(ConfigInfo configInfo)
        {
            this.Config = configInfo;
        }
        public ConfigSetEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as ConfigState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);

                if (modelState.Settings.ContainsKey(Config.Key))
                {
                    modelState.Settings[Config.Key] = Config;
                }
                else
                {
                    modelState.Settings.Add(Config.Key, Config);
                }
            }
        }
    }
}
