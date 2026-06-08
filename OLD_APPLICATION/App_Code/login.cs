using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for login
/// </summary>
public class login
{
	public string constring;
    public SqlConnection theConnection;
    public SqlCommand theCommand;
    public SqlDataAdapter theAdapter;
    public DataTable hospitalinfo;

    public login(string con)
	{
        constring = con;
	}
    public DataTable CompanyInfo(string compcode, string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = constring;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from parms where compcode='" + compcode + "' and yearcode='" + yearcode + "' ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalinfo = new DataTable();
        theAdapter.Fill(hospitalinfo); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalinfo;
    }
}