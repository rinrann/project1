using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_TestPopup12345
/// </summary>
public class PH_TestPopup
{
	public PH_TestPopup(string con)
	{
        conString = con;
	}





    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


    public DataTable GridFill(string name, string code, string dept)
    {
        string tname, tcode, tdept;
        if (name == "")
            tname = "null";
        else
            tname = name.ToString();

        if (code == "")
            tcode = "null";
        else
            tcode = code.ToString();

        if (dept == "")
            tdept = "null";
        else
            tdept = dept.ToString();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_TestDetailsPopup " + tcode + "," + tname + "," + tdept + "";
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


    public DataTable DropDownFill()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_DepartmentMaster where Status=1 and DeptCode not in('D0002')";
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


    public DataTable gettestrec(string id,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_RequisitionTestMap where compcode='"+compcode+"' and testCode='"+id+"' and Status=1";
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


    public DataTable getSugTestRec(string id, string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from OPD_Inv_Sugg_Med where compcode='" + compcode + "' and testId='" + id + "' and Status=1";
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


    public DataTable getGrptest(string id, string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_ProfileMapping where compcode='" + compcode + "' and testId='" + id + "'";
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

    public bool Deletetest(string id, string cocode,string type)
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
            if (type == "P")
            {
                theCommand.CommandText = "delete from PH_TestMaster  WHERE TestId='" + id + "' and compcode='" + cocode + "'";
            }
            else
            {
                theCommand.CommandText = "delete from PH_TestXRAYMaster  WHERE TestId='" + id + "' and compcode='" + cocode + "'";
            }
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