using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for OperationNote12
/// </summary>
public class OperationNote
{
    public OperationNote(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;


    public DataTable Getonltreq(string reg,string compcode,string yearcode)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select otr.OperationReqID from IPD_OTRequisition otr where otr.compcode='"+compcode+"' and otr.yearcode='"+yearcode+"' and otr.PatientRegId='" + reg + "'";

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
    public DataTable GridFillSecond(string reg,string compcode,string yearcode)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select req.OperationReqID,req.PatientRegId, ot.OperationTypeName,od.OperationName from dbo.IPD_OperationDetails od,dbo.IPD_OperationType ot,dbo.IPD_OTRequisition req where od.compcode=req.compcode and ot.compcode=req.compcode and req.OperationTypeID=ot.OperationTypeID and req.OperationID=od.OperationID and ot.OperationTypeID=od.OperationTypeID and ot.status=1 and od.status=1 and req.compcode='"+compcode+"' and req.yearcode='"+yearcode+"' and req.PatientRegId ='" + reg + "'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public DataTable GetAllOperationNote(string reg, string req,string compcode,string yearcode)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CONVERT(varchar, pr.ProbableDischargeDate,103) ProbableDischargeDate, od.OperationName,opn.AnesthesiaType,opn.PatientRegId, opn.AdditionalDoctor1,opn.AdditionalDoctor2,opn.AdditionalDoctor3,opn.AnesthetistName1," +
"opn.AnesthetistName2,opn.Assistant1,opn.Assistant2,opn.Assistant3,opn.EndTime,CONVERT(varchar,opn.OperationDate,103) opdate, " +
"opn.OperationNoteId,opn.StartTime,opn.SurgeonID,opn.Remarks, CONVERT(varchar,pr.AdmissionDate,103) adate,pr.patient_name, " +
"bm.BedNoText  from  " +
"IPD_OTRequisition otr,IPD_OperationDetails od,IPD_OperationType ot,dbo.IPD_OTOperationNote opn,GN_PatientReg pr,IPD_BedAllocation ba, " +
"IPD_BedMaster bm where otr.compcode=pr.compcode and ot.compcode=pr.compcode and od.compcode=pr.compcode and ba.compcode=bm.compcode and bm.compcode=pr.compcode and opn.OTReqID=otr.OperationReqID and otr.PatientRegId=pr.PatientReg and otr.OperationTypeID=ot.OperationTypeID " +
 "and otr.OperationID=od.OperationID and opn.PatientRegId=pr.PatientReg and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and ba.todate is null and  " +
   "pr.PatientReg='" + reg + "' and otr.OperationReqID='" + req + "' and otr.compcode='"+compcode+"' and otr.yearcode='"+yearcode+"' and ba.ToDate is null";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }
    public DataTable GetAllOperationreq(string reg, string req,string compcode,string yearcode)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select CONVERT(varchar, pr.ProbableDischargeDate,103) ProbableDischargeDate,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText,od.OperationName,orr.SurgeonID,orr.AddDocID1,orr.AddDocID2,orr.AddDocID3,orr.Anesthetist1,orr.Anesthetist2,orr.OTRoomNo,orr.OperationTypeID,orr.OperationID, pr.patient_name,pr.PhNo1, orr.*,dbo.fn_splitOnlyNames(dm.doc_name) doc_name,isnull(orr.Implant,'0') Implant1,CONVERT(varchar,orr.OperationDate,103) otdate from  dbo.IPD_OperationDetails od, dbo.IPD_OTRequisition orr,GN_PatientReg pr,GN_DoctorMaster dm,IPD_BedMaster bm,IPD_BedAllocation ba  where ba.compcode=bm.compcode and orr.compcode=od.compcode and pr.compcode=orr.compcode and dm.compcode=orr.compcode and orr.Status!='Cancel' and od.OperationID=orr.OperationID and   pr.PatientReg =orr.PatientRegId AND dm.doc_id=orr.SurgeonID and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and ba.todate is null and orr.PatientRegId like '" + reg + "%' and orr.OperationReqID='" + req + "' and orr.compcode='"+compcode+"' and orr.yearcode='"+yearcode+"' and pr.CheckStatus=1";
        
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public DataTable Getonlypat(string reg, string req,string compcode,string yearcode)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select  CONVERT(varchar, pr.ProbableDischargeDate,103) ProbableDischargeDate,  otr.StartTime,otr.SurgeonID,otr.Anesthetist1,otr.EndTime,convert(varchar,otr.OperationDate,103) otdate,otr.OperationReqID,od.OperationName,pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr,IPD_OTRequisition otr,IPD_OperationDetails od,IPD_OperationType ot where ba.compcode=bm.compcode and bm.compcode=pr.compcode and pr.compcode=otr.compcode and od.compcode=otr.compcode and ot.compcode=otr.compcode and otr.PatientRegId= pr.PatientReg and otr.OperationID=od.OperationID and ot.OperationTypeID=otr.OperationTypeID and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and ba.todate is null and bm.Allotted =1 and  pr.PatientReg='" + reg + "' and otr.OperationReqID='" + req + "' and otr.compcode='"+compcode+"' and otr.yearcode='"+yearcode+"'  and ba.ToDate is null and ba.ToTime is null";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }



   

