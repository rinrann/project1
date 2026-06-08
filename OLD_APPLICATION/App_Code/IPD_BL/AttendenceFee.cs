using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for AttendenceFee1
/// </summary>
public class AttendenceFee
{
    public AttendenceFee(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetAllBiopsy(string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select map.RowId,map.BiopsyNoteId,od.OperationName,map.TypeOfTissue,map.ExamRequired,map.Remarks,pr.patient_name,pr.PatientReg,otr.OperationReqID,CONVERT(varchar,pr.AdmissionDate,103) adate,CONVERT(varchar,map.DateOfCollection,103) coldate,(select bm.BedNoText from IPD_BedAllocation ba,IPD_BedMaster bm where bm.BedNo=ba.BedNo and PatientReg='" + reg + "' and ba.FromDate in (select MAX(bd.FromDate) dff from IPD_BedAllocation bd where PatientReg='" + reg + "') group by bm.BedNoText) BedNoText from  GN_PatientReg pr,IPD_OperationDetails od,IPD_OTBiopsyNote bn,dbo.IPD_OTBiopsyNoteMapping map,IPD_OTRequisition otr where od.OperationID=otr.OperationID and otr.PatientRegId=pr.PatientReg and bn.BiopsyNoteId=map.BiopsyNoteId and bn.OperationReqID=otr.OperationReqID group by map.RowId,od.OperationName,map.TypeOfTissue,map.ExamRequired,map.Remarks,pr.patient_name,pr.PatientReg,otr.OperationReqID,pr.AdmissionDate,map.DateOfCollection,map.BiopsyNoteId";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return BedTable;
    }

    public DataTable FetchFron2ndGrid(string compcode, string yearcode, string reg, string req)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select pr.patient_name,oac.SurgeonCharge,od.OperationName,CONVERT(varchar,oac.IssDate,103) issudate,pr.PatientReg,otr.OperationReqID,CONVERT(varchar,pr.AdmissionDate,103) adate,oac.AnesthetistCharge1,oac.AnesthetistCharge2,oac.Asst1Charge,oac.Asst2Charge,oac.Asst3Charge,oac.Doc1Charge,oac.Doc2Charge,oac.Doc3Charge,oac.Remarks,oac.RowID ,bm.BedNoText from  GN_PatientReg pr,IPD_BedAllocation ba,IPD_BedMaster bm,dbo.OT_AttendenceCharges oac,IPD_OTRequisition otr,IPD_OperationDetails od  where ba.compcode=bm.compcode and pr.compcode=otr.compcode and bm.compcode=otr.compcode and od.compcode=otr.compcode and oac.compcode=otr.compcode and oac.yearcode=otr.yearcode and ba.BedNo=bm.BedNo and ba.PatientReg =pr.PatientReg and pr.PatientReg=oac.PatientRegId and oac.OperationReqID=otr.OperationReqID and otr.OperationID= od.OperationID and oac.OperationReqID=otr.OperationReqID and otr.OperationReqID='" + req + "' and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "' and ba.ToDate is null and ba.ToTime is null";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return BedTable;
    }

    public DataTable Getonltreq(string compcode, string yearcode,string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select otr.OperationReqID from IPD_OTRequisition otr where otr.PatientRegId='" + reg + "' and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return BedTable;
    }

    public DataTable GenerateBiopsycode()
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_IPD_GetBiopsyNoteID";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }


