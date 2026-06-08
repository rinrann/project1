using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for SubModuleDetails123456
/// </summary>
public class SubModuleDetails
{
	public SubModuleDetails(string con)
	{
        conString = con;
	}



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable SubModuleTable;

    public DataTable  GetAllSubModule(string compcode,string Module)
    {
        if (Module == "" || Module == "0")
            Module = "null";
        else
            Module = "'" + Module + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "EXEC sp_Security_SubModule_MenuDetails  1," + Module + ",null,'"+ compcode +"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable SubModuleTable = new DataTable();
        theAdapter.Fill(SubModuleTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();


        return SubModuleTable;
    }
    public int GetSubModuleID()
    {

        //Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        //Command.
        theAdapter = new SqlDataAdapter("SELECT max(SubModuleID) as SubModuleID FROM GN_SubModuleDetails Where Status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int SubModuleID = 0;
        if (ds.Tables[0].Rows[0]["SubModuleID"] == DBNull.Value)
        {
            SubModuleID = 1;
        }
        else
        {
            SubModuleID = Convert.ToInt32(ds.Tables[0].Rows[0]["SubModuleID"]) + 1;
        }
        return SubModuleID;

    }
    public DataTable DropdownID(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ModuleID,ModuleName from  GN_ModuleDetails where compcode='" + compcode + "' and status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        SubModuleTable = new DataTable();
        theAdapter.Fill(SubModuleTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return SubModuleTable;
    }
    public bool InsertSubModuleDtails(string CompCode, string SubModuleName, string ModuleID, string LoginUser)
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
            theCommand.CommandText = "INSERT INTO GN_SubModuleDetails(CompCode,SubModuleName, ModuleID, CreatedBy, Status) VALUES ('" + CompCode + "','" + SubModuleName + "', '" + ModuleID + "', '" + LoginUser + "', 1)";
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
    public bool UpdateSubModuleDetails(string SubModuleID, string SubModuleName, string ModuleID, string CompCode)
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
            theCommand.CommandText = "Update GN_SubModuleDetails set  SubModuleName='" + SubModuleName + "', ModuleID='" + ModuleID + "' where SubModuleID = '" + SubModuleID + "' and CompCode='" + CompCode + "'";
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
    public bool DeleteSubModuleDetails(string SubModuleID,string compcode)
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
            theCommand.CommandText = "Update GN_SubModuleDetails set  status=0 WHERE SubModuleID = '" + SubModuleID + "' and compcode='"+ compcode +"'";
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