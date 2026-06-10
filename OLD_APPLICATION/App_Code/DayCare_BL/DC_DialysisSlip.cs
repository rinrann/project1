using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_DialysisSlip123
/// </summary>
public class DC_DialysisSlip
{
	public DC_DialysisSlip(string con)
	{
        conString = con;
	}


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetNoofDia(string CustID,string compcode)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pr.DialyserNo, COUNT(*) DiaNo from DC_PatientDialysisMap map,GN_PatientReg pr where pr.compcode=map.compcode and pr.DialyserNo=map.DialysisNo	 and map.PatientReg='" + CustID + "' and map.compcode='"+compcode+"' group by pr.DialyserNo";
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


    public DataTable GetPatientDetails(string RegNo,string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select CONVERT(varchar,pa.AppDate,103) ADate,pa.AdvAmount Amount,* from dbo.DC_ShiftDtls s,dbo.GN_PatientReg pd,dbo.DC_PatientAppointment pa where s.compcode=pd.compcode and pa.compcode=pd.compcode and s.ShiftID=pa.ShiftId and pa.status=1 and pd.PatientReg=pa.PatientReg and pa.compcode='"+compcode+"' and pa.yearcode='"+yearcode+"' and pd.PatientReg='" + RegNo + "'";
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

    public DataTable GetPatientDetailsDuplicate(string RegNo,string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select CONVERT(varchar,pa.AppDate,103) ADate,pa.AdvAmount Amount,* from dbo.DC_ShiftDtls s,dbo.GN_PatientReg pd,dbo.DC_PatientAppointment pa where s.compcode=pd.compcode and pa.compcode=pd.compcode and s.ShiftID=pa.ShiftId and pd.PatientReg=pa.PatientReg and pa.compcode='" + compcode + "' and pa.yearcode='" + yearcode + "' and pd.PatientReg='" + RegNo + "' and pa.AppDate=(select MAX(p.AppDate) from DC_PatientAppointment p where  p.PatientReg='" + RegNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "')";
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