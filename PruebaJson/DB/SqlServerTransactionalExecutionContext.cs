using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;

namespace DataLayer
{   
    public sealed class SqlServerTransactionalExecutionContext : ISqlServerExecutionContext
    {
        
        private SqlTransaction transaction;
        private SqlServerDatabase database;
        
        [DebuggerNonUserCode()]
        private void CanIOperate(TransactionAction transactionAction)
        {
            switch (TransactionState)
            {
                case TransactionState.InProgress:
                    if (transactionAction == TransactionAction.Dispose)
                        throw new TransactionInProgressException();
                    break;
                case TransactionState.Ended:
                    if (transactionAction != TransactionAction.Dispose)
                        throw new TransactionEndedException();
                    break;
                case TransactionState.Disposed:
                    throw new TransactionDisposedException();
            }
        }

        //[DebuggerNonUserCode()]
        internal SqlServerTransactionalExecutionContext(SqlTransaction transaction, SqlServerDatabase database)
        {
            this.transaction = transaction;
            this.database = database;
        }

        [DebuggerNonUserCode()]
        private static SqlCommand CreateStoredProcedureSqlCommand(string procedureName, SqlTransaction transaction, IEnumerable<SqlParameter> parameters)
        {
            SqlCommand command = new SqlCommand(procedureName, transaction.Connection, transaction);
            command.CommandType = CommandType.StoredProcedure;
            SqlServerHelper.CopyParameters(command, parameters);
            return command;
        }

        [DebuggerNonUserCode()]
        private static SqlCommand CreateTextSqlCommand(string text, SqlTransaction transacción, IEnumerable<SqlParameter> parameters)
        {
            SqlCommand command = new SqlCommand(text, transacción.Connection, transacción);
            command.CommandType = CommandType.Text;
            SqlServerHelper.CopyParameters(command, parameters);
            return command;
        }
        
        [DebuggerNonUserCode()]
        public void Dispose()
        {
            CanIOperate(TransactionAction.Dispose);
            transaction.Dispose();
            transaction = null;
            GC.SuppressFinalize(this);
        }
        
        [DebuggerNonUserCode()]
        private void EndTransaction()
        {
            database.DecrementTransactionsInProgress();
            database = null;
        }

        [DebuggerNonUserCode()]
        public object ExecuteScalarThroughSqlCommand(SqlCommand command)
        {
            CanIOperate(TransactionAction.DataOperation);
            object result;
            SqlServerHelper.CheckSqlCommandParameter(command);
            int ticket = SqlServerHelper.PrepareSqlCommandWithSqlTransaction(command,transaction);
            result = command.ExecuteScalar();
            SqlServerHelper.RestoreSqlCommand(command, ticket);
            return result;
        }

        [DebuggerNonUserCode()]
        public object ExecuteScalarThroughText(string text, params SqlParameter[] parameters)
        {
            CanIOperate(TransactionAction.DataOperation);
            using (SqlCommand command = CreateTextSqlCommand(text, transaction, parameters))
            {
                return command.ExecuteScalar();
            }
        }

        [DebuggerNonUserCode()]
        public object ExecuteScalarThroughStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(TransactionAction.DataOperation);
            using (SqlCommand command = CreateStoredProcedureSqlCommand(procedureName, transaction, parameters))
            {
                return command.ExecuteScalar();
            }
        }

        [DebuggerNonUserCode()]
        public int ExecuteNonQueryThroughSqlCommand(SqlCommand command)
        {
            CanIOperate(TransactionAction.DataOperation);
            int result;
            SqlServerHelper.CheckSqlCommandParameter(command);
            int ticket = SqlServerHelper.PrepareSqlCommandWithSqlTransaction(command, transaction);
            result = command.ExecuteNonQuery();
            SqlServerHelper.RestoreSqlCommand(command, ticket);
            return result;
        }

        [DebuggerNonUserCode()]
        public int ExecuteNonQueryThroughText(string text, params SqlParameter[] parameters)
        {
            CanIOperate(TransactionAction.DataOperation);
            using (SqlCommand command = CreateTextSqlCommand(text, transaction, parameters))
            {
                return command.ExecuteNonQuery();
            }
        }

