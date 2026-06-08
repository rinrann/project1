using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for RoomMaster12
/// </summary>
public class RoomMaster
{
    public RoomMaster(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable RoomTable;

    public DataTable GetAllRoom(string cocode)
    {


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM IPD_RoomMaster T1,IPD_FloorMaster T2,IPD_WingsMaster T3,IPD_RoomType T4 WHERE T1.FloorID=T2.FloorID AND T1.WingsID=T3.WingsID AND T1.RoomCategoryID=T4.RoomCategoryID AND T1.[Status]=1  order by RoomName asc";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        RoomTable = new DataTable();
        theAdapter.Fill(RoomTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return RoomTable;
    }
    public int GetRoomID()
    {
        //Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        //Command.
        theAdapter = new SqlDataAdapter("SELECT max(RoomID) as RoomID FROM IPD_RoomMaster Where Status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int RoomID = 0;
        if (ds.Tables[0].Rows[0]["RoomID"] == DBNull.Value)
        {
            RoomID = 1;
        }
        else
        {
            RoomID = Convert.ToInt32(ds.Tables[0].Rows[0]["RoomID"]) + 1;
        }


        // Clean up.
        theConnection.Dispose();
        theAdapter.Dispose();

        return RoomID;
    }
    public DataTable DropdownID1(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select FloorID,FloorName from  IPD_FloorMaster where status=1 and compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        RoomTable = new DataTable();
        theAdapter.Fill(RoomTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();


        return RoomTable;
    }
    public DataTable DropdownID2(int FloorID,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (FloorID == 0)
            theCommand.CommandText = "select WingsID,WingsName from  IPD_WingsMaster where status=1 and compcode='"+compcode+"'";
        else
            theCommand.CommandText = "select WingsID,WingsName from  IPD_WingsMaster where status=1 AND FloorID='" + FloorID+"' and compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        RoomTable = new DataTable();
        theAdapter.Fill(RoomTable); // Fill data into data table.


        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();


        return RoomTable;
    }

    public DataTable DropdownID3(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select RoomCategoryID,RoomCategoryName from  IPD_RoomType where status=1 and compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        RoomTable = new DataTable();
        theAdapter.Fill(RoomTable); // Fill data into data table.


        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();


        return RoomTable;
    }
    public bool InsertRoomMaster(string RoomName, string FloorID, string WingsID, string RoomCategoryID, string PatternText, string LoginUser, string date,string cocode)
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
                        theCommand.CommandText = "INSERT INTO IPD_RoomMaster(compcode,RoomName, FloorID, WingsID, RoomCategoryID, PatternText, CreatedBy, Status,CreatedDate) VALUES ('"+cocode+"','" + RoomName + "', '" + FloorID + "', '" + WingsID + "', '" + RoomCategoryID + "', '" + PatternText + "', '" + LoginUser + "', 1,'" + date + "')";
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
    public bool UpdateRoomMaster(string RoomID, string RoomName, string FloorID, string WingsID, string RoomCategoryID, string PatternText, string date, string cocode)
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
                        theCommand.CommandText = "Update IPD_RoomMaster set CreatedDate='" + date + "', RoomName='" + RoomName + "', FloorID='" + FloorID + "', WingsID='" + WingsID + "', RoomCategoryID='" + RoomCategoryID + "', PatternText='" + PatternText + "' where RoomID = '" + RoomID + "'and compcome='"+cocode+"'";
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
    public bool DeleteRoomMaster(int RoomID, string cocode)
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
                        theCommand.CommandText = "delete IPD_RoomMaster WHERE RoomID= '" + RoomID + "'and compcode='"+cocode+"'";
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