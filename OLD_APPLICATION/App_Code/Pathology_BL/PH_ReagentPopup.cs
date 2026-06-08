using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_ReagentPopup123
/// </summary>
public class PH_ReagentPopup
{
	public PH_ReagentPopup(string con)
	{
        conString = con;
	}


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;
    public DataTable supplier(string compcode, string yearcode, string rname, string cname,string frmdt,string todt)
    {
        /*if (rname == "")
            rname = "null";
        if (cname == "")
            cname = "null";*/
        // Connection.

        if (frmdt == "" || frmdt == "null")
        {
            frmdt = "null";
        }
        else
        {
            frmdt="'"+frmdt+"'";
        }

        if (todt == "" || todt == "null")
        {
            todt = "null";
        }
        else
        {
            todt = "'" + todt + "'";
        }
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_ReagentSearch '"+compcode+"','"+yearcode+"','" + rname + "','" + cname + "',"+frmdt+","+todt+"";
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