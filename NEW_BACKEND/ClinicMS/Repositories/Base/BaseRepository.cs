using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ClinicMS.Repositories.Base
{
    /// <summary>
    /// Thin ADO.NET wrapper.  Every repository inherits this.
    /// All queries use parameterised commands — no string concatenation.
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly string ConnectionString;

        protected BaseRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ClinicMSConnection"].ConnectionString;
        }
        //protected BaseRepository()
        //{
        //    ConnectionString =
        //        ConfigurationManager.ConnectionStrings["ClinicMSConnection"].ConnectionString;

        //    throw new Exception(ConnectionString);
        //}

        protected SqlConnection CreateConnection() => new SqlConnection(ConnectionString);

        // ── Execute helpers ──────────────────────────────────────────────────────

        protected DataTable QueryTable(string sql, Action<SqlCommand> paramSetup = null,
            CommandType cmdType = CommandType.Text)
        {
            using (var conn = CreateConnection())
            using (var cmd  = new SqlCommand(sql, conn) { CommandType = cmdType, CommandTimeout = 60 })
            {
                paramSetup?.Invoke(cmd);
                conn.Open();
                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        protected DataSet QueryDataSet(string sql, Action<SqlCommand> paramSetup = null,
            CommandType cmdType = CommandType.Text)
        {
            using (var conn = CreateConnection())
            using (var cmd  = new SqlCommand(sql, conn) { CommandType = cmdType, CommandTimeout = 60 })
            {
                paramSetup?.Invoke(cmd);
                conn.Open();
                var da = new SqlDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        protected int Execute(string sql, Action<SqlCommand> paramSetup = null,
            CommandType cmdType = CommandType.Text)
        {
            using (var conn = CreateConnection())
            using (var cmd  = new SqlCommand(sql, conn) { CommandType = cmdType, CommandTimeout = 60 })
            {
                paramSetup?.Invoke(cmd);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        protected object ExecuteScalar(string sql, Action<SqlCommand> paramSetup = null,
            CommandType cmdType = CommandType.Text)
        {
            using (var conn = CreateConnection())
            using (var cmd  = new SqlCommand(sql, conn) { CommandType = cmdType, CommandTimeout = 60 })
            {
                paramSetup?.Invoke(cmd);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        // ── Null-safe reader helpers ─────────────────────────────────────────────

        protected static string S(DataRow r, string col)
            => r.Table.Columns.Contains(col) && r[col] != DBNull.Value ? r[col].ToString() : null;

        protected static int I(DataRow r, string col)
            => r.Table.Columns.Contains(col) && r[col] != DBNull.Value ? Convert.ToInt32(r[col]) : 0;

        protected static decimal D(DataRow r, string col)
            => r.Table.Columns.Contains(col) && r[col] != DBNull.Value ? Convert.ToDecimal(r[col]) : 0m;

        protected static DateTime? DT(DataRow r, string col)
            => r.Table.Columns.Contains(col) && r[col] != DBNull.Value ? (DateTime?)Convert.ToDateTime(r[col]) : null;

        protected static bool B(DataRow r, string col)
            => r.Table.Columns.Contains(col) && r[col] != DBNull.Value && Convert.ToBoolean(r[col]);
    }
}
