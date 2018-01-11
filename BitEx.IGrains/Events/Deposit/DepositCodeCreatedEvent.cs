using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class DepositCodeCreatedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string CurrencyId { get; set; }
        public string FundSourceId { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public decimal Amount { get; set; }
        public DepositCodeStatus Status { get; set; }
        public int OperatorId { get; set; }
        public int CreatedBy { get; set; }
        private static string _TypeCode = typeof(DepositCodeCreatedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public DepositCodeCreatedEvent()
        {
        }
        public DepositCodeCreatedEvent(string commandId, string currencyId, string fundSourceId, decimal amount, string code, string password, int operatorId)
        {
            this.CommandId = commandId;
            this.CurrencyId = currencyId;
            this.FundSourceId = fundSourceId;
            this.Amount = amount;
            this.Code = code;
            this.Password = password;
            this.Status = DepositCodeStatus.Actived;
            this.OperatorId = operatorId;
            this.CreatedBy = operatorId;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as DepositCodeState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.CurrencyId = this.CurrencyId;
                modelState.FundSourceId = this.FundSourceId;
                modelState.Code = this.Code;
                modelState.Amount = this.Amount;
                modelState.Password = this.Password;
                modelState.Status = this.Status;
                modelState.OperatorId = this.OperatorId;
                modelState.CreatedBy = this.CreatedBy;
            }
        }
    }
}
