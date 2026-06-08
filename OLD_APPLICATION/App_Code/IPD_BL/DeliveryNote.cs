using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DeliveryNote12
/// </summary>
public class DeliveryNote
{
	public DeliveryNote(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;
    public DataTable Getonltreq(string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select otr.OperationReqID from IPD_OTRequisition otr where otr.PatientRegId='" + reg + "'";

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

    public DataTable GetAllDelivery(string reg, string req,string compcode,string yearcode)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select od.OperationName, CONVERT(varchar,map.DeliveryDate,103) ddate,map.DeliveryTime, CONVERT(varchar,pr.AdmissionDate,103) adate,pr.patient_name,dn.DeliveryNoteID, dn.ModeofDelivery,dn.NoofBaby,dn.PatientReg,dn.PatientReq,map.AGScore,map.AGScoreAfter,map.Cry,map.Liquor,map.Maturity,map.Remarks,map.RowID,map.Sex,map.Weight,bm.BedNoText  from IPD_BedAllocation ba,IPD_BedMaster bm, GN_PatientReg pr,IPD_OTRequisition otr,dbo.OT_DeliveryNote dn,dbo.OT_DeliveryNoteMapping map, IPD_OperationDetails od where ba.compcode=bm.compcode and bm.compcode=pr.compcode and pr.compcode=otr.compcode and dn.compcode=otr.compcode and dn.yearcode=otr.yearcode and map.yearcode=dn.yearcode and map.compcode=otr.compcode and od.compcode=otr.compcode and map.DeliveryNoteID=dn.DeliveryNoteID and pr.PatientReg=dn.PatientReg and otr.OperationReqID=dn.PatientReq and ba.BedNo=bm.BedNo and ba.PatientReg=pr.PatientReg and otr.OperationID=od.OperationID and otr.compcode='"+compcode+"' and otr.yearcode='"+yearcode+"' and pr.PatientReg='" + reg + "' and  otr.OperationReqID='" + req + "' group by od.OperationName ,pr.AdmissionDate,pr.patient_name,dn.DeliveryNoteID, dn.ModeofDelivery,dn.NoofBaby,dn.PatientReg,dn.PatientReq,map.AGScore,map.AGScoreAfter,map.Cry,map.Liquor,map.Maturity,map.Remarks,map.RowID,map.Sex,map.Weight,map.DeliveryDate,map.DeliveryTime,bm.BedNoText";

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

    public DataTable Getonlypat(string reg, string req,string compcode,string yearocde)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select otr.OperationReqID,od.OperationName,pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from IPD_BedAllocation ba,IPD_BedMaster bm, GN_PatientReg pr,IPD_OTRequisition otr,IPD_OperationDetails od,IPD_OperationType ot where ba.compcode=bm.compcode and bm.compcode=pr.compcode and pr.compcode=otr.compcode and od.compcode=otr.compcode and ot.compcode=otr.compcode and otr.PatientRegId= pr.PatientReg and otr.OperationID=od.OperationID and ot.OperationTypeID=otr.OperationTypeID and otr.compcode='"+compcode+"' and otr.yearcode='"+yearocde+"' and pr.PatientReg='" + reg + "' and ba.BedNo=bm.BedNo and ba.PatientReg=pr.PatientReg and otr.OperationReqID='" + req + "' group by otr.OperationReqID,od.OperationName, pr.PatientReg,pr.patient_name ,pr.AdmissionDate,bm.BedNoText ";

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

    public DataTable DropdownDeliveryMode(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from OT_DeliveryMode where compcode='"+compcode+"' and Status=1";
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

    public DataTable DropdownChildSex(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_ChildSex where Status=1 and compcode='"+compcode+"'";
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

    public DataTable DropdownCry(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from OT_DeliveryCry where status=1 and compcode='"+compcode+"'";
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

    public DataTable DropdownLiquor(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from OT_DeliveryLiquor where status=1 and compcode='"+compcode+"'";
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


    public DataTable DropdownMaturity(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from OT_DeliveryMaturity where status=1 and compcode='"+compcode+"'";
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

    public bool InsertDelNoteMap(string DeliveryDate, string DeliveryTime, string reg, string req, string Sex, string Weight, string Maturity, string Cry, string Liquor, string AGScore, string AGScoreAfter, string Remarks,string compcode,string user,string yearcode)
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
                        theCommand.CommandText = "INSERT INTO OT_DeliveryNoteMapping(compcode,yearcode,DeliveryNoteID,Sex,Weight,Maturity,Cry,Liquor,AGScore,AGScoreAfter,Remarks,DeliveryDate,DeliveryTime,User01,logdt01) VALUES ('"+compcode+"','"+yearcode+"',(select dn.DeliveryNoteID from dbo.OT_DeliveryNote dn where dn.PatientReg='" + reg + "' and dn.PatientReq='" + req + "'),'" + Sex + "','" + Weight + "', '" + Maturity + "', '" + Cry + "','" + Liquor + "','" + AGScore + "','" + AGScoreAfter + "','" + Remarks + "','" + DeliveryDate + "','" + DeliveryTime + "','"+user+"',GETDATE())";
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

    public bool InsertDeliveryNote(string PatientReg, string PatientReq, string ModeofDelivery, string NoofBaby,string compcode,string user,string yearcode)
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

                        theCommand.CommandText = "INSERT INTO OT_DeliveryNote(compcode,yearcode,LedgerId,PatientReg,PatientReq,ModeofDelivery,NoofBaby,Status,user01,logdt01) VALUES ('"+compcode+"','"+yearcode+"','" + LedgerId + "','" + PatientReg + "','" + PatientReq + "','" + ModeofDelivery + "','" + NoofBaby + "',1,'"+user+"',GETDATE())";
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

    public bool UpdateDeliveryNote(string PatientReg, string PatientReq,string ModeofDelivery, string NoofBaby,string compcode,string user,string yearcode)
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
                        using (  theCommand = theConnection.CreateCommand())
                        {
            theCommand.CommandText = "update OT_DeliveryNote set ModeofDelivery='" + ModeofDelivery + "',NoofBaby='" + NoofBaby + "',user02='"+user+"',logdt02=GETDATE() where compcode='"+compcode+"' and PatientReg='" + PatientReg + "' and yearcode='"+yearcode+"' and PatientReq='" + PatientReq + "'";
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
    public bool DeleteDeliveryNote(string id,string compcode,string yearcode)
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
                        using (  theCommand = theConnection.CreateCommand())
                        {
            theCommand.CommandText = "delete from dbo.OT_DeliveryNoteMapping where compcode='"+compcode+"' and DeliveryNoteID='" + id + "' and yearcode='"+yearcode+"'";
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

        theCommand.CommandText = "select otr.PatientRegId from IPD_OTRequisition otr where otr.compcode='"+compcode+"' and otr.yearcode='"+yearcode+"' and otr.OperationReqID='" + req + "'";

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