using System;
using System.Threading.Tasks;

namespace Coin.Core
{
    public class LocalCache<T>
    {
        public DateTime GetTime = DateTime.UtcNow;
        public T Data;
        public T Get(float seconds, Func<T> func)
        {
            if (DateTime.UtcNow.Subtract(GetTime).TotalMilliseconds >= seconds * 1000 || Data == null)
            {
                Data = func();
                GetTime = DateTime.UtcNow;
            }
            return Data;
        }
        public async Task<T> GetAsync(float seconds, Func<Task<T>> func)
        {
            if (DateTime.UtcNow.Subtract(GetTime).TotalMilliseconds >= seconds * 1000 || Data == null)
            {
                Data = await func();
                GetTime = DateTime.UtcNow;
            }
            return Data;
        }
    }
}
