using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for RepairReceived
/// </summary>
public class RepairReceived
{
    public RepairReceived(string con)
    {
        conString = con;
    }

    public string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable InstrumentsTable;
    public DataTable GetAllInstrumentRepairReceived(string type, string insid, string modelno,string compcode)
    {

        if (type == "" || type == "0")
            type = "null";
        else
            type = "'" + type + "'";

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

        theCommand.CommandText = "exec [sp_OT_InstrumentRepairReceived] " + type + "," + insid + "," + modelno + ",'" + compcode + "'";
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

    public DataTable DropdownInstrumentTye()
    {
        //Connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("select * from OT_InstrumentType where status=1", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);

        theConnection.Dispose();
        theAdapter.Dispose();


        return dt;
    }
    public bool InsertOTInstrumentsRepairReceived(string InstrumentId, string ModelNo, string ReceivedDate, string CreatedBy)
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
                        theCommand.CommandText = "INSERT INTO OT_InstrumentRepairReceive(InstrumentId,ModelNo,ReceivedDate,CreatedBy) VALUES ('" + InstrumentId + "','" + ModelNo + "','" + ReceivedDate + "','" + CreatedBy + "')";
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
    public bool UpdateOTInstrumentsRepairReceived(string RowId, string InstrumentId, string ModelNo, string ReceivedDate)
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
                        theCommand.CommandText = "Update OT_InstrumentRepairReceive set InstrumentId='" + InstrumentId + "', ModelNo='" + ModelNo + "',ReceivedDate='" + ReceivedDate + "'   where RowId = '" + RowId + "'";
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
    public bool DeleteOTInstrumentsRepairReceived(string RowId)
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
                        theCommand.CommandText = "delete OT_InstrumentRepairReceive  WHERE RowId = " + RowId + "";
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