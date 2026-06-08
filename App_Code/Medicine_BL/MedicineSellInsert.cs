using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for MedicineSellInsert
/// </summary>
public class MedicineSellInsert
{
    public MedicineSellInsert(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable PurchaseMedicineTable;


    public DataTable DropdownSubGroup(string group)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (group == "0")
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup order by SubGrName";
        else
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup sub where sub.GroupID='" + group + "'  order by SubGrName";
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


    public DataTable GetPurchasePricePerUnit(string medicineID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        if (medicineID == "0")
            theAdapter = new SqlDataAdapter("SELECT * FROM MD_PurchaseMedicineDetails", theConnection);
        else
            theAdapter = new SqlDataAdapter("SELECT * FROM MD_PurchaseMedicineDetails WHERE MedicineID = '" + medicineID + "'", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);

        // Clean up.
        theConnection.Dispose();
        theAdapter.Dispose();

        return dt;
    }

    public DataTable GetSellUnit(string medicineID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        if (medicineID == "0")
            theAdapter = new SqlDataAdapter("select u.UnitName,u.UnitId from IPD_MedicineMaster s,dbo.GN_UnitMaster u where u.UnitId=s.SellingUnit", theConnection);
        else
            theAdapter = new SqlDataAdapter("select u.UnitName,u.UnitId from IPD_MedicineMaster s,dbo.GN_UnitMaster u where u.UnitId=s.SellingUnit and s.MedicineID= '" + medicineID + "'", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);

        // Clean up.
        theConnection.Dispose();
        theAdapter.Dispose();

        return dt;
    }

    public DataTable GetAlldetails(string batch)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("SELECT *,CONVERT(varchar,ExpiryDate,103) ExDate FROM MD_PurchaseMedicineDetails WHERE BatchNo = '" + batch + "'", theConnection);
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);

        // Clean up.
        theConnection.Dispose();
        theAdapter.Dispose();

        return dt;
    }


    public DataTable DropdownID2()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MCode,MName from  PH_ManufactureMaster where status=1 order by MName";
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
    public DataTable DropdownID3()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineGroupID,MedicineGroupName from  IPD_MedicineGroup where status=1  order by MedicineGroupName";
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

    public DataTable DropdownID4(string sub)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (sub == "0")
            theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1  order by MedicineName";
        else
            theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1 AND  SubGroupid='" + sub + "'  order by MedicineName";
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

    public DataTable DropdownID5()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  GN_DoctorMaster";
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

   
   // public DataTable FillTable(string id)
    public DataSet FillTable(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = " exec  sp_MD_SellInsertDetails " + id + "";
        //theCommand.CommandText = "select md.*,pm.*,isnull(pm.PatientName,'0')Patient_Name,isnull(pm.WingsID,0)Wings_ID,CONVERT(varchar,pm.SaleDate,103) purdate,mm.MedicineName,CONVERT(varchar,md.ExpiryDate,103) exdate,dbo.Fnc_GetWingName(pm.WingsID)WingName,dbo.Fnc_GetPatientNameFromRegno(pm.PatientName)PatientFullname from dbo.MD_MedicineSales pm,IPD_MedicineMaster mm,dbo.MD_MedicineSaleDtls md where pm.SalesBillNo=md.SaleBillNoID and mm.MedicineID=md.MedicineID and pm.SalesBillNo='" + id + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataSet PurchaseMedicineTable = new DataSet();
       // DataTable PurchaseMedicineTable = new DataTable();
        theAdapter.Fill(PurchaseMedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PurchaseMedicineTable;
    }

    public DataTable IssueFillTable(string id,string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select md.*,pm.*,isnull(pm.SLCODE,'0')Patient_Name,isnull(pm.issue_to,0) Wings_ID,CONVERT(varchar,pm.DOCDT,103) purdate,pm.Issueby,pm.ReceiveBy,CONVERT(varchar,pm.ReceiveDate,103) rcvdt,mm.MedicineName,CONVERT(varchar,md.EXPDATE,103) exdate,dbo.Fnc_GetWingName('" + compcode + "',isnull(pm.issue_to,0))WingName,dbo.Fnc_GetPatientNameFromRegno('" + compcode + "',pm.SLCODE)PatientFullname,mfg.MName,gr.MedicineGroupName,sgr.SubGrName from dbo.INVHEAD pm,IPD_MedicineMaster mm,dbo.INVDETL md,IPD_MedicineGroup gr,IPD_MedicineSubGroup sgr,PH_ManufactureMaster mfg where pm.compcode=md.compcode and pm.yearcode=md.yearcode and mm.compcode=pm.compcode and pm.DOCNO=md.DOCNO and mm.icode=md.icode and mfg.MCode=mm.MCode and gr.MedicineGroupID=mm.MedicineGroupID and sgr.ID=mm.SubGroupid and pm.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "' and pm.DOCNO='" + id + "'";
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

    public bool InsertSaleMedicine(string PurchaseMedicineID, string MedicineGroup, string MedicineSubGroup, string MedicineId, string BatchNo, string ExpiryDate, string SellingPrice)
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
                        theCommand.CommandText = "INSERT INTO IPD_MedicineSell(PurchaseMedicineID,MedicineGroup,MedicineSubGroup, MedicineId, BatchNo, ExpiryDate,SellingPrice) VALUES ('" + PurchaseMedicineID + "','" + MedicineGroup + "','" + MedicineSubGroup + "','" + MedicineId + "','" + BatchNo + "', '" + ExpiryDate + "', '" + SellingPrice + "')";
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

    public bool DeleteSaleMedicine(string PurchaseMedicineID)
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
                        theCommand.CommandText = "delete IPD_MedicineSell where PurchaseMedicineID='" + PurchaseMedicineID + "'";
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