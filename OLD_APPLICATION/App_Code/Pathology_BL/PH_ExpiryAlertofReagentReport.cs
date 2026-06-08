using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_ExpiryAlertofReagentRepor1233
/// </summary>
public class PH_ExpiryAlertofReagentReport
{
	public PH_ExpiryAlertofReagentReport(string con)
	{
        conString = con;
	}


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable Gettestdtls(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select E.*,convert(varchar,E.ExpiryDate,103) as ExDate,M.RName ReagentName from PH_ReagentEntry E,PH_ReagentMaster M where M.compcode=E.compcode and M.RCode=E.ReagentId and E.compcode='" + compcode + "'";
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