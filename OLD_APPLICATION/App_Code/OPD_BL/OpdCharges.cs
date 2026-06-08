using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for OpdCharges
/// </summary>
public class OpdCharges
{
    public OpdCharges(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    public DataTable PatientFill(string compcode, string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "select *,CONVERT(varchar,opr.AppointMentDate,103) date1 from OPD_PatientRegistration opr where opr.PatientRegNo='" + reg + "'";
        theCommand.CommandText = "select * from OPD_PatientRegistration opd where opd.compcode='"+compcode+"' and opd.PatientRegNo='" + reg + "'";
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


        return hospitalTable;
    }
    public DataTable DropdownGetBillType(string compcode,string id=null)
    {
        DataTable Bill;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (id == null)
        {
            theCommand.CommandText = "select b.BillTypeId,b.BillTypeName from OPD_BillTypeMaster b where compcode='" + compcode + "' union select 0,'Select' order by BillTypeId";
            theCommand.CommandType = CommandType.Text;
        }
        else
        {
            theCommand.CommandText = "select b.BillTypeId,b.BillTypeName from OPD_BillTypeMaster b where compcode='" + compcode + "' and BillTypeId='" + id + "'";
            theCommand.CommandType = CommandType.Text;
        }
        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        Bill = new DataTable();
        theAdapter.Fill(Bill); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return Bill;
    }

    public bool InsertChargeDetails(string compcode,string yearcode,string PatientRegNo, string TrunsactionDate, string RegnCharge, string DoctorFees, string USGCharge, string IUICharge, string Investigation, string OperationCharge, string MedicineCharge,string user,string date)
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
                        theCommand.CommandText = "SELECT a.LedgerID FROM dbo.AC_Ledger a where a.LedgerFK='" + PatientRegNo + "' /*and CONVERT(varchar, a.CreatedDate,103)=CONVERT(varchar, GETDATE(),103)*/ and compcode='" + compcode + "' and ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        string ledgerid = theReader[0].ToString();
                        theReader.Close();


                        theCommand.CommandText = "select * from OPD_ChargeDetails c where  c.PatientReg='" + PatientRegNo + "' /*and CONVERT(varchar, c.IssueDate,103)=CONVERT(varchar, GETDATE(),103)*/and ledgerid='" + ledgerid + "' and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();

                        if (theReader1.HasRows)
                        {
                            theReader1.Close();
                            theCommand.CommandText = "update  OPD_ChargeDetails  set RegnFees='" + RegnCharge + "',DoctorFees='" + DoctorFees + "',USGCharge='" + USGCharge + "',IUICharge='" + IUICharge + "',InvestigationCharge='" + Investigation + "',OperationCharge='" + OperationCharge + "',MedicineCharge='" + MedicineCharge + "'  where   PatientReg='" + PatientRegNo + "' /*and CONVERT(varchar, IssueDate,103)=CONVERT(varchar, GETDATE(),103) */ and LedgerId='" + ledgerid + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        else
                        {
                            theReader1.Close();
                            theCommand.CommandText = "insert into  OPD_ChargeDetails(compcode,yearcode,PatientReg,LedgerId,RegnFees,DoctorFees,USGCharge,IUICharge,InvestigationCharge,OperationCharge,MedicineCharge,Status,IssueDate) VALUES('"+compcode+"','"+yearcode+"','" + PatientRegNo + "','" + ledgerid + "','" + RegnCharge + "','" + DoctorFees + "','" + USGCharge + "','" + IUICharge + "','" + Investigation + "','" + OperationCharge + "','" + MedicineCharge + "',1,'" + TrunsactionDate + "')";
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

        return true;
    }
    public bool InsertChargeDetailsMapping(string compcode, string yearcode, string PatientRegNo, string TrunsactionDate, string OthersDet, string Otherschrg, string user, string date)
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
                        theCommand.CommandText = "SELECT a.LedgerID FROM dbo.AC_Ledger a where a.LedgerFK='" + PatientRegNo + "' /*and CONVERT(varchar, a.CreatedDate,103)=CONVERT(varchar, GETDATE(),103)*/and compcode='" + compcode + "' and ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        string ledgerid = theReader[0].ToString();
                        theReader.Close();


                        if (Otherschrg != "0.00")
                        {
                            theCommand.CommandText = "select * from OPD_ChargeDetailsMapping c where  c.PatientReg='" + PatientRegNo + "' /*and CONVERT(varchar, c.IssueDate,103)=CONVERT(varchar, GETDATE(),103) */and ledgerid='" + ledgerid + "' and BillTypeId='" + OthersDet + "' and compcode='" + compcode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader1 = theCommand.ExecuteReader();
                            theReader1.Read();
                            if (theReader1.HasRows)
                            {
                                theReader1.Close();
                                theCommand.CommandText = "update OPD_ChargeDetailsMapping  set Charge='" + Otherschrg + "' where  PatientReg='" + PatientRegNo + "' and ledgerid='" + ledgerid + "' and BillTypeId='" + OthersDet + "' and compcode='" + compcode + "' and yearcode='"+yearcode+"'";
                                theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                theReader1.Close();
                                theCommand.CommandText = "insert into  OPD_ChargeDetailsMapping(compcode,yearcode,PatientReg,LedgerId,BillTypeId,Charge,IssueDate) VALUES('" + compcode + "','" + yearcode + "','" + PatientRegNo + "','" + ledgerid + "','" + OthersDet + "','" + Otherschrg + "','" + TrunsactionDate + "')";
                                theCommand.Transaction = tran as SqlTransaction;
                                theCommand.ExecuteNonQuery(); // Execute insert query.
                            }
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

        return true;
    }
    public DataSet GetBillDetails(string compcode, string yearcode, string regno)
    {
        if (regno == null)
            regno = "null";

        DataSet Bill;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "sp_OPD_GetBillDetails '"+compcode+"','"+yearcode+"'," + regno + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        Bill = new DataSet();
        theAdapter.Fill(Bill); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return Bill;
    }


    public DataTable GetPaymentDetails(string compcode, string yearcode, string regno)
    {
        DataTable Bill;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "SELECT (case when SUM(at.Credit) IS null then '0.00' else SUM(at.Credit) end) Credit FROM dbo.AC_Ledger a,dbo.AC_Transaction at where a.compcode=at.compcode and a.LedgerID=at.LadgerId and a.LedgerFK='" + regno + "' and a.activestatus=1 and a.compcode='"+compcode+"' and at.yearcode='"+yearcode+"' and PaymentType in('1','3','5') /*and CONVERT(varchar,a.CreatedDate,103)=CONVERT(varchar,GETDATE(),103)*/";
        theCommand.CommandText = "exec sp_GN_GetTotalIncome '" + compcode + "','" + yearcode + "',null,null,'" + regno + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        Bill = new DataTable();
        theAdapter.Fill(Bill); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return Bill;
    }
    public DataTable BillDetails(string compcode, string yearcode, string reg)
    {
        DataTable Bill;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_OPD_BillDetails '"+compcode+"','"+yearcode+"'," + reg + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        Bill = new DataTable();
        theAdapter.Fill(Bill); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return Bill;
    }
    public DataTable GetamountinWords(string value)
    {
        if (value == "")
            value = "0";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec [sp_AmountToWords] " + value + "";
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
    public DataTable GetAllLedgerId(string compcode, string regno)
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select LedgerID from AC_Ledger where LedgerFK='" + regno + "' and COMPCODE='" + compcode + "' and ActiveStatus=0";
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
    public DataSet GetBillDetails_Duplicate(string compcode, string yearcode, string regno,string LedgerId)
    {
        if (regno == null)
            regno = "null";

        DataSet Bill;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "sp_OPD_GetBillDetails_duplicate '" + compcode + "','" + yearcode + "'," + regno + ",'" + LedgerId + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        Bill = new DataSet();
        theAdapter.Fill(Bill); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return Bill;
    }
    public DataTable GetPaymentDetails_LedgerWise(string compcode, string yearcode,string regno, string ledgerid)
    {
        DataTable Bill;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "SELECT (case when SUM(at.Credit) IS null then '0.00' else SUM(at.Credit) end) Credit FROM dbo.AC_Ledger a,dbo.AC_Transaction at where a.compcode=at.compcode and a.LedgerID=at.LadgerId and a.LedgerFK='" + regno + "' and a.activestatus=1 and a.compcode='"+compcode+"' and at.yearcode='"+yearcode+"' and PaymentType in('1','3','5') /*and CONVERT(varchar,a.CreatedDate,103)=CONVERT(varchar,GETDATE(),103)*/";
        theCommand.CommandText = "exec sp_GN_GetPaymentLedgerWise '" + compcode + "','" + yearcode + "',null,null,'" + regno + "','"+ledgerid+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        Bill = new DataTable();
        theAdapter.Fill(Bill); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return Bill;
    }
}