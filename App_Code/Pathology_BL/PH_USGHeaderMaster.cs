using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_USGHeaderMaster12345
/// </summary>
public class PH_USGHeaderMaster
{
	public PH_USGHeaderMaster(string con)
	{
        conString = con;
	}
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;



    public DataTable GridFill(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select un.id,un.HeaderContent, gr.GroupName,subgr.SubGrName,un.HeaderName from dbo.PH_USGHeaderMaster un,dbo.PH_USGGRMaster gr,dbo.PH_USGSubGrMaster subgr where un.compcode=gr.compcode and un.compcode=subgr.compcode and un.GroupCode=gr.GroupCode and UN.SubGrCode=subgr.SubGrID and un.Status=1 and un.compcode='"+compcode+"'";
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
    public DataTable DropdownGroup(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_USGGRMaster where status=1 and compcode='"+compcode+"'";
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

    public DataTable DropdownSubGroup(string sub,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_USGSubGrMaster u where u.Status=1 and u.GroupID='" + sub + "' and u.compcode='"+compcode+"'";
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
    public bool InsertGroup(string GroupCode, string subGroupcede, string name, string headercontent, string created,string cocode)
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
            theCommand.CommandText = "INSERT INTO PH_USGHeaderMaster(compcode,GroupCode,SubGrCode,HeaderName,HeaderContent,Status,CreatedBy) VALUES('"+cocode+"','" + GroupCode + "','" + subGroupcede + "','" + name + "','" + headercontent + "',1,'" + created + "')";
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
    public bool UpdateGroup(string id, string GroupCode, string subGroupcede, string name, string content,string cocode)
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

            // Command.  GroupCode,SubGrCode,HeaderName,HeaderContent
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "update PH_USGHeaderMaster set GroupCode='" + GroupCode + "', SubGrCode='" + subGroupcede + "',HeaderName='" + name + "',HeaderContent='" + content + "'  where ID='" + id + "'and compcode='" + cocode + "'";
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
    public bool DeleteGroup(string GroupCode,string cocode)
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
            theCommand.CommandText = "delete PH_USGHeaderMaster where ID='" + GroupCode + "'and compcode='" + cocode + "'";
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