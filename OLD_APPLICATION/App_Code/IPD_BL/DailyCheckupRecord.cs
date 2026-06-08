using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DailyCheckupRecord12
/// </summary>
public class DailyCheckupRecord
{
    public DailyCheckupRecord(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;


    public DataTable GetAllPatientRecord(string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select um.UnitName,dr.RecordName,pdr.RowID,pdr.Value,pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText,CONVERT(varchar,pdr.Date,103) cdate from  GN_PatientReg pr,dbo.IPD_BedMaster bm,dbo.IPD_BedAllocation ba,dbo.IPD_DailyRecordMaster dr,GN_UnitMaster um,dbo.IPD_PatientDailyRecord pdr where dr.UnitId=um.UnitId and dr.RecordID=pdr.RRecordID and pdr.PatientReg=pr.PatientReg and bm.BedNo=ba.BedNo and ba.PatientReg=pr.PatientReg and bm.Allotted=1 and pr.PatientReg='" + reg + "'  and ba.ToDate is null and  pdr.Date>=pr.AdmissionDate";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return BedTable;
    }

    public DataTable GetAllPatientRecord1(string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        if (conString != "")
        {
            theConnection.ConnectionString = conString;
            theConnection.Open();
        }

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        string sql = "select pdr.RowID,pdr.RRecordID,um.UnitName,pdm.RecordName,pdr.Value from dbo.IPD_PatientDailyRecord pdr,dbo.IPD_DailyRecordMaster pdm,GN_UnitMaster um,GN_PatientReg pr where  pr.PatientReg=pdr.PatientReg and pdm.UnitId=um.UnitId and  pdr.RRecordID=pdm.RecordID and pdr.PatientReg='" + reg + "' and pdr.Date>=pr.AdmissionDate";
        SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
            BedTable = dt;
        else
        {
            string sql1 = "select null RowID,pdm.RecordID RRecordID,um.UnitName,pdm.RecordName,null Value from IPD_DailyRecordMaster  pdm,GN_UnitMaster um where pdm.UnitId=um.UnitId and pdm.status=1";
            SqlDataAdapter da1 = new SqlDataAdapter(sql1, theConnection);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            BedTable = dt1;
        }
        return BedTable;
    }


    public DataTable Getonlypat(string compcode,string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr where pr.compcode=bm.compcode and ba.compcode=bm.compcode and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and bm.Allotted =1 and pr.compcode='"+compcode+"' and pr.PatientReg='" + reg + "'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return BedTable;
    }

    public DataTable DropdownDailyRecord()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from IPD_DailyRecordMaster where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }




    public bool InsertCheckup(string PatientReg, string Date, string RRecordID, string Value)
    {
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
            theCommand.Connection = theConnection;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "INSERT INTO IPD_PatientDailyRecord(PatientReg,Date,RRecordID, Value) VALUES ('" + PatientReg + "','" + Date + "', '" + RRecordID + "', '" + Value + "')";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
        }
    }
    public bool UpdateCheckup(string id, string Date, string RRecordID, string Value)
    {
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
            theCommand.Connection = theConnection;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "Update IPD_PatientDailyRecord set Date='" + Date + "',RRecordID='" + RRecordID + "',Value='" + Value + "'  where RowID = '" + id + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute update query.
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {

            theConnection.Dispose();
            theCommand.Dispose();
        }
    }
}