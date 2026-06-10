using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for InstrumentPurchaseReport
/// </summary>
public class InstrumentPurchaseReport
{
    public InstrumentPurchaseReport(string con)
    {
        conString = con;
    }

    public string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable InstrumentsTable;
    public DataTable GetAllInstrument(string type, string name, string modelno,string compcode)
    {
        if (type == "" || type == "0")
            type = "null";
        if (name == "" || name == "0")
            name = "null";

        if (modelno == "" || modelno == "0")
            modelno = "null";
        else
            modelno = "'" + modelno + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_OT_InstrumentPurchaseReport " + type + "," + name + "," + modelno + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        InstrumentsTable = new DataTable();
        theAdapter.Fill(InstrumentsTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return InstrumentsTable;
    }


    public DataTable GetAllInstrumentAMC(string type, string name, string modelno,string compcode)
    {
        if (type == "" || type == "0" || type == "All")
            type = "null";
        if (name == "" || name == "0" || name == "All")
            name = "null";

        if (modelno == "" || modelno == "0")
            modelno = "null";
        else
            modelno = "'" + modelno + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_OT_InstrumentAMCReport " + type + "," + name + "," + modelno + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        InstrumentsTable = new DataTable();
        theAdapter.Fill(InstrumentsTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return InstrumentsTable;
    }


    public DataTable GetAllInstrumentReject(string type, string name, string modelno,string compcode)
    {
        if (type == "" || type == "0")
            type = "null";
        if (name == "" || name == "0")
            name = "null";

        if (modelno == "" || modelno == "0")
            modelno = "null";
        else
            modelno = "'" + modelno + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_OT_InstrumentReject " + type + "," + name + "," + modelno + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        InstrumentsTable = new DataTable();
        theAdapter.Fill(InstrumentsTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return InstrumentsTable;
    }

    public DataTable GetAllInstrumentRepairReceive(string type, string name, string modelno,string compcode)
    {
        if (type == "" || type == "0" || type == "All")
            type = "null";

        if (name == "" || name == "0" || name == "All")
            name = "null";

        if (modelno == "" || modelno == "0")
            modelno = "null";
        else
            modelno = "'" + modelno + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_OT_InstrumentRepairReceived " + type + "," + name + "," + modelno + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        InstrumentsTable = new DataTable();
        theAdapter.Fill(InstrumentsTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return InstrumentsTable;
    }

    public DataTable GetAllInstrumentRepair(string type, string name, string modelno,string compcode)
    {

        if (type == "" || type == "0" || type == "All")
            type = "null";

        if (name == "" || name == "0" || name == "All")
            name = "null";

        if (modelno == "" || modelno == "0")
            modelno = "null";
        else
            modelno = "'" + modelno + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_OT_InstrumentRepair " + type + "," + name + "," + modelno + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        InstrumentsTable = new DataTable();
        theAdapter.Fill(InstrumentsTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return InstrumentsTable;
    }

    public DataTable DropdownInstrumentTye(string compcode)
    {
        //Connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("select * from OT_InstrumentType where status=1 and compcode='"+compcode+"'", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);
        theConnection.Dispose();
        theAdapter.Dispose();
        return dt;
    }
    public DataTable DropdownInstrumentName(string Type,string compcode)
    {
        //Connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        if (Type == "All" || Type == "0")
            theAdapter = new SqlDataAdapter("select * from OT_InstrumentMaster where Status=1 and compcode='"+compcode+"'", theConnection);
        else
            theAdapter = new SqlDataAdapter("select * from OT_InstrumentMaster where InstrumentType='" + Type + "' and Status=1 and compcode='"+compcode+"'", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);
        theConnection.Dispose();
        theAdapter.Dispose();

        return dt;
    }
}