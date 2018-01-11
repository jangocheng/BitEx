using System;

namespace BitEx.IGrain.Entity
{
    public class Cache<T>
    {
        public T Data { get; set; }
        public DateTime LastUpdatedAt { get; set; } = DateTime.MinValue;
    }
}
