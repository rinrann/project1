using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for OperationDetails12
/// </summary>
public class OperationDetails
{
    public OperationDetails(string con)
    {
        conString = con;
    }

    public string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable OperationTable;
    public DataTable GetAllOperation(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM IPD_OperationDetails T1,IPD_OperationType T2 WHERE t1.compcode=T2.compcode and T1.OperationTypeID=T2.OperationTypeID AND T1.[status]=1 and T1.compcode='"+compcode+"' order by OperationName asc";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        OperationTable = new DataTable();
        theAdapter.Fill(OperationTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return OperationTable;
    }
    public int GetOperationID(string compcode)
    {
        //Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        //Command.
        theAdapter = new SqlDataAdapter("SELECT max(OperationID) as OperationID FROM IPD_OperationDetails Where Status =1 and compcode='"+compcode+"'", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int OperationID = 0;
        if (ds.Tables[0].Rows[0]["OperationID"] == DBNull.Value)
        {
            OperationID = 1;
        }
        else
        {
            OperationID = Convert.ToInt32(ds.Tables[0].Rows[0]["OperationID"]) + 1;
        }

        theConnection.Dispose();
        theAdapter.Dispose();

        return OperationID;
    }
    public DataTable DropdownID(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select OperationTypeID,OperationTypeName from  IPD_OperationType where compcode='"+compcode+"' and status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        OperationTable = new DataTable();
        theAdapter.Fill(OperationTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return OperationTable;
    }
    public bool InsertOperationDetails(string OperationId,string OperationName, string OperationTypeID, string OperationCost, string OperationSummary, string Duration, string LoginUser,string cocode,string delivery)
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
                        theCommand.CommandText = "INSERT INTO IPD_OperationDetails(compcode,OperationID,OperationName, OperationTypeID, OperationCost, OperationSummary, Duration, CreatedBy, Status,Delivery) VALUES ('" + cocode + "','" + OperationId + "','" + OperationName + "', '" + OperationTypeID + "', '" + OperationCost + "', '" + OperationSummary + "', '" + Duration + "', '" + LoginUser + "', 1,'" + delivery + "')";
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
    public bool UpdateOperationDetails(string OperationID, string OperationName, string OperationTypeID, string OperationCost, string OperationSummary, string Duration, string cocode,string delivery)
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
                        theCommand.CommandText = "Update IPD_OperationDetails set  OperationName='" + OperationName + "', OperationTypeID='" + OperationTypeID + "', OperationCost='" + OperationCost + "', OperationSummary='" + OperationSummary + "', Duration='" + Duration + "',Delivery='" + delivery + "' where OperationID = '" + OperationID + "' and compcode='" + cocode + "'";
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
    public bool DeleteOperationDetails(int OperationID, string cocode)
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
                        theCommand.CommandText = "Update IPD_OperationDetails set  status=2 WHERE OperationID = '" + OperationID + "'and compcode='"+cocode+"'";
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