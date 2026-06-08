using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for CostCentre
/// </summary>
public class CostCentre
{
    public CostCentre(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;


    public DataTable GetCostCentre(string cocode,string yearcode)
    {
        DataTable CostTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "Select COMPCODE,COSTCODE,CCNAME,CFILLER02, CASE CFILLER02 WHEN 'I' THEN 'IPD' WHEN 'O' THEN 'OPD' WHEN 'T' THEN 'OT' WHEN 'P' THEN 'PATHOLOGY' WHEN 'U' THEN 'USG' WHEN 'X' THEN 'XRAY' WHEN 'D' THEN 'DIALYSIS' ELSE '' END TYPE from ccmast where compcode='" + cocode + "'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        CostTable = new DataTable();
        theAdapter.Fill(CostTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return CostTable;
    }

    public Int32 CheckCostCode(string costcode, string cocode, string yearcode)
    {


        Int32 rec;
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
                        theCommand.CommandText = "Select count(COSTCODE) rec from CCMAST where compcode='" + cocode + "'  and COSTCODE='" + costcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        rec = int.Parse(theReader[0].ToString());
                        theReader.Close();

                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    theConnection.Dispose();
                    theCommand.Dispose();
                }
            }
            return rec;
        
        
            
    }

    public bool InsertCostCentre(string costcode,string name,string type,string cocode,string yearcode,string user,string date)
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
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "INSERT INTO CCMAST(COMPCODE,COSTCODE,CCNAME,CFILLER01, CFILLER02, USER01, LOGDT01,tag) VALUES ('" + cocode + "','" + costcode + "','" + name + "','" + yearcode + "', '" + type + "', '" + user + "', '" + date + "','1')";
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

    public bool UpdateCostCentre(string costcode, string name, string type, string cocode, string yearcode, string user, string date)
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
                        theCommand.CommandText = "Update CCMAST set CCNAME='" + name + "', CFILLER02='" + type + "',USER02='" + user + "',LOGDT02='"+date+"' where COMPCODE = '" + cocode + "' and COSTCODE='" + costcode + "'";
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }

    public bool DeleteCostCentre(string costcode, string cocode, string yearcode)
    {
        try
        {
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }

            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "Delete from CCMAST where COMPCODE='" + cocode + "' and COSTCODE='" + costcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();
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