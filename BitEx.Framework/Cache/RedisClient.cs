using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.IO;
using Newtonsoft.Json;
using BitEx.Framework.Log;

namespace BitEx.Framework.Cache
{
    public class RedisClient
    {
        public static Init( string connection)
        {
            _multiplexer = ConnectionMultiplexer.Connect(connection);
        }
        static RedisClient instance;
        static object singleLock = new object();
        public static RedisClient Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singleLock)
                    {
                        if (instance == null)
                            instance = new RedisClient();
                    }
                }
                return instance;
            }
        }
        static ConnectionMultiplexer _multiplexer;
        /// <summary>
        /// redis连接端
        /// </summary>
        public static ConnectionMultiplexer Multiplexer
        {
            get
            {
                return _multiplexer;
            }
        }
        public IDatabase DataBase
        {
            get
            {
                return Multiplexer.GetDatabase(0);
            }
        }
        public TimeSpan CacheExpireSpan = TimeSpan.FromMinutes(30);

        public Task DeleteAsync(string key)
        {
            return DataBase.KeyDeleteAsync(key);
        }

        public async Task ExpireAsync(string key, TimeSpan expire)
        {
            await DataBase.KeyPersistAsync(key);
            await DataBase.KeyExpireAsync(key, expire);
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            try
            {
                var value = await DataBase.StringGetAsync(key);
                if (!value.IsNullOrEmpty)
                {
                    return ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(value));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                await LogManage.GetLogger(typeof(RedisClient)).Fatal(e);
                return null;
            }
        }
        public async Task BatchDeleteAsync(string expression)
        {
            while (true)
            {
                var keys = Multiplexer.GetServer(DataBase.IdentifyEndpoint()).Keys(0, expression, 100, CommandFlags.None).ToList();
                foreach (var key in keys)
                {
                    await DeleteAsync(key);
                }
                if (keys.Count < 100) break;
            }
        }
        public Task SetAsync<T>(string key, T Entity, TimeSpan? expiry = null)
        {
            MemoryStream ms = new MemoryStream();
            ProtoBuf.Serializer.Serialize<T>(ms, Entity);
            byte[] data = new byte[ms.Position];
            ms.Position = 0;
            ms.Read(data, 0, data.Length);
            ms.Dispose();
            return DataBase.StringSetAsync(key, data, expiry ?? CacheExpireSpan);
        }

        public async Task<string> StringGetAsync(string key)
        {
            var v = await DataBase.StringGetAsync(key);
            if (!v.IsNullOrEmpty)
            {
                return v;
            }
            else
            {
                return null;
            }

        }

        public Task StringSetAsync(string key, string value, TimeSpan? expiry = null)
        {
            return DataBase.StringSetAsync(key, value, expiry ?? CacheExpireSpan);
        }
    }
}
