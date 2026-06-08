using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DoctorAvailablity
/// </summary>
public class DoctorAvailablity
{
	public DoctorAvailablity(string con)
	{
        conString = con;
	}
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    public DataTable GridHoliday(string compcode,string DocId)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  h.*,CONVERT(varchar,FromDate,103) DayoffDay1,CONVERT(varchar,ToDate,103) DayoffDay2,d.DocTypeid,d.doc_name from OPD_DoctorsHolidayPlan h,GN_DoctorMaster d where d.compcode=h.compcode and d.doc_id=h.DoctorId and d.compcode='" + compcode + "' and d.doc_id='"+DocId+"'";
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
    public bool InsUpdHoliday(string compcode,string yearcode,int Mode, string RowId,string DoctorId, string DoctorTypeId,  string FromDate, string ToDate, string Remarks,string user)
    {
        int effectedRows = 0;
        try
        {
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            // Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            if (Mode == 1)
                theCommand.CommandText = "INSERT INTO OPD_DoctorsHolidayPlan(compcode,yearcode,DoctorTypeId,DoctorId,FromDate,ToDate,Remarks,user01,logdt01) VALUES('" + compcode + "','"+yearcode+"','" + DoctorTypeId + "','" + DoctorId + "','" + FromDate + "','" + ToDate + "','" + Remarks + "','"+user+"',getdate())";

            if (Mode == 2)
                theCommand.CommandText = "update  OPD_DoctorsHolidayPlan  set  FromDate='" + FromDate + "',DoctorTypeId='" + DoctorTypeId + "',DoctorId='" + DoctorId + "',ToDate='" + ToDate + "',Remarks='" + Remarks + "',user02='"+user+"',logdt02=getdate() where  RowId='" + RowId + "' and compcode='"+compcode+"'";

            if (Mode == 3)
                theCommand.CommandText = "delete OPD_DoctorsHolidayPlan where RowId='" + RowId + "'";

            theCommand.CommandType = CommandType.Text;
            effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.

        }
        catch
        {
        }
        if (effectedRows > 0)
            return true;
        else
            return false;

    }

    public DataTable GetDiscpline(string compcode)
    {
        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorType  where compcode='" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        typeTable = new DataTable();
        theAdapter.Fill(typeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return typeTable;
    }

    public DataTable GetDoctors(string dis, string compcode)
    {
        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select doc_id Id,doc_name Name from GN_DoctorMaster where DocTypeId like '" + dis + "%' and compcode='" + compcode + "'";
        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        typeTable = new DataTable();
        theAdapter.Fill(typeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return typeTable;
    }

    public DataTable GetDoctorAvailabilityDtls(string compcode, string doctype, string docid, string frmdate, string todate)
    {
        DataTable typeTable;
        if (frmdate != "")
        {
            frmdate = "'" + frmdate + "'";
        }
        if (todate != "")
        {
            todate = "'" + todate + "'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_OPD_DoctorAvailabilityDetails '" + doctype + "','" + docid + "'," + frmdate + "," + todate + ",'" + compcode + "'";
        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        typeTable = new DataTable();
        theAdapter.Fill(typeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return typeTable;
    }
}