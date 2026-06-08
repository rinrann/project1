using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ServiceDetails12
/// </summary>
public class ServiceDetails
{
    public ServiceDetails(string con)
    {
        conString = con;
    }

    private string conString;
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
        theCommand.CommandText = "SELECT * FROM IPD_ServiceDetails T1,IPD_ServiceCategory T2,IPD_ServiceProvider T3 WHERE  T1.ServiceCategoryID=T2.ServiceCategoryID AND T1.ServiceProviderID=T3.ServiceProviderID AND T1.[status]=1 order by ServiceName asc";
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
        //Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        //Command.
        theAdapter = new SqlDataAdapter("SELECT max(ServiceID) as ServiceID FROM IPD_ServiceDetails Where Status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int ServiceID = 0;
        if (ds.Tables[0].Rows[0]["ServiceID"] == DBNull.Value)
        {
            ServiceID = 1;
        }
        else
        {
            ServiceID = Convert.ToInt32(ds.Tables[0].Rows[0]["ServiceID"]) + 1;
        }

        // Clean up.
        theConnection.Dispose();
        theAdapter.Dispose();

        return ServiceID;

    }
    public DataTable DropdownID1()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ServiceCategoryID,ServiceCategoryName from  IPD_ServiceCategory where status=1";
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
    public DataTable DropdownID2()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ServiceProviderID,ServiceProviderName from  IPD_ServiceProvider where status=1";
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
    public bool InsertServiceDetails(string ServiceName, string ServiceCategoryID, string ServiceProviderID, string Quantity, string Charges, string LoginUser)
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
                        theCommand.CommandText = "INSERT INTO IPD_ServiceDetails(ServiceName, ServiceCategoryID, ServiceProviderID,Quantity, Charges, CreatedBy, Status) VALUES ('" + ServiceName + "', '" + ServiceCategoryID + "', '" + ServiceProviderID + "','" + Quantity + "', '" + Charges + "', '" + LoginUser + "', 1)";
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
    public bool UpdateServiceDetails(string ServiceID, string ServiceName, string ServiceCategoryID, string ServiceProviderID, string Quantity, string Charges)
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
                        theCommand.CommandText = "Update IPD_ServiceDetails set Quantity='" + Quantity + "', ServiceName='" + ServiceName + "', ServiceCategoryID='" + ServiceCategoryID + "', ServiceProviderID='" + ServiceProviderID + "', Charges='" + Charges + "' where ServiceID = '" + ServiceID + "'";
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
    public bool DeleteServiceDetails(int ServiceID)
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
                        theCommand.CommandText = "Update IPD_ServiceDetails set  status=2 WHERE ServiceID = " + ServiceID + "";
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