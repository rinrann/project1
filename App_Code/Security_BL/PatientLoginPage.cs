using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Enc;

/// <summary>
/// Summary description for LoginPage
/// </summary>
public class PatientLogin_Page
{
    public PatientLogin_Page(string con)
	{
        conString = con;
	}

    private string conString ;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataReader theReader;
    private SqlDataAdapter theAdapter;

    public DataTable Login(string UName)
    {
        DataTable dtUser;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM GN_UserDetails where UserId='" + UName + "'";

        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        dtUser = new DataTable();
        theAdapter.Fill(dtUser);

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return dtUser;

    }
    public DataTable getCompanies()
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theAdapter = new SqlDataAdapter("select LTrim(RTrim(COMPCODE))+'~'+LTrim(RTrim(yearcode))+'~'+LTrim(RTrim(coname)) as COMPCODE,compcode+' : '+coname+' : '+yearcode as CONAME from parms", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);
        return dt;

    }
    public bool Login(string UName, string Pwd, string compcode,string clintIp)
    {
        //conString = ConfigurationManager.ConnectionStrings["HMSConnectionString1"].ToString();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "SELECT * FROM GN_UserDetails where UserId='" + UName + "' and compcode='" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Reader
        theReader = theCommand.ExecuteReader();
        theReader.Read();

        //string adf = Enc.MAIN.SCrypt(Pwd,true);
        try
        {
            if (theReader.HasRows == true)
            {
                string password = theReader["password"].ToString();
                //string password_DE = EncryptionDecryption.CryptorEngine.DecryptData(password);
                //if (password == Pwd)
                if (password == Enc.MAIN.SCrypt(Pwd, true))
                {
                    theReader.Close();

                    
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            if (theReader.HasRows == true)
            {
                string password = theReader["password"].ToString();
                //string password_DE = EncryptionDecryption.CryptorEngine.Decrypt(password,true);
                if (password == Enc.MAIN.SCrypt(Pwd, true))
                {
                    return true;
                    /*theCommand.CommandText = "insert into LoginHistory(UserName,Logindate)values('" + UName + "',getdate())";
                    theCommand.CommandType = CommandType.Text;
                    theCommand.ExecuteNonQuery();*/
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
    public void insertLoginHistory(string UName, string clintIp)
    {
        try
        {
            // Connection.
            theConnection = new SqlConnection();
            theConnection.ConnectionString = conString;
            theConnection.Open();

            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "insert into LoginHistory(UserName,Logindate,IPAddress)values('" + UName + "',getdate(),'" + clintIp + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();
        }
        catch { }
    }
    public bool UserLogged(string UName, string compcode)
    {
        //conString = ConfigurationManager.ConnectionStrings["HMSConnectionString1"].ToString();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        try
        {
            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;

            theCommand.CommandText = "SELECT * FROM LoginHistory where UserName='" + UName + "' and LogoutDate is null";
            theCommand.CommandType = CommandType.Text;

            // Reader
            theReader = theCommand.ExecuteReader();
            theReader.Read();

            //string adf = Enc.MAIN.SCrypt(Pwd,true);
        
            if (theReader.HasRows == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
    public void ForceLogout(string UName, string compcode)
    {
        try
        {
            // Connection.
            theConnection = new SqlConnection();
            theConnection.ConnectionString = conString;
            theConnection.Open();

            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "update LoginHistory set LogoutDate=getdate(),flag=1 where UserName='" + UName + "' and LogoutDate is null";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();
        }
        catch
        {

        }
    }
    public void insertAutoMedicine(string cocode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM IPD_MedicineInsertCheck M WHERE CONVERT(VARCHAR,M.InsertDate,103)=CONVERT(VARCHAR,GETDATE(),103) and compcode='" + cocode + "'";
        theCommand.CommandType = CommandType.Text;
        
        // Reader
        theReader = theCommand.ExecuteReader();
        theReader.Read();

        if (theReader.HasRows)
        {
            theReader.Close();
        }
        else
        {
            theReader.Close();
            theCommand.CommandText = "EXEC  sp_IPD_AutomaticMedicineAdd " + cocode + "";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();

            theCommand.CommandText = "INSERT INTO IPD_MedicineInsertCheck(InsertDate,compcode) VALUES(GETDATE(),'" + cocode + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();
        }
    }

    public void insertAutoServices(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_AutoInsertService '"+compcode+"'";
        theCommand.CommandType = CommandType.Text;
        theCommand.ExecuteNonQuery();

    }

    public void insertAutoServicesPrescription(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_AutoInsertServicePresCription '" + compcode + "'";
        theCommand.CommandType = CommandType.Text;
        theCommand.ExecuteNonQuery();

    }

    public void insertAutoInstrument(string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_AutoInsertInstrument '" + compcode + "','"+yearcode+"'";
        theCommand.CommandType = CommandType.Text;
        theCommand.ExecuteNonQuery();

    }

    public void insertAutoInsertSysterAya(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_AutoInsertSysterAya '" + compcode + "'";
        theCommand.CommandType = CommandType.Text;
        theCommand.ExecuteNonQuery();

    }
    public void updateLoginHistory(string user)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "update LoginHistory set LogoutDate=getdate() where UserName='" + user + "' and LogoutDate is null ";
        theCommand.CommandType = CommandType.Text;
        theCommand.ExecuteNonQuery();

    }


    public DataTable getUserDetails(string username, string compcode)
    {
        DataTable dt = new DataTable();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT * FROM GN_UserDetails where UserId='" + username + "' and compcode='" + compcode + "'";

        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        theAdapter.Fill(dt);

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();


       
        return dt;
    }
}