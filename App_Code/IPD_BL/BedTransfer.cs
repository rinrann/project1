using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BedTransfer12
/// </summary>
public class BedTransfer
{
    public BedTransfer(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;
    public DataTable GetPatientDetails(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select b.BedNo,pr.PatientReg,pr.patient_name,pr.age,bm.BedNoText from dbo.IPD_BedAllocation b,dbo.GN_PatientReg pr,IPD_BedMaster bm where b.compcode=bm.compcode and bm.compcode=pr.compcode and bm.BedNo=b.BedNo and b.PatientReg=pr.PatientReg and pr.PatientReg='" + regno + "' and b.compcode='"+compcode+"' and b.ToDate is null"; 
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
    public int GetBedNo()
    {
        //Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        //Command.
        theAdapter = new SqlDataAdapter("SELECT max(BedNo) as BedNo FROM IPD_BedMaster", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int BedNo = 0;
        if (ds.Tables[0].Rows[0]["BedNo"] == DBNull.Value)
        {
            BedNo = 1;
        }
        else
        {
            BedNo = Convert.ToInt32(ds.Tables[0].Rows[0]["BedNo"]) + 1;
        }

        theConnection.Dispose();
        theAdapter.Dispose();

        return BedNo;
    }
    public DataTable DropdownID1()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select FloorID,FloorName from  IPD_FloorMaster where status=1";
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
    public DataTable DropdownID2(int FloorID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select WingsID,WingsName from  IPD_WingsMaster where status=1 AND FloorID=" + FloorID;
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
    public DataTable GridFill(string regno,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pr.patient_name,ba.RowId,ba.BedNo BNo,pr.age,CONVERT(varchar,ba.FromDate,103) fromdate,ba.FromTime,CONVERT(varchar,ba.ToDate,103) todate,ba.ToTime,b.BedNoText BedNo from IPD_BedAllocation ba,GN_PatientReg pr,IPD_BedMaster b where ba.compcode=b.compcode and b.compcode=pr.compcode and pr.PatientReg=ba.PatientReg and pr.LedgerId=ba.LedgerId and ba.BedNo=b.BedNo and ba.FromDate>=pr.AdmissionDate and ba.PatientReg='" + regno + "' and pr.compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable RoomTable = new DataTable();
        theAdapter.Fill(RoomTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return RoomTable;
    }
    public DataTable DropdownID3()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select RoomCategoryID,RoomCategoryName from  IPD_RoomType where status=1";
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
    public DataTable DropdownID4(int RoomCategoryID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select RoomID,RoomName from  IPD_RoomMaster where status=1 AND RoomCategoryID=" + RoomCategoryID;
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


    public DataTable DropdownBed(string RoomID, string wing, string floor, string category)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.IPD_BedMaster b where b.Allotted=0 and b.FloorID='" + floor + "' and b.WingsID='" + wing + "' and b.RoomCategoryID='" + category + "' and b.RoomID='" + RoomID + "' and b.status=1";
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
    public bool InsertBedMaster(string regno, string current, string transferingbed, string date, string time,string compcode,string user)
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
                        string LedgerId = "";

                        theCommand.CommandText = "select LedgerID from AC_Ledger al where al.compcode='"+compcode+"' and LedgerFK='" + regno + "' and ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();


                        theCommand.CommandText = "update IPD_BedAllocation set ToDate='" + date + "', ToTime='" + time + "',user02='"+user+"',logdt02=GETDATE() where LedgerId='" + LedgerId + "'  and BedNo='" + current + "' and PatientReg='" + regno + "' and compcode='"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update IPD_BedMaster set Allotted=0,user02='"+user+"',logdt02=GETDATE() where BedNo='" + current + "' and compcode='"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "INSERT INTO IPD_BedAllocation(compcode,LedgerId,PatientReg, BedNo, FromDate, FromTime,user01,logdt01) VALUES ('"+compcode+"','" + LedgerId + "','" + regno + "', '" + transferingbed + "', '" + date + "', '" + time + "','"+user+"',GETDATE())";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "update IPD_BedMaster set Allotted=1,user02='" + user + "',logdt02=GETDATE() where BedNo='" + transferingbed + "' and compcode='"+compcode+"'";
                        theCommand.CommandType = CommandType.Text;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "update GN_PatientReg set bedallocation='" + transferingbed + "',user02='" + user + "',logdt02=GETDATE() where PatientReg='" + regno + "' and compcode='"+compcode+"'";
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }

    public bool UpdateBedMaster(string RowId,string regno, string current, string transferingbed,string compcode,string user)
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
            string LedgerId = "";
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
                        theCommand.CommandText = "select LedgerID from AC_Ledger al where al.compcode='"+compcode+"' and LedgerFK='" + regno + "' and ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        theCommand.CommandText = "update IPD_BedAllocation set LedgerId='" + LedgerId + "', BedNo='" + transferingbed + "',user02='"+user+"',logdt02=GETDATE() where RowId='" + RowId + "' and compcode='"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update IPD_BedMaster set Allotted=0,user02='" + user + "',logdt02=GETDATE() where BedNo='" + current + "' and compcode='"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "update IPD_BedMaster set Allotted=1,user02='" + user + "',logdt02=GETDATE() where BedNo='" + transferingbed + "' and compcode='"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();
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

    public bool DeleteBedMaster(int BedNo)
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
                        theCommand.CommandText = "Update IPD_BedMaster set status=2 WHERE BedNo= " + BedNo + "";
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
            theCommand.Dispose();
            theConnection.Dispose();
        }
    }
}