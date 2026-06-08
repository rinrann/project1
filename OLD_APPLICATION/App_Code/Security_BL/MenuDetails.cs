using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for MenuDetails1235
/// </summary>
public class MenuDetails
{
	public MenuDetails(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable MenuTable;


    public DataTable GetAllMenu(string compcode,string Module,string SubModule)
    {
        DataTable custTable;
        if (Module == "" || Module == "0")
            Module = "null";
        else
            Module = "'" + Module + "'";

        if (SubModule == "" || SubModule == "0")
            SubModule = "null";
        else
            SubModule = "'" + SubModule + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "EXEC sp_Security_SubModule_MenuDetails  2," + Module + "," + SubModule + ",'"+ compcode +"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return custTable;
    }
     public DataTable DropdownID1()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
        theCommand.CommandText = "select ModuleID,ModuleName from  GN_ModuleDetails where status=1 and compcode='" + Compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MenuTable = new DataTable();
        theAdapter.Fill(MenuTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return MenuTable;
    }
    public DataTable DropdownID2(int moduleID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
        theCommand.CommandText = "select SubModuleID,SubModuleName from  GN_SubModuleDetails where status=1 and compcode='" + Compcode + "' AND ModuleID=" + moduleID;
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MenuTable = new DataTable();
        theAdapter.Fill(MenuTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return MenuTable;
    }
    public DataTable DropdownID2()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
        theCommand.CommandText = "select SubModuleID,SubModuleName from  GN_SubModuleDetails where status=1 and compcode='" + Compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MenuTable = new DataTable();
        theAdapter.Fill(MenuTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return MenuTable;
    }
    public bool InsertMenuDtails(string MenuName, string ModuleID, string SubModuleID, string URL, string LoginUser, string CreateDate)
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
            string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
            theCommand.CommandText = "INSERT INTO GN_MenuDetails(Compcode,MenuName, ModuleID, SubModuleID, URL, CreatedBy,CreateDate, Status) VALUES ('" + Compcode + "','" + MenuName + "', '" + ModuleID + "', '" + SubModuleID + "', '" + URL + "', '" + LoginUser + "','" + CreateDate + "', 1)";
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
    public bool UpdateMenuDetails(string MenuID, string MenuName, string ModuleID, string SubModuleID, string URL)
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
            string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
            theCommand.CommandText = "Update GN_MenuDetails set  MenuName='" + MenuName + "', ModuleID='" + ModuleID + "', SubModuleID='" + SubModuleID + "', URL='" + URL + "' where MenuID = '" + MenuID + "' and compcode='"+ Compcode +"'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute update query.
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
    public bool DeleteMenuDetails(int MenuID)
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
            string Compcode = HttpContext.Current.Session["CoCode"].ToString().Trim();
            theCommand.CommandText = "Update GN_MenuDetails set status=2 WHERE MenuID= " + MenuID + " and compcode='" + Compcode + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute Delete query.
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