        [DebuggerNonUserCode()]
        public int ExecuteNonQueryThroughStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(TransactionAction.DataOperation);
            using (SqlCommand command = CreateStoredProcedureSqlCommand(procedureName, transaction, parameters))
            {
                return command.ExecuteNonQuery();
            }
        }

        [DebuggerNonUserCode()]
        public SqlDataReader ExecuteReaderThroughSqlCommand(SqlCommand command)
        {
            CanIOperate(TransactionAction.DataOperation);
            SqlDataReader result;
            SqlServerHelper.CheckSqlCommandParameter(command);
            int ticket = SqlServerHelper.PrepareSqlCommandWithSqlTransaction(command, transaction);
            result = command.ExecuteReader();
            SqlServerHelper.RestoreSqlCommand(command, ticket);
            return result;
        }

        [DebuggerNonUserCode()]
        public SqlDataReader ExecuteReaderThroughText(string text, params SqlParameter[] parameters)
        {
            CanIOperate(TransactionAction.DataOperation);
            using (SqlCommand command = CreateTextSqlCommand(text, transaction, parameters))
            {
                return command.ExecuteReader();
            }
        }
        [DebuggerNonUserCode()]
        public SqlDataReader ExecuteReaderThroughStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(TransactionAction.DataOperation);
            using (SqlCommand command = CreateStoredProcedureSqlCommand(procedureName, transaction, parameters))
            {
                return command.ExecuteReader();
            }
        }

        [DebuggerNonUserCode()]
        public void FillDataTableWithSqlCommand(DataTable dataTable, SqlCommand command)
        {
            CanIOperate(TransactionAction.DataOperation);
            SqlServerHelper.CheckDataTableParameter(dataTable);
            SqlServerHelper.CheckSqlCommandParameter(command);
            int ticket = SqlServerHelper.PrepareSqlCommandWithSqlTransaction(command, transaction);
            SqlServerHelper.Adapt(dataTable, command);
            SqlServerHelper.RestoreSqlCommand(command, ticket);
        }
        [DebuggerNonUserCode()]
        public void FillDataTableWithText(DataTable dataTable, string text, params SqlParameter[] parameters)
        {
            CanIOperate(TransactionAction.DataOperation);
            SqlServerHelper.CheckDataTableParameter(dataTable);
            using (SqlCommand command = CreateTextSqlCommand(text, transaction, parameters))
            {
                SqlServerHelper.Adapt(dataTable, command);
            }
        }
        [DebuggerNonUserCode()]
        public void FillDataTableWithStoredProcedure(DataTable dataTable, string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(TransactionAction.DataOperation);
            SqlServerHelper.CheckDataTableParameter(dataTable);
            using (SqlCommand command = CreateStoredProcedureSqlCommand(procedureName, transaction, parameters))
            {
                SqlServerHelper.Adapt(dataTable, command);
            }
        }

        [DebuggerNonUserCode()]
        public void FillDataSetWithSqlCommand(DataSet dataSet, SqlCommand command)
        {
            CanIOperate(TransactionAction.DataOperation);
            SqlServerHelper.CheckDataSetParameter(dataSet);
            SqlServerHelper.CheckSqlCommandParameter(command);
            int ticket = SqlServerHelper.PrepareSqlCommandWithSqlTransaction(command, transaction);
            SqlServerHelper.Adapt(dataSet, command);
            SqlServerHelper.RestoreSqlCommand(command, ticket);
        }

        [DebuggerNonUserCode()]
        public void FillDataSetWithText(DataSet dataSet, string text, params SqlParameter[] parameters)
        {
            CanIOperate(TransactionAction.DataOperation);
            SqlServerHelper.CheckDataSetParameter(dataSet);
            using (SqlCommand command = CreateTextSqlCommand(text, transaction, parameters))
            {
                SqlServerHelper.Adapt(dataSet, command);
            }
        }

        [DebuggerNonUserCode()]
        public void FillDataSetWithStoredProcedure(DataSet dataSet, string procedureName, params SqlParameter[] parameters)
        {
            CanIOperate(TransactionAction.DataOperation);
            SqlServerHelper.CheckDataSetParameter(dataSet);
            using (SqlCommand command = CreateStoredProcedureSqlCommand(procedureName, transaction, parameters))
            {
                SqlServerHelper.Adapt(dataSet, command);
            }
        }

        

        [DebuggerNonUserCode()]
        public void Commit()
        {
            CanIOperate(TransactionAction.Commit);
            transaction.Commit();
            EndTransaction();
        }
        [DebuggerNonUserCode()]
        public void Rollback()
        {
            CanIOperate(TransactionAction.Rollback);
            transaction.Rollback();
            EndTransaction();
        }
        
        [DebuggerNonUserCode()]
        ~SqlServerTransactionalExecutionContext()
        {
            throw new TransactionNotDisposedException();
        }

        //[DebuggerNonUserCode()]
        public TransactionState TransactionState
        {
            get
            {
                if (database != null)
                    return TransactionState.InProgress;
                else if (transaction != null)
                    return TransactionState.Ended;
                else
                    return TransactionState.Disposed;
            }
        }
    }
}
