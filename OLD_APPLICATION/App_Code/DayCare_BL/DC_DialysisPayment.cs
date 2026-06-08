using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DC_DialysisPayment123
/// </summary>
public class DC_DialysisPayment
{
    public DC_DialysisPayment(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;


    public DataTable GetDialysisDetails(string CustID,string cocode)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_DC_PatientDialysisPayment '" + CustID + "','" + cocode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataTable Get_Refund_Discount(string compcode,string yearcode,string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from (select (case when SUM(at.Credit) is null then 0 else SUM(at.Credit) end)  Refund from AC_Transaction at,AC_Ledger al where at.LadgerId=al.LedgerID and al.compcode=at.compcode and al.compcode='" + compcode + "' and at.yearcode='" + yearcode + "' and al.LedgerFK='" + CustID + "' and al.ActiveStatus=1 and at.PaymentType='4') T1,(select (case when SUM(at.Credit) is null then 0 else SUM(at.Credit) end)  Discount from AC_Transaction at,AC_Ledger al where at.LadgerId=al.LedgerID and al.compcode=at.compcode and al.compcode='" + compcode + "' and at.yearcode='" + yearcode + "' and al.LedgerFK='" + CustID + "' and al.ActiveStatus=1 and at.PaymentType='3') T2";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataTable DropdownShift()
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select s.ShiftID,s.ShiftName from DC_ShiftDtls s where s.status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();


        return custTable;
    }


    public DataTable GetAmountfromAppo(string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select AdvAmount  Amt  from dbo.DC_PatientAppointment  where PatientReg= '" + CustID + "' and Status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataTable GetAmount(string CustID,string cocode)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "sp_DC_PatientDashBoard null,null,null," + CustID + ",'" + cocode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public bool InsertDialysisPayment(string COMPCODE, string YEARCODE, string Reason, string ReceiptNo, int Payment, string Due, string Discount, string Refund, string Regno, string name, string TrunsactionDate, string EntryBy, string PaymentMode, string txtreason)
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
                        theCommand.CommandText = "select al.LedgerID from dbo.AC_Ledger al where al.LedgerFK='" + Regno + "' and al.ActiveStatus=1 and  al.LedgerName='" + name + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        string LadgerId = theReader[0].ToString();
                        theReader.Close();

                        if (Payment != 0)
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(COMPCODE,YEARCODE,Reason,ReceiptNo,LadgerId,Credit,TrunsactionDate,PaymentType,EntryBy,PaymentMode,ReasonText) VALUES('" + COMPCODE + "','" + YEARCODE + "','" + Reason + "','" + ReceiptNo + "','" + LadgerId + "','" + Payment + "','" + TrunsactionDate + "',1,'" + EntryBy + "','" + PaymentMode + "','" + txtreason + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }

                        if (Due != "0")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(COMPCODE,YEARCODE,Reason,ReceiptNo,LadgerId,Debit,TrunsactionDate,PaymentType,EntryBy,PaymentMode,ReasonText) VALUES('" + COMPCODE + "','" + YEARCODE + "','" + Reason + "','" + ReceiptNo + "','" + LadgerId + "','" + Due + "','" + TrunsactionDate + "',2,'" + EntryBy + "','" + PaymentMode + "','" + txtreason + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }

                        if (Discount != "0")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(COMPCODE,YEARCODE,Reason,ReceiptNo,LadgerId,Credit,TrunsactionDate,PaymentType,EntryBy,PaymentMode,ReasonText) VALUES('" + COMPCODE + "','" + YEARCODE + "','" + Reason + "','" + ReceiptNo + "','" + LadgerId + "','" + Discount + "','" + TrunsactionDate + "',3,'" + EntryBy + "','" + PaymentMode + "','" + txtreason + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }

                        if (Refund != "0")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(COMPCODE,YEARCODE,Reason,ReceiptNo,LadgerId,Credit,TrunsactionDate,PaymentType,EntryBy,PaymentMode,ReasonText) VALUES('" + COMPCODE + "','" + YEARCODE + "','" + Reason + "','" + ReceiptNo + "','" + LadgerId + "','" + Refund + "','" + TrunsactionDate + "',4,'" + EntryBy + "','" + PaymentMode + "','" + txtreason + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
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

    public bool UpdateAppointment(string reg, string amt)
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
                        theCommand.CommandText = "update DC_PaymentTransaction set Amount='" + amt + "' where PatientReg = '" + reg + "'";
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

    public bool DeleteAppointment(string id)
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
                        theCommand.CommandText = "update DC_PatientAppointment set status=0 WHERE AppNo='" + id + "'";
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