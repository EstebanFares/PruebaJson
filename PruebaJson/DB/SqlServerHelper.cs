using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace DataLayer
{
    public static class SqlServerHelper
    {
        private struct ConnectionTransactionPair {
            public readonly SqlConnection Connection;
            public readonly SqlTransaction Transaction;
            [DebuggerNonUserCode()]
            public ConnectionTransactionPair(SqlConnection connection, SqlTransaction transaction)
            {
                Connection = connection;
                Transaction = transaction;
	        }
        }
        private static int nextTicket;
        private static Dictionary<int, ConnectionTransactionPair> savedParameters = new Dictionary<int,ConnectionTransactionPair>();

        [DebuggerNonUserCode()]
        public static DataTable GetDataTableWithSqlCommand(ISqlServerExecutionContext executionContext, SqlCommand command)
        {
            CheckISqlServerExecutionContext(executionContext);
            DataTable dataTable = new DataTable();
            dataTable.Locale = CultureInfo.CurrentCulture;
            executionContext.FillDataTableWithSqlCommand(dataTable, command);
            return dataTable;
        }
        [DebuggerNonUserCode()]
        public static DataTable GetDataTableWithText(ISqlServerExecutionContext executionContext, string text, params SqlParameter[] parameters)
        {
            CheckISqlServerExecutionContext(executionContext);
            DataTable dataTable = new DataTable();
            dataTable.Locale = CultureInfo.CurrentCulture;
            executionContext.FillDataTableWithText(dataTable, text, parameters);
            return dataTable;
        }
        [DebuggerNonUserCode()]
        public static DataTable GetDataTableWithStoredProcedure(ISqlServerExecutionContext executionContext, string procedureName, params SqlParameter[] parameters)
        {
            CheckISqlServerExecutionContext(executionContext);
            DataTable dataTable = new DataTable();
            dataTable.Locale = CultureInfo.CurrentCulture;
            executionContext.FillDataTableWithStoredProcedure(dataTable, procedureName, parameters);
            return dataTable;
        }

        [DebuggerNonUserCode()]
        public static DataSet GetDataSetWithSqlCommand(ISqlServerExecutionContext executionContext, SqlCommand command)
        {
            CheckISqlServerExecutionContext(executionContext);
            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.CurrentCulture;
            executionContext.FillDataSetWithSqlCommand(dataSet, command);
            return dataSet;
        }
        [DebuggerNonUserCode()]
        public static DataSet GetDataSetWithText(ISqlServerExecutionContext executionContext, string text, params SqlParameter[] parameters)
        {
            CheckISqlServerExecutionContext(executionContext);
            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.CurrentCulture;
            executionContext.FillDataSetWithText(dataSet, text, parameters);
            return dataSet;
        }
        [DebuggerNonUserCode()]
        public static DataSet GetDataSetWithStoredProcedure(ISqlServerExecutionContext executionContext, string procedureName, params SqlParameter[] parameters)
        {
            CheckISqlServerExecutionContext(executionContext);
            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.CurrentCulture;
            executionContext.FillDataSetWithStoredProcedure(dataSet, procedureName, parameters);
            return dataSet;
        }
        [DebuggerNonUserCode()]
        public static SqlParameter CreateInputParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value ?? DBNull.Value);
        }
        [DebuggerNonUserCode()]
        internal static void CopyParameters(SqlCommand command, IEnumerable<SqlParameter> parameters)
        {
            if (parameters == null) return;
            foreach (SqlParameter parameter in parameters)
                command.Parameters.Add(parameter);
        }
        [DebuggerNonUserCode()]
        private static void CheckISqlServerExecutionContext(ISqlServerExecutionContext executionContext)
        {
            if (executionContext == null) throw new ArgumentNullException("executionContext");
        }
        [DebuggerNonUserCode()]
        internal static void CheckDataSetParameter(DataSet dataSet)
        {
            if (dataSet == null) throw new ArgumentNullException("dataSet");
        }
        [DebuggerNonUserCode()]
        internal static void CheckDataTableParameter(DataTable dataTable)
        {
            if (dataTable == null) throw new ArgumentNullException("dataTable");
        }
        [DebuggerNonUserCode()]
        internal static void CheckTextParameter(string text)
        {
            if (text == null) throw new ArgumentNullException("text");
        }
        [DebuggerNonUserCode()]
        internal static void CheckProcedureNameParameter(string procedureName)
        {
            if (procedureName == null) throw new ArgumentNullException("procedureName");
        }
        [DebuggerNonUserCode()]
        internal static void CheckSqlCommandParameter(SqlCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");
        }

        [DebuggerNonUserCode()]
        internal static void RestoreSqlCommand(SqlCommand command, int ticket)
        {
            ConnectionTransactionPair pair = savedParameters[ticket];
            savedParameters.Remove(ticket);
            command.Connection = pair.Connection;
            command.Transaction = pair.Transaction;
        }
        
        [DebuggerNonUserCode()]
        internal static int PrepareSqlCommandWithSqlConnection(SqlCommand command, SqlConnection connection)
        {
            int ticket = nextTicket++;
            savedParameters.Add(ticket, new ConnectionTransactionPair(command.Connection, command.Transaction));
            command.Transaction = null;
            command.Connection = connection;
            return ticket;
        }
        
        [DebuggerNonUserCode()]
        internal static int PrepareSqlCommandWithSqlTransaction(SqlCommand command, SqlTransaction transaction)
        {
            int ticket = nextTicket++;
            savedParameters.Add(ticket, new ConnectionTransactionPair(command.Connection, command.Transaction));
            command.Transaction = transaction;
            command.Connection = transaction.Connection;
            return ticket;
        }

        [DebuggerNonUserCode()]
        internal static void Adapt(DataSet dataSet, SqlCommand command)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataSet);
            }
        }

        [DebuggerNonUserCode()]
        internal static void Adapt(DataTable dataTable, SqlCommand command)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }
        }
    }
}
