using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_BillSlip12
/// </summary>
public class DC_BillSlip
{
	public DC_BillSlip(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetPatientDetails(string RegNo, string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,pa.AppDate,103) AppDate1,bm.Charges,s.ShiftName ,pd.PatientReg,pd.patient_name,pd.vill_city,pd.PhNo1,pa.AdvAmount,pd.DialyserNo "+
            "from IPD_BedMaster bm, dbo.DC_PatientAppointment pa,DC_ShiftDtls s,dbo.GN_PatientReg pd,dbo.DC_PatientMonitor pm where "+
            "pd.compcode=bm.compcode and pd.compcode=pa.compcode and pd.compcode=s.compcode and pd.compcode=pm.compcode and "+
            "pa.ShiftId=s.ShiftID and pa.status=1 and pm.Status=1 and pm.PatientReg=pd.PatientReg and pd.PatientReg=pa.PatientReg and "+
            "bm.BedNo=pd.bedallocation and pd.PatientReg='" + RegNo + "' and pd.compcode='" + compcode + "'";
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
    public DataTable GetamountinWords(string value)
    {
        if (value == "")
            value = "0";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec [sp_AmountToWords] " + value + "";
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

    public DataTable GetLabAdvance(string RegNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pr.AdAmt from PH_PatientReq pr where pr.RegistrationNo='" + RegNo + "' and pr.Status=1";
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

    public DataTable GetPatientDetails_Duplicate(string compcode,string RegNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        hospitalTable = new DataTable();
        string sql;
        sql = "select CONVERT(varchar,pa.AppDate,103) AppDate1,bm.Charges,s.ShiftName ,pd.PatientReg,pd.patient_name,pd.vill_city,pd.PhNo1,pa.AdvAmount,pd.DialyserNo,pd.Startdate "+
        "from IPD_BedMaster bm, dbo.DC_PatientAppointment pa,DC_ShiftDtls s,dbo.GN_PatientReg pd where "+
        "bm.compcode=pa.compcode and bm.compcode=s.compcode and bm.compcode=pd.compcode and pa.ShiftId=s.ShiftID and "+
        "pd.PatientReg=pa.PatientReg and bm.BedNo=pd.bedallocation and bm.compcode='"+compcode+"' and pd.PatientReg='" + RegNo + "' and pa.AppDate=(select MAX(pa.AppDate) from DC_PatientAppointment pa where pa.PatientReg='" + RegNo + "')";
        SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
            hospitalTable = dt;
        else
        {
            hospitalTable = new DataTable();
            string sql1;
            sql1 = "select CONVERT(varchar,pa.AppDate,103) AppDate1,bm.Charges,s.ShiftName ,pd.PatientReg,pd.patient_name,pd.vill_city,pd.PhNo1,pa.AdvAmount,pd.DialyserNo,pd.Startdate from IPD_BedMaster bm, dbo.DC_PatientAppointment pa,DC_ShiftDtls s,dbo.GN_PatientReg pd where  pa.ShiftId=s.ShiftID and pd.PatientReg=pa.PatientReg and bm.BedNo=pd.bedallocation  and pd.PatientReg='" + RegNo + "' and pa.AppDate=(select top (1) pa.AppDate from DC_PatientAppointment pa where pa.PatientReg='" + RegNo + "')";
            SqlDataAdapter da1 = new SqlDataAdapter(sql1, theConnection);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            hospitalTable = dt1;
        }


        // Clean up.
        theConnection.Dispose(); 
        return hospitalTable;
    }

    public DataTable GetFeesDetails(string compcode,string yearcode,string RegNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from DC_ChargeDetails cd where cd.status=1 and cd.PatientReg='" + RegNo + "' and cd.compcode='"+compcode+"' and yearcode='"+yearcode+"'";
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

    public DataTable GetDuplicateFees(string RegNo,string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from DC_ChargeDetails cd where cd.PatientReg='" + RegNo + "' and cd.compcode='"+compcode+"' and cd.yearcode='"+yearcode+"' and cd.Status=0 and  cd.Date=(select MAX(cd.Date) from DC_ChargeDetails cd  where cd.PatientReg='" + RegNo + "'  and cd.Status=0 and cd.compcode='"+compcode+"' and cd.yearcode='"+yearcode+"')";
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