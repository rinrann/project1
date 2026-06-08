using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for LaprosopicNoteClass
/// </summary>
public class LaprosopicNoteClass
{
    public LaprosopicNoteClass(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable Laprosopic;
    

    public DataTable DropdownOprationType()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from IPD_OperationType where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        Laprosopic = new DataTable();
        theAdapter.Fill(Laprosopic); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return Laprosopic;
    }

    public DataTable DropdownOperationName(string typeid)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from IPD_OperationDetails  where OperationTypeID='" + typeid + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        Laprosopic = new DataTable();
        theAdapter.Fill(Laprosopic); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return Laprosopic;
    }



    public DataTable LapNote(string optype,string opname)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select LaproscopicNote from OT_LaproscopicNote where OperationTypeId='" + optype + "' and OperationId='" + opname  + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        Laprosopic = new DataTable();
        theAdapter.Fill(Laprosopic); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return Laprosopic;
    }


    public bool Insert_Update_LaprosopicNote(string LapNote, string OperationTypeID, string OperationID, string PatientReg)
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
                        theCommand.CommandText = "SELECT al.OperationTypeId, al.OperationId FROM OT_LaproscopicNote al where OperationTypeId='" + OperationTypeID + "' and OperationId='" + OperationID + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader2 = theCommand.ExecuteReader();
                        theReader2.Read();

                        if (theReader2.HasRows == false)
                        {
                            theReader2.Close();
                            theCommand.CommandText = "INSERT INTO OT_LaproscopicNote(OperationTypeId, OperationId, LaproscopicNote) VALUES ('" + OperationTypeID + "', '" + OperationID + "', '" + LapNote + "')";
                            
                        }
                        theReader2.Close();
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.
                      


                        theCommand.CommandText = "update  IPD_OTOperationNote set LaproscopicNote='" + LapNote + "'  where PatientRegId='" + PatientReg + "'";
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