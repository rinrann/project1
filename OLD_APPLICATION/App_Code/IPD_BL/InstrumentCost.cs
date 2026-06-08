using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for InstrumentCost12
/// </summary>
public class InstrumentCost
{
    public InstrumentCost(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;
    //public DataTable Getonltreq(string reg)
    //{
    //    if (reg == "")
    //        reg = "null";
    //    // Connection.
    //    theConnection = new SqlConnection();
    //    theConnection.ConnectionString = conString;

    //    // Command.
    //    theCommand = new SqlCommand();
    //    theCommand.Connection = theConnection;

    //    theCommand.CommandText = "select otr.OperationReqID from IPD_OTRequisition otr where otr.PatientRegId='" + reg + "'";

    //    theCommand.CommandType = CommandType.Text;

    //    // Adapter.
    //    theAdapter = new SqlDataAdapter();
    //    theAdapter.SelectCommand = theCommand;

    //    // Datatable.
    //    DataTable BedTable = new DataTable();
    //    theAdapter.Fill(BedTable); // Fill data into data table.

    //    // Clean up.
    //    theConnection.Dispose();
    //    theCommand.Dispose();
    //    theAdapter.Dispose();

    //    return BedTable;
    //}
    public DataTable GridFillSecond(string compcode, string yearcode, string reg,string type)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "T")
        {
            theCommand.CommandText = "select req.OperationReqID,req.PatientRegId, ot.OperationTypeName,od.OperationName from dbo.IPD_OperationDetails od,dbo.IPD_OperationType ot,dbo.IPD_OTRequisition req where od.compcode=req.compcode and ot.compcode=req.compcode and req.OperationTypeID=ot.OperationTypeID and req.OperationID=od.OperationID and ot.OperationTypeID=od.OperationTypeID and ot.status=1 and od.status=1 and req.PatientRegId ='" + reg + "' and req.compcode='" + compcode + "' and req.yearcode='" + yearcode + "'";
        }
        else
        {
            theCommand.CommandText = "select distinct(a.ServiceId),a.RowId,a.Quantity,a.Price,t.ServiceTemplateName,CONVERT(varchar,a.IssueDate,103) IssueDate,ISNULL(a.ServCont,0) ServCont,pr.PatientReg,pr.patient_name,tc.TemplateCategoryId,tc.CategoryName, CONVERT(varchar,pr.AdmissionDate,103)  adate,bm.BedNoText FROM  dbo.IPD_AddServices a,GN_PatientReg pr,dbo.IPD_BedMaster bm,IPD_Service_Cons_Template t,IPD_Service_Cons_TemplateCategory tc    WHERE tc.COMPCODE=a.COMPCODE and t.COMPCODE=a.COMPCODE and pr.COMPCODE=a.COMPCODE and bm.COMPCODE=a.COMPCODE and tc.TemplateCategoryId=t.TemplateCategoryId    AND a.ServiceId=t.NameID and  pr.PatientReg=a.PatientReg and bm.BedNo=pr.bedallocation and bm.Allotted=1  AND a.COMPCODE='" + compcode + "' and pr.PatientReg='" + reg + "'";
        }
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

