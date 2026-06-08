using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DischargeCertificate12
/// </summary>
public class DischargeCertificate
{
	public DischargeCertificate(string con)
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
        theCommand.CommandText = "sp_GetVisitedDoctor " + regno + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new  DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataSet GetVisitedDoctor(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "sp_GetVisitedDoctor " + regno + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataSet ds = new DataSet();
        theAdapter.Fill(ds); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return ds;
    }



    public DataTable GetDoctorDetailsDtls(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select dbo.fn_splitOnlyNames(d.doc_name) doc_name,d.Qualification,d.DrRegNo from dbo.GN_PatientReg pr,dbo.GN_DoctorMaster d  where d.compcode=pr.compcode and d.doc_id=pr.underDoctor and pr.compcode='"+compcode+"' and pr.PatientReg='" + regno + "'";
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

    public DataTable GetOperation(string regno,string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select onn.Remarks,onn.OTReqID,onn.AnesthesiaType,od.OperationName,dbo.fn_splitOnlyNames(dm.doc_name) doc_name,onn.OperationNoteId,onn.PatientRegId,onn.SurgeonId,onn.AdditionalDoctor1,onn.AdditionalDoctor2 , onn.AdditionalDoctor3,onn.AnesthetistName1,onn.AnesthetistName2, onn.Assistant1,onn.Assistant2,onn.Assistant3, onn.StartTime,onn.EndTime,convert(varchar,otr.OperationDate,103) otdate  from dbo.IPD_OTOperationNote onn, dbo.IPD_OTRequisition otr, dbo.IPD_OperationDetails od,GN_DoctorMaster dm,GN_PatientReg pr  where onn.compcode=pr.compcode and otr.compcode=pr.compcode and od.compcode=pr.compcode and dm.compcode=pr.compcode and onn.PatientRegId=pr.PatientReg and onn.SurgeonID=dm.doc_id and   onn.PatientRegId=otr.PatientRegId and otr.OperationID=od.OperationID and   otr.OperationDate>=pr.AdmissionDate and otr.OperationReqID=onn.OTReqID and otr.compcode='"+compcode+"' and otr.yearcode='"+yearcode+"'  and   onn.PatientRegId='" + regno + "'";  
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

    public DataTable DischargeDtks(string regno,string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select d.DischargeTime,d.Remarks,convert(varchar,d.DischargeDate,103) disdate from GN_DischargeDetail d,GN_PatientReg pr where d.compcode=pr.compcode and d.PatientReg=pr.PatientReg and d.DischargeDate>=pr.AdmissionDate and d.compcode='"+compcode+"' and d.yearcode='"+yearcode+"' and d.PatientReg='" + regno + "'";
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


    public DataTable BiopsyTissue(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select map.TypeOfTissue,map.ExamRequired from dbo.IPD_OTBiopsyNote bn ,dbo.IPD_OTBiopsyNoteMapping map where map.compcode=bn.compcode and bn.BiopsyNoteId=map.BiopsyNoteId and bn.PatientRegId='" + regno + "' and bn.Status=1 and bn.compcode='"+compcode+"'";
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