using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Sister_Aya_Charges123
/// </summary>
public class Sister_Aya_Charges
{
    public Sister_Aya_Charges(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetAllhospital(string compcode,string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,ch.RowId iddd,isnull(sys_cont,0)cont,CONVERT(varchar,pr.AdmissionDate,103) adate,CONVERT(varchar,ch.Date,103) dt from GN_PatientReg pr ,IPD_BedAllocation ba,IPD_BedMaster bm, dbo.IPD_PatientSisterAyaCharges ch,dbo.GN_Shift s,dbo.GN_SisterAyaType t where bm.compcode=ba.compcode and pr.compcode=bm.compcode and ch.compcode=pr.compcode and s.compcode=ch.compcode and t.compcode=ch.compcode and bm.BedNo=ba.BedNo and ba.PatientReg=ch.PatientReg and pr.PatientReg=ch.PatientReg and ch.Shift=s.ID and ch.Type=t.ID and ch.compcode='" + compcode + "' and ch.PatientReg='" + reg + "'  and ba.ToDate is null and ba.ToTime is null and ch.Date>=pr.AdmissionDate";
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

    public DataTable GetonlyPatient(string compcode,string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) adate from GN_PatientReg pr ,IPD_BedAllocation ba,IPD_BedMaster bm where bm.compcode=ba.compcode and pr.compcode=bm.compcode and bm.BedNo=ba.BedNo and ba.PatientReg=pr.PatientReg and pr.compcode='"+compcode+"' and pr.PatientReg='" + reg + "'  and ba.ToDate is null and ba.ToTime is null";
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

    public DataTable GetAllType()
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_SisterAyaType";
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

    public DataTable GetCharges(string type)
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ch.DayCharge,ch.NightCharge,ch.DayNightCharge from dbo.IPD_SisterAyaCharges ch where ch.Type='" + type + "'";
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

    public DataTable GetAllShift()
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_Shift";
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

    public bool InsertSister_Aya_Charges(string Time, string PatientReg, string Type, string Shift, string Charges, string Date, string Remarks,string cocode,string cont)
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
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();


                        string cd = "INSERT INTO IPD_PatientSisterAyaCharges(compcode,LedgerId,Time,PatientReg,Type,Shift,Charges,Date,Remarks,sys_cont) VALUES('" + cocode + "','" + LedgerId + "','" + Time + "','" + PatientReg + "','" + Type + "','" + Shift + "','" + Charges + "','" + Date + "','" + Remarks + "','"+cont+"')";
                        theCommand.CommandText = cd;
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

    public bool UpdateSister_Aya_Charges(string id, string PatientReg, string Type, string Shift, string Charges, string Date, string Remarks, string cocode)
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
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        theCommand.CommandText = "update IPD_PatientSisterAyaCharges set  Type='" + Type + "',LedgerId='" + LedgerId + "',Shift='" + Shift + "',Charges='" + Charges + "',Date='" + Date + "',Remarks='" + Remarks + "' where RowId='" + id + "'and compcode='"+cocode+"'";
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

    public bool DeleteSister_Aya_Charges(string id, string reg, string cocode)
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
                        if (id == "0")
                            theCommand.CommandText = "DELETE FROM IPD_PatientSisterAyaCharges  WHERE PatientReg='" + reg + "'and compcode='"+cocode+"'";
                        else
                            theCommand.CommandText = "DELETE FROM IPD_PatientSisterAyaCharges  WHERE RowId='" + id + "'and compcode='" + cocode + "'";
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