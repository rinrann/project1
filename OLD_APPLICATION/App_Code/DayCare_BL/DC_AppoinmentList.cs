using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_AppoinmentList12
/// </summary>
public class DC_AppoinmentList
{
	public DC_AppoinmentList(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


    public DataTable DropdownShift(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  DC_ShiftDtls where status=1 and compcode='"+compcode+"'";
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
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable GridEnable()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select PatientReg from dbo.GN_PatientReg";
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
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable CheckChargeDetails(string compcode, string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CheckStatus from dbo.DC_ChargeDetails where PatientReg='" + regno + "' and Status=1 and compcode='"+compcode+"'";
        //"exec [dbo].[sp_PatientAppoList]  null,null,null,null";

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
        theAdapter.Dispose();

        return hospitalTable;
    }


    public DataTable CheckRegistration(string compcode, string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CheckStatus from dbo.GN_PatientReg where PatientReg='" + regno + "' and compcode='"+compcode+"'";
        //"exec [dbo].[sp_PatientAppoList]  null,null,null,null";

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
        theAdapter.Dispose();

        return hospitalTable;
    }


    public DataTable CheckMonitor(string compcode,string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CheckStatus from dbo.DC_PatientMonitor where Status=1 and PatientReg='" + regno + "' and compcode='"+compcode+"'";
        //"exec [dbo].[sp_PatientAppoList]  null,null,null,null";

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
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable CheckLabDetails(string compcode, string yearcode, string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select Status from dbo.PH_PatientReq where RegistrationNo='" + regno + "' and Status=1 and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
        //"exec [dbo].[sp_PatientAppoList]  null,null,null,null";

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
        theAdapter.Dispose();

        return hospitalTable;
    } 
 
    public DataTable PatientDashBoard(string compcode,string shift, string date, string name, string reg)
    {
        string shiftPara;
        if (reg == "")
            reg = "null";
        else
            reg = "'" + reg + "'";

        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";

        if (shift == "0" || shift=="")
            shiftPara = "null";
        else
            shiftPara = shift.ToString();


        if (date != "" && date != "null")
            date = "'" + date + "'";
        else
            date = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec  sp_DC_PatientDashBoard  " + shiftPara + "," + date + "," + name + "," + reg + ",'"+compcode+"'";

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
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable GridAppoList(string compcode, string yearcode, string shift, string date, string name, string reg)
    {
        string shiftPara;
        if (reg == "")
            reg = "null";
        else
            reg = "'" + reg + "'";

        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";

        if (shift == "0")
            shiftPara = "null";
        else
            shiftPara = shift.ToString();


        if (date != "" && date != "null")
            date = "'" + date + "'";
        else
            date = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec  sp_DC_PatientAppoList  '"+compcode+"','"+yearcode+"'," + shiftPara + "," + date + "," + name + "," + reg + "";

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
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable rescheduleList(string id)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select pa.*,CONVERT(varchar,pa.AppDate,103) AppDate1,s.ShiftName	 from DC_PatientAppointment as pa ,dbo.DC_ShiftDtls s where pa.status=1 and s.ShiftID=pa.ShiftId and Appno='" + id + "'";

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
        theAdapter.Dispose();

        return hospitalTable;
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
            theCommand.CommandText = "update DC_PatientAppointment set status=2 WHERE AppNo='" + id + "'";
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