using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ReferPatientList
/// </summary>
public class ReferPatientList
{
    public ReferPatientList(string con)
	{
        conString = con;
	}
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;

    public DataTable getDoc(string doctype,string compcode)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (doctype == "D")
        {
            theCommand.CommandText = "select * from GN_DoctorMaster dt where dt.status=1 and compcode='"+compcode+"'";
        }
        else
        {
            if (doctype == "Q")
            {
                theCommand.CommandText = "select * from GN_QuackMaster dt where dt.status=1 and compcode='"+compcode+"'";
            }
        }
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

    public DataTable GetPatientDtls(string refId,string frmdt,string todt,string compcode)
    {
        DataTable custTable;

        // Connection.

        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "Select pt.PatientReg,pt.patient_name,pt.guardian_name,CONVERT(varchar,pt.AdmissionDate,103) ADate,pt.underDoctor,'Dr. '+dc.doc_name doctor,(Select CONVERT(varchar,DischargeDate,103)from GN_DischargeDetail where PatientReg=pt.PatientReg and compcode=pt.compcode) discdt from GN_PatientReg pt,GN_DoctorMaster dc where dc.compcode=pt.compcode and dc.doc_id=pt.underDoctor and pt.compcode='" + compcode + "' and pt.ReferID='" + refId + "' and DATEADD(D, 0, DATEDIFF(D, 0,pt.AdmissionDate))>=DATEADD(D, 0, DATEDIFF(D, 0,'" + frmdt + "')) and DATEADD(D, 0, DATEDIFF(D, 0,pt.AdmissionDate))<=DATEADD(D, 0, DATEDIFF(D, 0,'" + todt + "'))order by pt.AdmissionDate DESC";
        
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

    public DataTable GetPatientComissionDtls(string doctype, string docId,string compcode,string yearcode)
    {
        DataTable custTable;
        // Connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (doctype == "D")
        {
            theCommand.CommandText = "Select pt.PatientReg,pt.patient_name,pt.guardian_name,CONVERT(varchar,pt.AdmissionDate,103) ADate,tn.Debit from GN_PatientReg pt,AC_Transaction tn,GN_DoctorMaster dc,AC_Ledger ld where pt.compcode=tn.compcode and tn.compcode=dc.compcode and  pt.LedgerId=tn.paymentfor and dc.compcode=ld.compcode and dc.doc_id=pt.ReferID and ld.LedgerFK=dc.doc_id and tn.compcode='" + compcode + "' and tn.yearcode='" + yearcode + "' and tn.LadgerId=(select LedgerID from AC_Ledger where LedgerFK='" + docId + "' and compcode='" + compcode + "') and pt.ReferID='" + docId + "' and Reason='Refer patient' order by AdmissionDate desc;";
        }
        else
        {
            theCommand.CommandText = "Select pt.PatientReg,pt.patient_name,pt.guardian_name,CONVERT(varchar,pt.AdmissionDate,103) ADate,tn.Debit from GN_PatientReg pt,AC_Transaction tn,GN_QuackMaster dc,AC_Ledger ld where pt.compcode=tn.compcode and tn.compcode=dc.compcode and pt.PatientReg=tn.RegNo and dc.compcode=ld.compcode and dc.QuackId=pt.ReferID and ld.LedgerFK=dc.QuackId and tn.compcode='" + compcode + "' and tn.yearcode='" + yearcode + "' and tn.LadgerId=(select LedgerID from AC_Ledger where LedgerFK='" + docId + "' and compcode='" + compcode + "') and pt.ReferID='" + docId + "' and Reason='Refer patient' order by AdmissionDate desc;";
        }
        theCommand.CommandType = CommandType.Text;

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