    public DataTable GetAllInstrumentCost(string compcode, string yearcode, string reg, string req,string type)
    {
        if (reg == "")
            reg = "null";
        DataTable BedTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.OT_InstrumentMaster
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select od.OperationName,inscost.InstrumentId,inscost.OperationReqID,inscost.PatientReg,inscost.Remarks,inscost.RowID,inscost.Unit,inscost.UsageCost,isnull(inscost.ins_cont,0) cont,CONVERT(varchar,inscost.EntryDate,103) endate,CONVERT(varchar,pr.AdmissionDate,103) adate,pr.patient_name,bm.BedNoText  from IPD_BedAllocation ba,IPD_BedMaster bm, GN_PatientReg pr,dbo.OT_InstrumentCost inscost,IPD_OTRequisition otr,IPD_OperationDetails od where ba.compcode=bm.compcode and pr.compcode=otr.compcode and bm.compcode=otr.compcode and od.compcode=otr.compcode and inscost.compcode=otr.compcode and inscost.yearcode=otr.yearcode and ba.BedNo =bm.BedNo and ba.PatientReg=pr.PatientReg and pr.PatientReg=inscost.PatientReg and inscost.OperationReqID=otr.OperationReqID and otr.OperationID=od.OperationID and  inscost.EntryDate>=pr.AdmissionDate and pr.PatientReg='" + reg + "' and otr.OperationReqID='" + req + "' and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "'  and ba.ToDate is null and ba.ToTime is null and inscost.Ins_type='" + type + "'";

            theCommand.CommandType = CommandType.Text;

            // Adapter.
            theAdapter = new SqlDataAdapter();
            theAdapter.SelectCommand = theCommand;

            // Datatable.
            BedTable = new DataTable();
            theAdapter.Fill(BedTable); // Fill data into data table.

            if (BedTable.Rows.Count == 0)
            {

                theCommand.CommandText = "select null OperationName,inscost.InstrumentId,inscost.OperationReqID,inscost.PatientReg,inscost.Remarks, inscost.RowID,inscost.Unit,inscost.UsageCost,isnull(inscost.ins_cont,0) cont,CONVERT(varchar,inscost.EntryDate,103) endate, CONVERT(varchar,pr.AdmissionDate,103) adate,pr.patient_name,bm.BedNoText  from IPD_BedAllocation ba, IPD_BedMaster bm, GN_PatientReg pr,dbo.OT_InstrumentCost inscost  where ba.BedNo =bm.BedNo and ba.PatientReg=pr.PatientReg and pr.PatientReg=inscost.PatientReg and   pr.PatientReg='" + reg + "'  and inscost.EntryDate>=pr.AdmissionDate   and ba.ToDate is null and ba.ToTime is null and inscost.Ins_type='" + type + "'";


                theCommand.CommandType = CommandType.Text;

                // Adapter.
                theAdapter = new SqlDataAdapter();
                theAdapter.SelectCommand = theCommand;

                // Datatable.
                BedTable = new DataTable();
                theAdapter.Fill(BedTable); // Fill data into data table.
            }
        
        

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public DataTable GetAllServInstrumentCost(string compcode, string yearcode, string reg, string req, string type)
    {
        if (reg == "")
            reg = "null";
        if (req == "")
        {
            req = "%%";
        }
        
        DataTable BedTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.OT_InstrumentMaster
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select od.ServiceTemplateName OperationName,inscost.InstrumentId,inscost.OperationReqID,inscost.PatientReg,inscost.Remarks,inscost.RowID,inscost.Unit,inscost.UsageCost,CONVERT(varchar,inscost.EntryDate,103) endate,CONVERT(varchar,pr.AdmissionDate,103) adate,pr.patient_name,bm.BedNoText  from IPD_BedAllocation ba,IPD_BedMaster bm, GN_PatientReg pr,dbo.OT_InstrumentCost inscost,IPD_AddServices a,IPD_Service_Cons_Template od where ba.compcode=bm.compcode and pr.compcode=a.compcode and bm.compcode=a.compcode and od.compcode=a.compcode and inscost.compcode=a.compcode and ba.BedNo =bm.BedNo and ba.PatientReg=pr.PatientReg and pr.PatientReg=inscost.PatientReg and inscost.OperationReqID=cast(a.ServiceId as varchar(50)) and a.ServiceId=od.NameID and  inscost.EntryDate>=pr.AdmissionDate and pr.PatientReg='" + reg + "' and a.ServiceId='" + req + "' and a.compcode='" + compcode + "' and ba.ToDate is null and ba.ToTime is null";
        theCommand.CommandText = "select inscost.InstrumentId,inscost.OperationReqID,inscost.PatientReg,inscost.Remarks,inscost.RowID,inscost.Unit,inscost.UsageCost,isnull(inscost.ins_cont,0) cont,CONVERT(varchar,inscost.EntryDate,103) endate,CONVERT(varchar,pr.AdmissionDate,103) adate,pr.patient_name,bm.BedNoText/*,od.ServiceTemplateName OperationName*/, null OperationName from OT_InstrumentCost inscost,GN_PatientReg pr,IPD_BedAllocation ba,IPD_BedMaster bm/*,IPD_Service_Cons_Template od*/ where ba.COMPCODE=bm.COMPCODE and bm.COMPCODE=inscost.COMPCODE and /*od.compcode=inscost.compcode and*/ bm.BedNo=ba.BedNo and bm.Allotted='1'  and ba.PatientReg=pr.PatientReg and pr.COMPCODE=inscost.COMPCODE and pr.PatientReg=inscost.PatientReg and inscost.COMPCODE='" + compcode + "'  /*and inscost.OperationReqID like'" + req + "'*/ and inscost.EntryDate>=pr.AdmissionDate and ba.ToDate is null  and ba.ToTime is null and /*od.NameID=inscost.OperationReqID and*/ inscost.PatientReg='" + reg + "' and inscost.Ins_type='" + type + "'";
        
        theCommand.CommandType = CommandType.Text;

            // Datatable.
        BedTable = new DataTable();
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
       theConnection.Dispose();
       theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }


    
    public DataTable Getonlypat(string compcode, string yearcode, string reg, string req,string type)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select otr.OperationReqID,od.OperationName,pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from GN_PatientReg pr,IPD_BedAllocation ba,IPD_BedMaster bm,IPD_OTRequisition otr,IPD_OperationDetails od,IPD_OperationType ot where ba.compcode=bm.compcode and bm.compcode=otr.compcode and od.compcode=otr.compcode and pr.compcode=otr.compcode and ba.FromDate>=pr.AdmissionDate and ba.BedNo=bm.BedNo and ba.PatientReg=pr.PatientReg and otr.PatientRegId= pr.PatientReg and otr.OperationID=od.OperationID and ot.OperationTypeID=otr.OperationTypeID and pr.PatientReg='" + reg + "' and otr.OperationReqID='" + req + "' and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "' and ba.ToDate is null and ba.ToTime is null";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        if (BedTable.Rows.Count == 0)
        {
            theCommand.CommandText = "select null OperationReqID,null OperationName,pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from GN_PatientReg pr,IPD_BedAllocation ba,IPD_BedMaster bm where ba.compcode=bm.compcode and bm.compcode=pr.compcode and ba.FromDate>=pr.AdmissionDate and ba.BedNo=bm.BedNo and ba.PatientReg=pr.PatientReg and pr.PatientReg='" + reg + "' and pr.compcode='"+compcode+"' and  ba.ToDate is null and ba.ToTime is null";
            theCommand.CommandType = CommandType.Text;

            // Adapter.
            theAdapter = new SqlDataAdapter();
            theAdapter.SelectCommand = theCommand;

            // Datatable.
            BedTable = new DataTable();
            theAdapter.Fill(BedTable); // Fill data into data table.
        }
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }


