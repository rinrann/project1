using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_ReagentOrder123
/// </summary>
public class PH_ReagentOrder
{
	public PH_ReagentOrder(string con)
	{
        conString = con;
	}



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;

    public DataTable Dropdown_Reagent()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_ReagentMaster where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return hospitalTable;
    }


    public DataTable GridChemical()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ro.*,convert(varchar,ro.Date,103) date1 from PH_ReagentOrder ro";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return hospitalTable;
    }

    public string GeneratePurchaseID()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GeneratepurchaseID";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        assoidtable = new DataTable();
        theAdapter.Fill(assoidtable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return assoidtable.Rows[0][0].ToString();
    }

    public bool InsertAppointment(string purchaid, string reagentname, string id, string date, string purchaseqty, string bonusqty, string price, string totalprice, string created)
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
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "INSERT INTO PH_ReagentOrder(PurchaseId,ID,ReagentName,Date,PurchaseQty,BonusQty,PerPrice,TotalPrice,CreatedBy) VALUES('" + purchaid + "','" + id + "','" + reagentname + "','" + date + "','" + purchaseqty + "','" + bonusqty + "','" + price + "','" + totalprice + "','" + created + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
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

    public bool UpdateAppointment(string pid, string rename, string id, string date, string purchaseqty, string bqty, string price, string total)
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

            theCommand.CommandText = "update PH_ReagentOrder set ID='" + id + "', ReagentName='" + rename + "', Date='" + date + "', PurchaseQty='" + purchaseqty + "',BonusQty='" + bqty + "',PerPrice='" + price + "',TotalPrice='" + total + "' where PurchaseId = '" + pid + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.
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

    public bool DeleteAppointment(string id)
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
            theCommand.CommandText = "delete PH_ReagentOrder WHERE PurchaseId='" + id + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.
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