using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Events.Account
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinAmounUpdateEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        /// <summary>
        /// 增加的金额
        /// </summary>
        public decimal AddBalance { get; set; }
        /// <summary>
        /// 增加后的余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 增加的锁定金额
        /// </summary>
        public decimal AddLocked { get; set; }
        /// <summary>
        /// 增加锁定金额之后的金额
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// 增加的抵押金额
        /// </summary>
        public decimal AddMortgaged { get; set; }
        /// <summary>
        /// 增加后的抵押金额
        /// </summary>
        public decimal Mortgaged { get; set; }
        /// <summary>
        /// 操作者ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作备注
        /// </summary>
        public string Remark { get; set; }
        private static string _TypeCode = typeof(CoinAmounUpdateEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinAmounUpdateEvent()
        {
        }
        public CoinAmounUpdateEvent(decimal addBalance, decimal balance, decimal addLocked, decimal locked, decimal addMortgaged, decimal mortgaged, string operatorId, string remark)
        {
            this.AddBalance = addBalance;
            this.Balance = balance;
            this.AddLocked = addLocked;
            this.Locked = locked;
            this.AddMortgaged = addMortgaged;
            this.Mortgaged = mortgaged;
            this.OperatorId = operatorId;
            this.Remark = remark;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Balance = this.Balance;
                modelState.Locked = this.Locked;
                modelState.Mortgaged = this.Mortgaged;
            }
        }
    }
}
