using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace wpf_demo_phonebook
{
    public class DbConnection
    {
        public SqlDataAdapter DataAdapter { get; set; } = new SqlDataAdapter();
        public static SqlConnection Connection { get; set; }

        public DbConnection()
        {
            if (Connection == null)
                Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }

        private SqlConnection open()
        {
            if (Connection.State == ConnectionState.Closed || 
                Connection.State == ConnectionState.Broken)
            {
                Connection.Open();
            }

            return Connection;
        }

        private void writeError(string _message)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            MethodBase methodBase = stackFrame.GetMethod();

            var msg = $"Error - {methodBase.Name} - {_message}";
            Console.WriteLine(msg);
            Debug.WriteLine (msg);
        }

        public DataTable ExecuteSelectQuery(string _query, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand();
            DataTable dataTable = null;
            DataSet ds = new DataSet();

            try
            {
                command.Connection = open();
                command.CommandText = _query;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
                DataAdapter.SelectCommand = command;
                DataAdapter.Fill(ds);
                dataTable = ds.Tables[0];

            }
            catch (Exception ex)
            {
                writeError($"Requête : {_query} \nSqlException : {ex.StackTrace.ToString()}");
            }
            finally
            {
                Connection.Close();
            }

            return dataTable;
        }

        public int ExecutInsertQuery(string _query, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand();
            int result = -1;

            try
            {
                command.Connection = open();
                command.CommandText = _query;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                DataAdapter.InsertCommand = command;
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                writeError($"Requête : {_query} \nSqlException : {ex.StackTrace.ToString()}");
            }
            finally
            {
                Connection.Close();
            }

            return result;
        }

        public int ExecutUpdateQuery(string _query, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand();
            int result = 0;

            try
            {
                command.Connection = open();
                command.CommandText = _query;
                command.Parameters.AddRange(parameters);
                DataAdapter.UpdateCommand = command;
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                writeError($"Requête : {_query} \nSqlException : {ex.StackTrace.ToString()}");
            }
            finally
            {
                Connection.Close();
            }

            return result;
        }
    }
}
