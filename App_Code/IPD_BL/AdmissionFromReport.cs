using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AdmissionFromReport
/// </summary>
public class AdmissionFromReport
{
	public AdmissionFromReport(string con)
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
        theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) ADate  from  dbo.GN_PatientReg pr,IPD_BedAllocation b,dbo.GN_DoctorMaster d,IPD_BedMaster bm,GN_Diagnosis dia where ba.compcode=bm.compcode and bm.compcode=pr.compcode and bm.BedNo=b.BedNo and pr.underDoctor=d.doc_id and pr.PatientReg=b.PatientReg and pr.compcode='"+compcode+"' and pr.PatientReg='" + regno + "' and pr.Diagnosis=dia.DiagnosisId";
        //theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) ADate  from  dbo.GN_PatientReg pr,IPD_BedAllocation ba,dbo.GN_DoctorMaster d,IPD_BedMaster bm,GN_Diagnosis dia where ba.compcode=bm.compcode and bm.compcode=pr.compcode and bm.BedNo=ba.BedNo and pr.underDoctor=d.doc_id and pr.PatientReg=ba.PatientReg and pr.compcode='GFC' and pr.PatientReg='GFC1307160003' and pr.Diagnosis=dia.DiagnosisId";
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