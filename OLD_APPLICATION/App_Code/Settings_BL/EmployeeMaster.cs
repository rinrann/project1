using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for EmployeeMaster12
/// </summary>
public class EmployeeMaster
{
    public EmployeeMaster(string con)
    {
        conString = con;
    }

    public string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable EmployeeTable;
    public DataTable GetAllEmployee(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT *,convert(varchar,T1.JoiningDate,120) jdate,convert(varchar,T1.LeavingDate,120) ldate,convert(varchar,T1.dob,120) dobdt FROM GN_Gender g,GN_Religion r, GN_EmployeeMaster T1,GN_DesignationMaster T2 WHERE r.ID=T1.Religion and g.ID=t1.Sex and T1.DesignationID=T2.DesignationID AND T1.status=1 and T1.compcode='" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        EmployeeTable = new DataTable();
        theAdapter.Fill(EmployeeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return EmployeeTable;
    }
    public int GetEmployeeID()
    {
        //Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        //Command.
        theAdapter = new SqlDataAdapter("SELECT max(EmployeeID) as EmployeeID FROM GN_EmployeeMaster Where Status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int EmployeeID = 0;
        if (ds.Tables[0].Rows[0]["EmployeeID"] == DBNull.Value)
        {
            EmployeeID = 1;
        }
        else
        {
            EmployeeID = Convert.ToInt32(ds.Tables[0].Rows[0]["EmployeeID"]) + 1;
        }

        theConnection.Dispose();
        theAdapter.Dispose();
        return EmployeeID;
    }
    public DataTable DropdownID()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select DesignationId,DesignationName from  GN_DesignationMaster where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        EmployeeTable = new DataTable();
        theAdapter.Fill(EmployeeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return EmployeeTable;
    }


    public DataTable DropdownGender()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Gender where Status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        EmployeeTable = new DataTable();
        theAdapter.Fill(EmployeeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return EmployeeTable;
    }

    public DataTable DropdownReligion()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Religion where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        EmployeeTable = new DataTable();
        theAdapter.Fill(EmployeeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return EmployeeTable;
    }

    public DataTable DropdownState()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_StateMaster where Status=1 order by State_Name desc ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        EmployeeTable = new DataTable();
        theAdapter.Fill(EmployeeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return EmployeeTable;
    }

    public DataTable GenerateStaffID(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_GenerateStaffID '" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        EmployeeTable = new DataTable();
        theAdapter.Fill(EmployeeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return EmployeeTable;
    }

    public bool InsertEmployeeMaster(string compcode, string EmployeeName, string DesignationID, string Sex, string Address, string City, string State, string PhoneNo_1, string PhoneNo_2, string JoiningDate, string Age, string Nationality, string Religion, string LeavingDate,string dob, string LoginUser,string Empno)
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
                        theCommand.CommandText = "exec sp_GenerateLedgerId  '" + compcode + "','S'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        string ledgerid = theReader1[0].ToString();
                        theReader1.Close();

                        theCommand.CommandText = "insert into AC_Ledger (compcode,LedgerID,LedgerFK,LedgerName,LedgerType,CreatedDate,ActiveStatus)  values('" + compcode + "','" + ledgerid + "','" + Empno + "','" + EmployeeName + "','S',getdate(),1)";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "INSERT INTO GN_EmployeeMaster(compcode,EmployeeName, DesignationID, Sex, Address, City, State, PhoneNo_1,PhoneNo_2, JoiningDate, Age, Nationality, Religion, LeavingDate, CreatedBy,Status,dob,Empno) VALUES ('" + compcode + "', '" + EmployeeName + "', '" + DesignationID + "', '" + Sex + "','" + Address + "','" + City + "','" + State + "','" + PhoneNo_1 + "', '" + PhoneNo_2 + "', '" + JoiningDate + "','" + Age + "','" + Nationality + "','" + Religion + "', '" + LeavingDate + "', '" + LoginUser + "', 1,'" + dob + "','" + Empno + "')";
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
    public bool UpdateEmployeeMaster(string compcode, string EmployeeID, string EmployeeName, string DesignationID, string Sex, string Address, string City, string State, string PhoneNo_1, string PhoneNo_2, string JoiningDate, string Age, string Nationality, string Religion, string LeavingDate,string dob)
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
                        theCommand.CommandText = "Update GN_EmployeeMaster set  EmployeeName='" + EmployeeName + "', DesignationID='" + DesignationID + "', Sex='" + Sex + "', Address='" + Address + "', City='" + City + "', State='" + State + "', PhoneNo_1='" + PhoneNo_1 + "', PhoneNo_2='" + PhoneNo_2 + "', JoiningDate='" + JoiningDate + "', Age='" + Age + "', Nationality='" + Nationality + "', Religion='" + Religion + "', LeavingDate='" + LeavingDate + "',dob='" + dob + "' where compcode='" + compcode + "' and EmployeeID = '" + EmployeeID + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute update query. 
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
            theConnection.Dispose();
            theCommand.Dispose();
        }
    }
    public bool DeleteEmployeeMaster(int EmployeeID)
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
                        theCommand.CommandText = "Update GN_EmployeeMaster set status=2 WHERE EmployeeID = " + EmployeeID + "";
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