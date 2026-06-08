using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for AllDoctorVisitClass
/// </summary>
public class AllDoctorVisitClass
{
    public AllDoctorVisitClass(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GridShow(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select pr.PatientReg,pr.patient_name,pr.vill_city,bm.BedNo,CONVERT(varchar,pr.AdmissionDate,103)AdmissionDate,gd.DiagnosisName from GN_PatientReg pr,IPD_BedMaster bm,GN_Diagnosis gd,IPD_BedAllocation iba where pr.bedallocation=bm.BedNo and pr.Diagnosis=gd.DiagnosisId and pr.PatientReg=iba.PatientReg and iba.ToDate is null ";
        //theCommand.CommandText = "select pr.PatientReg,pr.patient_name,pr.vill_city,bm.BedNoText,CONVERT(varchar,pr.AdmissionDate,103)AdmissionDate,gd.DiagnosisName,gm.doc_id,gt.DocTypeId from GN_PatientReg pr,IPD_BedMaster bm,GN_Diagnosis gd,IPD_BedAllocation iba,GN_DoctorMaster gm,GN_DoctorType gt where iba.compcode=bm.compcode and bm.compcode=pr.compcode and gd.compcode=iba.compcode and gt.compcode=iba.compcode and gm.compcode=iba.compcode and pr.bedallocation=bm.BedNo and  pr.Diagnosis=gd.DiagnosisId and pr.PatientReg=iba.PatientReg and pr.underDoctor=gm.doc_id  and gm.DocTypeId=gt.DocTypeId and iba.compcode='"+compcode+"' and iba.ToDate is null ";
        // change by Sankar on 16/04/2016

        theCommand.CommandText = " select * from (select pr.PatientReg,pr.patient_name,pr.vill_city,bm.BedNoText, CONVERT(varchar,pr.AdmissionDate,103)AdmissionDate,gm.doc_id,gt.DocTypeId , pr.Diagnosis  from GN_PatientReg pr,IPD_BedMaster bm,IPD_BedAllocation iba,GN_DoctorMaster gm,GN_DoctorType gt  where iba.compcode=bm.compcode and bm.compcode=pr.compcode and gt.compcode=iba.compcode and gm.compcode=iba.compcode and pr.bedallocation=bm.BedNo  and pr.PatientReg=iba.PatientReg and pr.underDoctor=gm.doc_id  and gm.DocTypeId=gt.DocTypeId and iba.compcode='" + compcode + "'  and iba.ToDate is null and    pr.PatientType='I') tab1 left join GN_Diagnosis tab2   on tab1.Diagnosis=tab2.DiagnosisId";
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

    public DataTable DoctorType(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorType dt where dt.status=1 and compcode='"+compcode+"'";
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


    public DataTable DoctorMaster(string type,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorMaster dm where dm.DocTypeId='" + type + "' and compcode='"+compcode+"'";
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


    public DataTable DoctorSearch(string DocId,string doctype,string compcode)
    {
        if (doctype == "RMO")
        {
            DocId = "null";
        }
        else
        {
            if (DocId == "0")
                DocId = "null";
            else
                DocId = "'" + DocId + "'";
        }


        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = " exec sp_IPD_AllDoctorVisit " + DocId + ",'" + compcode + "'";
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


    public DataTable DateTimeShow(string PId)
    {

        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "  select convert(varchar,MAX(v.Date),103)+'     '+ max(v.Time) DateTimeShow from IPD_PatientDoctorVisit v where  v.PatientReg='" + PId + "'";
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

    public bool Insert_IPDPatientDoctorVisit(string PatientReg, string Docid, string Date, string Time, string DocTypeID, string Remarks, string BillEffectFlag,string compcode,string yearcode,string user)
    {
        string LedgerId = "";
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
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        //theCommand.CommandText = "INSERT INTO IPD_PatientDoctorVisit(PatientReg,Docid,Date,Time,DocTypeID,Remarks,BillEffectFlag,LedgerId) VALUES ('" + PatientReg + "','" + Docid + "','" + Date + "','" + Time + "','" + DocTypeID + "','" + Remarks + "', '" + BillEffectFlag + "','" + LedgerId + "')";
                        theCommand.CommandText = "INSERT INTO IPD_PatientDoctorVisit(compcode,yearcode,PatientReg,Docid,Date,Time,DocTypeID,Remarks,BillEffectFlag,LedgerId,user01,logdt01) VALUES ('"+compcode+"','"+yearcode+"','" + PatientReg + "','" + Docid + "','" + Date + "','" + Time + "','" + DocTypeID + "','" + Remarks + "', '" + BillEffectFlag + "','" + LedgerId + "','"+user+"',GETDATE())";
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
            theConnection.Close();
            theCommand.Dispose();
        }
    }


}