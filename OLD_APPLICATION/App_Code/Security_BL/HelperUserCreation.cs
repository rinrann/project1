using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for UserCreation
/// </summary>
public class HelperUserCreation
{
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable UserDetailsTable;
    private SqlDataReader theReader;
    //private DataTable UserTable;
    public HelperUserCreation(string connectionString)
    {
        conString = connectionString;
    }
    public DataTable GetAllUser(string CompCode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT isNull(AdminUser,'0') AdminUser,case isNull(AdminUser,'0') when '0' then 'No' else 'Yes' end as AdminUserText,*,CONVERT(VARCHAR,ExpiryDate,103) ExDate FROM GN_UserDetails T1,GN_UserRole T2 WHERE T1.compcode=T2.compcode and T1.UserRoleID=T2.UserRoleID AND T1.[Status]=1 and T1.CompCode='"+ CompCode +"' order by UserName asc";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        UserDetailsTable = new DataTable();
        theAdapter.Fill(UserDetailsTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return UserDetailsTable;
    }
    public DataTable DropdownID()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select UserRoleID,UserRoleName from  GN_UserRole where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        UserDetailsTable = new DataTable();
        theAdapter.Fill(UserDetailsTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return UserDetailsTable;
    }
    public bool CheckUser(string UName)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "SELECT * FROM GN_UserDetails where UserId='" + UName + "'";
        theCommand.CommandType = CommandType.Text;

        // Reader
        theReader = theCommand.ExecuteReader();
        theReader.Read();


        if (theReader.HasRows == true)
        {
             return true;
        }
        else
        {
            return false;
        }
    }

    public bool ChangePassword(string UserName, string NewPassword)
    {
        int effectedRows = 0;
        try
        {

            string conString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            SqlConnection theConnection;
           
            theConnection = new SqlConnection();
            theConnection.ConnectionString = conString;
            theConnection.Open();

            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "update GN_UserDetails set Password='" + NewPassword + "' where UserId='" + UserName.ToString() + "'";
            theCommand.CommandType = CommandType.Text;

            effectedRows = theCommand.ExecuteNonQuery();
            if (effectedRows > 0)
                return true;
            else
                return false;


        }
        catch
        {
            return false;
        }
        //finally
        //{
        //    theConnection.Close();
        //}
    }
    public bool InsertUserCreation(string UserID, string UserName, string UserRoleID, string Password, string PhoneNo_1, string PhoneNo_2, string EmailId, string ExpiryDate, string LoginUser, string cocode, string adminflag)
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
            theCommand.CommandText = "INSERT INTO GN_UserDetails(compcode,CreateDate,UserID, UserName, UserRoleID, Password, PhoneNo_1,PhoneNo_2, EmailId, CreatedBy, ExpiryDate, Status,AdminUser) VALUES ('" + cocode + "',convert(date,getdate(),103),'" + UserID + "', '" + UserName + "', '" + UserRoleID + "', '" + Password + "', '" + PhoneNo_1 + "', '" + PhoneNo_2 + "', '" + EmailId + "', '" + LoginUser + "', convert(date,'" + ExpiryDate + "',103), 1,'" + adminflag + "')";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.
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
    public bool UpdateUserCreation(string UserID, string UserName, string UserRoleID, string Password, string PhoneNo_1, string PhoneNo_2, string EmailId, string ExpiryDate,string cocode,string adminflag)
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
            theCommand.CommandText = "Update GN_UserDetails set  UserName='" + UserName + "', UserRoleID='" + UserRoleID + "', Password='" + Password + "', PhoneNo_1='" + PhoneNo_1 + "', PhoneNo_2='" + PhoneNo_2 + "', EmailId='" + EmailId + "', ExpiryDate=convert(date,'" + ExpiryDate + "',103),AdminUser='" + adminflag + "'  where UserID = '" + UserID + "'and compcode='" + cocode + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute update query.
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
    public bool DeleteUserCreation(string  UserID,string cocode)
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
            theCommand.CommandText = "Update GN_UserDetails set status=2 WHERE UserID = '" + UserID + "'and compcode='" + cocode + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute Delete query.
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