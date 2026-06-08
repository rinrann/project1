using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for ConsumableItems12
/// </summary>
public class ConsumableItems
{
    public ConsumableItems(string con)
    {
        conString = con;
    }

    public string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable ConsumableTable;

    public DataTable GetAllConItem(string cocode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT ConItemID,T1.ConItemName,T1.BuyingPrice,T1.BillingPrice,T1.UnitId,T2.UnitName FROM OPD_ConsumableItems T1,GN_UnitMaster T2 WHERE T1.compcode=T2.compcode and T1.UnitId=T2.UnitId AND T1.status=1 and t1.compcode='" + cocode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        ConsumableTable = new DataTable();
        theAdapter.Fill(ConsumableTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return ConsumableTable;
    }
    public int GetConItemID()
    {
        //Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        //Command.
        theAdapter = new SqlDataAdapter("SELECT max(ConItemID) as ConItemID FROM IPD_ConsumableItems Where Status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int ConItemID = 0;
        if (ds.Tables[0].Rows[0]["ConItemID"] == DBNull.Value)
        {
            ConItemID = 1;
        }
        else
        {
            ConItemID = Convert.ToInt32(ds.Tables[0].Rows[0]["ConItemID"]) + 1;
        }
        return ConItemID;
    }
    public DataTable DropdownID(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  GN_UnitMaster where Status=1 and compcode='" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        ConsumableTable = new DataTable();
        theAdapter.Fill(ConsumableTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return ConsumableTable;
    }
    public bool InsertConsumableItems(string ConGrId, string ConItemName, string UnitId, string BuyingPrice, string BillingPrice, string LoginUser,string cocode)
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
                        theCommand.CommandText = "INSERT INTO OPD_ConsumableItems(compcode,ConItemID,ConItemName, UnitId, BuyingPrice,BillingPrice, CreatedBy, Status) VALUES ('" + cocode + "',(select isNull(max(ConItemID),0)+1 from OPD_ConsumableItems where compcode='"+ cocode +"'),'" + ConItemName + "', '" + UnitId + "', '" + BuyingPrice + "','" + BillingPrice + "', '" + LoginUser + "', 1)";
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }

    public bool UpdateConsumableItems(string ConGrId, string ConItemID, string ConItemName, string UnitId, string BuyingPrice, string BillingPrice, string cocode)
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
                        theCommand.CommandText = "Update OPD_ConsumableItems set BuyingPrice='" + BuyingPrice + "', ConItemName='" + ConItemName + "', UnitId='" + UnitId + "', BillingPrice='" + BillingPrice + "' where ConItemID = '" + ConItemID + "'and compcode='"+cocode+"'";
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }

    public bool DeleteConsumableItems(int ConItemID, string cocode)
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
                        theCommand.CommandText = "Update IPD_ConsumableItems set  status=2 WHERE ConItemID = '" + ConItemID + "'and compcode='"+cocode+"'";
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }
}