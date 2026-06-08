using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_DayOff
/// </summary>
public class DC_DayOff
{
    public DC_DayOff(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


    public DataTable GridDayoff()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  *,CONVERT(varchar,DayoffDay,103) DayoffDay1  from dbo.DC_DayOff";
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

    public bool InsertDayOff(string DayoffDay, string DayoffReason)
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
            // string s = Session["userName"];
            theCommand.Connection = theConnection;

              using (IDbTransaction tran = theConnection.BeginTransaction())
                {
                    try
                    {
                        // transactional code...
                        using (  theCommand = theConnection.CreateCommand())
                        {
            theCommand.CommandText = "INSERT INTO DC_DayOff(DayoffDay, DayoffReason) VALUES('" + DayoffDay + "','" + DayoffReason + "')";
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

    public bool UpdateDayOff(string id, string DayoffDay, string DayoffReason)
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
            theCommand.CommandText = "update DC_DayOff set DayoffDay='" + DayoffDay + "' ,DayoffReason='" + DayoffReason + "'  where RowId = '" + id + "'";
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

    public bool DeleteDayOff(string id)
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
            theCommand.CommandText = "delete DC_DayOff  WHERE RowId='" + id + "'";
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