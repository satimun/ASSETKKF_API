using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace ASSETKKF_ADO.Mssql
{
    public abstract class Base
    {
        public static string conString { get; set; }
        public static string sql { get; set; }
        private static string error { get; set; }

        private string getConStr(string conStr)
        {
            return string.IsNullOrEmpty(conStr) ? conString : conStr;
        }

        protected string QuoteStr(string str)
        {
            return String.IsNullOrEmpty(str) ? "''" : "\'" + str.Replace("'", $"{(char)39}") + "\'";
        }

        protected T ExecuteScalar<T>(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
                {
                    try
                    {
                        var res = SqlMapper.ExecuteScalar<T>(conn, cmdTxt, parameter, null, 600);
                        return res;
                    }
                    catch (SqlException ex)
                    {
                        error = "Execute exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    catch (InvalidOperationException ex)
                    {
                        error = "Connection Exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }

                    
                }

            }
            catch (SqlException ex) 
            {
                error = "Execute exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (InvalidOperationException ex) 
            {
                error = "Connection Exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (Exception ex) 
            {
                error = "Exception Message: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            

           
        }
        protected T ExecuteScalarSP<T>(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
                {
                    try
                    {
                        var res = SqlMapper.ExecuteScalar<T>(conn, cmdTxt, parameter, null, 600, System.Data.CommandType.StoredProcedure);
                        return res;
                    }
                    catch (SqlException ex)
                    {
                        error = "Execute exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    catch (InvalidOperationException ex)
                    {
                        error = "Connection Exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    
                }
            }
            catch (SqlException ex)
            {
                error = "Execute exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (InvalidOperationException ex)
            {
                error = "Connection Exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                error = "Exception Message: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            
        }

        protected int ExecuteNonQuery(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
                {
                    try
                    {
                        var res = SqlMapper.Execute(conn, cmdTxt, parameter, null, 600);
                        return res;
                    }
                    catch (SqlException ex)
                    {
                        error = "Execute exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    catch (InvalidOperationException ex)
                    {
                        error = "Connection Exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    
                }
            }
            catch (SqlException ex)
            {
                error = "Execute exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (InvalidOperationException ex)
            {
                error = "Connection Exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                error = "Exception Message: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            
        }
        protected int ExecuteNonQuerySP(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
                {
                    try
                    {
                        var res = SqlMapper.Execute(conn, cmdTxt, parameter, null, 600, System.Data.CommandType.StoredProcedure);
                        return res;
                    }
                    catch (SqlException ex)
                    {
                        error = "Execute exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    catch (InvalidOperationException ex)
                    {
                        error = "Connection Exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                   
                }
            }
            catch (SqlException ex)
            {
                error = "Execute exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (InvalidOperationException ex)
            {
                error = "Connection Exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                error = "Exception Message: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            
        }

        protected IEnumerable<T> ExecQuery<T>(string cmdTxt, string conStr = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
                {
                    try
                    {
                        var res = conn.Query<T>(cmdTxt);
                        return res;
                    }
                    catch (SqlException ex)
                    {
                        error = "Execute exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    catch (InvalidOperationException ex)
                    {
                        error = "Connection Exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    
                }
            }
            catch (SqlException ex)
            {
                error = "Execute exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (InvalidOperationException ex)
            {
                error = "Connection Exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                error = "Exception Message: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            
        }

        protected IEnumerable<T> Query<T>(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
                {
                    try
                    {
                        var res = SqlMapper.Query<T>(conn, cmdTxt, parameter, null, true, 600);
                        return res;
                    }
                    catch (SqlException ex)
                    {
                        error = "Execute exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    catch (InvalidOperationException ex)
                    {
                        error = "Connection Exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    
                }
            }
            catch (SqlException ex)
            {
                error = "Execute exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (InvalidOperationException ex)
            {
                error = "Connection Exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                error = "Exception Message: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            
        }

        protected IEnumerable<T> QuerySP<T>(string spName, DynamicParameters parameter = null, string conStr = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
                {
                    try
                    {
                        var res = SqlMapper.Query<T>(conn, spName, parameter, null, true, 600, System.Data.CommandType.StoredProcedure);
                        return res;
                    }
                    catch (SqlException ex)
                    {
                        error = "Execute exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    catch (InvalidOperationException ex)
                    {
                        error = "Connection Exception issue: " + ex.Message;
                        Console.WriteLine(error);
                        throw new Exception(error);
                    }
                    
                }
            }
            catch (SqlException ex)
            {
                error = "Execute exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (InvalidOperationException ex)
            {
                error = "Connection Exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                error = "Exception Message: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
           
        }
        protected IEnumerable<T> QuerySP<T>(SqlTransaction transaction, string spName, DynamicParameters parameter = null)
        {
            try
            {
                if (transaction == null)
                {
                    return QuerySP<T>(spName, parameter);
                }
                var res = transaction.Connection.Query<T>(spName, parameter, transaction, true, 600, System.Data.CommandType.StoredProcedure);
                return res;
            }
            catch (SqlException ex)
            {
                error = "Execute exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (InvalidOperationException ex)
            {
                error = "Connection Exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                error = "Exception Message: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
           
        }

        // Transaction Rollback
        protected T ExecuteScalar<T>(SqlTransaction transaction, string cmdTxt, DynamicParameters parameter = null)
        {
            try
            {
                if (transaction == null)
                {
                    return ExecuteScalar<T>(cmdTxt, parameter);
                }
                var res = SqlMapper.ExecuteScalar<T>(transaction.Connection, cmdTxt, parameter, transaction, 600);
                return res;
            }
            catch (SqlException ex)
            {
                error = "Execute exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (InvalidOperationException ex)
            {
                error = "Connection Exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                error = "Exception Message: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }

           
        }
        protected int ExecuteNonQuery(SqlTransaction transaction, string cmdTxt, DynamicParameters parameter = null)
        {
            try
            {
                if (transaction == null)
                {
                    return ExecuteNonQuery(cmdTxt, parameter);
                }
                var res = SqlMapper.Execute(transaction.Connection, cmdTxt, parameter, transaction, 600);
                return res;
            }
            catch (SqlException ex)
            {
                error = "Execute exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (InvalidOperationException ex)
            {
                error = "Connection Exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                error = "Exception Message: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }

            
        }
        protected IEnumerable<T> Query<T>(SqlTransaction transaction, string cmdTxt, DynamicParameters parameter = null)
        {
            try
            {
                if (transaction == null)
                {
                    return Query<T>(cmdTxt, parameter);
                }
                var res = SqlMapper.Query<T>(transaction.Connection, cmdTxt, parameter, transaction, true, 600);
                return res;
            }
            catch (SqlException ex)
            {
                error = "Execute exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (InvalidOperationException ex)
            {
                error = "Connection Exception issue: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                error = "Exception Message: " + ex.Message;
                Console.WriteLine(error);
                throw new Exception(error);
            }

           
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

        public static DataTable ToDataTable(IEnumerable<dynamic> items)
        {
            if (items == null) return null;
            var data = items.ToArray();
            if (data.Length == 0) return null;

            var dt = new DataTable();
            foreach (var pair in ((IDictionary<string, object>)data[0]))
            {
                dt.Columns.Add(pair.Key, (pair.Value ?? string.Empty).GetType());
            }
            foreach (var d in data)
            {
                dt.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
            }
            return dt;
        }

        
    }
}
