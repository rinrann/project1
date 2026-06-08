using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for MedicineDashboard
/// </summary>
public class MedicineDashboard
{
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlCommand theCommand1;
    private SqlDataAdapter theAdapter;
    private DataTable PurchaseMedicineTable;
    public MedicineDashboard(string con)
	{
        conString = con;
	}

    public DataTable getMfg(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MCode,MName from  PH_ManufactureMaster where status=1 and compcode='"+ compcode+"' order by MName ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PurchaseMedicineTable = new DataTable();
        theAdapter.Fill(PurchaseMedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();


        return PurchaseMedicineTable;
    }

    public DataTable getMedGrp(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  * from IPD_MedicineGroup  where status='1' and compcode='"+ compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PurchaseMedicineTable = new DataTable();
        theAdapter.Fill(PurchaseMedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PurchaseMedicineTable;
    }

    public DataTable MedicineDetails(string compcode, string yearcode, string med)
    {
        
        if (med == "0" || med== "")
        {
            med = "%%";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //if (frmdt != "" && todt != "")
        //{
        //    theCommand.CommandText = "select mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=dbo.Fnc_GetMedicineIcode(mm.compcode,mm.MedicineID) and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where MedicineID=mm.MedicineID) and mm.MCode like '" + mfg + "' and mm.MedicineGroupID like'" + grp + "' and mm.SubGroupid like '" + sgrp + "' and  mm.MedicineID like '" + med + "' and PurchaseDate>='" + frmdt + "' and PurchaseDate<='" + todt + "' order by PurchaseDate desc";
        //}
        //else if (frmdt != "" && todt == "")
        //{
        //    theCommand.CommandText = "select mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=dbo.Fnc_GetMedicineIcode(mm.compcode,mm.MedicineID) and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where MedicineID=mm.MedicineID) and mm.MCode like '" + mfg + "' and mm.MedicineGroupID like'" + grp + "' and mm.SubGroupid like '" + sgrp + "' and  mm.MedicineID like '" + med + "' and PurchaseDate>='" + frmdt + "' order by PurchaseDate desc";
        //}
        //else if (frmdt == "" && todt != "")
        //{
        //    theCommand.CommandText = "select mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=dbo.Fnc_GetMedicineIcode(mm.compcode,mm.MedicineID) and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where MedicineID=mm.MedicineID) and mm.MCode like '" + mfg + "' and mm.MedicineGroupID like'" + grp + "' and mm.SubGroupid like '" + sgrp + "' and  mm.MedicineID like '" + med + "' and PurchaseDate<='" + todt + "' order by PurchaseDate desc";
        //}
        //else
        //{
        //    theCommand.CommandText = "select mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=dbo.Fnc_GetMedicineIcode(mm.compcode,mm.MedicineID) and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where MedicineID=mm.MedicineID) and mm.MCode like '" + mfg + "' and mm.MedicineGroupID like'" + grp + "' and mm.SubGroupid like '" + sgrp + "' and  mm.MedicineID like '" + med + "' order by PurchaseDate desc";
        //}
        theCommand.CommandText = "select mm.icode,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,CAST(bat.curstock AS dec(10)) curstock,CONVERT(varchar,bat.PurDate,103) purchaseDate,bat.BatchNo,Right(CONVERT(varchar,bat.ExpDate,103),7) ExpiryDate from IPD_MedicineMaster mm,BATDETL bat where bat.compcode=mm.compcode and bat.icode=mm.ICODE and bat.compcode='"+ compcode +"' and bat.yearcode='"+ yearcode +"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PurchaseMedicineTable = new DataTable();
        theAdapter.Fill(PurchaseMedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PurchaseMedicineTable;

    }

    public DataTable DropdownSubGroup(string compcode, string group)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (group == "0")
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup where status=1 and compcode='"+ compcode+"' order by SubGrName";
        else
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup sub where status=1 and compcode='" + compcode + "'and  sub.GroupID='" + group + "' order by SubGrName";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PurchaseMedicineTable = new DataTable();
        theAdapter.Fill(PurchaseMedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PurchaseMedicineTable;
    }

    public DataTable DropdownMedicine(string compcode, string sub)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (sub == "0")
            theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1 and compcode='" + compcode + "' order by MedicineName";
        else
            theCommand.CommandText = "select mm.MedicineID,mm.MedicineName from  IPD_MedicineMaster mm where status=1 and compcode='" + compcode + "' and mm.SubGroupid='" + sub + "'  order by MedicineName";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PurchaseMedicineTable = new DataTable();
        theAdapter.Fill(PurchaseMedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PurchaseMedicineTable;
    }

    public DataTable getSuplrinfo(string compcode, string supcd)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select SCode,SName from  PH_SuppilierMaster where SCode='" + supcd + "'and compcode='" + compcode + "' and status=1 order by SName";
        
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PurchaseMedicineTable = new DataTable();
        theAdapter.Fill(PurchaseMedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PurchaseMedicineTable;
    }
}