    public DataTable GetonlyServpat(string compcode, string yearcode, string reg, string req, string type)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select a.ServiceId OperationReqID,od.ServiceTemplateName OperationName,pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from GN_PatientReg pr,IPD_BedAllocation ba,IPD_BedMaster bm,IPD_AddServices a,IPD_Service_Cons_Template od where ba.compcode=bm.compcode and bm.compcode=a.compcode and od.compcode=a.compcode and pr.compcode=a.compcode and ba.FromDate>=pr.AdmissionDate and ba.BedNo=bm.BedNo and ba.PatientReg=pr.PatientReg and a.PatientReg= pr.PatientReg and a.ServiceId=od.NameID and pr.PatientReg='" + reg + "' and a.ServiceId='" + req + "' and a.compcode='" + compcode + "' /*and otr.yearcode='" + yearcode + "'*/ and ba.ToDate is null and ba.ToTime is null";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        if (BedTable.Rows.Count == 0)
        {
            theCommand.CommandText = "select null OperationReqID,null OperationName,pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from GN_PatientReg pr,IPD_BedAllocation ba,IPD_BedMaster bm where ba.compcode=bm.compcode and bm.compcode=pr.compcode and ba.FromDate>=pr.AdmissionDate and ba.BedNo=bm.BedNo and ba.PatientReg=pr.PatientReg and pr.PatientReg='" + reg + "' and pr.compcode='" + compcode + "' and  ba.ToDate is null and ba.ToTime is null";
            theCommand.CommandType = CommandType.Text;

            // Adapter.
            theAdapter = new SqlDataAdapter();
            theAdapter.SelectCommand = theCommand;

            // Datatable.
            BedTable = new DataTable();
            theAdapter.Fill(BedTable); // Fill data into data table.
        }
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }


    public DataTable DropdownInstrument(string compcode,string type)
    {
        if (type == "")
        {
            type = "T";
        }
       
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from OT_InstrumentMaster where Status=1 and compcode='"+compcode+"' and ins_type='"+type+"'";
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


    public bool InsertInstrumentCost(string PatientReg, string OperationReqID, string EntryDate, string InstrumentId, string Unit, string UsageCost, string Remarks, string compcode, string user, string yearcode,string type,string cont)
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

                        theCommand.CommandText = "INSERT INTO OT_InstrumentCost(compcode,yearcode,LedgerId,PatientReg,OperationReqID,EntryDate,InstrumentId,Unit,UsageCost,Remarks,Status,user01,logdt01,Ins_type,ins_cont) VALUES ('" + compcode + "','" + yearcode + "','" + LedgerId + "','" + PatientReg + "','" + OperationReqID + "','" + EntryDate + "', '" + InstrumentId + "', '" + Unit + "','" + UsageCost + "', '" + Remarks + "',1,'" + user + "',GETDATE(),'" + type + "','"+cont+"')";
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
    public bool DeleteInstrument(string compcode, string yearcode, string reg, string req,string type)
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
                        theCommand.CommandText = "delete from  OT_InstrumentCost where PatientReg='" + reg + "' and OperationReqID='" + req + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "' and Ins_type='"+type+"'";
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

    public DataTable GetCost(string compcode, string inst, string type)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "Select isNull(UnitCost,'0.00') UnitCost from OT_InstrumentMaster where compcode='" + compcode + "' and RowID='" + inst + "' and ins_type='" + type + "'";

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

    public bool Update_Delete_Addservice(int mode, string patientreg, string RowId, string unit, string Price, string IssueDate, string cont, string compcode,string yearcode,string user)
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
                        if (mode == 2)
                        {
                            theCommand.CommandText = "update  OT_InstrumentCost set Unit='" + unit + "',UsageCost='" + Price + "',EntryDate='" + IssueDate + "',ins_cont='" + cont + "',user02='" + user + "',logdt02=GETDATE()  where compcode='" + compcode + "' and yearcode='"+yearcode+"' and RowId='" + RowId + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        else
                        {

                            theCommand.CommandText = "delete  OT_InstrumentCost  where compcode='" + compcode + "' and RowId='" + RowId + "' and EntryDate='" + IssueDate + "'";

                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();

                        }
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