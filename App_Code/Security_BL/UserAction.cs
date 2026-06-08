using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for UserAction2121132
/// </summary>
public class UserAction
{
	public UserAction(string con)
	{
		conString=con;
	}


      private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable ActionTable;
 
    public DataTable GetAllAction()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM GN_UserAction where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        ActionTable = new DataTable();
        theAdapter.Fill(ActionTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return ActionTable;
    }
   
    public bool InsertUserAction(string CreateDate, string ActionName, string LoginUser)
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
            theCommand.CommandText = "INSERT INTO GN_UserAction(ActionName, CreatedBy,CreateDate, Status) VALUES ('" + ActionName + "', '" + LoginUser + "','" + CreateDate + "',1)";
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
    public bool UpdateUserAction(string ActionID, string ActionName)
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
            theCommand.CommandText = "Update GN_UserAction set ActionName='" + ActionName + "'  where ActionID = '" + ActionID + "'";
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
    public bool DeleteUserAction(int ActionID)
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
            theCommand.CommandText = "Update GN_UserAction set status=2 WHERE ActionID = " + ActionID + "";
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