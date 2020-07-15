using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public abstract class Base
    {
        public static string conString { get; set; }
        public static string sql { get; set; }

        private string getConStr(string conStr)
        {
            return string.IsNullOrEmpty(conStr) ? conString : conStr;
        }

        protected string QuoteStr(string str)
        {
            return "\'" + str.Replace("'", $"{(char)39}") + "\'";
        }

        protected T ExecuteScalar<T>(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.ExecuteScalar<T>(conn, cmdTxt, parameter, null, 600);
                return res;
            }
        }
        protected T ExecuteScalarSP<T>(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.ExecuteScalar<T>(conn, cmdTxt, parameter, null, 600, System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        protected int ExecuteNonQuery(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.Execute(conn, cmdTxt, parameter, null, 600);
                return res;
            }
        }
        protected int ExecuteNonQuerySP(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.Execute(conn, cmdTxt, parameter, null, 600, System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        protected IEnumerable<T> ExecQuery<T>(string cmdTxt, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res =  conn.Query<T>(cmdTxt);
                return res;
            }
        }

        protected IEnumerable<T> Query<T>(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.Query<T>(conn, cmdTxt, parameter, null, true, 600);
                return res;
            }
        }

        protected IEnumerable<T> QuerySP<T>(string spName, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.Query<T>(conn, spName, parameter, null, true, 600, System.Data.CommandType.StoredProcedure);
                return res;
            }
        }
        protected IEnumerable<T> QuerySP<T>(SqlTransaction transaction, string spName, DynamicParameters parameter = null)
        {
            if (transaction == null)
            {
                return QuerySP<T>(spName, parameter);
            }
            var res = transaction.Connection.Query<T>(spName, parameter, transaction, true, 600, System.Data.CommandType.StoredProcedure);
            return res;
        }

        // Transaction Rollback
        protected T ExecuteScalar<T>(SqlTransaction transaction, string cmdTxt, DynamicParameters parameter = null)
        {
            if (transaction == null)
            {
                return ExecuteScalar<T>(cmdTxt, parameter);
            }
            var res = SqlMapper.ExecuteScalar<T>(transaction.Connection, cmdTxt, parameter, transaction, 600);
            return res;
        }
        protected int ExecuteNonQuery(SqlTransaction transaction, string cmdTxt, DynamicParameters parameter = null)
        {
            if (transaction == null)
            {
                return ExecuteNonQuery(cmdTxt, parameter);
            }
            var res = SqlMapper.Execute(transaction.Connection, cmdTxt, parameter, transaction, 600);
            return res;
        }
        protected IEnumerable<T> Query<T>(SqlTransaction transaction, string cmdTxt, DynamicParameters parameter = null)
        {
            if (transaction == null)
            {
                return Query<T>(cmdTxt, parameter);
            }
            var res = SqlMapper.Query<T>(transaction.Connection, cmdTxt, parameter, transaction, true, 600);
            return res;
        }

        public static SqlConnection OpenConnection(string conStr = null)
        {
            SqlConnection conn = new SqlConnection(string.IsNullOrEmpty(conStr) ? conString : conStr);
            return conn;
        }
        public static SqlTransaction BeginTransaction()
        {
            var conn = OpenConnection();
            conn.Open();
            return conn.BeginTransaction();
        }
    }
}
