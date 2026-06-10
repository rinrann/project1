using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_RegistrationPopup123
/// </summary>
public class DC_RegistrationPopup
{
    public DC_RegistrationPopup(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;

    public DataTable GridPopup(string reg, string name, string ph, string address)
    {
        if (reg == "")
            reg = "null";
        else
            reg = "'" + reg + "'";
        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";

        if (ph == "")
            ph = "null";
        else
            ph = "'" + ph + "'";

        if (address == "")
            address = "null";
        else
            address = "'" + address + "'";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec  [sp_DC_ReegPopup]   " + reg + "," + name + "," + ph + "," + address + "";
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

    public DataTable AnutimePaymentPopup(string reg, string name, string ph, string address,string cocode)
    {
        if (reg == "")
            reg = "null";
        else
            reg = "'" + reg + "'";
        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";

        if (ph == "")
            ph = "null";
        else
            ph = "'" + ph + "'";

        if (address == "")
            address = "null";
        else
            address = "'" + address + "'";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "exec sp_AnyTimePaymentPopup  " + reg + "," + name + "," + ph + "," + address + "";
        theCommand.CommandText = "exec sp_AnyTimePaymentPopup  '" + cocode + "'," + reg + "," + name + "," + ph + "," + address + ",NULL";
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

    public DataTable RegistrationPopup(string compcode,string date, string name, string ph, string address)
    {
        if (date == "")
            date = "null";
        else
            date = "'" + date + "'";
        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";

        if (ph == "")
            ph = "null";
        else
            ph = "'" + ph + "'";

        if (address == "")
            address = "null";
        else
            address = "'" + address + "'";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_DC_RegistrationPopup '"+compcode+"'," + date + "," + name + "," + ph + "," + address + "";
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

    // for registration popup

    public DataTable GridRegistrationpopup(string reg, string name, string ph, string address)
    {
        if (reg == "")
            reg = "null";
        else
            reg = "'" + reg + "'";
        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";

        if (ph == "")
            ph = "null";
        else
            ph = "'" + ph + "'";

        if (address == "")
            address = "null";
        else
            address = "'" + address + "'";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec   sp_DC_PatientRegPopup   " + reg + "," + name + "," + ph + "," + address + "";
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


    // For patient Appointment popup

    public DataTable GridAppointmentPopup(string reg, string name, string ph, string address,string compcode)
    {
        if (reg == "")
            reg = "null";
        else
            reg = "'" + reg + "'";
        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";

        if (ph == "")
            ph = "null";
        else
            ph = "'" + ph + "'";

        if (address == "")
            address = "null";
        else
            address = "'" + address + "'";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_DC_PatientAppoPopup  " + reg + "," + name + "," + ph + "," + address + ",'"+compcode+"'";
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
}