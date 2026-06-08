using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AmbulanceCharge
/// </summary>
public class AmbulanceCharge
{
	public AmbulanceCharge(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
   

    public DataTable GridFill(string reg,string compcode)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select ac.RowId,ac.Charges,ac.FromAddress,ac.Kelometer,ac.PatientReg,ac.ToAddress,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate,CONVERT(varchar,ac.DelDate,103) DelDate,ba.BedNo ,bm.BedNoText from  GN_PatientReg pr,dbo.IPD_AmbulanceCharge ac,IPD_BedAllocation ba,IPD_BedMaster bm where ba.compcode=bm.compcode and bm.compcode=pr.compcode and pr.compcode=ac.compcode and bm.BedNo=ba.BedNo and ba.PatientReg=pr.PatientReg and  pr.PatientReg=ac.PatientReg and ba.ToDate is null and ba.PatientReg='" + reg + "' and ac.compcode='"+compcode+"' and ac.DelDate>=pr.AdmissionDate";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return BedTable;
    }

    public DataTable PatientDtls(string reg)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select pr.*,CONVERT(varchar,pr.AdmissionDate,103) adate,ba.BedNo ,bm.BedNoText from  GN_PatientReg pr,IPD_BedAllocation ba,IPD_BedMaster bm where bm.BedNo=ba.BedNo and ba.PatientReg=pr.PatientReg  and ba.PatientReg='"+reg+"' and ba.ToDate is null";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return BedTable;
    }

    public bool InsertAmbulance(string PatientReg, string FromAddress, string ToAddress, string Kelometer, string Charges, string DelDate,string compcode,string user)
    {
        string LedgerId = "";
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
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();


                        theCommand.CommandText = "INSERT INTO IPD_AmbulanceCharge(compcode,LedgerId,PatientReg,FromAddress,ToAddress,Kelometer,Charges,DelDate,user01,logdt01) VALUES('"+compcode+"','" + LedgerId + "','" + PatientReg + "','" + FromAddress + "','" + ToAddress + "','" + Kelometer + "','" + Charges + "','" + DelDate + "','"+user+"',GETDATE())";
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

    public bool UpdateAmbulance(string rowid, string PatientReg, string FromAddress, string ToAddress, string Kelometer, string Charges, string DelDate,string compcode,string user)
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
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "update IPD_AmbulanceCharge set PatientReg='" + PatientReg + "', FromAddress='" + FromAddress + "', ToAddress='" + ToAddress + "', Kelometer='" + Kelometer + "', Charges='" + Charges + "', DelDate='" + DelDate + "',user02='"+user+"',logdt02=GETDATE() where RowId='" + rowid + "'";
            theCommand.CommandType = CommandType.Text;

            effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.
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

    public bool DeleteAddmedicine(string ID, string rowid)
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
            if (rowid == "")
                theCommand.CommandText = "delete from IPD_PatientMedicine where PatientReg = '" + ID + "'";
            else
                theCommand.CommandText = "delete from IPD_PatientMedicine where PatientReg = '" + ID + "' and RowID='" + rowid + "'";
            theCommand.CommandType = CommandType.Text;

            effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.
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