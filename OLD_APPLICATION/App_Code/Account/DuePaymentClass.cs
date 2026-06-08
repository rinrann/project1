using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for DuePaymentClass
/// </summary>
public class DuePaymentClass
{
    public DuePaymentClass(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;


    public DataTable GridFill(string compcode,string yearcode,string regno, string name, string address, string phno)
    {
        DataTable custTable;
        if (regno == "")
            regno = "null";
        else
            regno = "'" + regno + "'";

        if (name == "")
            name = "null";
        else
            name = "'" + name + "'";

        if (address == "")
            address = "null";
        else
            address = "'" + address + "'";

        if (phno == "")
            phno = "null";
        else
            phno = "'" + phno + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec  sp_GN_DuePaymentSearch '"+compcode+"','"+yearcode+"'," + regno + "," + name + "," + address + "," + phno + "";
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

    public DataTable Get_IPD_PatientDetails(string CustID)
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
        theCommand.CommandText = "select  *  from GN_PatientReg pr where pr.PatientReg='" + CustID + "'";
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

    public DataTable GenerateReceiptCode(string compcode,string yearcode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "EXEC sp_ACCOUNT_GenerateReceiptNo '"+compcode+"'";
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

    public bool InsertPayment(string cocode, string yearcode, string LedgerId, string TransactionId, string date, string createdby, string reg, string Reason, string ReceiptNo, string payment, string due, string discount, string PaymentMode, string bkcode)
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
                        if (payment != "" && payment != "0")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,PaymentMode,LadgerId,Credit,TrunsactionDate,EntryBy,Reason,ReceiptNo,PaymentType) VALUES('" + cocode + "','" + yearcode + "','" + PaymentMode + "','" + LedgerId + "','" + payment + "','" + date + "','" + createdby + "','" + Reason + "','" + ReceiptNo + "',1)";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        if (due != "" && due != "0")
                        {
                            theCommand.CommandText = "update  AC_Transaction set Debit='" + due + "'   where TransactionId='" + TransactionId + "' and compcode='" + cocode + "' and yearcode='" + yearcode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        else
                        {
                            theCommand.CommandText = "update  AC_Transaction set Status=0   where TransactionId='" + TransactionId + "' and compcode='" + cocode + "' and yearcode='" + yearcode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        if (discount != "" && discount != "0")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,LadgerId,Credit,TrunsactionDate,EntryBy,Reason,PaymentType) VALUES('" + cocode + "','" + yearcode + "','" + LedgerId + "','" + discount + "','" + date + "','" + createdby + "','Discount Amount',3)";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        if (bkcode.Trim() != "0")
                        {
                            theCommand.CommandText = "exec sp_PaymentPatientwise '" + cocode + "','" + yearcode + "','" + LedgerId + "','" + bkcode + "','" + date + "','" + createdby + "'";
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