using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using System.Globalization;
using System.Threading;

namespace DataLayer
{
    public sealed class SqlServerDatabase : ISqlServerExecutionContext
    {
        private SqlConnection connection;
        //private System.Data.OleDb.OleDbConnection connection;
        private int transactionsInProgress;

        [DebuggerNonUserCode()]
        public DatabaseState DatabaseState
        {
            get
            {
                if (connection == null)
                    return DatabaseState.Disposed;
                else if (connection.State == ConnectionState.Closed)
                    return DatabaseState.Closed;
                else if (transactionsInProgress == 0)
                    return DatabaseState.Opened;
                else
                    return DatabaseState.WithTransactionsInProgress;
            }
        }

        [DebuggerNonUserCode()]
        private void CanIOperate(DatabaseAction databaseAction)
        {
            switch (DatabaseState)
            {
                case DatabaseState.Closed:
                    if (databaseAction == DatabaseAction.Close ||
                        databaseAction == DatabaseAction.EndsLastTransaction ||
                        databaseAction == DatabaseAction.BeginsFirstTransaction ||
                        databaseAction == DatabaseAction.DataOperation)
                        throw new DatabaseClosedException();
                    break;
                case DatabaseState.Opened:
                    if (databaseAction == DatabaseAction.Dispose ||
                        databaseAction == DatabaseAction.Open ||
                        databaseAction == DatabaseAction.EndsLastTransaction)
                        throw new DatabaseOpenedException();
                    break;
                case DatabaseState.WithTransactionsInProgress:
                    if (databaseAction == DatabaseAction.Open ||
                        databaseAction == DatabaseAction.Close ||
                        databaseAction == DatabaseAction.Dispose ||
                        databaseAction == DatabaseAction.BeginsFirstTransaction)
                        throw new DatabaseWithTransactionsInProgressException();
                    break;
                case DatabaseState.Disposed:
                    throw new DatabaseDisposedException();
            }
        }

        [DebuggerNonUserCode()]
        public void OpenConnection()
        {
            CanIOperate(DatabaseAction.Open);
            connection.Open();
        }
        [DebuggerNonUserCode()]
        public void CloseConnection()
        {
            CanIOperate(DatabaseAction.Close);
            connection.Close();
        }
        [DebuggerNonUserCode()]
        private void IncrementTransactionsInProgress()
        {
            if (transactionsInProgress == 0)
                CanIOperate(DatabaseAction.BeginsFirstTransaction);
            transactionsInProgress++;
        }
        [DebuggerNonUserCode()]
        internal void DecrementTransactionsInProgress()
        {
            if (transactionsInProgress == 1)
                CanIOperate(DatabaseAction.EndsLastTransaction);
            transactionsInProgress--;
        }

        [DebuggerNonUserCode()]
        public SqlServerDatabase(string connectionStr)
        {
            connection = new SqlConnection(connectionStr);                        
            transactionsInProgress = 0;
        }
        [DebuggerNonUserCode()]
        public SqlServerTransactionalExecutionContext BeginTransaction(IsolationLevel isolationLevel)
        {
            try
            {
                IncrementTransactionsInProgress();
                return new SqlServerTransactionalExecutionContext(connection.BeginTransaction(isolationLevel), this);
            }
            catch (InvalidOperationException)
            {
                DecrementTransactionsInProgress();
                throw;
            }

        }

        [DebuggerNonUserCode()]
        private static SqlCommand CreateStoredProcedureSqlCommand(string procedureName, SqlConnection connection, IEnumerable<SqlParameter> parameters)
        {
            SqlServerHelper.CheckProcedureNameParameter(procedureName);
            SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlServerHelper.CopyParameters(command, parameters);
            return command;
        }

        [DebuggerNonUserCode()]
        private static SqlCommand CreateTextSqlCommand(string text, SqlConnection connection, IEnumerable<SqlParameter> parameters)
        {
            SqlServerHelper.CheckTextParameter(text);
            SqlCommand command = new SqlCommand(text, connection);
            command.CommandType = CommandType.Text;
            SqlServerHelper.CopyParameters(command, parameters);
            return command;
        }

        [DebuggerNonUserCode()]
        public object ExecuteScalarThroughSqlCommand(SqlCommand command)
        {
            CanIOperate(DatabaseAction.DataOperation);
            object result;
            SqlServerHelper.CheckSqlCommandParameter(command);
            int ticket = SqlServerHelper.PrepareSqlCommandWithSqlConnection(command, connection);
            result = command.ExecuteScalar();
            SqlServerHelper.RestoreSqlCommand(command, ticket);
            return result;
        }

        [DebuggerNonUserCode()]
        public object ExecuteScalarThroughText(string text, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            using (SqlCommand comando = CreateTextSqlCommand(text, connection, parameters))
            {
                return comando.ExecuteScalar();
            }
        }

        [DebuggerNonUserCode()]
        public object ExecuteScalarThroughStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            using (SqlCommand comando = CreateStoredProcedureSqlCommand(procedureName, connection, parameters))
            {
                return comando.ExecuteScalar();
            }
        }

        [DebuggerNonUserCode()]
        public int ExecuteNonQueryThroughSqlCommand(SqlCommand command)
        {
            CanIOperate(DatabaseAction.DataOperation);
            int result;
            SqlServerHelper.CheckSqlCommandParameter(command);
            int ticket = SqlServerHelper.PrepareSqlCommandWithSqlConnection(command, connection);
            result = command.ExecuteNonQuery();
            SqlServerHelper.RestoreSqlCommand(command, ticket);
            return result;
        }

