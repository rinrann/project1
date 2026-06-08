using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ChargeDetails
/// </summary>
public class ChargeDetails
{
    public ChargeDetails(string con)
    {
        conString = con;
    }
    //  New Projecter ..



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetPatient(string compcode, string yearcode, string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  *,CONVERT(VARCHAR,AppDate,103) APPO   from dbo.GN_PatientReg pr , IPD_BedMaster bm ,DC_PatientAppointment pa where pr.compcode=bm.compcode and pr.compcode=pa.compcode and bm.BedNo=pr.bedallocation and pa.PatientReg=pr.PatientReg and pa.status=1  and pr.PatientReg='" + reg + "' and pr.compcode='"+compcode+"'";
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

    public DataTable GetPreviousDue(string compcode, string yearcode, string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select at.TransactionId,at.Debit from  dbo.AC_Transaction at,AC_Ledger al where al.compcode=at.compcode and al.LedgerID=at.LadgerId and al.ActiveStatus=0 and at.PaymentType=2 and al.LedgerFK='" + reg + "' and al.compcode='"+compcode+"' and at.TrunsactionDate>=(select MAX(pa.AppDate) from  DC_PatientAppointment pa where pa.status=0 and pa.PatientReg='" + reg + "' and pa.compcode='"+compcode+"')";
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

    public DataTable GetChargeDetais(string compcode, string yearcode, string PatientReg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  *  from dbo.DC_ChargeDetails dc  where PatientReg='" + PatientReg + "' and Status=1 and dc.compcode='"+compcode+"' and yearcode='"+yearcode+"'";
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

    public DataTable GetLabChargeDetais(string compcode, string yearcode, string RegnNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "EXEC  sp_PH_ChargeDetails '"+compcode+"','"+yearcode+"'," + RegnNo + "";
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

    public DataTable GridFill(string compcode,string yearcode,string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  *,CONVERT(varchar,pr.Date,103) Date1 from DC_ChargeDetails pr where pr.PatientReg='" + reg + "' and pr.Status=1 and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
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

    public bool InsertDischarge(string compcode, string yearcode, string Date, string PatientReg, string DialysisCharge, string ServiceCharge, string Medicine, string RequisitionCharge, string OtherCharge, string DoctorFees, string DispsableCharge, string PreviousDue)
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

                        double total = 0;
                        total = Convert.ToDouble(DialysisCharge) + Convert.ToDouble(ServiceCharge) + Convert.ToDouble(Medicine) + Convert.ToDouble(RequisitionCharge) + Convert.ToDouble(OtherCharge) + Convert.ToDouble(DoctorFees) + Convert.ToDouble(DispsableCharge) + Convert.ToDouble(PreviousDue);
                        
                        theCommand.CommandText = "select al.LedgerID from AC_Ledger al where al.LedgerFK='" + PatientReg + "'  and al.ActiveStatus=1 and compcode='"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        string ledgerid = theReader["LedgerID"].ToString();
                        theReader.Close();

                        theCommand.CommandText = "update  DC_ChargeDetails set Date='" + Date + "',PreviousDue='" + PreviousDue + "',DialysisCharge='" + DialysisCharge + "',ServiceCharge='" + ServiceCharge + "',Medicine='" + Medicine + "',RequisitionCharge='" + RequisitionCharge + "',OtherCharge='" + OtherCharge + "',DoctorFees='" + DoctorFees + "',DispsableCharge='" + DispsableCharge + "',CheckStatus=1  where Status=1 and PatientReg='" + PatientReg + "' and LedgerId='" + ledgerid + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "select * from AC_Ledger al,dbo.AC_Transaction at where al.compcode=at.compcode and al.LedgerID=at.LadgerId and al.LedgerFK='" + PatientReg + "' and at.PaymentType=11 and al.ActiveStatus=1 and al.compcode='"+compcode+"' and at.yearcode='"+yearcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();

                        if (theReader1.HasRows == true)
                        {
                            theReader1.Close();
                            theCommand.CommandText = "update AC_Transaction set Debit='" + total + "' where LadgerId='" + ledgerid + "' and PaymentType=11 and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }

                        else
                        {
                            theReader1.Close();
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,LadgerId,Debit,TrunsactionDate,Status,Reason,PaymentType) VALUES ('"+compcode+"','"+yearcode+"','" + ledgerid + "','" + total + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "',1,'Total Dialysis Amount',11)";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }

                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
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
        return true;
    }

    public bool Deletecharge(string compcode, string yearcode, string RowId)
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
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "delete DC_ChargeDetails where RowId='" + RowId + "' and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
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