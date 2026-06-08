using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DoctorSpecialty12
/// </summary>
public class DoctorSpecialty
{
    public DoctorSpecialty(string con)
    {
        conString = con;
    }

    public string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable SpecialtyTable;
    public DataTable GetAllSpecialty(string cocode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM GN_DoctorSpecialty where Status=1 AND Compcode='"+cocode+"' order by SpecialtyName asc";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        SpecialtyTable = new DataTable();
        theAdapter.Fill(SpecialtyTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return SpecialtyTable;
    }
    public int GetSpecialtyID()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("SELECT max(SpecialtyID) as SpecialtyID FROM GN_DoctorSpecialty Where status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int SpecialtyID = 0;
        if (ds.Tables[0].Rows[0]["SpecialtyID"] == DBNull.Value)
        {
            SpecialtyID = 1;
        }

        else
        {
            SpecialtyID = Convert.ToInt32(ds.Tables[0].Rows[0]["SpecialtyID"]) + 1;
        }

        //clear 
        theAdapter.Dispose();
        theConnection.Dispose();

        return SpecialtyID;
    }
    public bool InsertDoctorSpecialty(string SpecialtyName, string LoginUser,string cocode)
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
                        theCommand.CommandText = "INSERT INTO GN_DoctorSpecialty(compcode,SpecialtyName, CreatedBy, status) VALUES ('"+cocode+"','" + SpecialtyName + "', '" + LoginUser + "', 1)";
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

    public bool UpdateDoctorSpecialty(string SpecialtyID, string SpecialtyName, string cocode)
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
                        theCommand.CommandText = "Update GN_DoctorSpecialty set SpecialtyName='" + SpecialtyName + "' where SpecialtyID = '" + SpecialtyID + "'and compcode='"+cocode+"'";
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
    public bool DeleteDoctorSpecialty(int SpecialtyID, string cocode)
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
                        theCommand.CommandText = "Update GN_DoctorSpecialty set  status=2 WHERE SpecialtyID = '" + SpecialtyID + "'and compcode='"+cocode+"'";
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