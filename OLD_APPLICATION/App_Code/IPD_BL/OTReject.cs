using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for OTReject
/// </summary>
public class OTReject
{
    public OTReject(string con)
    {
        conString = con;
    }

    public string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable InstrumentsTable;
    public DataTable GetAllInstrumentReject(string typeid, string insid, string modelno,string compcode)
    {
        if (typeid == "" || typeid == "0")
            typeid = "null";
        else
            typeid = "'" + typeid + "'";

        if (insid == "" || insid == "0")
            insid = "null";
        else
            insid = "'" + insid + "'";


        if (modelno == "")
            modelno = "null";
        else
            modelno = "'" + modelno + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_OT_InstrumentReject " + typeid + ", " + insid + "," + modelno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        InstrumentsTable = new DataTable();
        theAdapter.Fill(InstrumentsTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return InstrumentsTable;
    }



    public DataTable GetModel(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        if (id == "0")

            theCommand.CommandText = "SELECT *,CONVERT(varchar,oi.PurchaseDate,103) pdate FROM IPD_OTInstruments oi,PH_ManufactureMaster mm where   mm.MCode=oi.ManufacturingCompany";
        else

            theCommand.CommandText = "SELECT *,CONVERT(varchar,oi.PurchaseDate,103) pdate FROM IPD_OTInstruments oi,PH_ManufactureMaster mm where oi.InstrumentID='" + id + "' and mm.MCode=oi.ManufacturingCompany and oi.RejectDate is null";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        InstrumentsTable = new DataTable();
        theAdapter.Fill(InstrumentsTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return InstrumentsTable;
    }
    public DataTable GetAllInstrument(string id, string type)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "1")
            theCommand.CommandText = "SELECT *,CONVERT(varchar,oi.PurchaseDate,103) pdate FROM IPD_OTInstruments oi,PH_ManufactureMaster mm where oi.ModelNo='" + id + "' and mm.MCode=oi.ManufacturingCompany";
        else
            theCommand.CommandText = "SELECT *,CONVERT(varchar,oi.PurchaseDate,103) pdate FROM IPD_OTInstruments oi,PH_ManufactureMaster mm where oi.InstrumentId='" + id + "' and mm.MCode=oi.ManufacturingCompany";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        InstrumentsTable = new DataTable();
        theAdapter.Fill(InstrumentsTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return InstrumentsTable;
    }

    public DataTable DropdownInstrumentTye()
    {
        //Connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("select * from OT_InstrumentType where status=1", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);
        theAdapter.Dispose();

        theConnection.Dispose();
        theAdapter.Dispose();

        return dt;
    }

    public DataTable DropdownInstrument(string type)
    {
        //Connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        if (type == "0")
            theAdapter = new SqlDataAdapter("select * from OT_InstrumentMaster im where status=1", theConnection);
        else
            theAdapter = new SqlDataAdapter("select * from OT_InstrumentMaster im where status=1 and im.InstrumentType='" + type + "'", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);

        theConnection.Dispose();
        theAdapter.Dispose();

        return dt;
    }


    public bool InsertOTInstrumentsReject(string InstrumentID, string ModelNo, string RejectDate, string Quantity, string RejectReason, string CreatedBy)
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
                        theCommand.CommandText = "UPDATE IPD_OTInstruments SET RejectDate='" + RejectDate + "' ,RejectionReason='" + RejectReason + "' where ModelNo='" + ModelNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        // Command.
                        theCommand = new SqlCommand();
                        theCommand.Connection = theConnection;
                        theCommand.CommandText = "INSERT INTO OT_InstrumentReject(InstrumentID,ModelNo,RejectDate,Quantity,RejectReason,CreatedBy) VALUES ('" + InstrumentID + "','" + ModelNo + "','" + RejectDate + "','" + Quantity + "','" + RejectReason + "','" + CreatedBy + "')";
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
    public bool UpdateOTInstrumentsReject(string RowId, string InstrumentID, string ModelNo, string RejectDate, string Quantity, string RejectReason)
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
                        theCommand.CommandText = "Update OT_InstrumentReject set InstrumentID='" + InstrumentID + "', ModelNo='" + ModelNo + "',RejectDate='" + RejectDate + "', Quantity='" + Quantity + "',RejectReason='" + RejectReason + "'  where RowId = '" + RowId + "'";
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
    public bool DeleteOTInstruments(string RowId)
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
                        theCommand.CommandText = "delete OT_InstrumentReject  WHERE RowId = " + RowId + "";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute Delete query.

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