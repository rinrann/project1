using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PrescriptionTemplate123
/// </summary>
public class PrescriptionTemplate
{
	public PrescriptionTemplate(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable PrescriptionTemplateTable;

    public int GetPrescrpTemID()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("SELECT ISNULL(max(PrescrpTemID),0)+1 as PrescrpTemID FROM IPD_PrescriptionTmplate Where status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int PrescrpTemID = 0;
        if (ds.Tables[0].Rows[0]["PrescrpTemID"] == DBNull.Value)
        {
            PrescrpTemID = 1;
        }

        else
        {
            PrescrpTemID = Convert.ToInt32(ds.Tables[0].Rows[0]["PrescrpTemID"]) + 1;
        }

        theConnection.Dispose();
        theAdapter.Dispose();

        return PrescrpTemID;
    }
    public DataTable DropdownID1()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select PrescrpTemID,PrescrpTemName from  IPD_PrescriptionTmplate where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PrescriptionTemplateTable;
    }
    public DataTable DropdownID2()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineGroupID,MedicineGroupName from  IPD_MedicineGroup where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PrescriptionTemplateTable;
    }
    public DataTable DropdownID3(string MedicineGroupID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1 AND MedicineGroupID='" + MedicineGroupID + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PrescriptionTemplateTable;
    }
    public DataTable DropdownID4()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select RouteID,RouteName from  IPD_MedicineRoute where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PrescriptionTemplateTable;
    }


    public DataTable DropdownTempalteGroup()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select RouteID,RouteName from  IPD_PrescriptionTmplate where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PrescriptionTemplateTable;
    }
    public bool CheckIfTemplateExists(string tempName, string prescrpTemID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT COUNT(*) FROM IPD_PrescriptionTmplate WHERE PrescrpTemName='" + tempName + "' AND PrescrpTemID<>" + prescrpTemID;
        theCommand.CommandType = CommandType.Text;
        int count = (int)theCommand.ExecuteScalar();


        //Clean up ...............................

        theConnection.Dispose();
        theCommand.Dispose(); 

        return count > 0;
    }
    public bool InsertPrescriptionDetails(string PrescrpTemName, string CreatedDate, string LoginUser)
    {
        int effectedRows = 0;
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
            theCommand.CommandText = "INSERT INTO IPD_PrescriptionTmplate(PrescrpTemName, CreatedBy,CreatedDate, status) VALUES ('" + PrescrpTemName + "', '" + LoginUser + "','" + CreatedDate + "', 1)";
            theCommand.Transaction = tran as SqlTransaction;
            effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.
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

    public bool InsertPrescriptionMapping(string MedicineGroupID, string MedicineID, string RouteID, string DailyDose, string Duration, string CreatedBy, string CreatedDate)
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
            theCommand.CommandText = "INSERT INTO IPD_PrescriptionTmplateMapping(PrescrpTemID,MedicineGroupID,MedicineID,RouteID,DailyDose,Duration,CreatedBy,CreatedDate,status) VALUES ((SELECT max(PrescrpTemID) FROM IPD_PrescriptionTmplate), '" + MedicineGroupID + "', '" + MedicineID + "', '" + RouteID + "', '" + DailyDose + "', '" + Duration + "',  '" + CreatedBy + "','" + CreatedDate + "', 1)";
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




    public bool DeleteMap(string id)
    {
        int effectedRows = 0;
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
            theCommand.CommandText = "delete from IPD_PrescriptionTmplateMapping where PrescrpTemID='" + id + "'";
             theCommand.Transaction = tran as SqlTransaction;
            effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.
             
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


    public DataTable GetPrescriptionTemplateDetails(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.IPD_PrescriptionTmplateMapping map where  map.PrescrpTemID='" + id + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PrescriptionTemplateTable;
    }
}