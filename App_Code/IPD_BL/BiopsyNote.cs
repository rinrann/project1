using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BiopsyNote12
/// </summary>
public class BiopsyNote
{
    public BiopsyNote(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetAllBiopsy(string reg,string compcode,string yearcode)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select map.RowId,map.BiopsyNoteId,od.OperationName,map.TypeOfTissue,map.ExamRequired,map.Remarks,pr.patient_name,pr.PatientReg," +
                            "otr.OperationReqID,CONVERT(varchar,pr.AdmissionDate,103) adate,CONVERT(varchar,map.DateOfCollection,103) coldate, " +
                            "bm.BedNoText from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr,IPD_OperationDetails od,IPD_OTBiopsyNote bn, " +
                            "dbo.IPD_OTBiopsyNoteMapping map,IPD_OTRequisition otr where ba.compcode=bm.compcode and bm.compcode=pr.compcode and od.compcode=otr.compcode and pr.compcode=otr.compcode and bn.compcode=otr.compcode and bn.yearcode=otr.yearcode and map.compcode=bn.compcode and map.yearcode=bn.yearcode and ba.BedNo=bm.BedNo and ba.PatientReg=pr.PatientReg and " +
                              "od.OperationID=otr.OperationID and otr.PatientRegId=pr.PatientReg and bn.BiopsyNoteId=map.BiopsyNoteId and  " +
                              "bn.OperationReqID=otr.OperationReqID  and ba.ToDate is null and pr.PatientReg='" + reg + "' and otr.compcode='"+compcode+"' and otr.yearcode='"+yearcode+"'";

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

    public DataTable FetchFron2ndGrid(string compcode,string yearcode,string reg, string req)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select map.RowId,map.BiopsyNoteId,od.OperationName,map.TypeOfTissue,map.ExamRequired,map.Remarks,pr.patient_name,pr.PatientReg," +
                                "otr.OperationReqID,CONVERT(varchar,pr.AdmissionDate,103) adate,CONVERT(varchar,map.DateOfCollection,103) coldate,bm.BedNoText from  " +
                                 "GN_PatientReg pr,IPD_OperationDetails od,IPD_OTBiopsyNote bn,dbo.IPD_OTBiopsyNoteMapping map,IPD_OTRequisition otr,IPD_BedAllocation ba,  " +
                                 "IPD_BedMaster bm where ba.compcode=bm.compcode and bm.compcode=otr.compcode and pr.compcode=otr.compcode and od.compcode=otr.compcode and "+
                                 "bn.compcode=otr.compcode and bn.yearcode=otr.yearcode and map.compcode=otr.compcode and map.yearcode=bn.yearcode and  ba.BedNo=bm.BedNo and ba.PatientReg=pr.PatientReg and "+
                                 "otr.compcode='"+compcode+"' and otr.yearcode='"+yearcode+"' and otr.OperationReqID='" + req + "' and  " +
                                 "od.OperationID=otr.OperationID and otr.PatientRegId=pr.PatientReg and bn.BiopsyNoteId=map.BiopsyNoteId and  " +
                                 "bn.OperationReqID=otr.OperationReqID  and ba.ToDate is null  and map.DateOfCollection>=pr.AdmissionDate";

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

    public DataTable Getonltreq(string compcode,string yearcode,string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select otr.OperationReqID from IPD_OTRequisition otr where otr.PatientRegId='" + reg + "' and otr.compcode='"+compcode+"' and otr.yearcode='"+yearcode+"'";

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

    public DataTable GenerateBiopsycode(string compcode,string yearcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_IPD_GetBiopsyNoteID '"+compcode+"','"+yearcode+"'";

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


    public DataTable Getonlypat(string compcode, string yearcode,string reg, string req)
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
            theCommand.CommandText = "select pr.PatientReg,od.OperationName,pr.patient_name,otr.OperationReqID,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr,IPD_OTRequisition otr,IPD_OperationDetails od where ba.compcode=bm.compcode and bm.compcode=otr.compcode and pr.compcode=otr.compcode and od.compcode=otr.compcode and otr.OperationID=od.OperationID and pr.PatientReg=otr.PatientRegId and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and bm.Allotted =1 and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "' and pr.PatientReg='" + reg + "'  and ba.ToDate is null";
        else
            theCommand.CommandText = "select pr.PatientReg,od.OperationName,pr.patient_name,otr.OperationReqID,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr,IPD_OTRequisition otr,IPD_OperationDetails od where ba.compcode=bm.compcode and bm.compcode=otr.compcode and pr.compcode=otr.compcode and od.compcode=otr.compcode and otr.OperationID=od.OperationID and pr.PatientReg=otr.PatientRegId and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and bm.Allotted =1 and otr.compcode='" + compcode + "' and otr.yearcode='" + yearcode + "' and pr.PatientReg='" + reg + "' and otr.OperationReqID='" + req + "'  and  ba.ToDate is null and ba.ToTime  is null";

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

    public bool InsertBiopsyNote(string compcode,string user,string BiopsyNoteId, string PatientRegId, string OperationReqID,string yearcode)
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
                        theCommand.CommandText = "select LedgerID from AC_Ledger al where al.compcode='"+compcode+"' and LedgerFK='" + PatientRegId + "' and ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        theCommand.CommandText = "INSERT INTO IPD_OTBiopsyNote(compcode,yearcode,LedgerId,BiopsyNoteId,PatientRegId,OperationReqID,Status,user01,logdt01) VALUES('"+compcode+"','"+yearcode+"','" + LedgerId + "','" + BiopsyNoteId + "','" + PatientRegId + "','" + OperationReqID + "',1,'"+user+"',GETDATE())";
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
    public DataTable Getonlyreg(string compcode,string yearcode,string req)
    {
        if (req == "")
            req = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select otr.PatientRegId from IPD_OTRequisition otr where otr.OperationReqID='" + req + "' and otr.compcode='"+compcode+"' and otr.yearcode='"+yearcode+"'";

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

        theCommand.CommandText = "select req.OperationReqID,req.PatientRegId, ot.OperationTypeName,od.OperationName from dbo.IPD_OperationDetails od,dbo.IPD_OperationType ot,dbo.IPD_OTRequisition req where od.compcode=req.compcode and ot.compcode=req.compcode and req.OperationTypeID=ot.OperationTypeID and req.OperationID=od.OperationID and ot.OperationTypeID=od.OperationTypeID and ot.status=1 and od.status=1 and req.PatientRegId ='" + reg + "' and req.compcode='"+compcode+"' and req.yearcode='"+yearcode+"'";

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

    public bool InsertBiopsyMapping(string BiopsyNoteId, string DateOfCollection, string TypeOfTissue, string ExamRequired, string Remarks,string compcode,string user,string yearcode)
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
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "INSERT INTO IPD_OTBiopsyNoteMapping(compcode,yearcode,BiopsyNoteId,DateOfCollection,TypeOfTissue,ExamRequired,Remarks,user01,logdt01) VALUES('"+compcode+"','"+yearcode+"','" + BiopsyNoteId + "','" + DateOfCollection + "','" + TypeOfTissue + "','" + ExamRequired + "','" + Remarks + "','"+user+"',GETDATE())";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.
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

    public bool DeleteBiopsyMap(string ID,string compcode,string yearcode)
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
                        theCommand.CommandText = "delete from IPD_OTBiopsyNoteMapping where BiopsyNoteId = '" + ID + "' and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }
}