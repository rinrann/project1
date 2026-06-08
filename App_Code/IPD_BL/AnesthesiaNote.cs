using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AnesthesiaNote
/// </summary>
public class AnesthesiaNote
{
    public AnesthesiaNote(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;

    public DataTable GetAllAnesthesis(string compcode, string yearcode, string reg, string req)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.  
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select bm.BedNoText,od.OperationName,otr.OperationReqID, pr.PatientReg,pr.patient_name, an.AnesthesiaNoteId, an.PreBP,an.PreChest,CONVERT(varchar,an.Date,103) dt,an.PreO2,an.PreOthers,an.PrePulse,an.PreRemarks,an.PreRiskFactor,an.Time,CONVERT(varchar,pr.AdmissionDate,103) adate from IPD_OperationDetails od, dbo.IPD_AnesthesisNote an,GN_PatientReg pr,IPD_BedAllocation ba,IPD_OTRequisition otr,IPD_BedMaster bm where  od.OperationID=otr.OperationID and otr.OperationReqID=an.OperationReqID and an.PatientRegId=pr.PatientReg and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and bm.Allotted=1 and otr.PatientRegId='" + reg + "' and ba.ToDate is null";
        theCommand.CommandText = "select bm.BedNoText,od.OperationName,otr.OperationReqID, pr.PatientReg,pr.patient_name, an.AnesthesiaNoteId, an.BP PreBP,an.Chest PreChest,CONVERT(varchar,an.Date,103) dt,an.O2 PreO2,an.Others PreOthers,an.Pulse PrePulse,an.Remarks PreRemarks,an.RiskFactor PreRiskFactor,an.Time,an.Mode,CONVERT(varchar,pr.AdmissionDate,103) adate,an.PostBP,an.PostPulse,an.PostChest,an.PostO2,an.PostRiskFactor,an.PostOthers,an.PostRemarks,an.posttime from IPD_OperationDetails od, dbo.IPD_AnesthesisNote an,GN_PatientReg pr,IPD_BedAllocation ba,IPD_OTRequisition otr,IPD_BedMaster bm where ba.compcode=bm.compcode and bm.compcode=otr.compcode and pr.compcode=otr.compcode and od.compcode=otr.compcode and an.compcode=otr.compcode and an.yearcode=otr.yearcode and od.OperationID=otr.OperationID and otr.OperationReqID=an.OperationReqID and an.PatientRegId=pr.PatientReg and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and bm.Allotted=1 and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "' and otr.PatientRegId='" + reg + "' and ba.ToDate is null";
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

    public DataTable FetchFrom2ndGrid(string compcode, string yearcode,string reg, string req)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.  
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select bm.BedNoText,od.OperationName,otr.OperationReqID, pr.PatientReg,pr.patient_name, an.AnesthesiaNoteId," +
" an.BP PreBP,an.Chest PreChest,CONVERT(varchar,an.Date,103) dt,an.O2 PreO2,an.Mode,an.Others PreOthers,an.Pulse PrePulse,an.Remarks PreRemarks,an.RiskFactor PreRiskFactor,an.Time, " +
"an.PostBP,an.PostPulse,an.PostChest,an.PostO2,an.PostRiskFactor,an.PostOthers,an.PostRemarks,an.posttime," +
"CONVERT(varchar,pr.AdmissionDate,103) adate from IPD_OperationDetails od, dbo.IPD_AnesthesisNote an,GN_PatientReg pr," +
"IPD_BedAllocation ba,IPD_OTRequisition otr,IPD_BedMaster bm where ba.compcode=bm.compcode and bm.compcode=otr.compcode and pr.compcode=otr.compcode and "+
 "od.compcode=otr.compcode and an.compcode=otr.compcode and an.yearcode=otr.yearcode and od.OperationID=otr.OperationID and otr.OperationReqID=an.OperationReqID  " +
"and an.PatientRegId=pr.PatientReg and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo  and bm.Allotted=1 and "+
"otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "' and  " +
 "otr.PatientRegId='" + reg + "' and otr.OperationReqID='" + req + "' and ba.ToDate is null";

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

    public DataTable GridFillSecond(string compcode,string yearcode,string reg)
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
        BedTable = new DataTable();
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

        theCommand.CommandText = "select pr.PatientReg,pr.patient_name,bm.BedNoText,CONVERT(varchar,pr.AdmissionDate,103) adate,otr.OperationReqID,od.OperationName  from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr,IPD_OTRequisition otr,IPD_OperationDetails od where ba.compcode=bm.compcode and bm.compcode=otr.compcode and pr.compcode=otr.compcode and od.compcode=otr.compcode and pr.PatientReg=ba.PatientReg and otr.OperationReqID='" + req + "' and ba.BedNo=bm.BedNo and bm.Allotted =1 and  pr.PatientReg='" + reg + "' and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "' and od.OperationID=otr.OperationID and otr.PatientRegId=pr.PatientReg and ba.ToDate is null and ba.ToTime is null group  by pr.PatientReg,pr.patient_name,pr.AdmissionDate,otr.OperationReqID,od.OperationName,bm.BedNoText";

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
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }
    public DataTable Getonlyreq(string compcode, string yearcode, string reg)
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
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public DataTable getBPRange()
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from GN_BP_Range";

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


