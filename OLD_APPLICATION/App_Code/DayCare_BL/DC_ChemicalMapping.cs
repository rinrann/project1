using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_ChemicalMapping12
/// </summary>
public class DC_ChemicalMapping
{
	public DC_ChemicalMapping(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;
    private DataTable diano;
    private DataTable regno;

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

    public DataTable GridChemicalDetails(string TypeId, string DialysisModuleID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

     // Command.
        theCommand = new SqlCommand();

        string sql;
        sql = "select mm.MedicineID,mm.MedicineName,cl.PatientCover,cl.UsedFor,u.UnitName  from dbo.DC_ChemicalList cl,IPD_MedicineMaster mm,GN_UnitMaster u where u.UnitId=cl.UnitId    and cl.ChemicalName=mm.MedicineID AND cl.status=1 AND mm.status=1 AND u.Status=1";
        SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
        DataTable dt = new DataTable();
        da.Fill(dt);

        // Clean up.
            theConnection.Dispose();
            theCommand.Dispose();
            da.Dispose();
        return dt;
 
    }



    public DataTable GridChemicalDatabound(string TypeId, string DialysisModuleID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();

        string sql;
        sql = "select ChemId from dbo.DC_ChemicalMap where TYPEID='" + TypeId + "' and DialyserId='" + DialysisModuleID + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
        DataTable dt = new DataTable();
        da.Fill(dt);

        // Clean up. 
            theConnection.Dispose();
            theCommand.Dispose();
            da.Dispose();

        return (dt);
    }

   
    public DataTable DropdownDialyserName(int id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.DC_DialysisCharge c where c.Status=1 and TypeId='" + id + "'";
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
                        theCommand.CommandText = "delete from dbo.DC_ChemicalMap where TypeId='" + TypeId + "' and DialyserId='" + DialysisModuleID + "'and compcode='" + cocode + "'";
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
                       // theCommand.CommandText = "insert into dbo.DC_ChemicalMap values('"+cocode+"','" + TypeId + "','" + DialysisModuleID + "','" + ChemId + "')";
                        theCommand.CommandText = "insert into dbo.DC_ChemicalMap(compcode,TypeId,DialyserId,ChemId) values('" + cocode + "','" + TypeId + "','" + DialysisModuleID + "','" + ChemId + "')";
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