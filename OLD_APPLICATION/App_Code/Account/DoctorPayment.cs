using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DoctorPayment
/// </summary>
public class DoctorPayment
{
	public DoctorPayment(String con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;

    public DataTable DropdownPaymentMode(string cocode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  *  from GN_PaymentMode where ID in (3,5,7) and status=1 and compcode='" + cocode + "'";
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


    public DataTable GetDoctorSpeciality(string cocode)
    {
        DataTable splTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorSpecialty where Status=1 and compcode='"+cocode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        splTable = new DataTable();
        theAdapter.Fill(splTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return splTable;
    }

    public DataTable GetDoctorType(string cocode)
    {
        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorType where compcode='" + cocode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        typeTable = new DataTable();
        theAdapter.Fill(typeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return typeTable;
    }
    public String GetAnal(string cocode,string type)
    {
        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select analcode,analname from analmast where compcode='" + cocode + "' and cfiller02='" + type + "'";
        
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        typeTable = new DataTable();
        theAdapter.Fill(typeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return typeTable.Rows[0]["analcode"].ToString();
    }
    public String GetCostcode(string cocode, string type)
    {
        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select costcode,ccname from ccmast where compcode='" + cocode + "' and cfiller02='" + type + "'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        typeTable = new DataTable();
        theAdapter.Fill(typeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return typeTable.Rows[0]["costcode"].ToString();
    }

    public DataTable GetDialysisDetails(String docId,String type,String cocode)
    {
        DataTable docTable;
        if (docId == "")
        {
            docId = "null";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "D")
        {
            theCommand.CommandText = "select *,'Dr. '+doc_name dname from dbo.GN_DoctorMaster where doc_id='" + docId + "' and status='1' and compcode='"+cocode+"'";
        }
        else
        {
            theCommand.CommandText = "select * from dbo.GN_QuackMaster where quackid='" + docId + "' and status='1' and compcode='" + cocode + "'";
        }
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        docTable = new DataTable();
        theAdapter.Fill(docTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return docTable;
    }
    public DataTable GetDoctorPayment(String cocode, String yearcode,String docId, String docType,  String PatientLedger)
    {
        DataTable docTable;
        if (docId == "")
        {
            docId = "null";
        }
        if (PatientLedger == "")
        {
            PatientLedger = "null";
        }
        else
        {
            PatientLedger = PatientLedger.Replace("','", "~");
            PatientLedger = "'" + PatientLedger + "'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "exec sp_IPD_DoctorPayment NULL,NULL,'" + docType + "','" + docId + "','"+cocode+"','"+yearcode+"'";
        theCommand.CommandText = "exec Dsp_IPD_DoctorPaymentAnalysiswise NULL,NULL,'" + docType + "','" + docId + "','" + cocode + "','" + yearcode + "'," + PatientLedger + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        docTable = new DataTable();
        theAdapter.Fill(docTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return docTable;
    }
    public bool Insertdocpayment(string docid, string OTcostcode, string IpdCostcode, string OpdCostcode, string OTanalcode, string VisitAnalcode, string ReferAnalcode, string amtOT, string discountOT, string amtAn, string discountAn, string amtVisit, string discountVisit, string amtRefer, string discountRefer, string discount, string date, string mode, string book, string user, string cocode, string yearcode, string chqdtl, string PatientLedger, string ReceiptNo)
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
                        theCommand.CommandText = "select l.LedgerID  from dbo.AC_Ledger l  where  l.LedgerFK='" + docid + "' and l.activestatus=1 and compcode='"+cocode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        string LedgerId = theReader[0].ToString();
                        theReader.Close();

                        if (amtOT != "")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,analcode,costcode,LadgerId,Credit,reason,TrunsactionDate,EntryBy,PaymentMode,PaymentType,chqdetl,ReceiptNo) VALUES('" + cocode + "','" + yearcode + "','" + OTanalcode + "','" + OTcostcode + "','" + LedgerId + "','" + amtOT + "','Surgeon Payment','" + date + "','" + user + "','" + mode + "',1,'" + chqdtl + "','" + ReceiptNo + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        /*if (discountOT != "")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,analcode,costcode,LadgerId,Credit,reason,TrunsactionDate,EntryBy,PaymentMode,PaymentType,chqdetl,ReceiptNo) VALUES('" + cocode + "','" + yearcode + "','" + OTanalcode + "','" + OTcostcode + "','" + LedgerId + "','" + discountOT + "','Surgeon Discount','" + date + "','" + user + "','" + mode + "',5,'" + chqdtl + "','" + ReceiptNo + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }*/
                        if (amtAn != "")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,analcode,costcode,LadgerId,Credit,reason,TrunsactionDate,EntryBy,PaymentMode,PaymentType,chqdetl,ReceiptNo) VALUES('" + cocode + "','" + yearcode + "','" + OTanalcode + "','" + OTcostcode + "','" + LedgerId + "','" + amtAn + "','Anesthesis Payment','" + date + "','" + user + "','" + mode + "',2,'" + chqdtl + "','" + ReceiptNo + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        /*if (discountAn != "")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,analcode,costcode,LadgerId,Credit,reason,TrunsactionDate,EntryBy,PaymentMode,PaymentType,chqdetl,ReceiptNo) VALUES('" + cocode + "','" + yearcode + "','" + OTanalcode + "','" + OTcostcode + "','" + LedgerId + "','" + discountAn + "','Anesthesis Discount','" + date + "','" + user + "','" + mode + "',5,'" + chqdtl + "','" + ReceiptNo + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }*/
                        if (amtVisit != "")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,analcode,costcode,LadgerId,Credit,reason,TrunsactionDate,EntryBy,PaymentMode,PaymentType,chqdetl,ReceiptNo) VALUES('" + cocode + "','" + yearcode + "','" + VisitAnalcode + "','" + IpdCostcode + "','" + LedgerId + "','" + amtVisit + "','Visit Payment','" + date + "','" + user + "','" + mode + "',3,'" + chqdtl + "','" + ReceiptNo + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        /*if (discountVisit != "")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,analcode,costcode,LadgerId,Credit,reason,TrunsactionDate,EntryBy,PaymentMode,PaymentType,chqdetl,ReceiptNo) VALUES('" + cocode + "','" + yearcode + "','" + VisitAnalcode + "','" + IpdCostcode + "','" + LedgerId + "','" + discountVisit + "','Visit Discount','" + date + "','" + user + "','" + mode + "',5,'" + chqdtl + "','" + ReceiptNo + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }*/
                        if (amtRefer != "")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,analcode,costcode,LadgerId,Credit,reason,TrunsactionDate,EntryBy,PaymentMode,PaymentType,chqdetl,ReceiptNo) VALUES('" + cocode + "','" + yearcode + "','" + ReferAnalcode + "','" + IpdCostcode + "','" + LedgerId + "','" + amtRefer + "','Commission Payment','" + date + "','" + user + "','" + mode + "',4,'" + chqdtl + "','" + ReceiptNo + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        /*if (discountRefer != "")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,analcode,costcode,LadgerId,Credit,reason,TrunsactionDate,EntryBy,PaymentMode,PaymentType,chqdetl,ReceiptNo) VALUES('" + cocode + "','" + yearcode + "','" + ReferAnalcode + "','" + IpdCostcode + "','" + LedgerId + "','" + discountRefer + "','Commission Discount','" + date + "','" + user + "','" + mode + "',5,'" + chqdtl + "','" + ReceiptNo + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }*/
                        if (discount != "")
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,analcode,costcode,LadgerId,Credit,reason,TrunsactionDate,EntryBy,PaymentMode,PaymentType,chqdetl,ReceiptNo) VALUES('" + cocode + "','" + yearcode + "',NULL,'" + IpdCostcode + "','" + LedgerId + "','" + discount + "','Doctor Discount','" + date + "','" + user + "','" + mode + "',5,'" + chqdtl + "','" + ReceiptNo + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        //1-Surgeon,2-Anasthesis,3-visit,4-Commission,5=Discount
                        if (PatientLedger == "")
                        {
                            theCommand.CommandText = "update GN_DoctorPayment set status=1,ReceiptNo='" + ReceiptNo + "' where isnull(status,0)=0 and receiptNo is null and DoctorLedger='" + LedgerId + "'";
                        }
                        else
                        {
                            theCommand.CommandText = "update GN_DoctorPayment set status=1,ReceiptNo='" + ReceiptNo + "' where PatientLedger in('" + PatientLedger + "') and DoctorLedger='" + LedgerId + "'";
                        }
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); 

                        theCommand.CommandText = "exec Dsp_PaymentToDoctor '" + cocode + "','" + yearcode + "','" + LedgerId + "','" + book + "','" + date + "','" + user + "'";
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
            theConnection.Dispose();
            theCommand.Dispose();
        }
    }
}