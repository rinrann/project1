using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ModuleDetails123
/// </summary>
public class ModuleDetails
{
	public ModuleDetails(string con)
	{
        conString = con;
	}


      private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable ModuleTable;
  
    public DataTable GetAllModule(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM GN_ModuleDetails  where compcode='" + compcode + "'  order by ModuleID ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        ModuleTable = new DataTable();
        theAdapter.Fill(ModuleTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return ModuleTable;
    }
    
    public bool InsertModuleDetails(string CompCode, string ModuleName, string LoginUser, string CreateDate)
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
            theCommand.CommandText = "INSERT INTO GN_ModuleDetails(Compcode, ModuleName, CreatedBy,CreateDate, Status) VALUES ('" + CompCode + "','" + ModuleName + "', '" + LoginUser + "','" + CreateDate + "',1)";
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
    public bool UpdateModuleDetails(string ModuleID, string ModuleName, string CompCode)
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
            theCommand.CommandText = "Update GN_ModuleDetails set ModuleName='" + ModuleName + "'  where ModuleID = '" + ModuleID + "' and compcode='" + CompCode + "'";
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
    public bool DeleteModuleDetails(int ModuleID)
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
            theCommand.CommandText = "Update GN_ModuleDetails set Status=2 WHERE ModuleID = " + ModuleID + "";
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