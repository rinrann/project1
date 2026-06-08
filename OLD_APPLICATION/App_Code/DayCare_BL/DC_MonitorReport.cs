using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_MonitorReport123
/// </summary>
public class DC_MonitorReport
{
	public DC_MonitorReport(string con)
	{
        conString = con;
	}


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    
    public DataTable GetPatientDetails(string RegNo,string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pd.*, pm.*,gen.SexName from  dbo.DC_PatientMonitor pm,GN_PatientReg pd,GN_Gender gen where pm.compcode=pd.compcode and gen.compcode=pd.compcode and pm.PatientReg=pd.PatientReg and pd.sex=gen.id  and pm.PatientReg='" + RegNo + "' and pm.compcode='"+compcode+"' and pm.yearcode='"+yearcode+"'";
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


    public DataTable GetPatientDetails_Duplicate(string RegNo,string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        hospitalTable = new DataTable();
        string sql;
        sql = "select CONVERT(varchar,pa.AppDate,103) AppDate1,bm.Charges,s.ShiftName ,pd.PatientReg,pd.patient_name,pd.vill_city,pd.PhNo1,pa.AdvAmount,pd.DialyserNo,pd.Startdate from IPD_BedMaster bm, dbo.DC_PatientAppointment pa,DC_ShiftDtls s,dbo.GN_PatientReg pd where bm.compcode=pa.compcode and s.compcode=pa.compcode and pd.compcode=pa.compcode and pa.ShiftId=s.ShiftID and pd.PatientReg=pa.PatientReg and bm.BedNo=pd.bedallocation  and pd.PatientReg='" + RegNo + "' and pa.compcode='"+compcode+"' and pa.yearcode='"+yearcode+"' and pa.AppDate=(select MAX(pa.AppDate) from DC_PatientAppointment pa where pa.PatientReg='" + RegNo + "' and pa.compcode='"+compcode+"' and pa.yearcode='"+yearcode+"')";
        SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
            hospitalTable = dt;
        else
        {
            hospitalTable = new DataTable();
            string sql1;
            sql1 = "select CONVERT(varchar,pa.AppDate,103) AppDate1,bm.Charges,s.ShiftName ,pd.PatientReg,pd.patient_name,pd.vill_city,pd.PhNo1,pa.AdvAmount,pd.DialyserNo,pd.Startdate from IPD_BedMaster bm, dbo.DC_PatientAppointment pa,DC_ShiftDtls s,dbo.GN_PatientReg pd where bm.compcode=pa.compcode and s.compcode=pa.compcode and pd.compcode=pa.compcode and pa.ShiftId=s.ShiftID and pd.PatientReg=pa.PatientReg and bm.BedNo=pd.bedallocation  and pd.PatientReg='" + RegNo + "' and pa.compcode='" + compcode + "' and pa.yearcode='" + yearcode + "' and pa.AppDate=(select top (1) pa.AppDate from DC_PatientAppointment pa where pa.PatientReg='" + RegNo + "' and pa.compcode='" + compcode + "' and pa.yearcode='" + yearcode + "')";
            SqlDataAdapter da1 = new SqlDataAdapter(sql1, theConnection);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            hospitalTable = dt1;
        }

        // Clean up.
        theConnection.Dispose();
        da.Dispose();

        return hospitalTable;
    }
}