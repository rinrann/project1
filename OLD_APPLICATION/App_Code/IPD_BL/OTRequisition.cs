using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for OTRequisition12
/// </summary>
public class OTRequisition
{
    public OTRequisition(string con)
    {
        conString = con;
    }



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;


    public DataTable GenerateOTRequisitionNo(string compcode)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_GenerateOTRequisitionNo '"+compcode+"'";
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

    public DataTable GetSurgeon(string compcode)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.GN_DoctorMaster dm where compcode='"+compcode+"' and dm.Status=1";
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


    public DataTable GetOTRoom(string compcode)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.IPD_RoomMaster r where r.compcode='"+compcode+"' and r.RoomCategoryID=13";
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


    public DataTable GetDoctor(string compcode)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select doc_name,doc_id from dbo.GN_DoctorMaster dm where dm.compcode='"+compcode+"' ";
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
    public DataTable GetAnthetist()
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.GN_DoctorMaster dm,dbo.GN_DoctorType dt where dm.DocTypeId=dt.DocTypeId and dt.DocTypeId=2";
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
    public DataTable GetImplant(string compcode)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select cl.ImplantId,cl.Description from dbo.IPD_ImplantMaster cl where cl.compcode='"+compcode+"' and cl.status='1'";
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
    public DataTable GetOperationType(string compcode)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select cl.OperationTypeID,cl.OperationTypeName from dbo.IPD_OperationType cl where cl.compcode='"+compcode+"' and status='1'";
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
    public DataTable pname(string compcode,string reg)
    {
        if (reg == "")
            reg = "null";
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr where ba.compcode=bm.compcode and bm.compcode=pr.compcode and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and bm.Allotted =1 and pr.compcode='"+compcode+"' and pr.PatientReg='" + reg + "' and ba.ToDate is null and ba.ToTime is null";
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

    public DataTable GridFill(string compcode,string yearcode,string reg)
    {
        /*if (reg == "")
            reg = "null";*/
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "select od.OperationName,ot.OperationTypeName,orr.SurgeonID,orr.AddDocID1,orr.AddDocID2,orr.AddDocID3,orr.Anesthetist1,orr.Anesthetist2,orr.OTRoomNo,orr.OperationTypeID,orr.OperationID, pr.patient_name,pr.PhNo1, orr.*,dbo.fn_splitOnlyNames(dm.doc_name) doc_name,isnull(orr.Implant,'0') Implant1,CONVERT(varchar,orr.OperationDate,103) otdate from  dbo.IPD_OperationDetails od,dbo.IPD_OperationType ot, dbo.IPD_OTRequisition orr,GN_PatientReg pr,GN_DoctorMaster dm  where orr.Status!='Cancel' and  ot.OperationTypeID=orr.OperationTypeID  and od.OperationID=orr.OperationID and   pr.PatientReg =orr.PatientRegId AND dm.doc_id=orr.SurgeonID and orr.PatientRegId like '" + reg + "%'";
        theCommand.CommandText = "select od.OperationName,orr.SurgeonID,orr.AddDocID1,orr.AddDocID2,orr.AddDocID3,orr.Anesthetist1,orr.Anesthetist2,orr.OTRoomNo,orr.OperationTypeID,orr.OperationID, pr.patient_name,pr.PhNo1, orr.*,dbo.fn_splitOnlyNames(dm.doc_name) doc_name,isnull(orr.Implant,'0') Implant1,CONVERT(varchar,orr.OperationDate,103) otdate from  dbo.IPD_OperationDetails od, dbo.IPD_OTRequisition orr,GN_PatientReg pr,GN_DoctorMaster dm  where dm.compcode=pr.compcode and od.compcode=pr.compcode and orr.compcode=pr.compcode and orr.Status!='Cancel' and od.OperationID=orr.OperationID and   pr.PatientReg =orr.PatientRegId AND dm.doc_id=orr.SurgeonID and orr.compcode='"+compcode+"' and orr.yearcode='"+yearcode+"' and orr.PatientRegId like '" + reg + "%' and pr.CheckStatus=1";
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

    public bool Insert_Update_Delete_OTRequisition(string Implant, int mode,string OTRoomNo, string regno, string reqno, string ottype, string otname, string surgeon, string adddoc1, string adddoc2, string adddoc3, string antdoc1, string anthdoc2, string otdate, string starttime, string endtime,string compcode,string yearcode,string user)
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
                        if (mode == 1)
                        {
                            theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + regno + "' AND al.ActiveStatus=1";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader = theCommand.ExecuteReader();
                            theReader.Read();
                            LedgerId = theReader[0].ToString();
                            theReader.Close();
                            theCommand.CommandText = "INSERT INTO IPD_OTRequisition(compcode,yearcode,Implant,LedgerId,OTRoomNo,PatientRegId,OperationReqID,OperationTypeID,OperationID,SurgeonID,AddDocID1,AddDocID2,AddDocID3,Anesthetist1,Anesthetist2,OperationDate,StartTime,Status,user01,logdt01) VALUES('"+compcode+"','"+yearcode+"','" + Implant + "','" + LedgerId + "','" + OTRoomNo + "','" + regno + "','" + reqno + "','" + ottype + "','" + otname + "','" + surgeon + "','" + adddoc1 + "','" + adddoc2 + "','" + adddoc3 + "','" + antdoc1 + "','" + anthdoc2 + "','" + otdate + "','" + starttime + "','Pending','"+user+"',GETDATE())";
                        }

                        if (mode == 2)
                            theCommand.CommandText = "update IPD_OTRequisition set Implant='" + Implant + "', OTRoomNo='" + OTRoomNo + "',OperationTypeID='" + ottype + "',OperationID='" + otname + "',SurgeonID='" + surgeon + "',AddDocID1='" + adddoc1 + "',AddDocID2='" + adddoc2 + "',AddDocID3='" + adddoc3 + "',Anesthetist1='" + antdoc1 + "',Anesthetist2='" + anthdoc2 + "',OperationDate='" + otdate + "',StartTime='" + starttime + "',user02='"+user+"',logdt02=GETDATE()  where compcode='"+compcode+"' and yearcode='"+yearcode+"' and OperationReqID='" + reqno + "'";

                        if (mode == 3)
                            theCommand.CommandText = "update  IPD_OTRequisition  set Status='Cancel'  where OperationReqID='" + reqno + "'";

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
}