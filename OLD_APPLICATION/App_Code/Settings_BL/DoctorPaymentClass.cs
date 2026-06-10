using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for DoctorPaymentClass
/// </summary>
public class DoctorPaymentClass
{
    public DoctorPaymentClass(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;



    public DataTable DoctorType(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorType where status=1 and compcode='"+compcode+"'";
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

    public DataTable GetPayeeName(string compcode,string yearcode,string type, string doctype, string docid, string fromdt, string todt)
    {

        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "D")
        {
            if (docid == "0")
            {
                theCommand.CommandText = "Select act.ReceiptNo,CONVERT(varchar,act.TrunsactionDate,103) Date,sum(isNull(act.Debit,0))Debit,sum(isNull(act.Credit,0)) Credit,acl.LedgerId,acl.LedgerFK,acl.ledgerName,gd.DocTypeId from AC_Transaction act,AC_Ledger acl,GN_DoctorMaster gd  where act.compcode=acl.compcode and act.compcode=gd.compcode and act.LadgerId=acl.LedgerId and acl.LedgerType ='D' and gd.doc_id=acl.LedgerFK and act.compcode='" + compcode + "' and act.yearcode='" + yearcode + "' and act.Reason like '%Payment%' and act.ReceiptNo is not null group by act.ReceiptNo,act.TrunsactionDate,acl.LedgerId,acl.LedgerFK,acl.ledgerName,gd.DocTypeId";
            }
            else
            {
                theCommand.CommandText = "Select act.ReceiptNo,CONVERT(varchar,act.TrunsactionDate,103) Date,sum(isNull(act.Debit,0))Debit,sum(isNull(act.Credit,0)) Credit,acl.LedgerId,acl.LedgerFK,acl.ledgerName,gd.DocTypeId from AC_Transaction act,AC_Ledger acl,GN_DoctorMaster gd  where act.compcode=acl.compcode and act.compcode=gd.compcode and act.LadgerId=acl.LedgerId and acl.LedgerType ='D' and gd.doc_id=acl.LedgerFK and act.compcode='" + compcode + "' and act.yearcode='" + yearcode + "' and act.Reason like '%Payment%' and act.ReceiptNo is not null and acl.LedgerFK='" + docid + "'  group by act.ReceiptNo,act.TrunsactionDate,acl.LedgerId,acl.LedgerFK,acl.ledgerName,gd.DocTypeId";
            }
        }
        else
        {
            if (docid == "0")
            {
                //theCommand.CommandText = "select QuackId Id,QuackName Name,case type  when 'A' then 'Asha' when 'R' then 'Car Rented' when 'P' then 'Car Private' when 'B' then 'Ambulance' when 'Q' then 'Rural Doctor' else 'Others' end Type from GN_QuackMaster where type like '" + doctype + "%'";
                theCommand.CommandText = "Select act.ReceiptNo,CONVERT(varchar,act.TrunsactionDate,103) Date,sum(isNull(act.Debit,0))Debit,sum(isNull(act.Credit,0)) Credit,acl.LedgerId,acl.LedgerFK,acl.ledgerName,qm.Type from AC_Transaction act,AC_Ledger acl,GN_QuackMaster qm  where act.compcode=acl.compcode and act.compcode=qm.compcode and act.LadgerId=acl.LedgerId and acl.LedgerType ='Q' and qm.QuackId=acl.LedgerFK and act.compcode='" + compcode + "' and act.yearcode='" + yearcode + "' and act.Reason like '%Payment%' and act.ReceiptNo is not null group by act.ReceiptNo,act.TrunsactionDate,acl.LedgerId,acl.LedgerFK,acl.ledgerName,qm.Type";
            }
            else
            {
                theCommand.CommandText = "Select act.ReceiptNo,CONVERT(varchar,act.TrunsactionDate,103) Date,sum(isNull(act.Debit,0))Debit,sum(isNull(act.Credit,0)) Credit,acl.LedgerId,acl.LedgerFK,acl.ledgerName,qm.Type from AC_Transaction act,AC_Ledger acl,GN_QuackMaster qm  where act.compcode=acl.compcode and act.compcode=qm.compcode and act.LadgerId=acl.LedgerId and acl.LedgerType ='Q' and qm.QuackId=acl.LedgerFK and act.compcode='" + compcode + "' and act.yearcode='" + yearcode + "' and act.Reason like '%Payment%' and act.ReceiptNo is not null and acl.LedgerFK='" + docid + "' group by act.ReceiptNo,act.TrunsactionDate,acl.LedgerId,acl.LedgerFK,acl.ledgerName,qm.Type";
            }
        }
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

    public DataTable GetDocName(string compcode, string type, string doctype)
    {
        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "D")
        {
            theCommand.CommandText = "select doc_id Id,doc_name Name, 'Consultant Doctor' Type from GN_DoctorMaster where status=1 and compcode='"+compcode+"' and DocTypeId like '" + doctype + "%'";

        }
        else
        {
            theCommand.CommandText = "select QuackId Id,QuackName Name,case type  when 'A' then 'Asha' when 'R' then 'Car Rented' when 'P' then 'Car Private' when 'B' then 'Ambulance' when 'Q' then 'Rural Doctor' else 'Others' end Type from GN_QuackMaster where status=1 and compcode='" + compcode + "' and type like '" + doctype + "%'";

        }


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
    public DataTable GetDiscPayment(string compcode, string yearcode, string recptno)
    {
        DataTable typeTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;


        theCommand.CommandText = "Select isnull(Credit,0) Credit from AC_Transaction act where act.compcode='"+compcode+"' and act.yearcode='"+yearcode+"' and act.ReceiptNo='" + recptno + "' and act.Reason like '%Discount%' and paymenttype=5";
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

    public DataTable GetrecptNo(string compcode, string yearcode, string docId, string from, string to)
    {
        DataTable typeTable;

        //if (from == "" || from == "null")
        //    from = "null";
        //else
        //    from = "'" + from + "'";

        //if (to == "" || to == "null")
        //    to = "null";
        //else
        //    to = "'" + to + "'";


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "Select ReceiptNo from AC_Transaction where compcode='" + compcode + "' and act.yearcode='" + yearcode + "' and LadgerId=(Select LedgerID from AC_Ledger where LedgerFK='" + docId + "') and ReceiptNo!='NULL' group by ReceiptNo";

        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        typeTable = new DataTable();
        theAdapter.Fill(typeTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return typeTable;

    }

    public DataTable DoctorMaster(string compcode,string type)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_DoctorMaster dm  where status=1 and compcode='"+compcode+"' and dm.DocTypeId='" + type + "'";
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


    public DataSet GetDoctorPaymentInfo(string from, string to, string DoctorType, string DoctorName)
    {


        if (DoctorType == "0")
            DoctorType = "null";
        else
            DoctorType = "'" + DoctorType + "'";

        if (DoctorName == "0")
            DoctorName = "null";
        else
            DoctorName = "'" + DoctorName + "'";

        if (from == "" || from == "null")
            from = "null";
        else
            from = "'" + from + "'";

        if (to == "" || to == "null")
            to = "null";
        else
            to = "'" + to + "'";


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = " sp_IPD_Report_DoctorPayment " + from + "," + to + "," + DoctorType + "," + DoctorName + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataSet Bill1 = new DataSet();
        theAdapter.Fill(Bill1); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return Bill1;
    }



}