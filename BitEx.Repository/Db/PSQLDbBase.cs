using BitEx.Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace BitEx.Repository.Db
{
    public class PSQLDbBase
    {
        static DbFactory dbFactory;
        static PSqlConfig Config;
        static PSQLDbBase()
        {
            Config = Extensions.IocProvider.GetService<IOptions<PSqlConfig>>().Value;
            dbFactory = new DbFactory(Dapper.SqlAdapter.SqlType.Npgsql, Config.CoreDbConnection);
        }
        static object getLock = new object();
        static ConcurrentDictionary<string, DbFactory> factoryDict = new ConcurrentDictionary<string, DbFactory>();
        public static DapperConnection GetCoreConnection()
        {
            return dbFactory.GetConnection();
        }
        private static DapperConnection CreateConnection(string conn = null)
        {
            DbFactory factory = null;
            if (!string.IsNullOrEmpty(conn))
            {
                if (!factoryDict.TryGetValue(conn, out factory))
                {
                    lock (getLock)
                    {
                        if (!factoryDict.TryGetValue(conn, out factory))
                        {
                            factory = new DbFactory(Dapper.SqlAdapter.SqlType.Npgsql, conn);
                            factoryDict.TryAdd(conn, factory);
                        }
                    }
                }
            }
            if (factory == null)
                return null;
            return factory.GetConnection();
        }
        public static DapperConnection GetMarketConnection(string marketId)
        {
            var conn = string.Format(Config.marketDbConnection, marketId);
            return CreateConnection(conn);
        }
    }
}