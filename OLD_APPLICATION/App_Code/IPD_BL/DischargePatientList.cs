using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for DischargePatientList
/// </summary>
public class DischargePatientList
{
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlCommand theCommand1;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DischargePatientList(string con)
	{
        conString = con;
	}

    public DataTable DiscPatientList(string patient, string frmdt, string todt,string compcode,string yearcode)
    {
        DataTable custTable;
        // Connection.

        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "Select pt.PatientReg,pt.patient_name,pt.guardian_name,CONVERT(varchar,pt.AdmissionDate,103) ADate,pt.PhNo1,pt.underDoctor,'Dr. '+dc.doc_name doctor,(Select CONVERT(varchar,DischargeDate,103)from GN_DischargeDetail where PatientReg=pt.PatientReg) discdt from GN_PatientReg pt,GN_DoctorMaster dc where dc.doc_id=pt.underDoctor and pt.ReferID='" + refId + "' and DATEADD(D, 0, DATEDIFF(D, 0,pt.AdmissionDate))>=DATEADD(D, 0, DATEDIFF(D, 0,'" + frmdt + "')) and DATEADD(D, 0, DATEDIFF(D, 0,pt.AdmissionDate))<=DATEADD(D, 0, DATEDIFF(D, 0,'" + todt + "'))";
        if (frmdt != "" && todt != "")
        {
            theCommand.CommandText = "Select dd.PatientReg,CONVERT(varchar,dd.DischargeDate,103) DiscDate,pt.patient_name,pt.guardian_name,pt.PhNo1,CONVERT(varchar,pt.AdmissionDate,103) ADate,pt.vill_city address,pt.underDoctor,'Dr. '+dc.doc_name DocName,pt.ReferName,pt.Diagnosis,dg.DiagnosisName from GN_DischargeDetail dd,GN_PatientReg pt,GN_DoctorMaster dc,GN_Diagnosis dg where pt.compcode=dd.compcode and dc.compcode=pt.compcode and dg.compcode=pt.compcode and pt.Patientreg=dd.PatientReg and dc.doc_id=pt.underDoctor and dg.DiagnosisId=pt.Diagnosis and dd.compcode='"+compcode+"' and dd.yearcode='"+yearcode+"' and dd.DischargeDate>='" + frmdt + "' and dd.DischargeDate<='" + todt + "' and pt.patient_name like '%" + patient + "%' order by dd.DischargeDate desc";
        }
        else if (frmdt != "" && todt == "")
        {
            theCommand.CommandText = "Select dd.PatientReg,CONVERT(varchar,dd.DischargeDate,103) DiscDate,pt.patient_name,pt.guardian_name,pt.PhNo1,CONVERT(varchar,pt.AdmissionDate,103) ADate,pt.vill_city address,pt.underDoctor,'Dr. '+dc.doc_name DocName,pt.ReferName,pt.Diagnosis,dg.DiagnosisName from GN_DischargeDetail dd,GN_PatientReg pt,GN_DoctorMaster dc,GN_Diagnosis dg where pt.compcode=dd.compcode and dc.compcode=pt.compcode and dg.compcode=pt.compcode and pt.Patientreg=dd.PatientReg and dc.doc_id=pt.underDoctor and dg.DiagnosisId=pt.Diagnosis and dd.compcode='" + compcode + "' and dd.yearcode='" + yearcode + "' and dd.DischargeDate>='" + frmdt + "' and pt.patient_name like '%" + patient + "%' order by dd.DischargeDate desc";
        }
        else if (frmdt == "" && todt != "")
        {
            theCommand.CommandText = "Select dd.PatientReg,CONVERT(varchar,dd.DischargeDate,103) DiscDate,pt.patient_name,pt.guardian_name,pt.PhNo1,CONVERT(varchar,pt.AdmissionDate,103) ADate,pt.vill_city address,pt.underDoctor,'Dr. '+dc.doc_name DocName,pt.ReferName,pt.Diagnosis,dg.DiagnosisName from GN_DischargeDetail dd,GN_PatientReg pt,GN_DoctorMaster dc,GN_Diagnosis dg where pt.compcode=dd.compcode and dc.compcode=pt.compcode and dg.compcode=pt.compcode and pt.Patientreg=dd.PatientReg and dc.doc_id=pt.underDoctor and dg.DiagnosisId=pt.Diagnosis and dd.compcode='" + compcode + "' and dd.yearcode='" + yearcode + "' and dd.DischargeDate<='" + todt + "' and pt.patient_name like '%" + patient + "%' order by dd.DischargeDate desc";
        }
        else
        {
            theCommand.CommandText = "Select dd.PatientReg,CONVERT(varchar,dd.DischargeDate,103) DiscDate,pt.patient_name,pt.guardian_name,pt.PhNo1,CONVERT(varchar,pt.AdmissionDate,103) ADate,pt.vill_city address,pt.underDoctor,'Dr. '+dc.doc_name DocName,pt.ReferName,pt.Diagnosis,dg.DiagnosisName from GN_DischargeDetail dd,GN_PatientReg pt,GN_DoctorMaster dc,GN_Diagnosis dg where pt.compcode=dd.compcode and dc.compcode=pt.compcode and dg.compcode=pt.compcode and pt.Patientreg=dd.PatientReg and dc.doc_id=pt.underDoctor and dg.DiagnosisId=pt.Diagnosis and dd.compcode='" + compcode + "' and dd.yearcode='" + yearcode + "' and pt.patient_name like '%" + patient + "%' order by dd.DischargeDate desc";
        }

        theCommand.CommandType = CommandType.Text;

        //Adapter
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable);

        // clean up
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return custTable;
    }


    public DataTable getOtdeta(string preg)
    {
        DataTable custTable;
        // Connection.

        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "Select otrec.operationId,otdt.operationname from IPD_OTRequisition otrec,IPD_OperationDetails otdt where otdt.operationId=otrec.operationId and otrec.patientregId='"+preg+"'";
        theCommand.CommandType = CommandType.Text;

        //Adapter
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable);

        // clean up
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return custTable;
    }
}