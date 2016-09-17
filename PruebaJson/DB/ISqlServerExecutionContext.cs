using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public interface ISqlServerExecutionContext : IDisposable
    {
        object ExecuteScalarThroughSqlCommand(SqlCommand command);
        object ExecuteScalarThroughText(string text, params SqlParameter[] parameters);
        object ExecuteScalarThroughStoredProcedure(string procedureName, params SqlParameter[] parameters);
        int ExecuteNonQueryThroughSqlCommand(SqlCommand command);
        int ExecuteNonQueryThroughText(string text, params SqlParameter[] parameters);
        int ExecuteNonQueryThroughStoredProcedure(string procedureName, params SqlParameter[] parameters);
        SqlDataReader ExecuteReaderThroughSqlCommand(SqlCommand command);
        SqlDataReader ExecuteReaderThroughText(string text, params SqlParameter[] parameters);
        SqlDataReader ExecuteReaderThroughStoredProcedure(string procedureName, params SqlParameter[] parameters);

        void FillDataTableWithSqlCommand(DataTable dataTable, SqlCommand command);
        void FillDataTableWithText(DataTable dataTable, string text, params SqlParameter[] parameters);
        void FillDataTableWithStoredProcedure(DataTable dataTable, string procedureName, params SqlParameter[] parameters);

        void FillDataSetWithSqlCommand(DataSet dataSet, SqlCommand command);
        void FillDataSetWithText(DataSet dataSet, string text, params SqlParameter[] parameters);
        void FillDataSetWithStoredProcedure(DataSet dataSet, string procedureName, params SqlParameter[] parameters);
    }
}