        [DebuggerNonUserCode()]
        public int ExecuteNonQueryThroughText(string text, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            using (SqlCommand comando = CreateTextSqlCommand(text, connection, parameters))
            {
                comando.CommandTimeout = 90000;
                return comando.ExecuteNonQuery();
            }
        }

        [DebuggerNonUserCode()]
        public int ExecuteNonQueryThroughStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            using (SqlCommand comando = CreateStoredProcedureSqlCommand(procedureName, connection, parameters))
            {                
                return comando.ExecuteNonQuery();
            }
        }

        [DebuggerNonUserCode()]
        public int ExecuteNonQueryThroughStoredProcedureLargo(string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            using (SqlCommand comando = CreateStoredProcedureSqlCommand(procedureName, connection, parameters))
            {
                comando.CommandTimeout = 90000;
                return comando.ExecuteNonQuery();
            }
        }

        [DebuggerNonUserCode()]
        public SqlDataReader ExecuteReaderThroughSqlCommand(SqlCommand command)
        {
            CanIOperate(DatabaseAction.DataOperation);
            SqlDataReader result;
            SqlServerHelper.CheckSqlCommandParameter(command);
            int ticket = SqlServerHelper.PrepareSqlCommandWithSqlConnection(command, connection);
            result = command.ExecuteReader();
            SqlServerHelper.RestoreSqlCommand(command, ticket);
            return result;
        }

        [DebuggerNonUserCode()]
        public SqlDataReader ExecuteReaderThroughText(string text, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            using (SqlCommand comando = CreateTextSqlCommand(text, connection, parameters))
            {
                return comando.ExecuteReader();
            }
        }

        [DebuggerNonUserCode()]
        public SqlDataReader ExecuteReaderThroughStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            using (SqlCommand comando = CreateStoredProcedureSqlCommand(procedureName, connection, parameters))
            {
                return comando.ExecuteReader();
            }
        }

        [DebuggerNonUserCode()]
        public void FillDataTableWithSqlCommand(DataTable dataTable, SqlCommand command)
        {
            CanIOperate(DatabaseAction.DataOperation);
            SqlServerHelper.CheckDataTableParameter(dataTable);
            SqlServerHelper.CheckSqlCommandParameter(command);
            int ticket = SqlServerHelper.PrepareSqlCommandWithSqlConnection(command, connection);
            SqlServerHelper.Adapt(dataTable, command);
            SqlServerHelper.RestoreSqlCommand(command, ticket);
        }

        [DebuggerNonUserCode()]
        public void FillDataTableWithText(DataTable dataTable, string text, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            SqlServerHelper.CheckDataTableParameter(dataTable);
            using (SqlCommand comando = CreateTextSqlCommand(text, connection, parameters))
            {
                SqlServerHelper.Adapt(dataTable, comando);
            }
        }

        [DebuggerNonUserCode()]
        public void FillDataTableWithStoredProcedure(DataTable dataTable, string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            SqlServerHelper.CheckDataTableParameter(dataTable);
            using (SqlCommand comando = CreateStoredProcedureSqlCommand(procedureName, connection, parameters))
            {
                SqlServerHelper.Adapt(dataTable, comando);
            }
        }

        [DebuggerNonUserCode()]
        public void FillDataTableWithStoredProcedureLargo(DataTable dataTable, string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            SqlServerHelper.CheckDataTableParameter(dataTable);
            using (SqlCommand comando = CreateStoredProcedureSqlCommand(procedureName, connection, parameters))
            {
                comando.CommandTimeout = 90000;
                SqlServerHelper.Adapt(dataTable, comando);
            }
        }

        [DebuggerNonUserCode()]
        public void FillDataSetWithSqlCommand(DataSet dataSet, SqlCommand command)
        {
            CanIOperate(DatabaseAction.DataOperation);
            SqlServerHelper.CheckDataSetParameter(dataSet);
            SqlServerHelper.CheckSqlCommandParameter(command);
            int ticket = SqlServerHelper.PrepareSqlCommandWithSqlConnection(command, connection);
            SqlServerHelper.Adapt(dataSet, command);
            SqlServerHelper.RestoreSqlCommand(command, ticket);
        }

        [DebuggerNonUserCode()]
        public void FillDataSetWithText(DataSet dataSet, string text, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            SqlServerHelper.CheckDataSetParameter(dataSet);
            using (SqlCommand comando = CreateTextSqlCommand(text, connection, parameters))
            {
                SqlServerHelper.Adapt(dataSet, comando);
            }
        }

        [DebuggerNonUserCode()]
        public void FillDataSetWithStoredProcedure(DataSet dataSet, string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(DatabaseAction.DataOperation);
            SqlServerHelper.CheckDataSetParameter(dataSet);
            using (SqlCommand comando = CreateStoredProcedureSqlCommand(procedureName, connection, parameters))
            {
                SqlServerHelper.Adapt(dataSet, comando);
            }
        }

        [DebuggerNonUserCode()]
        public void Dispose()
        {
            CanIOperate(DatabaseAction.Dispose);
            connection.Dispose();
            connection = null;
            GC.SuppressFinalize(this);
        }
        [DebuggerNonUserCode()]
        ~SqlServerDatabase()
        {
            //throw new DatabaseNotDisposedException();
        }
    }
}
