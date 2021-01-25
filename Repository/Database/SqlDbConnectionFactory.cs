// Decompiled with JetBrains decompiler
// Type: Repository.Database.SqlDbConnectionFactory
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Unity.Attributes;

namespace Repository.Database
{
    public class SqlDbConnectionFactory : DbConnectionFactoryBase
    {
        private IDbTransaction _transaction;
        private SqlConnection _connection;
        private string _connectionString;

        public override string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
        }

        public SqlDbConnectionFactory(string connectionString)
        {
            this._connectionString = connectionString;
        }

        [InjectionConstructor]
        public SqlDbConnectionFactory()
        {
            //this._connectionString = "data source = " + ConfigurationManager.AppSettings["ServerName"].ToString() + "; Database = " + ConfigurationManager.AppSettings["DatabaseName"].ToString() + "; uid = sa; pwd = sigma@123; multipleactiveresultsets = true;";
            this._connectionString = "data source = " + ConfigurationManager.AppSettings["ServerName"].ToString() + "; Database = " + ConfigurationManager.AppSettings["DatabaseName"].ToString() + "; uid = sa; pwd = Dumal@1991; multipleactiveresultsets = true;";
        }

        public override IDbConnection GetOpenConnection(bool withTransaction)
        {
            if (this._connection == null || string.IsNullOrEmpty(this._connection.ConnectionString))
                this._connection = new SqlConnection(this.ConnectionString);
            if (this._connection.State != ConnectionState.Open)
            {
                this._connection.Open();
                if (withTransaction)
                    this._transaction = (IDbTransaction)this._connection.BeginTransaction();
            }
            return (IDbConnection)this._connection;
        }

        public override async Task<IDbConnection> GetOpenConnectionAsync(bool withTransaction)
        {
            if (this._connection == null)
                this._connection = new SqlConnection(this.ConnectionString);
            if (this._connection.State != ConnectionState.Open)
                await this._connection.OpenAsync();
            if (withTransaction)
                this._transaction = (IDbTransaction)this._connection.BeginTransaction();
            return (IDbConnection)this._connection;
        }

        public override IDbTransaction GetTransaction()
        {
            return this._transaction;
        }
    }
}