    public DataTable DropdownMode(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Mode where compcode='"+compcode+"'";
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

    public bool InsertAnesthesis(string PatientReg, string OperationReqID, string Date, string Time,  string BP, string Pulse, string Chest, string O2, string RiskFactor, string Others, string Remarks,string postbp,string postpulse,string postchest,string posto2,string postriskfactor,string postother,string postremarks,string posttime,string compcode,string user,string yearcode)
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
                string LedgerId = "";
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "select LedgerID from AC_Ledger al where al.compcode='"+compcode+"' and LedgerFK='" + PatientReg + "' and ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        theCommand.CommandText = "INSERT INTO IPD_AnesthesisNote(Compcode,yearcode,LedgerId,PatientRegId,OperationReqID,Date,Time,BP,Pulse,Chest,O2,RiskFactor,Others,Remarks,Status,PostBP,PostPulse,PostChest,PostO2,PostRiskFactor,PostOthers,PostRemarks,posttime,user01,logdt01) VALUES ('"+compcode+"','"+yearcode+"','" + LedgerId + "','" + PatientReg + "','" + OperationReqID + "', '" + Date + "', '" + Time + "','" + BP + "','" + Pulse + "','" + Chest + "','" + O2 + "','" + RiskFactor + "','" + Others + "','" + Remarks + "',1,'"+postbp+"','"+postpulse+"','"+postchest+"','"+posto2+"','"+postriskfactor+"','"+postother+"','"+postremarks+"','"+posttime+"','"+user+"',GETDATE())";
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
            theConnection.Dispose();
            theCommand.Dispose();
        }
    }

    public bool UpdateAnesthesis(string ID, string PatientReg, string OperationReqID, string Date, string Time, string BP, string Pulse, string Chest, string O2, string RiskFactor, string Others, string Remarks, string postbp, string postpulse, string postchest, string posto2, string postriskfactor, string postother, string postremarks,string posttime,string compcode,string user,string yearcode)
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
                        theCommand.CommandText = "select LedgerID from AC_Ledger al where al.compcode='"+compcode+"' and LedgerFK='" + PatientReg + "' and ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        theCommand.CommandText = "Update IPD_AnesthesisNote set LedgerId='" + LedgerId + "', Date='" + Date + "',Time='" + Time + "', BP='" + BP + "',Pulse='" + Pulse + "',Chest='" + Chest + "',O2='" + O2 + "',RiskFactor='" + RiskFactor + "',Remarks='" + Remarks + "',Others='" + Others + "',PostBP='"+postbp+"',PostPulse='"+postpulse+"',PostChest='"+postchest+"',PostO2='"+posto2+"',PostRiskFactor='"+postriskfactor+"',PostOthers='"+postother+"',PostRemarks='"+postremarks+"',posttime='"+posttime+"',user02='"+user+"',logdt02=GETDATE()  where compcode='"+compcode+"' and yearcode='"+yearcode+"' and PatientRegId = '" + PatientReg + "'   and  OperationReqID = '" + OperationReqID + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute update query.
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
}
  