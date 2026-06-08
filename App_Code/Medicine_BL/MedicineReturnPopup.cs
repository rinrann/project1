using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for MedicineReturnPopup
/// </summary>
public class MedicineReturnPopup
{
	public MedicineReturnPopup(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;
    public DataTable DropdownID4(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_SuppilierMaster where Status=1 and compcode='"+ compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable PurchaseMedicineTable = new DataTable();
        theAdapter.Fill(PurchaseMedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose(); 

        return PurchaseMedicineTable;
    }

    public DataTable GridPopup(string compcode, string yearcode, string invoiceid, string scode, string fromdate, string total, string todate)
    {
        if (invoiceid == "")
            invoiceid = "null";
        if (scode == "")
            scode = "null";
        if (total == "")
            total = "null";
        if (fromdate == "" || fromdate == "null")
        {
            fromdate = "null";
        }
        else
        {
            fromdate = "'" + fromdate + "'";
        }

        if (todate == "" || todate == "null")
        {
            todate = "null";
        }
        else
        {
            todate = "'" + todate + "'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_MD_MedicineReturn '" + compcode + "','" + yearcode + "'," + invoiceid + "," + scode + "," + fromdate + "," + total + "," + todate + "";
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