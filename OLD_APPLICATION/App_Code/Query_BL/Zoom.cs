using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Zoom
/// </summary>
public class Zoom
{
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    public Zoom(string con)
	{
        conString = con;
	}
    public DataTable GetAllMedicine(string compcode,string yearcode,string icode)
    {
        if (icode == "")
        {
            icode = "'%%'";
        }
        else
        {
            icode = "'" + icode + "'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT itemmast.itype,itemmast.icode,itemmast.iname,itemmst1.istock,itemmst1.IEPVAL,itemmst1.curstock,itemmst1.curvalue,dbo.Fnc_GetUnitname(itemmast.compcode,itemmast.iunit)iunit,itemmst1.invrate, " +
                "dbo.Fnc_GetItemIssueRecd(itemmast.compcode,itemmst1.yearcode,itemmast.icode,'P',null)as recqty, 0 recval, " +
                "dbo.Fnc_GetItemIssueRecd(itemmast.compcode,itemmst1.yearcode,itemmast.icode,'I',null)as issqty, 0 issval " +
                "FROM itemmast,itemmst1 WHERE itemmast.compcode = itemmst1.compcode and itemmast.icode = itemmst1.icode and itemmst1.compcode='" + compcode + "' and itemmst1.yearcode= '" + yearcode + "' and itemmast.itype='M' and itemmast.icode like" + icode + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable MasterTable = new DataTable();
        theAdapter.Fill(MasterTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MasterTable;
    }
    public DataTable GetAllBatchMedicinewise(string compcode, string yearcode,string icode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        /*theCommand.CommandText = "SELECT batdetl.icode,convert(varchar,docdt,103)Purdate,batdetl.BATCHNO,convert(varchar,batdetl.EXPDATE,103)EXPDATE,batdetl.OPSTOCK,batdetl.CURSTOCK, " +
                "dbo.Fnc_GetItemIssueRecd(batdetl.compcode,batdetl.yearcode,batdetl.icode,'P',batdetl.BATCHNO)as recqty, 0 recval, " +
                "dbo.Fnc_GetItemIssueRecd(batdetl.compcode,batdetl.yearcode,batdetl.icode,'I',batdetl.BATCHNO)as issqty, 0 issval " +
                "FROM batdetl,invhead,invdetl WHERE INVHEAD.compcode = INVDETL.compcode and INVHEAD.compcode = batdetl.compcode and INVHEAD.yearcode = INVDETL.yearcode and " +
                "INVHEAD.docno = INVDETL.docno and INVHEAD.yearcode = batdetl.yearcode and batdetl.BATCHNO = INVDETL.BATCHNO and batdetl.compcode='" + compcode + "' and " +
                "batdetl.yearcode= '" + yearcode + "' and ltrim(rtrim(batdetl.icode))=ltrim(rtrim('" + icode + "')) and INVHEAD.types = INVDETL.types and INVDETL.types='P' " +
                "order by invhead.docno desc,batdetl.BATCHNO";*/
        theCommand.CommandText = "SELECT icode,convert(varchar,batdetl.PURDATE,103)Purdate,BATCHNO,convert(varchar,batdetl.EXPDATE,103)EXPDATE,OPSTOCK,CURSTOCK, " +
                "dbo.Fnc_GetItemIssueRecd(compcode,yearcode,icode,'P',BATCHNO)as recqty, 0 recval, " +
                "dbo.Fnc_GetItemIssueRecd(compcode,yearcode,icode,'I',BATCHNO)as issqty, 0 issval " +
                "FROM batdetl WHERE compcode='" + compcode + "' and yearcode= '" + yearcode + "' and ltrim(rtrim(icode))=ltrim(rtrim('" + icode + "')) order by batdetl.PURDATE desc,BATCHNO";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable MasterTable = new DataTable();
        theAdapter.Fill(MasterTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MasterTable;
    }



    public DataTable getMedicineData(string compcode, string yearcode, string icode, string batch)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select d.BillNo,h.SLCODE,s.SName from INVDETL d,INVHEAD h,PH_SuppilierMaster s where s.COMPCODE=h.COMPCODE and s.SCode=h.SLCODE and h.COMPCODE=d.COMPCODE and h.YEARCODE=d.YEARCODE and h.TYPES=d.TYPES and h.DOCNO=d.DOCNO and d.ICODE='"+icode+"' and d.TYPES='P' and d.BATCHNO='"+batch+"' and d.compcode='"+compcode+"' and d.yearcode='"+yearcode+"'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable MasterTable = new DataTable();
        theAdapter.Fill(MasterTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MasterTable;

    }
    public string PurchaseDate(string compcode, string yearcode, string icode,string batchno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT convert(varchar,docdt,103)docdt from INVHEAD,INVDETL WHERE INVHEAD.compcode = INVDETL.compcode and INVHEAD.yearcode = INVDETL.yearcode and INVHEAD.docno = INVDETL.docno and " +
            "INVDETL.compcode='" + compcode + "' and INVDETL.yearcode= '" + yearcode + "' and ltrim(rtrim(INVDETL.icode))=ltrim(rtrim('" + icode + "')) and ltrim(rtrim(INVDETL.batchno))=ltrim(rtrim('" + batchno + "'))";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable MasterTable = new DataTable();
        theAdapter.Fill(MasterTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MasterTable.Rows[0]["docdt"].ToString();
    }
    public DataTable GetAllMedicineBrkup(string compcode, string yearcode, string icode,string Batchno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT INVHEAD.docno,INVDETL.icode,convert(varchar,docdt,103)Purdate,INVDETL.BATCHNO,convert(varchar,INVDETL.EXPDATE,103)EXPDATE, " +
                "INVDETL.types,case INVDETL.types when 'P' then 'Purchase' else 'Issue' end type,dbo.Fnc_GetIssueTo(INVDETL.compcode,INVDETL.yearcode,issue_to)as issueto," +
                "iqty,irate,iamount FROM invhead,invdetl WHERE INVHEAD.compcode = INVDETL.compcode and INVHEAD.yearcode = INVDETL.yearcode and " +
                "INVHEAD.docno = INVDETL.docno and INVDETL.compcode='" + compcode + "' and INVDETL.yearcode= '" + yearcode + "' and INVHEAD.bkcode in('RC','IC')" +
                " and ltrim(rtrim(INVDETL.icode))=ltrim(rtrim('" + icode + "')) and ltrim(rtrim(INVDETL.batchno))=ltrim(rtrim('" + Batchno + "')) and "+
                "INVHEAD.types = INVDETL.types and INVDETL.types in('P','I') order by INVDETL.types desc,invhead.docdt desc,INVDETL.BATCHNO";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable MasterTable = new DataTable();
        theAdapter.Fill(MasterTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MasterTable;
    }
    public string ItemName(string compcode, string icode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT iname from ITEMMAST WHERE compcode='" + compcode + "'  and ltrim(rtrim(icode))=ltrim(rtrim('" + icode + "'))";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable MasterTable = new DataTable();
        theAdapter.Fill(MasterTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MasterTable.Rows[0]["iname"].ToString();
    }
    public string InvoiceNo(string compcode, string yearcode, string docno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT chno from INVHEAD WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "' and ltrim(rtrim(docno))=ltrim(rtrim('" + docno + "'))";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable MasterTable = new DataTable();
        theAdapter.Fill(MasterTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MasterTable.Rows[0]["chno"].ToString();
    }
}