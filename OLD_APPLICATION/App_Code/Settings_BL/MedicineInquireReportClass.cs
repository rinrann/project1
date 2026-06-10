using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for MedicineInquireReportClass
/// </summary>
public class MedicineInquireReportClass
{
    public MedicineInquireReportClass(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


    public DataTable MedicineGroup()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from IPD_MedicineGroup dt where dt.status=1";
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

    public DataTable MedicineSubGroup(string GrId)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from IPD_MedicineSubGroup dt where dt.GroupID='" + GrId + "' and dt.Status=1";
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

    public DataTable MedicineName(string GrId,string SubGr)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from IPD_MedicineMaster dt where dt.MedicineGroupID='" + GrId + "' and dt.SubGroupid='" + SubGr + "' and dt.Status=1";
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


    public DataSet MedInquireReport(int mode,string FromDate, string ToDate,string MedGr,string SubGr,string MedName,string compcode,string yearcode)
    {




        if (FromDate == "" || FromDate == "null")
            FromDate = "null";
        else
            FromDate = "'" + FromDate + "'";

        if (ToDate == "" || ToDate == "null")
            ToDate = "null";
        else
            ToDate = "'" + ToDate + "'";


        if (MedGr == "0")
            MedGr = "null";
        else
            MedGr = "'" + MedGr + "'";

        if (SubGr == "0")
            SubGr = "null";
        else
            SubGr = "'" + SubGr + "'";

        if (MedName == "0")
            MedName = "null";
        else
            MedName = "'" + MedName + "'";


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = " sp_IPD_MedicineInquire_Report  " + mode + "," + FromDate + "," + ToDate + "," + MedGr + "," + SubGr + "," + MedName + ",'"+compcode+"','"+yearcode+"'";
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


}