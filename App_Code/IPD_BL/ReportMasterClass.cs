using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ReportMasterClass
/// </summary>
public class ReportMasterClass
{
    public ReportMasterClass(string con)
    {
        conString = con;
    }

    public string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable RoomTable;


    public DataTable GetReportMaster(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM IPD_FormMaster where Status=1 and compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        RoomTable = new DataTable();
        theAdapter.Fill(RoomTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();


        return RoomTable;
    }
    public bool Insert_Update_Delete_ReportMaster(int mode,string id, string Language, string FormType, string FormName, string FormContext,string cocode)
    {
         try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        if (mode == 1)
                            theCommand.CommandText = "INSERT INTO IPD_FormMaster(compcode,Language, FormType, FormName,FormContext, Status,Serverflag) VALUES ('"+cocode+"','" + Language + "', '" + FormType + "', N'" + FormName + "',N'" + FormContext + "', 1,1)";
                        if (mode == 2)
                            theCommand.CommandText = "update IPD_FormMaster set Language='" + Language + "', FormType='" + FormType + "', FormName=N'" + FormName + "',FormContext=N'" + FormContext + "', Serverflag='1' where ID='" + id + "'and compcode='"+cocode+"'";
                        if (mode == 3)
                            theCommand.CommandText = "update  IPD_FormMaster set Status=0  where ID='" + id + "'and compcode='"+cocode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
        }
    }
}