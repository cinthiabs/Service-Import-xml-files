using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Import.files.Data
{
    public class Connection
    {
        private readonly SqlConnection con = new();

        private readonly int timeOut = 0;
        public Connection()
        {
            con.ConnectionString = Setting.ConnectionStringDefault;
        }
        public SqlConnection Connect()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                if (String.IsNullOrEmpty(con.ConnectionString))
                {
                    con.ConnectionString = Setting.ConnectionStringDefault;
                }
                con.Open();
            }
            return con;
        }
        public void Disconnect()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
        protected bool ExecuteCommandString(string sqlQuery)
        {
            using var connection = Connect();
            return connection.Execute(sqlQuery, commandTimeout: timeOut) > 0;
        }
        protected T ExecuteCommandInt<T>(string sqlQuery)
        {
            using var connection = Connect();
            return connection.QueryFirstOrDefault<T>(sqlQuery, commandTimeout: timeOut);
        }
    }
}
