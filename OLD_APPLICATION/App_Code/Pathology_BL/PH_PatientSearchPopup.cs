using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_PatientSearchPopup1325
/// </summary>
public class PH_PatientSearchPopup
{
	public PH_PatientSearchPopup(string con)
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
        if (name == "")
            name = "null";
        if (ph == "")
            ph = "null";
        if (address == "")
            address = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_PatientDetails  " + reg + "," + name + "," + ph + "," + address + "";
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