using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_DisposalMapping123
/// </summary>
public class DC_DisposalMapping
{
    public DC_DisposalMapping(string con)
    {
        conString = con;
    }



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable; 


    public DataTable DropdownDialysertype()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select t.TypeId,t.TypeName from dbo.DC_DialysisType t WHERE t.Status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable GridDisposalDetails(string TypeId, string DialysisModuleID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();

        string sql;
        sql = "select mm.MedicineID,mm.MedicineName,cl.PatientCover,cl.UsedFor,u.UnitName from dbo.DC_DisposableList cl,IPD_MedicineMaster mm ,GN_UnitMaster u where u.UnitId=cl.UnitId  and cl.ItemName=mm.MedicineID AND cl.status=1 AND mm.status=1 AND u.Status=1";
        SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
        DataTable dt = new DataTable();
        da.Fill(dt);

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose(); 
        return dt;
    }


    public DataTable GridDisposalDatabound(string TypeId, string DialysisModuleID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();

        string sql;
        sql = "select DisposalId from dbo.DC_DisposalMap where TYPEID='" + TypeId + "' and DialyserId='" + DialysisModuleID + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
        DataTable dt = new DataTable();
        da.Fill(dt);

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        da.Dispose();
        return (dt);
    }

    public DataTable GridChemicalRightDetails(string TypeId, string DialysisModuleID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();

        string sql;
        sql = "select mm.MedicineID,mm.MedicineName from dbo.DC_DisposableList cl,IPD_MedicineMaster mm , DC_DisposalMap map where map.DisposalId=mm.MedicineID and map.TYPEID='" + TypeId + "' and map.DialyserId='" + DialysisModuleID + "' and cl.ItemName=mm.MedicineID and cl.ItemName  in (select DisposalId from dbo.DC_DisposalMap where TYPEID='" + TypeId + "' and DialyserId='" + DialysisModuleID + "')  group by mm.MedicineID,mm.MedicineName";
        SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count == 0)
        {
            sql = "select mm.MedicineID,mm.MedicineName from dbo.DC_DisposableList cl,IPD_MedicineMaster mm  where  cl.ItemName=mm.MedicineID and cl.ItemName  in (select DisposalId from dbo.DC_DisposalMap where TYPEID=" + TypeId + " and DialyserId=" + DialysisModuleID + ")  group by mm.MedicineID,mm.MedicineName";
            da = new SqlDataAdapter(sql, theConnection);
            dt = new DataTable();
            da.Fill(dt);
        }

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        da.Dispose();
        return dt;
    }


    public DataTable DropdownDialyserName(int id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select c.DialysisName,c.ID from dbo.DC_DialysisCharge c where c.Status=1 and c.TypeId=" + id + " ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalTable;
    }

    public bool DeleteMapping(string TypeId, string DialysisModuleID,string cocode)
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
                        theCommand.CommandText = "delete from dbo.DC_DisposalMap where TypeId='" + TypeId + "' and DialyserId= '" + DialysisModuleID + "'and compcode='" + cocode + "'";
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

    public bool InsertMapping(string TypeId, string DialysisModuleID, string ChemId,string cocode)
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
                        theCommand.CommandText = "insert into dbo.DC_DisposalMap(compcode,TypeId,DialyserId,DisposalId) values('"+cocode+"','" + TypeId + "','" + DialysisModuleID + "','" + ChemId + "')";
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