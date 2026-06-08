using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_ReportTemplateMaster123
/// </summary>
public class PH_ReportTemplateMaster
{
	public PH_ReportTemplateMaster(string con)
	{
		conString=con ;
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
        theCommand.CommandText = "select * from PH_ReportTemplate rt,PH_DepartmentMaster d where rt.compcode=d.compcode and rt.DepertmentID=d.DeptCode and rt.Status=1 and rt.compcode='"+compcode+"'";
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

    public DataTable DropdownDepartment(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_DepartmentMaster where status=1 and compcode='"+compcode+"' /*DeptCode not in ('D0001')*/";
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

    public bool InsertTemplate(string TemplateName, string Template, string DepertmentID,string cocode)
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
            theCommand.CommandText = "INSERT INTO PH_ReportTemplate(compcode,TemplateName,Template,DepertmentID,Status) VALUES('"+cocode+"','" + TemplateName + "','" + Template + "','" + DepertmentID + "',1)";
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

    public bool UpdateTemplate(string id, string TemplateName, string Template, string DepertmentID,string cocode)
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
            theCommand.CommandText = "update PH_ReportTemplate set TemplateName='" + TemplateName + "', Template='" + Template + "', DepertmentID='" + DepertmentID + "' where TemplateID='" + id + "'and compcode='" + cocode + "'";
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
    public bool DeleteTemplate(string id,string cocode)
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
            theCommand.CommandText = "delete PH_ReportTemplate   WHERE TemplateID='" + id + "'and compcode='" + cocode + "'";
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