    public DataTable Getonlypat(string compcode, string yearcode, string reg, string req)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (req == "")
            theCommand.CommandText = "select pr.PatientReg,od.OperationName,pr.patient_name,otr.OperationReqID,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr,IPD_OTRequisition otr,IPD_OperationDetails od where ba.compcode=bm.compcode and bm.compcode=otr.compcode and od.compcode=otr.compcode and pr.compcode=otr.compcode and otr.OperationID=od.OperationID and pr.PatientReg=otr.PatientRegId and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and bm.Allotted =1 and  pr.PatientReg='" + reg + "' and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "' and ba.ToDate is null and ba.ToTime is null";
        else
            theCommand.CommandText = "select pr.PatientReg,od.OperationName,pr.patient_name,otr.OperationReqID,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr,IPD_OTRequisition otr,IPD_OperationDetails od where ba.compcode=bm.compcode and bm.compcode=otr.compcode and od.compcode=otr.compcode and pr.compcode=otr.compcode and otr.OperationID=od.OperationID and pr.PatientReg=otr.PatientRegId and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and bm.Allotted =1 and  pr.PatientReg='" + reg + "' and otr.OperationReqID='" + req + "' and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "' and ba.ToTime is null and ba.ToDate is null";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public bool InsertAttendencecharge(string TypeID, string PatientRegId, string OperationReqID, string SurgeonCharge, string Doc1Charge, string Doc2Charge, string Doc3Charge, string AnesthetistCharge1, string AnesthetistCharge2, string Asst1Charge, string Asst2Charge, string Asst3Charge, string IssDate, string Remarks, string compcode, string user, string yearcode)
    {
        int effectedRows = 0;
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
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                string LedgerId = "";
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "select LedgerID from AC_Ledger al where al.compcode='"+compcode+"' and LedgerFK='" + PatientRegId + "' and ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        theCommand.CommandText = "INSERT INTO OT_AttendenceCharges(compcode,YearCode,LedgerId,TypeID,PatientRegId,OperationReqID,SurgeonCharge,Doc1Charge,Doc2Charge,Doc3Charge,AnesthetistCharge1,AnesthetistCharge2,Asst1Charge,Asst2Charge,Asst3Charge,IssDate,Remarks,Status,user01,logdt01) VALUES('"+compcode+"','"+yearcode+"','" + LedgerId + "','" + TypeID + "','" + PatientRegId + "','" + OperationReqID + "','" + SurgeonCharge + "','" + Doc1Charge + "','" + Doc2Charge + "','" + Doc3Charge + "','" + AnesthetistCharge1 + "','" + AnesthetistCharge2 + "','" + Asst1Charge + "','" + Asst2Charge + "','" + Asst3Charge + "','" + IssDate + "','" + Remarks + "',1,'"+user+"',GETDATE())";
                        theCommand.Transaction = tran as SqlTransaction;
                        effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.

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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }

    public bool UpdateAttCharge(string id, string SurgeonCharge, string Doc1Charge, string Doc2Charge, string Doc3Charge, string AnesthetistCharge1, string AnesthetistCharge2, string Asst1Charge, string Asst2Charge, string Asst3Charge, string IssDate, string Remarks, string compcode, string user, string yearcode)
    {
        int effectedRows = 0;
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
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "update OT_AttendenceCharges set SurgeonCharge='" + SurgeonCharge + "',Doc1Charge='" + Doc1Charge + "',Doc2Charge='" + Doc2Charge + "',Doc3Charge='" + Doc3Charge + "',AnesthetistCharge1='" + AnesthetistCharge1 + "',AnesthetistCharge2='" + AnesthetistCharge2 + "',Asst1Charge='" + Asst1Charge + "',Asst2Charge='" + Asst2Charge + "',Asst3Charge='" + Asst3Charge + "',IssDate='" + IssDate + "',Remarks='" + Remarks + "',user02='"+user+"',logdt02=GETDATE() where compcode='"+compcode+"' and yearcode='"+yearcode+"' and RowID='" + id + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.
                        theCommand.Transaction = tran as SqlTransaction;
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }
    public DataTable Getonlyreg(string compcode, string yearcode, string req)
    {
        if (req == "")
            req = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select otr.PatientRegId from IPD_OTRequisition otr where otr.OperationReqID='" + req + "' and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return BedTable;
    }

    public DataTable DropDownDoctor(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from GN_DoctorMaster where Status=1 and compcode='"+compcode+"' order by doc_name";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return BedTable;
    }
    public DataTable DropDownEmployee(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from GN_EmployeeMaster where status=1 and compcode='"+compcode+"'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public DataTable DropDowndoc()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from GN_DoctorMaster where Status=1 order by doc_name";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }


    public DataTable GetFromOTNote(string compcode, string yearcode,string reg, string req)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select otn.* from IPD_OTOperationNote otn where otn.PatientRegId='" + reg + "' and otn.OTReqID='" + req + "' and otn.compcode='"+compcode+"' and otn.yearcode='"+yearcode+"'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public DataTable GridFillSecond(string compcode, string yearcode,string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select req.OperationReqID,req.PatientRegId, ot.OperationTypeName,od.OperationName from dbo.IPD_OperationDetails od,dbo.IPD_OperationType ot,dbo.IPD_OTRequisition req where od.compcode=req.compcode and ot.compcode=req.compcode and req.OperationTypeID=ot.OperationTypeID and req.OperationID=od.OperationID and ot.OperationTypeID=od.OperationTypeID and ot.status=1 and od.status=1 and req.PatientRegId ='" + reg + "' and req.compcode='" + compcode + "' and req.yearcode='" + yearcode + "'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public bool DeleteBiopsyMap(string ID)
    {
        int effectedRows = 0;
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
                        theCommand.CommandText = "delete from IPD_OTBiopsyNoteMapping where BiopsyNoteId = '" + ID + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.

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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }
}