using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ServiceProvider132
/// </summary>
public class ServiceProvider
{
    public ServiceProvider(string con)
    {
        conString = con;
    }

    public string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable ServiceTable;
    public DataTable GetAllService()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM IPD_ServiceProvider where Status=1 order by ServiceProviderName asc";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        ServiceTable = new DataTable();
        theAdapter.Fill(ServiceTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();


        return ServiceTable;
    }
    public int GetServiceID()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("SELECT max(ServiceProviderID) as ServiceProviderID FROM IPD_ServiceProvider Where status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int ServiceProviderID = 0;
        if (ds.Tables[0].Rows[0]["ServiceProviderID"] == DBNull.Value)
        {
            ServiceProviderID = 1;
        }

        else
        {
            ServiceProviderID = Convert.ToInt32(ds.Tables[0].Rows[0]["ServiceProviderID"]) + 1;
        }

        // Clean up.
        theConnection.Dispose();
        theAdapter.Dispose();

        return ServiceProviderID;
    }
    public bool InsertServiceProvider(string ServiceProviderName, string Address, string PhoneNo_1, string PhoneNo_2, string Email, string LoginUser)
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
                        theCommand.CommandText = "INSERT INTO IPD_ServiceProvider(ServiceProviderName, Address, PhnNo_1,PhnNo_2, Email, CreatedBy,Status) VALUES ( '" + ServiceProviderName + "','" + Address + "','" + PhoneNo_1 + "', '" + PhoneNo_2 + "', '" + Email + "', '" + LoginUser + "', 1)";
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
    public bool UpdateServiceProvider(string ServiceProviderID, string ServiceProviderName, string Address, string PhoneNo_1, string PhoneNo_2, string Email)
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
                        theCommand.CommandText = "Update IPD_ServiceProvider set  ServiceProviderName='" + ServiceProviderName + "', Address='" + Address + "', PhnNo_1='" + PhoneNo_1 + "', PhnNo_2='" + PhoneNo_2 + "', Email='" + Email + "'  where ServiceProviderID = '" + ServiceProviderID + "'";
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
    public bool DeleteServiceProvider(int ServiceProviderID)
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
                        theCommand.CommandText = "Update IPD_ServiceProvider set status=2 WHERE ServiceProviderID = " + ServiceProviderID + "";
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