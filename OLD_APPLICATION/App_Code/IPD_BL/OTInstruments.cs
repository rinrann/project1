using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for OTInstruments12
/// </summary>
public class OTInstruments
{
    public OTInstruments(string con)
    {
        conString = con;
    }

    public string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable InstrumentsTable;
    public DataTable GetAllInstrument(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT oi.RowId,it.TypeName,mm.MName,oi.ManufacturingCompany,im.InstrumentName,oi.AMCPrice,oi.BillNo,oi.CreatedDate,oi.CurrentStatus,oi.InstrumentID,oi.InstrumentId InsNameId,oi.InstrumentType,oi.ModelNo,oi.PurchasePrice,oi.Quantity,oi.status,CONVERT(varchar,oi.PurchaseDate,103) pdate,CONVERT(varchar,oi.AMCCommencementDate,103) comdate,CONVERT(varchar,oi.AMCCompletionDate,103) pldate  FROM IPD_OTInstruments oi,OT_InstrumentType it,OT_InstrumentMaster im,dbo.PH_ManufactureMaster mm where oi.compcode=it.compcode and it.compcode=im.compcode and im.compcode=mm.compcode and mm.MCode=oi.ManufacturingCompany and oi.status=1 and oi.InstrumentId=im.RowID and oi.InstrumentType=it.TypeId and it.compcode='"+compcode+"'";
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

    public DataTable DropdownCompany(string compcode)
    {
        //Connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("select * from PH_ManufactureMaster where status=1 and compcode='"+compcode+"'", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);

        theConnection.Dispose();
        theAdapter.Dispose();
        return dt;
    }

    public DataTable DropdownInstrumentTye(string compcode)
    {
        //Connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("select * from OT_InstrumentType where status=1 and compcode='"+compcode+"'", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);

        theConnection.Dispose();
        theAdapter.Dispose();

        return dt;
    }
    public DataTable DropdownInstrumentName(string compcode,string Type)
    {
        //Connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        if (Type == "0")
            theAdapter = new SqlDataAdapter("select * from OT_InstrumentMaster where Status=1 and compcode='"+compcode+"' and ins_type='T'", theConnection);
        else
            theAdapter = new SqlDataAdapter("select * from OT_InstrumentMaster where InstrumentType='" + Type + "' and Status=1 and compcode='" + compcode + "' and ins_type='T'", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);

        theConnection.Dispose();
        theAdapter.Dispose();
        return dt;
    }
    public bool InsertOTInstruments(string InstrumentType, string ModelNo, string BillNo, string Quantity, string InstrumentName, string ManufacturingCompany, string PurchasePrice, string AMCPrice, string PurchaseDate, string AMCCommencementDate, string AMCCompletionDate, string LoginUser,string compcode)
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
                        theCommand.CommandText = "INSERT INTO IPD_OTInstruments(compcode,InstrumentType,ModelNo,BillNo,Quantity,InstrumentId, ManufacturingCompany, PurchasePrice, AMCPrice,  PurchaseDate, AMCCommencementDate, AMCCompletionDate, CreatedBy,Status,user01,logdt01) VALUES ('"+compcode+"','" + InstrumentType + "','" + ModelNo + "','" + BillNo + "','" + Quantity + "', '" + InstrumentName + "','" + ManufacturingCompany + "','" + PurchasePrice + "','" + AMCPrice + "', '" + PurchaseDate + "', '" + AMCCommencementDate + "','" + AMCCompletionDate + "', '" + LoginUser + "', 1,'"+LoginUser+"',GETDATE())";
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
    public bool UpdateOTInstruments(string InstrumentType, string ModelNo, string BillNo, string Quantity, string RowId, string InstrumentName, string ManufacturingCompany, string PurchasePrice, string AMCPrice, string PurchaseDate, string AMCCommencementDate, string AMCCompletionDate,string compcode,string user)
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
                        theCommand.CommandText = "Update IPD_OTInstruments set  InstrumentType='" + InstrumentType + "',ModelNo='" + ModelNo + "', BillNo='" + BillNo + "',Quantity='" + Quantity + "', InstrumentId='" + InstrumentName + "', ManufacturingCompany='" + ManufacturingCompany + "', PurchasePrice='" + PurchasePrice + "', AMCPrice='" + AMCPrice + "', PurchaseDate='" + PurchaseDate + "', AMCCommencementDate='" + AMCCommencementDate + "', AMCCompletionDate='" + AMCCompletionDate + "',user02='"+user+"',logdt02=GETDATE()  where RowId = '" + RowId + "' and compcode='"+compcode+"'";
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
    public bool DeleteOTInstruments(string RowId,string compcode)
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
                        theCommand.CommandText = "Update IPD_OTInstruments set status=2 WHERE RowId = " + RowId + " and compcode='"+compcode+"'";
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