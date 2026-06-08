using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for MD_MedicineGroup12
/// </summary>
public class MD_MedicineGroup
{
    public MD_MedicineGroup(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable MedicineTable;
    public DataTable GetAllMedicineGroup(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM IPD_MedicineGroup where Status=1 and compcode='"+ compcode +"' order by MedicineGroupName asc";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    }

    public int GetMedicineGroupID()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("SELECT max(MedicineGroupID) as MedicineGroupID FROM IPD_MedicineGroup Where status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int MedicineGroupID = 0;
        if (ds.Tables[0].Rows[0]["MedicineGroupID"] == DBNull.Value)
        {
            MedicineGroupID = 1;
        }

        else
        {
            MedicineGroupID = Convert.ToInt32(ds.Tables[0].Rows[0]["MedicineGroupID"]) + 1;
        }

        // Clean up.
        theConnection.Dispose();
        theAdapter.Dispose();

        return MedicineGroupID;
    }
    public bool InsertMedicineGroup(string MedicineGroupName, string LoginUser,string cocode)
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
                        theCommand.CommandText = "select * from IPD_MedicineGroup where MedicineGroupName='" + MedicineGroupName + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows)
                        {
                            theReader.Close();
                            return false;
                        }
                        else
                        {
                            theReader.Close();
                            theCommand.CommandText = "INSERT INTO IPD_MedicineGroup(compcode,MedicineGroupName, CreatedBy, status) VALUES ('"+cocode+"','" + MedicineGroupName + "', '" + LoginUser + "', 1)";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
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
    public bool UpdateMedicineGroup(string MedicineGroupID, string MedicineGroupName,string cocode)
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

                        theCommand.CommandText = "select * from IPD_MedicineGroup where MedicineGroupName='" + MedicineGroupName + "' AND MedicineGroupID != '" + MedicineGroupID + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows)
                        {
                            theReader.Close();
                            return false;
                        }
                        else
                        {
                            theReader.Close();
                            theCommand.CommandText = "Update IPD_MedicineGroup set MedicineGroupName='" + MedicineGroupName + "'  where MedicineGroupID = '" + MedicineGroupID + "'and compcode='" + cocode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute update query.
                        }
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
    public bool DeleteMedicineGroup(int MedicineGroupID,string cocode)
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
                        theCommand.CommandText = "delete IPD_MedicineGroup WHERE MedicineGroupID = '" + MedicineGroupID + "'and compcode='" + cocode + "'";
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