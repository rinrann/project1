using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for ExpiryAlertAndStockReport
/// </summary>
public class ExpiryAlertAndStockReport
{
    public ExpiryAlertAndStockReport(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable PurchaseMedicineTable;

 
    public DataTable DropDownManufacturingCompany(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_ManufactureMaster mc where mc.Status=1 and compcode='"+compcode+"'";
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
  
    public DataTable DropdownMedicineGroup(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  IPD_MedicineGroup where status=1 and compcode='"+compcode+"'";
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

    public DataTable DropdownMedicine(string compcode,string MCode, string sub)
    {
        if (MCode == "")
        {
            MCode = "%%";
        }
        if (sub == "")
        {
            sub = "%%";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select mm.MedicineID,mm.MedicineName from  IPD_MedicineMaster mm where status=1 and MCode like'" + MCode + "' and mm.SubGroupid like'" + sub + "'";
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

    public DataTable DropdownMedicineAll(string compcode)
    {
        
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select mm.MedicineID,mm.MedicineName from  IPD_MedicineMaster mm where status=1";
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
    
    public DataTable DropdownSubGroup(string compcode,string group)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup sub where sub.GroupID='" + group + "' and compcode='"+compcode+"'";
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


    public DataTable DropdownSubGroupAll(string compcode, string group)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup sub where compcode='" + compcode + "'";
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

    public DataTable GetmedicineExpiry (string compcode,string yearcode,string date, string Company,string group,string subgroup,string medicine,string month,int option)
    {
        if (Company == "" || Company == "0")
            Company="null";

        if (group == "" || group == "0")
            group = "null";

        if (subgroup == "" || subgroup == "0")
            subgroup = "null";

        if (medicine == "" || medicine == "0")
            medicine = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if(option==1)
        theCommand.CommandText = "EXEC sp_MD_ExpiryAlertReport '" + date + "'," + Company + "," + group + "," + subgroup + "," + medicine + ",'" + compcode + "','" + yearcode + "'," + month + "";
        else
            theCommand.CommandText = "EXEC Dsp_ExpiredMedReport '" + date + "','" + compcode + "','" + yearcode + "'";
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



    public DataTable GetStockReport(string compcode,string yearcode,string Company, string group, string subgroup, string medicine,string itype)
    {
        if (Company == "" || Company == "0")
            Company = "null";

        if (group == "" || group == "0")
            group = "null";

        if (subgroup == "" || subgroup == "0")
            subgroup = "null";

        if (medicine == "" || medicine == "0")
            medicine = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "EXEC  sp_MD_StockReport  " + Company + "," + group + "," + subgroup + "," + medicine + ",'"+compcode+"','"+yearcode+"','"+itype+"'";
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
    public DataTable GetStockAlert(string Company, string group, string subgroup, string medicine,string compcode,string yearcode)
    {
        if (Company == "" || Company == "0")
            Company = "null";

        if (group == "" || group == "0")
            group = "null";

        if (subgroup == "" || subgroup == "0")
            subgroup = "null";

        if (medicine == "" || medicine == "0")
            medicine = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "EXEC  sp_MD_StockReport  " + Company + "," + group + "," + subgroup + "," + medicine + ",1";
        theCommand.CommandText = "Exec sp_MD_StockAlertReport '"+compcode+"','"+yearcode+"'," + Company + "," + group + "," + subgroup + "," + medicine + ",1";
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
    public DataTable GetSupplier(string slcode,string compcode)
    {
        DataTable suppliertbl;
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select sup.SName from  PH_SuppilierMaster sup where sup.compcode='"+compcode+"' and sup.scode='"+slcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        suppliertbl = new DataTable();
        theAdapter.Fill(suppliertbl); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return suppliertbl;
    }

    public DataTable GetRetQty(string medID, string compcode,string yearcode,string batchno,string icode)
    {
        DataTable suppliertbl;
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select isNull(sum(isnull(IQTY,0)),0) retqty from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE='"+icode.Trim()+"' and batchno='"+batchno+"' and TYPES='Q'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        suppliertbl = new DataTable();
        theAdapter.Fill(suppliertbl); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return suppliertbl;
    }

    public DataTable GetIssueQty(string medID, string compcode, string yearcode, string batchno, string icode)
    {
        DataTable suppliertbl;
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select sum(isnull(IQTY,0)) issueqty from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE='" + icode + "' and batchno='" + batchno + "' and TYPES='I'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        suppliertbl = new DataTable();
        theAdapter.Fill(suppliertbl); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return suppliertbl;
    }

    public DataTable GetStockDetails(string frmdt, string todt,string compcode,string yearcode,string icode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        if (icode == "")
        {
            icode = "'%%'";
        }
        else
        {
            icode="'"+icode+"'";
        }

        if (frmdt != "" && todt != "")
        {
            //theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and MedicineID=mm.MedicineID) and PurchaseDate>='" + frmdt + "' and PurchaseDate<='" + todt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "'  group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
            theCommand.CommandText = "select pmd.ICODE,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,INVHEAD pm,INVDETL pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P') and pmd.TYPES='P' and DOCDT>='"+frmdt+"' and DOCDT<='"+todt+"'and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.ICODE like"+icode+" group by mm.ICODE,SRLNO,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mm.MedicineID,mg.MedicineGroupName,msg.SubGrName,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO,pmd.ICODE order by PurchaseDate desc";
        }
        else if (frmdt != "" && todt == "")
        {
            //theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=mm.MedicineID)  and PurchaseDate>='" + frmdt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "'  group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
            theCommand.CommandText = "select pmd.ICODE,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,INVHEAD pm,INVDETL pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P') and pmd.TYPES='P' and DOCDT>='" + frmdt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.ICODE like" + icode + " group by mm.ICODE,SRLNO,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mm.MedicineID,mg.MedicineGroupName,msg.SubGrName,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO,pmd.ICODE order by PurchaseDate desc";
        }
        else if (frmdt == "" && todt != "")
        {
          //  theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=mm.MedicineID) and PurchaseDate<='" + todt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "'  group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
            theCommand.CommandText = "select pmd.ICODE,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,INVHEAD pm,INVDETL pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P') and pmd.TYPES='P' and DOCDT<='" + todt + "'and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.ICODE like" + icode + " group by mm.ICODE,SRLNO,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mm.MedicineID,mg.MedicineGroupName,msg.SubGrName,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO,pmd.ICODE order by PurchaseDate desc";
        }
        else
        {
           // theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=mm.MedicineID) and pmd.compcode='"+compcode+"' and pmd.yearcode='"+yearcode+"' group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
            theCommand.CommandText = "select pmd.ICODE,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,INVHEAD pm,INVDETL pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P') and pmd.TYPES='P' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.ICODE like " + icode + " group by mm.ICODE,SRLNO,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mm.MedicineID,mg.MedicineGroupName,msg.SubGrName,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO,pmd.ICODE order by PurchaseDate desc";
        }

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

    public DataTable GetMedicineStockDetails(string compcode, string yearcode, string icode,string itype)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        if (icode == "")
        {
            icode = "'%%'";
        }
        else
        {
            icode = "'" + icode + "'";
        }

        //if (frmdt != "" && todt != "")
        //{
        //    //theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and MedicineID=mm.MedicineID) and PurchaseDate>='" + frmdt + "' and PurchaseDate<='" + todt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "'  group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
        //    theCommand.CommandText = "select pmd.ICODE,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,INVHEAD pm,INVDETL pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE /*and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P')*/ and pmd.TYPES='P' and DOCDT>='" + frmdt + "' and DOCDT<='" + todt + "'and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.itype='"+itype+"' and mm.ICODE like" + icode + " group by pmd.ICODE,mm.ICODE,SRLNO,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mm.MedicineID,mg.MedicineGroupName,msg.SubGrName,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO order by pmd.ICODE,PurchaseDate desc";
        //}
        //else if (frmdt != "" && todt == "")
        //{
        //    //theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=mm.MedicineID)  and PurchaseDate>='" + frmdt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "'  group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
        //    theCommand.CommandText = "select pmd.ICODE,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,INVHEAD pm,INVDETL pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE /*and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P')*/ and pmd.TYPES='P' and DOCDT>='" + frmdt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.itype='" + itype + "' and mm.ICODE like" + icode + " group by pmd.ICODE, mm.ICODE,SRLNO,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mm.MedicineID,mg.MedicineGroupName,msg.SubGrName,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO order by pmd.ICODE,PurchaseDate desc";
        //}
        //else if (frmdt == "" && todt != "")
        //{
        //    //  theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=mm.MedicineID) and PurchaseDate<='" + todt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "'  group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
        //    theCommand.CommandText = "select pmd.ICODE,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,INVHEAD pm,INVDETL pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE /*and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P')*/ and pmd.TYPES='P' and DOCDT<='" + todt + "'and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.itype='" + itype + "' and mm.ICODE like" + icode + " group by pmd.ICODE,mm.ICODE,SRLNO,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mm.MedicineID,mg.MedicineGroupName,msg.SubGrName,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO order by pmd.ICODE,PurchaseDate desc";
        //}
        //else
        //{
            // theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=mm.MedicineID) and pmd.compcode='"+compcode+"' and pmd.yearcode='"+yearcode+"' group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
           // theCommand.CommandText = "select pmd.ICODE,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,INVHEAD pm,INVDETL pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE /*and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P')*/ and pmd.TYPES='P' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.itype='" + itype + "' and mm.ICODE like " + icode + " group by pmd.ICODE,mm.ICODE,SRLNO,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mm.MedicineID,mg.MedicineGroupName,msg.SubGrName,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO order by pmd.ICODE,PurchaseDate desc";
        //}
        theCommand.CommandText = "select mm.icode,mm.MedicineID,mm.MedicineName,bat.BATCHNO,isNull(bat.PURSTOCK,0) PurchaseQty,isNull(bat.CURSTOCK,0) CURSTOCK,isNull(OUTSTOCK,0)OUTSTOCK,case isNull(bat.PURDATE,'') when'' then '' else  CONVERT(varchar,bat.PURDATE,103) end as  purchaseDate,CONVERT(varchar,bat.EXPDATE,103) ExpiryDate from IPD_MedicineMaster mm,BATDETL bat where bat.compcode=bat.compcode and bat.icode=mm.icode and mm.compcode='" + compcode + "' and mm.itype='" + itype + "' and bat.YEARCODE='" + yearcode + "' and mm.icode like" + icode + " order by mm.MedicineName";
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


    public DataTable GetReagentStockDetails(string frmdt, string todt, string compcode, string yearcode, string icode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        if (icode == "")
        {
            icode = "'%%'";
        }
        else
        {
            icode = "'" + icode + "'";
        }

        if (frmdt != "" && todt != "")
        {
            //theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and MedicineID=mm.MedicineID) and PurchaseDate>='" + frmdt + "' and PurchaseDate<='" + todt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "'  group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
            theCommand.CommandText = "select pmd.ICODE,mm.icode,mm.iname,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from ITEMMAST mm,ITEMMST1 ms/*,PH_ManufactureMaster mf*/,INVHEAD pm,INVDETL pmd where /*mf.compcode=mm.compcode and*/ ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode /*and  mf.MCode=mm.MCode*/ and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE /*and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P')*/ and pmd.TYPES='P' and DOCDT>='" + frmdt + "' and DOCDT<='" + todt + "'and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.itype='G' and mm.ICODE like" + icode + " group by pmd.ICODE,mm.ICODE,SRLNO,mm.INAME,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO order by pmd.ICODE,PurchaseDate desc";
        }
        else if (frmdt != "" && todt == "")
        {
            //theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=mm.MedicineID)  and PurchaseDate>='" + frmdt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "'  group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
            //theCommand.CommandText = "select pmd.ICODE,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,INVHEAD pm,INVDETL pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE /*and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P')*/ and pmd.TYPES='P' and DOCDT>='" + frmdt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.ICODE like" + icode + " group by pmd.ICODE, mm.ICODE,SRLNO,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mm.MedicineID,mg.MedicineGroupName,msg.SubGrName,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO order by pmd.ICODE,PurchaseDate desc";
            theCommand.CommandText = "select pmd.ICODE,mm.icode,mm.iname,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from ITEMMAST mm,ITEMMST1 ms/*,PH_ManufactureMaster mf*/,INVHEAD pm,INVDETL pmd where /*mf.compcode=mm.compcode and*/ ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode /*and  mf.MCode=mm.MCode*/ and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE /*and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P')*/ and pmd.TYPES='P' and DOCDT>='" + frmdt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.itype='G' and mm.ICODE like" + icode + " group by pmd.ICODE,mm.ICODE,SRLNO,mm.INAME,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO order by pmd.ICODE,PurchaseDate desc";
        }
        else if (frmdt == "" && todt != "")
        {
            //  theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=mm.MedicineID) and PurchaseDate<='" + todt + "' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "'  group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
            //theCommand.CommandText = "select pmd.ICODE,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,INVHEAD pm,INVDETL pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE /*and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P')*/ and pmd.TYPES='P' and DOCDT<='" + todt + "'and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.ICODE like" + icode + " group by pmd.ICODE,mm.ICODE,SRLNO,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mm.MedicineID,mg.MedicineGroupName,msg.SubGrName,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO order by pmd.ICODE,PurchaseDate desc";
            theCommand.CommandText = "select pmd.ICODE,mm.icode,mm.iname,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from ITEMMAST mm,ITEMMST1 ms/*,PH_ManufactureMaster mf*/,INVHEAD pm,INVDETL pmd where /*mf.compcode=mm.compcode and*/ ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode /*and  mf.MCode=mm.MCode*/ and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE /*and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P')*/ and pmd.TYPES='P' and DOCDT<='" + todt + "'and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.itype='G' and mm.ICODE like" + icode + " group by pmd.ICODE,mm.ICODE,SRLNO,mm.INAME,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO order by pmd.ICODE,PurchaseDate desc";
        }
        else
        {
            // theCommand.CommandText = "select pmd.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.qty),0) PurchaseQty,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,(select isnull(SUM(qty),0) from MD_ReturnMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) ReturnQty,(select isnull(SUM(qty),0) from MD_MedicineSaleDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=pmd.MedicineID) SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.MedicineID and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and  MedicineID=mm.MedicineID) and pmd.compcode='"+compcode+"' and pmd.yearcode='"+yearcode+"' group by mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,curstock,purchaseDate,pmd.BatchNo,ExpiryDate,pm.SCode,pmd.PurchaseMedicineID,pmd.MedicineID order by PurchaseDate desc";
            //theCommand.CommandText = "select pmd.ICODE,mm.MedicineID,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,ITEMMST1 ms,PH_ManufactureMaster mf,INVHEAD pm,INVDETL pmd where mf.compcode=mm.compcode and mg.compcode=mm.compcode and msg.compcode=mm.compcode and ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode and  mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE /*and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P')*/ and pmd.TYPES='P' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.ICODE like " + icode + " group by pmd.ICODE,mm.ICODE,SRLNO,mm.MedicineName,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mm.MedicineID,mg.MedicineGroupName,msg.SubGrName,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO order by pmd.ICODE,PurchaseDate desc";
            theCommand.CommandText = "select pmd.ICODE,mm.icode,mm.iname,CAST(ms.curstock AS dec(10)) curstock,CONVERT(varchar,pm.DOCDT,103) purchaseDate,pmd.BatchNo,isnull(sum(pmd.IQTY),0) PurchaseQty,CONVERT(varchar,pmd.EXPDATE,103) ExpiryDate,pm.SLCODE,pmd.DOCNO,pmd.SRLNO,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='Q') ReturnQty,(select isnull(SUM(IQTY),0) from INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=pmd.ICODE and TYPES='I') SaleQty from ITEMMAST mm,ITEMMST1 ms/*,PH_ManufactureMaster mf*/,INVHEAD pm,INVDETL pmd where /*mf.compcode=mm.compcode and*/ ms.compcode=mm.compcode and pm.compcode=mm.compcode and pmd.compcode=pm.compcode and pmd.yearcode=pm.yearcode and ms.yearcode=pm.yearcode /*and  mf.MCode=mm.MCode*/ and ms.icode=mm.ICODE and pm.DOCNO=pmd.DOCNO and pmd.ICODE=mm.ICODE /*and pmd.SRLNO=(select MAX(SRLNO) from INVDETL where ICODE=mm.ICODE and TYPES='P')*/ and pmd.TYPES='P' and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.itype='G' and mm.ICODE like" + icode + " group by pmd.ICODE,mm.ICODE,SRLNO,mm.INAME,curstock,DOCDT,pmd.BatchNo,EXPDATE,pm.SLCODE,pmd.DOCNO order by pmd.ICODE,PurchaseDate desc";
        }

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




    public DataTable getFloor(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select FloorId,FloorName from IPD_FloorMaster where status='1' and compcode='"+compcode+"'";
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
    public DataTable GetmedicineforAllot(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1 and compcode='" + compcode + "' order by MedicineName";
        theCommand.CommandText = "select icode MedicineID,iname MedicineName from  itemmast where tag=1 and compcode='" + compcode + "' and ITYPE='M' order by iname";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable medtable;
        medtable = new DataTable();
        theAdapter.Fill(medtable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return medtable;
    }

    public DataTable getWorkStation(string compcode,string floor)
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select WingsID ,WingsName,WorkStation from IPD_WingsMaster where FloorId='" + floor + "' and WorkStation!= 'null' and status='1' and compcode='"+compcode+"'";
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

    public DataTable GetWingWiseStockDetails(string compcode,string yearcode,string wing, string med, string frmdt, string todt)
    {
        if (wing == "0")
        {
            wing = "%%";
        }


        if (med == "0")
        {
            med = "%%";
        }

        //connection
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        //Command
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (frmdt != "" && todt != "")
        {
            theCommand.CommandText = "select msd.ICODE,mm.MedicineID,ms.issue_to WingsID,wm.WingsName,wm.WorkStation,CONVERT(VARCHAR,msd.EXPDATE,103) ExpiryDate,CONVERT(VARCHAR,ms.DOCDT,103) IssueDate,mm.MedicineName from INVHEAD ms,INVDETL msd,IPD_MedicineMaster mm,IPD_WingsMaster wm where ms.compcode=msd.compcode and ms.yearcode=msd.yearcode and mm.compcode=ms.compcode and wm.compcode=msd.compcode and ms.DOCNO=msd.DOCNO and wm.WingsID=ms.issue_to and mm.ICODE=msd.ICODE and msd.TYPES='I' and ms.compcode='" + compcode + "' and ms.yearcode='" + yearcode + "' and ms.issue_to like '" + wing + "' and msd.ICODE like '" + med + "' and ms.SaleDate>='" + frmdt + "' and ms.SaleDate<='" + todt + "' group by msd.ICODE,ms.issue_to,wm.WingsName,wm.WorkStation,msd.EXPDATE,ms.DOCDT,mm.MedicineID,mm.MedicineName order by ms.DOCDT desc, ms.issue_to";
        }
        else if (frmdt != "" && todt == "")
        {
            theCommand.CommandText = "select msd.ICODE,mm.MedicineID,ms.issue_to WingsID,wm.WingsName,wm.WorkStation,CONVERT(VARCHAR,msd.EXPDATE,103) ExpiryDate,CONVERT(VARCHAR,ms.DOCDT,103) IssueDate,mm.MedicineName from INVHEAD ms,INVDETL msd,IPD_MedicineMaster mm,IPD_WingsMaster wm where ms.compcode=msd.compcode and ms.yearcode=msd.yearcode and mm.compcode=ms.compcode and wm.compcode=msd.compcode and ms.DOCNO=msd.DOCNO and wm.WingsID=ms.issue_to and mm.ICODE=msd.ICODE and msd.TYPES='I' and ms.compcode='" + compcode + "' and ms.yearcode='" + yearcode + "' and ms.issue_to like '" + wing + "' and msd.ICODE like '" + med + "' and msd.MedicineID like '" + med + "' and ms.SaleDate>='" + frmdt + "' group by msd.ICODE,ms.issue_to,wm.WingsName,wm.WorkStation,msd.EXPDATE,ms.DOCDT,mm.MedicineID,mm.MedicineName order by ms.DOCDT desc, ms.issue_to";
        }
        else if (frmdt == "" && todt != "")
        {
            theCommand.CommandText = "select msd.ICODE,mm.MedicineID,ms.issue_to WingsID,wm.WingsName,wm.WorkStation,CONVERT(VARCHAR,msd.EXPDATE,103) ExpiryDate,CONVERT(VARCHAR,ms.DOCDT,103) IssueDate,mm.MedicineName from INVHEAD ms,INVDETL msd,IPD_MedicineMaster mm,IPD_WingsMaster wm where ms.compcode=msd.compcode and ms.yearcode=msd.yearcode and mm.compcode=ms.compcode and wm.compcode=msd.compcode and ms.DOCNO=msd.DOCNO and wm.WingsID=ms.issue_to and mm.ICODE=msd.ICODE and msd.TYPES='I' and ms.compcode='" + compcode + "' and ms.yearcode='" + yearcode + "' and ms.issue_to like '" + wing + "' and msd.ICODE like '" + med + "' and ms.SaleDate<='" + todt + "' group by msd.ICODE,ms.issue_to,wm.WingsName,wm.WorkStation,msd.EXPDATE,ms.DOCDT,mm.MedicineID,mm.MedicineName order by ms.DOCDT desc, ms.issue_to";
        }
        else
        {
            theCommand.CommandText = "select msd.ICODE,mm.MedicineID,ms.issue_to WingsID,wm.WingsName,wm.WorkStation,CONVERT(VARCHAR,msd.EXPDATE,103) ExpiryDate,CONVERT(VARCHAR,ms.DOCDT,103) IssueDate,mm.MedicineName from INVHEAD ms,INVDETL msd,IPD_MedicineMaster mm,IPD_WingsMaster wm where ms.compcode=msd.compcode and ms.yearcode=msd.yearcode and mm.compcode=ms.compcode and wm.compcode=msd.compcode and ms.DOCNO=msd.DOCNO and wm.WingsID=ms.issue_to and mm.ICODE=msd.ICODE and msd.TYPES='I' and ms.compcode='" + compcode + "' and ms.yearcode='" + yearcode + "' and ms.issue_to like '" + wing + "' and msd.ICODE like '" + med + "' group by msd.ICODE,ms.issue_to,wm.WingsName,wm.WorkStation,msd.EXPDATE,ms.DOCDT,mm.MedicineID,mm.MedicineName order by ms.DOCDT desc, ms.issue_to";
        }
        theCommand.CommandType = CommandType.Text;

        //Adapter
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        //DataTAble

        DataTable Stocktbl = new DataTable();
        theAdapter.Fill(Stocktbl);

        //Clean Up
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return Stocktbl;
    }

    public DataTable getQuantityWingWise(string compcode,string yearcode,string wing, string med)
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "EXEC  sp_CalMedQtyWingWise  '"+compcode+"','"+yearcode+"'," + wing + "," + med + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable medtable;
        medtable = new DataTable();
        theAdapter.Fill(medtable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return medtable;
    }

    public DataTable getMedicineData(string compcode,string icode)
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select * from IPD_MedicineMaster where compcode='"+compcode+"' and icode='"+icode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable medtable;
        medtable = new DataTable();
        theAdapter.Fill(medtable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return medtable;
    }

}