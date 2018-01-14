using System;
using System.Threading.Tasks;

namespace BitEx.Framework.Cache
{
    public class CacheCollection
    {
        string collection;
        public CacheCollection(string collection)
        {
            this.collection = collection;
        }
        public async Task<T> Get<T>(string key, Func<Task<T>> generator) where T : class
        {
            var data = await RedisClient.Instance.GetAsync<T>(collection + "_" + key);
            if (data == null)
            {
                data = await generator();
                await RedisClient.Instance.SetAsync(collection + "_" + key, data);
            }
            return data;
        }
        public Task Clear(string key = null)
        {
            if (!string.IsNullOrEmpty(key))
                return RedisClient.Instance.DeleteAsync(collection + "_" + key);
            else
                return RedisClient.Instance.BatchDeleteAsync(collection + "_*");
        }
        public Task BatchClear(string expression = null)
        {
            if (!string.IsNullOrEmpty(expression))
                return RedisClient.Instance.BatchDeleteAsync(collection + "_" + expression + "_*");
            else
                return RedisClient.Instance.BatchDeleteAsync(collection + "_*");
        }
    }
}
