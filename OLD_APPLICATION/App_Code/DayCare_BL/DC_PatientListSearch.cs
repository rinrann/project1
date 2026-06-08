using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for DC_PatientListSearch123
/// </summary>
public class DC_PatientListSearch
{
	public DC_PatientListSearch(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter; 

    //public DataTable GetPatientfortotal(string fromdate,string todate)
    //{
    //    DataTable custTable;
    //    if (fromdate == "")
    //        fromdate = "null";
    //    else
    //        fromdate = "'" + fromdate + "'";

    //    if (todate == "")
    //        todate = "null";
    //    else
    //        todate = "'" + todate + "'";
    //    // Connection.
    //    theConnection = new SqlConnection();
    //    theConnection.ConnectionString = conString;

    //    // Command.
    //    theCommand = new SqlCommand();
    //    theCommand.Connection = theConnection;
    //  theCommand.CommandText = "EXEC sp_DC_GetTotalPatient " + fromdate + "," + todate + "";

    //  //  theCommand.CommandText = "select COUNT(*) Total from dbo.GN_PatientReg where PatientType='D'";
    //    theCommand.CommandType = CommandType.Text;

    //    // Adapter.
    //    theAdapter = new SqlDataAdapter();
    //    theAdapter.SelectCommand = theCommand;

    //    // Datatable.
    //    custTable = new DataTable();
    //    theAdapter.Fill(custTable); // Fill data into data table.

    //    // Clean up.
    //    theConnection.Dispose();
    //    theCommand.Dispose();
    //    theAdapter.Dispose();

    //    return custTable;
    //}


    public DataTable GetPatientDtls(string reg, string name, string address, string ph, string from, string to,string compcode)
    {
        DataTable custTable;
        if (reg == "")
            reg = "null";
        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";
        if (address == "")
            address = "null";
        else
            address = "'" + address + "'";
        if (ph == "")
            ph = "null";
        else
            ph = "'" + ph + "'";


        if (from == "" || from=="null")
            from = "null";
        else
            from = "'" + from + "'";

        if (to == "" || to == "null")
            to = "null";
        else
            to = "'" + to + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "exec sp_DC_PatientDtls  " + reg + "," + name + "," + ph + "," + address + "," + from + "," + to + "";
        theCommand.CommandText = "exec sp_Dialysis  " + reg + "," + name + "," + ph + "," + address + "," + from + "," + to + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataTable GetPatientfordtls(string from,string to,string compcode)
    {
        if (from == "" || from == "null")
            from = "null";
        else
            from = "'" + from + "'";

        if (to == "" || to == "null")
            to = "null";
        else
            to = "'" + to + "'";
        DataTable custTable; 

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "sp_DC_PatientDtls null,null,null,null," + from + "," + to + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet GetPatientfortotal(string fromdate, string todate,string compcode)
    {
        if (fromdate == "")
            fromdate = "null";
        else
            fromdate = "'" + fromdate + "'";

        if (todate == "")
            todate = "null";
        else
            todate = "'" + todate + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = " sp_DC_GetTotalPatient " + fromdate + "," + todate + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataSet Bill1 = new DataSet();
        theAdapter.Fill(Bill1); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return Bill1;
    }

    //public DataTable GetTotalDia()
    //{
    //    DataTable custTable;

    //    // Connection.
    //    theConnection = new SqlConnection();
    //    theConnection.ConnectionString = conString;

    //    // Command.
    //    theCommand = new SqlCommand();
    //    theCommand.Connection = theConnection;
    //    theCommand.CommandText = "select COUNT(*) totaldia from dbo.DC_PatientDialysisMap";
    //    theCommand.CommandType = CommandType.Text;

    //    // Adapter.
    //    theAdapter = new SqlDataAdapter();
    //    theAdapter.SelectCommand = theCommand;

    //    // Datatable.
    //    custTable = new DataTable();
    //    theAdapter.Fill(custTable); // Fill data into data table.

    //    // Clean up.
    //    theConnection.Dispose();
    //    theCommand.Dispose();
    //    theAdapter.Dispose();

    //    return custTable;
    //}     
}