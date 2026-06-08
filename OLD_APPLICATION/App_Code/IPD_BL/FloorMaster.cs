using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for FloorMaster12
/// </summary>
public class FloorMaster
{
    public FloorMaster(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable FloorTable;

    public DataTable GetAllFloor(string cocode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM IPD_FloorMaster where Compcode='" + cocode + "' and Status=1 order by FloorID";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        FloorTable = new DataTable();
        theAdapter.Fill(FloorTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return FloorTable;
    }
    public int GetFloorID()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("SELECT max(FloorID) as FloorID FROM IPD_FloorMaster Where status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int FloorID = 0;
        if (ds.Tables[0].Rows[0]["FloorID"] == DBNull.Value)
        {
            FloorID = 1;
        }

        else
        {
            FloorID = Convert.ToInt32(ds.Tables[0].Rows[0]["FloorID"]) + 1;
        }

        theConnection.Dispose();
        theAdapter.Dispose();
        return FloorID;
    }
    public bool InsertFloorMaster(string FloorName, string PatternText, string LoginUser,string cocode)
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
                        theCommand.CommandText = "INSERT INTO IPD_FloorMaster(compcode,FloorName, PatternText, CreatedBy, status) VALUES ('" + cocode + "','" + FloorName + "', '" + PatternText + "', '" + LoginUser + "', 1)";
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
    public bool UpdateFloorMaster(string FloorID, string FloorName, string PatternText,string cocode)
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
                        theCommand.CommandText = "Update IPD_FloorMaster set FloorName='" + FloorName + "',PatternText='" + PatternText + "'  where FloorID = '" + FloorID + "'and COMPCODE='"+cocode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute update query. 
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
    public bool DeleteFloorMaster(int FloorID,string cocode)
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
                        theCommand.CommandText = "delete IPD_FloorMaster   WHERE FloorID = '" + FloorID + "'and COMPCODE='"+cocode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute Delete query.

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