using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DoctorPaymentDashBoard
/// </summary>
public class DoctorPaymentDashBoard
{

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;

    public DoctorPaymentDashBoard(string con)
	{
        conString = con;
	}


    public DataTable GetDoctorType()
    {
        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorType";
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

    public DataTable GetPayeeName(string compcode,string type, string doctype)
    {

        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "D")
        {
            theCommand.CommandText = "select doc_id Id,doc_name Name, 'Consultant Doctor' Type from GN_DoctorMaster where DocTypeId like '" + doctype + "%' and compcode='"+compcode+"'";
        }
        else
        {
            theCommand.CommandText = "select QuackId Id,QuackName Name,case type  when 'A' then 'Asha' when 'R' then 'Car Rented' when 'P' then 'Car Private' when 'B' then 'Ambulance' when 'Q' then 'Rural Doctor' else 'Others' end Type from GN_QuackMaster where type like '" + doctype + "%' and compcode='" + compcode + "'";
        }
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

    public DataTable GetQuackdetl(string compcode, string yearcode, string qid, string frmdate, string todate)
    {
        if (frmdate != "null")
        {
            frmdate = "'" + frmdate + "'";
        }
        if (todate != "null")
        {
            todate = "'" + todate + "'";
        }
        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select QuackId,QuackName,Address1,phNo1,case type  when 'A' then 'Asha' when 'R' then 'Car Rented' when 'P' then 'Car Private' when 'B' then 'Ambulance' when 'Q' then 'Rural Doctor' else 'Others' end Type from GN_QuackMaster where QuackId='" + qid + "'";
        theCommand.CommandText = "exec sp_DoctorDashboard '" + compcode + "','" + yearcode + "','" + qid + "'," + frmdate + "," + todate + "";
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

    public DataTable GetDocdetl(string compcode, string yearcode, string did, string frmdate, string todate)
    {
        if (frmdate != "null")
        {
            frmdate = "'" + frmdate + "'";
        }
        if (todate != "null")
        {
            todate = "'" + todate + "'";
        }
        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "Select pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) AdmissionDate,DiagnosisName from GN_PatientReg pr,GN_Diagnosis d where pr.Diagnosis=d.DiagnosisId and pr.";
        theCommand.CommandText = "exec sp_DoctorDashboard '" + compcode + "','" + yearcode + "','" + did + "'," + frmdate + "," + todate + "";
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

    public DataTable GetDoctorPayment(string compcode,string yearcode,String docId, String docType, String date1=null, String date2=null)
    {
        DataTable docTable;
        if (docId == "")
        {
            docId = "null";
        }
        if (date1 != "null")
        {
            date1 = "'" + date1 + "'";
        }
        if (date2 != "null")
        {
            date2 = "'" + date2 + "'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "exec sp_IPD_DoctorPayment " + date1 + "," + date2 + ",'" + docType + "','" + docId + "','"+compcode+"','"+yearcode+"'";
        theCommand.CommandText = "exec Dsp_IPD_DoctorPaymentAnalysiswise " + date1 + "," + date2 + ",'" + docType + "','" + docId + "','" + compcode + "','" + yearcode + "',NULL";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        docTable = new DataTable();
        theAdapter.Fill(docTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return docTable;
    }
    public DataTable GetDoctorPayslip(string compcode, string yearcode, String docId, String ReceiptNo)
    {
        DataTable docTable;
        if (docId == "")
        {
            docId = "null";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_DoctorPayslip '" + compcode + "','" + yearcode + "','" + docId + "','" + ReceiptNo + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        docTable = new DataTable();
        theAdapter.Fill(docTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return docTable;
    }
}