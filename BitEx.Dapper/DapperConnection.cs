using System;
using System.Data.Common;
using BitEx.Dapper.SqlAdapter;

namespace BitEx.Dapper
{
    public class DapperConnection : IDisposable
    {
        public DbConnection Base { get; set; }
        public SqlType SqlDbType { get; set; }
        public IProvider SqlProvider { get; set; }

        public static implicit operator DbConnection(DapperConnection connection)
        {
            return connection.Base;
        }
        public void Dispose()
        {
            Base.Dispose();
        }
    }
}
