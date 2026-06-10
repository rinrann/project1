using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AnyTimePayment
/// </summary>
public class AnyTimePayment
{
    public AnyTimePayment(string con)
    {
        conString = con;
    }

    public string AmountinWords { get; set; }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;

    public string Get_AmountinWords(string number)
    {  
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        using (IDbTransaction tran = theConnection.BeginTransaction())
        {
                     
            theCommand.CommandText = "select dbo.AmountToWords('" + number + "')";
            theCommand.Transaction = tran as SqlTransaction;
            SqlDataReader theReader = theCommand.ExecuteReader();
            theReader.Read();
            AmountinWords = theReader[0].ToString();
            theReader.Close();
        }
        return AmountinWords;
    }
    public DataTable Get_IPD_PatientDetails(string CustID,string cocode)
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
        theCommand.CommandText = "select  *  from GN_PatientReg pr where pr.PatientReg='" + CustID + "' and pr.compcode='" + cocode + "'";
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

    public DataTable GenerateReceiptCode(string cocode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "EXEC sp_ACCOUNT_GenerateReceiptNo '"+cocode+"'";
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

    public DataTable GetCredit(string CustID,string cocode)
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
        theCommand.CommandText = "select T1.Total-T2.Refund Credit from   (select ISNULL(SUM(Credit),0)+ ISNULL(SUM(Debit),0)  Total from  dbo.AC_Transaction t,dbo.AC_Ledger l  ,GN_PatientReg pr where pr.compcode=t.compcode and pr.compcode=l.compcode and pr.compcode='" + cocode + "' and pr.PatientReg=l.LedgerFK and t.TrunsactionDate>=pr.AdmissionDate and t.LadgerId=l.LedgerID and l.LedgerFK='" + CustID + "'  and t.PaymentType !=4) T1,(select (case when SUM(Credit) is null then 0 else SUM(Credit) end)   Refund from  dbo.AC_Transaction t,dbo.AC_Ledger l,GN_PatientReg pr where pr.compcode=t.compcode and pr.compcode=l.compcode and pr.compcode='" + cocode + "' and pr.PatientReg=l.LedgerFK and t.TrunsactionDate>=pr.AdmissionDate     and t.LadgerId=l.LedgerID and l.LedgerFK='" + CustID + "'  and t.PaymentType =4) T2";
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

    public DataTable DropdownPaymentMode(string cocode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  *  from GN_PaymentMode where ID in (3,5) and status=1 and compcode='" + cocode + "'";
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
    public DataTable StatusLinkAccount(string cocode,string yearcode)
    {
        DataTable custTable;
        /*string yearcode;
        string[] d = curdate.Split('-');
        if (Convert.ToDecimal(d[1]) > 3) { yearcode = d[0] + "-" + (Convert.ToDecimal(d[0]) + 1); }
        else { yearcode =  (Convert.ToDecimal(d[0]) - 1)+ "-" +d[0] ; }*/
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select LinkAccount from parms where compcode='" + cocode + "' and yearcode='" + yearcode + "'";
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
    public DataTable DropdownBkcodes(string cocode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select bkcode,bkname from bkcodes where compcode='" + cocode + "' and bkcodegl is not null and bktype in('B','C')";
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
    public DataTable MoneyReceiptGridFill(string RegNo,string cocode,string yearcode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select (Case when at.PaymentMode=3 then 'Cash' else 'Card' End) PaymentMode, al.LedgerFK,dbo.AmountToWords(at.Credit) AmountinWords,at.ReceiptNo,al.LedgerName,CONVERT(varchar,at.TrunsactionDate,103) Date1,at.Credit,at.Reason from AC_Transaction at,AC_Ledger al where at.compcode=al.compcode and at.compcode='"+cocode+"' and at.LadgerId=al.LedgerID and al.LedgerFK='" + RegNo + "' and at.Credit!=0.00 and at.yearcode='"+yearcode+"'";
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

    public bool CheckPatientGl(string cocode, string yearcode)
    {
        DataTable custTable;
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select patient_gl from pcparms1 where compcode='"+ cocode +"' and yearcode='" + yearcode + "'";
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
        if (custTable.Rows[0][0].ToString() == "")
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public bool checkDate(string cocode, string yearcode,string strdate)
    {
        DataTable custTable;
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from parms where compcode='" + cocode + "' and yearcode='" + yearcode + "' and convert(date,'" + strdate + "') between Convert(date,STARTDT) and convert(date,ENDDT)";
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
        if (custTable.Rows.Count>0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool InsertPayment(string cocode,string yearcode,string PaymentMode,string  credit, string date, string createdby, string reg, string Reason, string ReceiptNo, string due, string discount, string refund,string txtreason)
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

                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.LedgerFK='" + reg + "' AND al.ActiveStatus=1 and al.compcode='"+cocode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();


                        if (credit != "")
                        {
                            theCommand.CommandText = "select dbo.AmountToWords('" + credit + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader1 = theCommand.ExecuteReader();
                            theReader1.Read();
                            AmountinWords = theReader1[0].ToString();
                            theReader1.Close();
                        }

                        if (credit != "" && Convert.ToDecimal(credit)>0)
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,PaymentMode,LadgerId,Credit,TrunsactionDate,EntryBy,Reason,ReceiptNo,PaymentType,ReasonText) VALUES('" + cocode + "','" + yearcode + "','" + PaymentMode + "','" + LedgerId + "','" + credit + "','" + date + "','" + createdby + "','" + Reason + "','" + ReceiptNo + "',1,'"+txtreason+"')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        if (due != "" && Convert.ToDecimal(due) > 0)
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,LadgerId,Debit,TrunsactionDate,EntryBy,Reason,PaymentType,Status,ReasonText) VALUES('" + cocode + "','" + yearcode + "','" + LedgerId + "','" + due + "','" + date + "','" + createdby + "','Due Amount',2,1,'" + txtreason + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        if (discount != "" && Convert.ToDecimal(discount) > 0)
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,LadgerId,Credit,TrunsactionDate,EntryBy,Reason,PaymentType,ReasonText) VALUES('" + cocode + "','" + yearcode + "','" + LedgerId + "','" + discount + "','" + date + "','" + createdby + "','Discount Amount',3,'" + txtreason + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }

                        if (refund != "" && Convert.ToDecimal(refund) > 0)
                        {
                            theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,LadgerId,Credit,TrunsactionDate,EntryBy,Reason,PaymentType,ReasonText) VALUES('" + cocode + "','" + yearcode + "','" + LedgerId + "','" + refund + "','" + date + "','" + createdby + "','Refund Amount',4,'" + txtreason + "')";
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

    public bool PassJournal(string cocode,string yearcode,string reg,string curuser,string curdate,string type=null)
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
                        theCommand.CommandText = "select l.LedgerID  from dbo.AC_Ledger l  where  l.LedgerFK='" + reg + "' and l.activestatus=1 and l.compcode='"+cocode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        string ledgerId = theReader[0].ToString();
                        theReader.Close();

                        if (type == null) { type = "I"; }

                        theCommand.CommandText = "exec sp_passJournalPatientwise '" + cocode + "','" + yearcode + "','" + ledgerId + "','" + curdate + "','" + curuser + "','" + type + "'";
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

    public bool PassPayment(string cocode, string yearcode, string reg, string bkcode, string curuser, string curdate)
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
                        theCommand.CommandText = "select l.LedgerID  from dbo.AC_Ledger l  where  l.LedgerFK='" + reg + "' and l.activestatus=1 and l.compcode='"+cocode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        string ledgerId = theReader[0].ToString();
                        theReader.Close();

                        theCommand.CommandText = "exec sp_PaymentPatientwise '" + cocode + "','" + yearcode + "','" + ledgerId + "','" + bkcode + "','" + curdate + "','" + curuser + "'";
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

    public bool InsertUpdateAdvancePayment(string compcode, string yearcode, string receiptno, string regno, string amount, string paymode, string user,string DocId,string bankname,string chqno,string chqdate,string servCode,string servName,string servCost,string Type)
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

            int lastrcptno = Convert.ToInt32(receiptno.Substring(receiptno.Length - 4));
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
                        if (Type == "I")
                        {
                            theCommand.CommandText = "Insert into OPD_AdvanceBill(CompCode,YearCode,ReceiptNo,RegNo,AdvAmount,AdvDate,PaymentMode,Referdoc,Status,User01,Logdt01,Chq_CardNo,Bank_CardHolderName,TransactionId,testid,testname,testcost) values('" + compcode + "','" + yearcode + "','" + receiptno + "','" + regno + "','" + amount + "',getdate(),'" + paymode + "','" + DocId + "',0,'" + user + "',getdate(),'" + chqno + "','" + bankname + "','" + chqdate + "','" + servCode + "','" + servName + "','"+ servCost +"')";
                        }
                        else
                        {
                            theCommand.CommandText = "Update OPD_AdvanceBill set AdvAmount='" + amount + "',PaymentMode='" + paymode + "',Referdoc='" + DocId + "',user02='" + user + "',logdt02=getdate(),Chq_CardNo='" + chqno + "',Bank_CardHolderName='" + bankname + "',TransactionId='" + chqdate + "',testid='"+ servCost +"',testname='"+ servName +"',testcost='"+ servCost +"' where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ReceiptNo='" + receiptno + "' and RegNo='" + regno + "'";
                        }
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query. 

                        if (Type == "I")
                        {
                            theCommand.CommandText = "update OPD_BillTypeMaster set LastSerial=" + lastrcptno + " where compcode='" + compcode + "' and yearcode='" + yearcode + "' and BillTypeId='RCP' and Bill_Month=CONVERT(varchar(2),GETDATE(),101) ";
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

    public DataTable GetAdvanceDetails(string compcode, string yearcode, string pname,string regno,string phno)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        string lsSql = "select ReceiptNo,RegNo,PName,AdvAmount,convert(varchar(10),AdvDate,103) as AdvDate,A.PaymentMode,case A.PaymentMode when 'C' then 'Cash' when 'R' then 'Card' when 'E' then 'e-Wallet' else 'Net Banking' end as paymode ,isNull(A.Status,0)Status,case isNull(A.Status,0) when 1 then 'Adjusted' else 'Not Adjusted' end as StatusShow,A.Referdoc,D.doc_name  from OPD_PatientRegistration R,OPD_AdvanceBill A left join GN_DoctorMaster D  on D.compcode=A.compcode and D.doc_id=A.Referdoc where R.compcode=A.compcode and /*R.yearcode=A.yearcode and*/ R.PatientRegNo=A.RegNo and A.compcode='" + compcode + "' and A.yearcode='" + yearcode + "'";
        if (pname != "")
        {
            lsSql=lsSql +" And PName like '"+ pname +"%'";
        }

        if (regno != "")
        {
            lsSql = lsSql + " And RegNo = '" + regno + "'";
        }

        if (phno != "")
        {
            lsSql = lsSql + " And PhNo1 = '" + phno + "'";
        }
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = lsSql;
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


        return custTable;
    }

    public bool DeleteAdvancePay(string compcode, string yearcode, string receiptno, string regno)
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
                        theCommand.CommandText = "delete from OPD_AdvanceBill where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ReceiptNo='" + receiptno + "' and RegNo='"+ regno +"'";
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

    public DataTable GetPendingAdvanceDetails(string compcode,string yearcode,string regno)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        string lsSql = "select ROW_NUMBER() OVER(ORDER BY ReceiptNo ASC) AS Srl, ReceiptNo,RegNo,(AdvAmount-isNull(AdjustedAmt,0)) as BalAmount,convert(varchar(10),AdvDate,103) as AdvDate,PaymentMode,case PaymentMode when 'C' then 'Cash' when 'R' then 'Card' when 'E' then 'e-Wallet' else 'Net Banking' end as paymode  from OPD_AdvanceBill where compcode='" + compcode + "' /*and yearcode='" + yearcode + "'*/ and RegNo='" + regno + "' and (AdvAmount-isNull(AdjustedAmt,0))>0 order by AdvDate";
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = lsSql;
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


        return custTable;
    }

    public DataTable GetPatientAdvDetails(string compcode,string yearcode,string regno,string receiptNo)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        string lsSql = "select A.ReceiptNo,A.RegNo,A.AdvAmount,dbo.AmountToWords(A.AdvAmount) num2word,convert(varchar(10),A.AdvDate,103) as AdvDate,A.PaymentMode,isNull(testname,'')testname,isNull(testcost,0) testCost,case A.PaymentMode when 'C' then 'Cash' when 'R' then 'Card' when 'E' then 'e-Wallet' else 'Net Banking' end as paymode,A.Referdoc,dbo.fn_GetDoctor(A.compcode,A.Referdoc) as ReferalName,R.PName,R.Age,R.Address,R.PhNo1  from OPD_AdvanceBill A,OPD_PatientRegistration R where R.compcode=A.compcode /*and R.yearcode=A.yearcode*/ and R.PatientRegNo=A.RegNo and A.compcode='" + compcode + "' and A.yearcode='" + yearcode + "' and A.RegNo='" + regno + "' and A.ReceiptNo='" + receiptNo + "'";
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = lsSql;
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


        return custTable;
    }

    public DataTable getServiceList(string compcode)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select '' as TestId,'Select' as TestName Union Select TestId,TestName from PH_TestMaster where compcode='" + compcode + "' and groupcode in('G0005','G0006')";
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


        return custTable;
    }

    public DataTable getServiceDetails(string compcode,string testId)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select TestName,Cost from PH_TestMaster  where compcode='" + compcode + "' and TestId='" + testId + "'";
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


        return custTable;
    }

    public bool DeleteAdvance(string ReceiptNo, string cocode,string yearcode)
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
            theCommand.CommandText = "delete from OPD_AdvanceBill  WHERE compcode='" + cocode + "'and yearcode='" + yearcode + "' and ReceiptNo='" + ReceiptNo + "'";
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
}