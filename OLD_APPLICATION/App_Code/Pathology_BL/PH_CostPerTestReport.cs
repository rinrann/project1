using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_CostPerTestReport
/// </summary>
public class PH_CostPerTestReport
{
	public PH_CostPerTestReport(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetDepartmentCode()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select t.DepertmentID,d.DeptName from PH_TestMaster t, PH_DepartmentMaster d where t.DepertmentID=d.DeptCode  group by t.DepertmentID,d.DeptName";
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

    public DataTable GetGroup()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select p.ProfileName,p.Price from PH_ProfileMapping map,PH_ProfileMaster p where p.ProfileCode=map.ProfileCode group by  p.ProfileName,p.Price";
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


    public DataTable GetRadiology()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select name.ID,name.Name,sub.Charges from PH_USGSubGrMaster sub,PH_USGNameMaster name where sub.SubGrID=name.SubGroupCode";
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



    public DataTable Gettestdtls(string dept, string test)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (test == "")
            theCommand.CommandText = "select d.DeptName,t.* from PH_TestMaster t,PH_DepartmentMaster d where d.DeptCode=t.DepertmentID and t.DepertmentID='" + dept + "'";
        else
            theCommand.CommandText = "select d.DeptName,t.* from PH_TestMaster t,PH_DepartmentMaster d where d.DeptCode=t.DepertmentID and t.DepertmentID='" + dept + "' and t.TestName = '" + test + "'";
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
        theCommand.CommandText = "select * from PH_DepartmentMaster where Status=1";
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
}