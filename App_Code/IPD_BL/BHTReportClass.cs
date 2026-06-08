using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BHTReportClass
/// </summary>
public class BHTReportClass
{
    public BHTReportClass(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetPatientDtls(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) ADate  from  dbo.GN_PatientReg pr,IPD_BedAllocation b,dbo.GN_DoctorMaster d,IPD_BedMaster bm where b.compcode=bm.compcode and bm.compcode=pr.compcode and bm.BedNo=b.BedNo and pr.underDoctor=d.doc_id and pr.PatientReg=b.PatientReg and pr.compcode='"+compcode+"' and pr.PatientReg='" + regno + "'";
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


    public DataTable GetBHTDetails(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select cf.RowId,cf.PatientReg,CONVERT(varchar,cf.Date,103) Date,cf.BP,cf.Bleeding,cf.Chest,cf.Doppler,cf.Others,cf.PA,cf.PV,cf.Pulse,cf.SPO2,cf.Sunction,cf.Temp,cf.Time,cf.Urin from dbo.IPD_ClinicalFinding cf where cf.compcode='"+compcode+"' and cf.PatientReg='" + regno + "'";
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








    // *********************************   Total Income &  Expenditure     ************************************************************

    public DataSet GetTotalIncomeExpenditure(string compcode, string yearcode, string From, string To, string RegNo)
    {
        if (From == "")
            From = "null";
        else
            From = "'" + From + "'";

        if (To == "")
            To = "null";
        else
            To = "'" + To + "'";

        if (RegNo == "")
            RegNo = "null";
        else
            RegNo = "'" + RegNo + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "sp_GN_GetTotalIncome '" + compcode + "','" + yearcode + "'," + From + "," + To + "," + RegNo + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataSet hospitalTable = new DataSet();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }
    public DataSet GetDailyExpenditure(string compcode,string yearcode,string From, string To)
    {
        if (From == "")
            From = "null";
        else
            From = "'" + From + "'";

        if (To == "")
            To = "null";
        else
            To = "'" + To + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GN_GetDailyTransaction '"+compcode+"','"+yearcode+"'," + From + "," + To + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataSet DailyTable = new DataSet();
        theAdapter.Fill(DailyTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return DailyTable;
    }
}