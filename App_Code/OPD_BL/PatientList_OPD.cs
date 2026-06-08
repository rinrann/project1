using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PatientList12
/// </summary>
public class PatientList_OPD
{
    public PatientList_OPD(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GridPopupPatientdtls(string compcode, string reg, string name, string ph, string address,string status=null)
    {
        if (reg == "")
            reg = "null";
        if (name == "")
            name = "null";
        if (ph == "")
            ph = "null";
        if (address == "")
            address = "null";
        if (status==null)
            status = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_OPD_PatientDetailsPopup  '" + compcode + "'," + reg + "," + name + "," + ph + "," + address + "," + status + "";
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


    public DataTable GridPopupPatientAppdtls(string compcode, string reg, string name, string ph, string address, string status = null)
    {
        if (reg == "")
            reg = "null";
        if (name == "")
            name = "null";
        if (ph == "")
            ph = "null";
        if (address == "")
            address = "null";
        if (status == null)
            status = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec Dsp_OPD_PatientAppointmentPopup  " + compcode + "," + reg + "," + name + "," + ph + "," + address + "," + status + "";
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

    public DataTable GridPopup(string compcode,string doc, string date, string fromtime, string totime)
    {
        string dt1, ft, tt;
        if (doc == "" || doc == "0")
            doc = "null";
        if (totime == "" || totime == "0")
            tt = "null";
        else
            tt = "'" + totime + "'";

        if (fromtime == "" || fromtime == "0")
            ft = "null";
        else
            ft = "'" + fromtime + "'";
        if (date == "" || date == "0")
            dt1 = "null";
        else
            dt1 = "'" + date + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_OPD_PatientListDetails '"+compcode+"'," + doc + "," + dt1 + "," + ft + "," + tt + "";
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

    public DataTable GridPopupFill(string compcode, string date, string RegNo,string PName, string PhoneNo)
    {
       

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_OPD_PatientListDetailsSearch '" + compcode + "','" + date + "','" + RegNo + "','" + PName + "','" + PhoneNo + "'";
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

    public DataTable DropdownDoctor(string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from GN_DoctorMaster where compcode='"+compcode+"' and status=1 order by doc_name";
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

    public DataTable TotalPatient(string date)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select COUNT(*) from dbo.OPD_PatientRegistration pr where pr.AppointMentDate='" + date + "'";
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

    public DataTable CheckedPatient(string date)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select COUNT(*) Checked from dbo.OPD_PatientRegistration pr where pr.AppointMentDate='" + date + "' and pr.Checked=1";
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