using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for OTConsumable12
/// </summary>
public class OTConsumable
{
    public OTConsumable(string con)
    {
        conString = con;
    }
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

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

        theCommand.CommandText = "select pr.patient_name,od.OperationName,otcon.RowID,otcon.PatientRegNo,otcon.ConsumableGrID,otcon.ConsumableID,otcon.ActualQty,otcon.AdviceBy,otcon.BillQty,otcon.Comment,otcon.OTReqNo,CONVERT(varchar,otcon.IssueDate,103) issudate,pr.PatientReg,otr.OperationReqID,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from  GN_PatientReg pr,IPD_OperationDetails od ,IPD_OTRequisition otr,dbo.OT_Consumable otcon,dbo.IPD_ConsumableItems item, dbo.IPD_ConsumableGroup congrp ,IPD_BedAllocation ba,IPD_BedMaster bm where ba.compcode=bm.compcode and pr.compcode=otr.compcode and bm.compcode=otr.compcode and od.compcode=otr.compcode and otcon.compcode=otr.compcode and otcon.yearcode=otr.yearcode and item.compcode=otcon.compcode and congrp.compcode=otcon.compcode and bm.BedNo=ba.BedNo and ba.PatientReg=pr.PatientReg and otr.OperationReqID='" + req + "' and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "' and otr.OperationReqID=otcon.OTReqNo and otcon.ConsumableGrID=congrp.ConGrId  and otcon.ConsumableID=item.ConItemID and otcon.PatientRegNo=pr.PatientReg and od.OperationID=otr.OperationID and otr.PatientRegId=pr.PatientReg  and ba.ToTime is null and ba.ToDate is null";

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
            theCommand.CommandText = "select pr.PatientReg,od.OperationName,pr.patient_name,otr.OperationReqID,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr,IPD_OTRequisition otr,IPD_OperationDetails od where ba.compcode=bm.compcode and bm.compcode=otr.compcode and od.compcode=otr.compcode and pr.compcode=otr.compcode and otr.OperationID=od.OperationID and pr.PatientReg=otr.PatientRegId and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and bm.Allotted =1 and  pr.PatientReg='" + reg + "' and otr.compcode='"+compcode+"' and otr.yearcode='"+yearcode+"'  and ba.ToDate is null and ba.ToTime is null";
        else
            theCommand.CommandText = "select pr.PatientReg,od.OperationName,pr.patient_name,otr.OperationReqID,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr,IPD_OTRequisition otr,IPD_OperationDetails od where ba.compcode=bm.compcode and bm.compcode=otr.compcode and od.compcode=otr.compcode and pr.compcode=otr.compcode and otr.OperationID=od.OperationID and pr.PatientReg=otr.PatientRegId and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and bm.Allotted =1 and  pr.PatientReg='" + reg + "' and otr.OperationReqID='" + req + "' and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "'  and ba.ToDate is null and ba.ToTime is null";

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

    public bool InsertOTConsumable(string PatientRegNo, string OTReqNo, string ConsumableGrID, string ConsumableID, string IssueDate, string ActualQty, string BillQty, string AdviceBy, string Comment,string compcode,string user,string yearcode)
    {
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
                        theCommand.CommandText = "select LedgerID from AC_Ledger al where al.compcode='"+compcode+"' and LedgerFK='" + PatientRegNo + "' and ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        theCommand.CommandText = "INSERT INTO OT_Consumable(compcode,Yearcode,LedgerId,PatientRegNo,OTReqNo,ConsumableGrID,ConsumableID,IssueDate,ActualQty,BillQty,AdviceBy,Comment,Status,user01,logdt01) VALUES('"+compcode+"','"+yearcode+"','" + LedgerId + "','" + PatientRegNo + "','" + OTReqNo + "','" + ConsumableGrID + "','" + ConsumableID + "','" + IssueDate + "','" + ActualQty + "','" + BillQty + "','" + AdviceBy + "','" + Comment + "',1,'"+user+"',GETDATE())";
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

    public bool DelOTConsumable(string compcode,string yearcode,string PatientRegNo, string OTReqNo)
    {
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
                        theCommand.CommandText = "delete from OT_Consumable where PatientRegNo='" + PatientRegNo + "' and OTReqNo='" + OTReqNo + "' and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
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

    public DataTable DropDownConsumablegroup()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from IPD_ConsumableGroup where status=1 ";

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
    public DataTable DropDownConsumable(string ConGrId)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (ConGrId == "0")
            theCommand.CommandText = "select * from IPD_ConsumableItems where status=1";
        else
            theCommand.CommandText = "select * from IPD_ConsumableItems where ConGrId='" + ConGrId + "'  and status=1";

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

        theCommand.CommandText = "select dbo.fn_splitOnlyNames(doc_name) doc_name,doc_id from GN_DoctorMaster where Status=1 order by doc_name";

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

    public DataTable GridFillSecond(string compcode, string yearcode, string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select distinct req.OperationReqID,req.PatientRegId, ot.OperationTypeName,od.OperationName from dbo.IPD_OperationDetails od,OT_Consumable oc,GN_PatientReg pr,  dbo.IPD_OperationType ot,dbo.IPD_OTRequisition req where od.compcode=req.compcode and oc.compcode=req.compcode and pr.compcode=req.compcode and ot.compcode=req.compcode and oc.yearcode=req.yearcode and oc.PatientRegNo=pr.PatientReg and req.OperationTypeID=ot.OperationTypeID and  req.OperationID=od.OperationID and ot.OperationTypeID=od.OperationTypeID and ot.status=1 and od.status=1 and  req.PatientRegId ='" + reg + "' and oc.PatientRegNo=req.PatientRegId and req.compcode='"+compcode+"' and req.yearcode='"+yearcode+"' and oc.IssueDate>=req.OperationDate and req.OperationDate>=pr.AdmissionDate"; 
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
}