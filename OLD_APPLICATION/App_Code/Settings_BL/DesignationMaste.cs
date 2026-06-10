using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DesignationMaste12
/// </summary>
public class DesignationMaste
{
    public DesignationMaste(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable MasterTable;
    public DataTable GetAllDesignation()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM GN_DesignationMaster where Status=1 order by DesignationName asc";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MasterTable = new DataTable();
        theAdapter.Fill(MasterTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MasterTable;
    }
    //public int GetDesignationID()
    //{
    //    // Connection.
    //    theConnection = new SqlConnection();
    //    theConnection.ConnectionString = conString;

    //    // Command.
    //    theAdapter = new SqlDataAdapter("SELECT max(DesignationID) as DesignationID FROM GN_DesignationMaster Where status =1", theConnection);
    //    DataSet ds = new DataSet();
    //    theAdapter.Fill(ds);
    //    int DesignationID = 0;
    //    if (ds.Tables[0].Rows[0]["DesignationID"] == DBNull.Value)
    //    {
    //        DesignationID = 1;
    //    }

    //    else
    //    {
    //        DesignationID = Convert.ToInt32(ds.Tables[0].Rows[0]["DesignationID"]) + 1;
    //    }
    //    return DesignationID;
    //}
    public bool InsertDesignationMaster(string compcode,string DesignationName, string LoginUser)
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
                        theCommand.CommandText = "INSERT INTO GN_DesignationMaster(compcode,DesignationName, CreatedBy, status) VALUES ('"+ compcode +"','" + DesignationName + "', '" + LoginUser + "', 1)";
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
    public bool UpdateDesignationMaster(string compcode,string DesignationID, string DesignationName)
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
                        theCommand.CommandText = "Update GN_DesignationMaster set DesignationName='" + DesignationName + "'  where compcode='"+ compcode +"' and DesignationID = '" + DesignationID + "'";
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

    public bool DeleteDesignationMaster(int DesignationID)
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
                        theCommand.CommandText = "Update GN_DesignationMaster set status=2 WHERE DesignationID = " + DesignationID + "";
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