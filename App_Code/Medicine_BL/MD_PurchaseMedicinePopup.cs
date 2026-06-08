using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for MD_PurchaseMedicinePopup123
/// </summary>
public class MD_PurchaseMedicinePopup
{
    public MD_PurchaseMedicinePopup(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    private DataTable assoidtable;
    public DataTable DropdownID4()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_SuppilierMaster where Status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable PurchaseMedicineTable = new DataTable();
        theAdapter.Fill(PurchaseMedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PurchaseMedicineTable;
    }

    public DataTable GridPopup(string compcode,string yearcode,string invoiceid, string scode, string date, string total)
    {
        if (invoiceid == "")
            invoiceid = "null";
        if (scode == "")
            scode = "null";
        if (total == "")
            total = "null";
        if (date != "null")
            date = "'" + date + "'";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_MD_MedicinePurchase '"+ compcode +"','"+ yearcode +"'," + invoiceid + "," + scode + "," + date + "," + total + "";
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


    public DataTable GridFillFirst(string frmdt,string todt,string supplr,string compcode,string yearcode)
    {

        if (supplr == "" || supplr == "null")
        {
            supplr = "'%%'";
        }
        else
        {
            supplr = "'" + supplr + "%'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (frmdt == "" && todt == "")
        {
            theCommand.CommandText = "select distinct s.SCode,s.SName from dbo.MD_PurchaseMedicine p,PH_SuppilierMaster s where s.compcode=p.compcode and s.SCode=p.SCode and s.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' and s.SName like " + supplr + " order by s.SName";
        }
        else if (frmdt == "" && todt != "")
        {
            theCommand.CommandText = "select distinct s.SCode,s.SName from dbo.MD_PurchaseMedicine p,PH_SuppilierMaster s where s.compcode=p.compcode and s.SCode=p.SCode and s.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' and p.PurchaseDate<='" + todt + "' and s.SName like " + supplr + " order by s.SName";
        }
        else if (frmdt != "" && todt == "")
        {
            theCommand.CommandText = "select distinct s.SCode,s.SName from dbo.MD_PurchaseMedicine p,PH_SuppilierMaster s where s.compcode=p.compcode and s.SCode=p.SCode and s.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' and p.PurchaseDate>='" + frmdt + "' and s.SName like " + supplr + " order by s.SName";
        }
        else
        {
            theCommand.CommandText = "select distinct s.SCode,s.SName from dbo.MD_PurchaseMedicine p,PH_SuppilierMaster s where s.compcode=p.compcode and s.SCode=p.SCode and s.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' and p.PurchaseDate>='" + frmdt + "' and p.PurchaseDate<='" + todt + "' and s.SName like " + supplr + " order by s.SName";
        }
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

    public DataTable getMedicine(string frmdt, string todt,string med,string compcode,string yearcode,string itype)
    {
        if (med == "" || med == "null")
        {
            med = "'%%'";
        }
        else
        {
            med = "'" + med + "%'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (frmdt == "" && todt == "")
        {
            theCommand.CommandText = "select distinct pmd.MedicineID,mm.MedicineName from  IPD_MedicineMaster mm,MD_PurchaseMedicineDetails pmd where pmd.MedicineID=mm.MedicineID and pmd.compcode=mm.compcode and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.MedicineName like " + med + " and  mm.status=1 and mm.itype='"+itype+"' order by MedicineName";
        }
        else if (frmdt != "" && todt == "")
        {
            theCommand.CommandText = "select distinct pmd.MedicineID,mm.MedicineName from  IPD_MedicineMaster mm,MD_PurchaseMedicineDetails pmd,MD_PurchaseMedicine pm where mm.compcode=pmd.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and pmd.MedicineID=mm.MedicineID and pmd.PurchaseMedicineID=pm.PurchaseMedicineID and pm.PurchaseDate>='" + frmdt + "' and mm.MedicineName like " + med + " and  mm.status=1 and mm.itype='" + itype + "' order by MedicineName";
        }
        else if (frmdt == "" && todt != "")
        {
            theCommand.CommandText = "select distinct pmd.MedicineID,mm.MedicineName from  IPD_MedicineMaster mm,MD_PurchaseMedicineDetails pmd,MD_PurchaseMedicine pm where mm.compcode=pmd.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and  pmd.MedicineID=mm.MedicineID and pmd.PurchaseMedicineID=pm.PurchaseMedicineID and pm.PurchaseDate<='" + todt + "' and mm.MedicineName like " + med + " and  mm.status=1 and mm.itype='" + itype + "' order by MedicineName";
        }
        else
        {
            theCommand.CommandText = "select distinct pmd.MedicineID,mm.MedicineName from  IPD_MedicineMaster mm,MD_PurchaseMedicineDetails pmd,MD_PurchaseMedicine pm where mm.compcode=pmd.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and  pmd.MedicineID=mm.MedicineID and pmd.PurchaseMedicineID=pm.PurchaseMedicineID and pm.PurchaseDate>='" + frmdt + "' and pm.PurchaseDate<='" + todt + "' and mm.MedicineName like " + med + " and  mm.status=1 and mm.itype='" + itype + "' order by MedicineName";
        }
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

    public DataTable GridPopupsupMed(string compcode, string yearcode, string invoiceid, string code, string date, string total, string opt)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (opt == "s")
        {
            if (invoiceid == "")
                invoiceid = "null";
            if (code == "")
                code = "null";
            if (total == "")
                total = "null";
            if (date != "null")
                date = "'" + date + "'";
            theCommand.CommandText = "exec sp_MD_MedicinePurchase '" + compcode + "','" + yearcode + "'," + invoiceid + "," + code + "," + date + "," + total + "";
        }
        else
        {
            if (invoiceid == "")
            {
                invoiceid = "%%";
            }
            if (code == "")
            {
                code = "%%";
            }
            if (total == "")
            {
                total = "%%";
            }
            if (date == "" || date=="null")
            {
                theCommand.CommandText = "select mp.PurchaseMedicineID,CONVERT(varchar,mp.PurchaseDate,103) pdate,mp.Total,pmd.MedicineId from MD_PurchaseMedicine mp,MD_PurchaseMedicineDetails pmd  where mp.compcode=pmd.compcode and mp.yearcode=pmd.yearcode and mp.PurchaseMedicineID=pmd.PurchaseMedicineID and mp.status=1 and  mp.PurchaseMedicineID like '" + invoiceid + "' and mp.Total like '" + total + "' and pmd.MedicineId like'" + code + "'";
            }
            else
            {
                theCommand.CommandText = "select mp.PurchaseMedicineID,CONVERT(varchar,mp.PurchaseDate,103) pdate,mp.Total,pmd.MedicineId from MD_PurchaseMedicine mp,MD_PurchaseMedicineDetails pmd  where mp.compcode=pmd.compcode and mp.yearcode=pmd.yearcode and mp.PurchaseMedicineID=pmd.PurchaseMedicineID and mp.status=1 and mp.PurchaseDate ='" + date + "' and mp.PurchaseMedicineID like '" + invoiceid + "' and mp.Total like '" + total + "' and pmd.MedicineId like'" + code + "'";
            }
        }
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
}