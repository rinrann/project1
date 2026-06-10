using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for RegistrationPopup12
/// </summary>
public class RegistrationPopup12
{
	public RegistrationPopup12(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;

    public DataTable GridPopup(string reg, string name, string ph, string address,string compcode)
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

        theCommand.CommandText = "exec sp_IPD_AdmitPatient  " + reg + "," + name + "," + ph + "," + address + ",'"+compcode+"'";
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



    public DataTable GridPopup1(string reg, string name, string ph, string address,string compcode,string yearcode)
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
        theCommand.CommandTimeout = 0;
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_IPD_OTPatient  " + reg + "," + name + "," + ph + "," + address + ",'"+compcode+"','"+yearcode+"'";
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



    public DataTable GridPopup2(string reg, string name, string ph, string address,string compcode,string yearcode)
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

        theCommand.CommandText = "exec [sp_IPD_PatientDeliveryPopup]  " + reg + "," + name + "," + ph + "," + address + ",'"+compcode+"','"+yearcode+"'";
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