    public bool InsertAnesthesiaType_Medicine_Consumable(string PatientRegId, string ConsumableId, string PrescriptionId,string date,string compcode,string yearcode,string user)
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
                        theCommand.CommandText = "select LedgerID from AC_Ledger al where al.compcode='"+compcode+"' and al.LedgerFK='" + PatientRegId + "' and al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();


                        theCommand.CommandText = "DELETE OT_AnesthesiaConsumable  WHERE compcode='"+compcode+"' and yearcode='"+yearcode+"' and LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "   DELETE OT_AnesthesiaMedicine  WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "' and LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "EXEC  sp_OT_Anesthesia_Cons_MedicineAdd '" + date + "','" + ConsumableId + "','" + PatientRegId + "','" + PrescriptionId + "','" + LedgerId + "','"+compcode+"','"+yearcode+"','"+user+"'";
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
            theConnection.Dispose();
            theCommand.Dispose();
        }
    }

    public bool InsertOperation(string AnesthesiaType,string req, string PatientRegId, string ProbableDischargeDate, string SurgeonID, string AdditionalDoctor1, string AdditionalDoctor2, string AdditionalDoctor3, string AnesthetistName1, string AnesthetistName2, string Assistant1, string Assistant2, string Assistant3, string OperationDate, string StartTime, string EndTime, string Remarks,string compcode,string yearcode,string user)
    {
        if (ProbableDischargeDate == "")
            ProbableDischargeDate = "null";
        else
            ProbableDischargeDate = "'" + ProbableDischargeDate + "'";

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
                string LedgerId = "";
                try
                {
                    // transactional code...

                    using (theCommand = theConnection.CreateCommand())
                    {

                        theCommand.CommandText = "select LedgerID from AC_Ledger al where al.compcode='"+compcode+"' and al.LedgerFK='" + PatientRegId + "' and al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        theCommand.CommandText = "update GN_PatientReg set ProbableDischargeDate=" + ProbableDischargeDate + ",user02='"+user+"',logdt02=GETDATE()  where compcode='"+compcode+"' and PatientReg='" + PatientRegId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "update GN_PatientReg_History set ProbableDischargeDate=" + ProbableDischargeDate + ",user02='" + user + "',logdt02=GETDATE()  where compcode='" + compcode + "' and LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();


                        theCommand.CommandText = "INSERT INTO IPD_OTOperationNote(compcode,yearcode,AnesthesiaType,LedgerId,OTReqID,PatientRegId,SurgeonID,AdditionalDoctor1,AdditionalDoctor2,AdditionalDoctor3,AnesthetistName1,AnesthetistName2,Assistant1,Assistant2,Assistant3,OperationDate,StartTime,EndTime,Remarks,Status,user01,logdt01) VALUES ('"+compcode+"','"+yearcode+"','" + AnesthesiaType + "','" + LedgerId + "','" + req + "','" + PatientRegId + "','" + SurgeonID + "', '" + AdditionalDoctor1 + "', '" + AdditionalDoctor2 + "','" + AdditionalDoctor3 + "', '" + AnesthetistName1 + "','" + AnesthetistName2 + "','" + Assistant1 + "','" + Assistant2 + "','" + Assistant3 + "','" + OperationDate + "','" + StartTime + "','" + EndTime + "','" + Remarks + "',1,'"+user+"',GETDATE())";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "UPDATE IPD_OTRequisition SET Status='Done',SurgeonID='" + SurgeonID + "',AddDocID1='" + AdditionalDoctor1 + "',AddDocID2='" + AdditionalDoctor2 + "',AddDocID3='" + AdditionalDoctor3 + "',Anesthetist1='" + AnesthetistName1 + "',Anesthetist2='" + AnesthetistName2 + "',OperationDate='" + OperationDate + "',StartTime='" + StartTime + "',EndTime='" + EndTime + "',user02='"+user+"',logdt02=GETDATE()   where compcode='"+compcode+"' and yearcode='"+yearcode+"' and OperationReqID='" + req + "'";
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
            theConnection.Dispose();
            theCommand.Dispose();
        }
    }


    public bool UpdateOperation(string AnesthesiaType,string ID,string RequisitionId, string PatientRegId, string ProbableDischargeDate, string SurgeonID, string AdditionalDoctor1, string AdditionalDoctor2, string AdditionalDoctor3, string AnesthetistName1, string AnesthetistName2, string Assistant1, string Assistant2, string Assistant3, string OperationDate, string StartTime, string EndTime, string Remarks,string compcode,string yearcode,string user)
    {
        string LedgerId = "";
        if (ProbableDischargeDate == "")
            ProbableDischargeDate = "null";
        else
            ProbableDischargeDate = "'" + ProbableDischargeDate + "'";

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
                        theCommand.CommandText = "select LedgerID from AC_Ledger al where al.compcode='"+compcode+"' and al.LedgerFK='" + PatientRegId + "' and al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();


                        theCommand.CommandText = "update GN_PatientReg set ProbableDischargeDate=" + ProbableDischargeDate + "  where compcode='" + compcode + "' and PatientReg='" + PatientRegId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "update GN_PatientReg_History set ProbableDischargeDate=" + ProbableDischargeDate + "  where compcode='" + compcode + "' and LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();


                        theCommand.Connection = theConnection;
                        theCommand.CommandText = "Update IPD_OTOperationNote set AnesthesiaType='" + AnesthesiaType + "', SurgeonID='" + SurgeonID + "',AdditionalDoctor1='" + AdditionalDoctor1 + "',AdditionalDoctor2='" + AdditionalDoctor2 + "', AdditionalDoctor3='" + AdditionalDoctor3 + "', AnesthetistName1='" + AnesthetistName1 + "',AnesthetistName2='" + AnesthetistName2 + "',Assistant1='" + Assistant1 + "',Assistant2='" + Assistant2 + "',Assistant3='" + Assistant3 + "',Remarks='" + Remarks + "',OperationDate='" + OperationDate + "',StartTime='" + StartTime + "',EndTime='" + EndTime + "',user02='" + user + "',logdt02=GETDATE()  where compcode='" + compcode + "' and OperationNoteId = '" + ID + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute update query. 

                        theCommand.CommandText = "UPDATE IPD_OTRequisition SET SurgeonID='" + SurgeonID + "',AddDocID1='" + AdditionalDoctor1 + "',AddDocID2='" + AdditionalDoctor2 + "',AddDocID3='" + AdditionalDoctor3 + "',Anesthetist1='" + AnesthetistName1 + "',Anesthetist2='" + AnesthetistName2 + "',OperationDate='" + OperationDate + "',StartTime='" + StartTime + "',EndTime='" + EndTime + "',user02='" + user + "',logdt02=GETDATE()   where compcode='"+compcode+"' and yearcode='"+yearcode+"' and OperationReqID='" + RequisitionId + "'";
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
            theConnection.Dispose();
            theCommand.Dispose();
        }
    }

    public DataSet Get_Onlyreg_Sur_Doc_Anes_Emp(string req,string compcode,string yearcode)
    {
        if (req == "")
            req = "null";
        else
            req = "'" + req + "'";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "sp_IPD_Get_Onlyreg_Sur_Doc_Anes_Emp " + req + ",'"+compcode+"','"+yearcode+"'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataSet BedTable = new DataSet();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }
}