using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BedMaster12
/// </summary>
public class BedMaster
{
    public BedMaster(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;
    public DataTable GetAllBed(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM IPD_BedMaster T1,IPD_FloorMaster T2,IPD_WingsMaster T3,IPD_RoomType T4,IPD_RoomMaster T5 WHERE T1.compcode=T2.compcode and T3.Compcode=T1.compcode and T4.compcode=T1.compcode and T5.compcode=T1.compcode and T1.FloorID=T2.FloorID AND T1.WingsID=T3.WingsID AND T1.RoomCategoryID=T4.RoomCategoryID AND T1.RoomID=T5.RoomID AND T1.[Status]=1 and T1.compcode='"+compcode+"' order by BedNo ";
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

    public DataTable GenerateBedNo()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_GenerateBEDNO";
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


    public DataTable DropdownID1(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  IPD_FloorMaster where status=1 and compcode='"+compcode+"'";
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

    public DataTable DropdownID2(int FloorID,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (FloorID == 0)
            theCommand.CommandText = "select * from  IPD_WingsMaster where status=1 and compcode='"+compcode+"'";
        else
            theCommand.CommandText = "select  * from  IPD_WingsMaster where status=1 AND FloorID='" + FloorID+"' and compcode='"+compcode+"'";
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

    public DataTable DropdownID3(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from  IPD_RoomType where status=1 and compcode='"+compcode+"'";

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

    public DataTable DropdownID4(string RoomCategoryID, string floor, string wing,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (RoomCategoryID == "0" && floor == "0" && wing == "0")
            theCommand.CommandText = "select * from  IPD_RoomMaster where status=1";
        else
            theCommand.CommandText = "select * from  IPD_RoomMaster rm where rm.status=1 AND rm.FloorID= '" + floor + "' and rm.WingsID='" + wing + "' and rm.RoomCategoryID='" + RoomCategoryID + "' and compcode='"+compcode+"'";
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

    public DataTable DropdownPattern1(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  IPD_FloorMaster where FloorID='" + id + "' and status=1";
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

    public DataTable DropdownPattern2(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  IPD_WingsMaster where WingsID='" + id + "' and status=1";
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

    public DataTable DropdownPattern3(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select * from  IPD_RoomType where RoomCategoryID='" + id + "' and status=1";

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

    public DataTable DropdownPattern4(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  IPD_RoomMaster where RoomID='" + id + "' and status=1";
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


    public bool InsertBedMaster(string FloorID, string WingsID, string RoomCategoryID, string RoomID, string PatternText, string LoginUser, string date, string bedtextno, string charges,string cocode)
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
                        theCommand.CommandText = "INSERT INTO IPD_BedMaster(compcode,BedNoText,Allotted,FloorID, WingsID, RoomCategoryID, RoomID, PatternText, CreatedBy, Status,CreatedDate,Charges) VALUES ('"+cocode+"','" + bedtextno + "',0,'" + FloorID + "', '" + WingsID + "', '" + RoomCategoryID + "', '" + RoomID + "', '" + PatternText + "', '" + LoginUser + "', 1,'" + date + "','" + charges + "')";
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
    public bool UpdateBedMaster(string BedNo, string FloorID, string WingsID, string RoomCategoryID, string RoomID, string PatternText, string date, string bedtextno, string charges, string cocode)
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
                        theCommand.CommandText = "Update IPD_BedMaster set Charges='" + charges + "', BedNoText='" + bedtextno + "',CreatedDate='" + date + "', FloorID='" + FloorID + "', WingsID='" + WingsID + "', RoomCategoryID='" + RoomCategoryID + "', RoomID='" + RoomID + "', PatternText='" + PatternText + "' where BedNo = '" + BedNo + "'and compcode='"+cocode+"'";
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
    public bool DeleteBedMaster(int BedNo, string cocode)
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
                        theCommand.CommandText = "Update IPD_BedMaster set status=0 WHERE BedNo= '" + BedNo + "'and compcode='"+cocode+"'";
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }
}