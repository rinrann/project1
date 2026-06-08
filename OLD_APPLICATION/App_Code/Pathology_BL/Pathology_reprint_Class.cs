using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Pathology_reprint_Class
/// </summary>
public class Pathology_reprint_Class
{
    public Pathology_reprint_Class(string con)
	{
		conString=con ;
	}



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataSet hospitalDataSet;
    private DataTable assoidtable;

    public DataSet GridFillDetails(string compcode,string yearcode,string name,string co,string address,string phno)
    {
        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";

        if (co == "")
            co = "null";
        else
            co = "'" + co + "'";

        if (address == "")
            address = "null";
        else
            address = "'" + address + "'";

        if (phno == "")
            phno = "null";
        else
            phno = "'" + phno + "'";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec PH_LabSlipReprint '"+compcode+"','"+yearcode+"'," + name + "," + co + "," + address + "," + phno + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalDataSet = new DataSet();
        theAdapter.Fill(hospitalDataSet); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();


        return hospitalDataSet;
    }
 
}