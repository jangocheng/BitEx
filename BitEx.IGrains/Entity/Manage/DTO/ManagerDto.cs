using Orleans.Concurrency;
using System;

namespace BitEx.IGrain.Entity.Manage.Manager.DTO
{
    [Immutable]
    public class ManagerDto
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string NickName { get; set; }
        public string Roles { get; set; }
        public string RoleNames { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime LockedTime { get; set; }
        public int LockedBy { get; set; }
        public decimal DepositAmount { get; set; }
        public bool IsLocked { get; set; }
    }
}
