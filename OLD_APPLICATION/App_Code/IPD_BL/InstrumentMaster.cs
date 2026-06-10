using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for InstrumentMaster
/// </summary>
public class InstrumentMaster
{
    public InstrumentMaster(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;
    public DataTable GetAllInstrument(string cat, string subcat,string compcode,string type)
    {

        // Connection.
        if (cat == "0" || cat == "")
        {
            cat = "%%";
        }
        
        if (subcat == "0" || subcat == "")
            subcat = "%%";
        
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;


        //theCommand.CommandText = "exec sp_IPD_InstrumentDetails  " + cat + "," + subcat + ",'"+compcode+"'";
        theCommand.CommandText = "select im.RowID,im.InstrumentName,sub.SubCategoryId,sub.SubCategoryName,im.InstrumentType,it.TypeName,im.ins_type,isnull(im.UnitCost,0) UnitCost from  OT_InstrumentMaster im,OT_InstrumentType it,dbo.OT_InstrumentSubGroup sub where im.COMPCODE=it.COMPCODE and it.COMPCODE=sub.COMPCODE and im.COMPCODE='"+compcode+"' and im.InstrumentType=it.TypeId and im.Status=1 and im.InstrumentSubCategory=sub.SubCategoryId and im.InstrumentType like '" + cat + "'  and im.InstrumentSubCategory like '" + subcat + "' and im.ins_type='" + type + "' union select im.RowID,im.InstrumentName,0 SubCategoryId,null SubCategoryName,im.InstrumentType,ty.TypeName,im.ins_type,isnull(im.UnitCost,0) UnitCost from  OT_InstrumentMaster im,OT_InstrumentType ty where im.COMPCODE=ty.COMPCODE and im.COMPCODE='"+compcode+"' and im.InstrumentSubCategory=0 and im.InstrumentType=ty.TypeId and im.InstrumentType like '"+cat+"' /*and im.InstrumentSubCategory like '"+subcat+"'*/ and im.ins_type='"+type+"'";
        
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


    public DataTable DropDownType(string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from OT_InstrumentType where status=1 and compcode='"+compcode+"'";

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

    public DataTable DropDownSubCategory(string compcode,string type)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "0")
            theCommand.CommandText = "select * from OT_InstrumentSubGroup WHERE Status=1 and compcode='"+compcode+"'";
        else
            theCommand.CommandText = "select * from OT_InstrumentSubGroup where TypeId='" + type + "' and Status=1 and compcode='" + compcode + "'";

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

    public string GetInstrumentID(string compcode)
    {
        //Connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("exec sp_OT_GenerateInstrumentCode '"+compcode+"'", theConnection);
        //theAdapter = new SqlDataAdapter("exec sp_OT_GenerateInstrumentCode", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);
        string InstrumentID;

        InstrumentID = dt.Rows[0][0].ToString();

        theConnection.Dispose();
        theAdapter.Dispose();

        return InstrumentID;
    }


    public bool InsertInstrumentMaster(string type,string InstrumentSubCategory, string RowID, string InstrumentType, string InstrumentName,string compcode, string CreatedBy,string cost)
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
                        theCommand.CommandText = "INSERT INTO OT_InstrumentMaster(CompCode,InstrumentSubCategory,RowID,InstrumentType,InstrumentName,Status, CreatedBy,User01,Logdt01,ins_type,UnitCost) VALUES ('" + compcode + "','" + InstrumentSubCategory + "','" + RowID + "','" + InstrumentType + "','" + InstrumentName + "',1,'" + CreatedBy + "','" + CreatedBy + "',GETDATE(),'" + type + "','"+cost+"')";
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
    public bool UpdateInstrumentMaster(string type,string InstrumentSubCategory, string RowID, string InstrumentType, string InstrumentName,string compcode,string user,string cost)
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
                        theCommand.CommandText = "update OT_InstrumentMaster set ins_type='" + type + "', InstrumentSubCategory='" + InstrumentSubCategory + "', InstrumentType='" + InstrumentType + "',InstrumentName='" + InstrumentName + "',user02='" + user + "',logdt02=GETDATE(),UnitCost='"+cost+"' where RowID='" + RowID + "' and compcode='" + compcode + "'";
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

    public bool DeleteInstrumentMaster(string RowID,string compcode)
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
                        theCommand.CommandText = "delete OT_InstrumentMaster  WHERE RowID= '" + RowID + "' and compcode='"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;

                        theCommand.ExecuteNonQuery(); // Execute Delete query.
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
            theConnection.Dispose();
            theCommand.Dispose();
        }
    }
}