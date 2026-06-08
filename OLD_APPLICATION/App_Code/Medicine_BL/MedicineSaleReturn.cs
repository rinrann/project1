using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for MedicineSaleReturn
/// </summary>
public class MedicineSaleReturn
{
	public MedicineSaleReturn(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable PurchaseMedicineTable;


    public DataTable GetPurchaseMedicineID()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_MD_Generatepurchaseid";
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


    public int GetPurchasePricePerUnit(string medicineID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("SELECT PricePerUnit FROM IPD_MedicineMaster WHERE MedicineID = " + medicineID, theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int purchasePricePerUnit = 0;
        if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows[0]["PricePerUnit"] == DBNull.Value)
        {
            purchasePricePerUnit = 0;
        }

        else
        {
            purchasePricePerUnit = Convert.ToInt32(ds.Tables[0].Rows[0]["PricePerUnit"]);
        }

        // Clean up.
        theConnection.Dispose(); 
        theAdapter.Dispose();

        return purchasePricePerUnit;
    }
    public DataTable DropdownID1()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select PurchaseMedicineID,PurchaseMedicineName from  MD_PurchaseMedicine where status=1";
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
    public bool UpdatePurchaseMedicine(string PurchaseMedicineID, string SCode, string PurchaseDate, string BillNo, string Total)
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
                        theCommand.CommandText = "UPDATE MD_PurchaseMedicine SET  SCode = '" + SCode + "', PurchaseDate = '" + PurchaseDate + "', BillNo= '" + BillNo + "',Total='" + Total + "' WHERE PurchaseMedicineID='" + PurchaseMedicineID + "'";
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
    public DataTable DropdownID2()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MCode,MName from  PH_ManufactureMaster where status=1";
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
        theCommand.CommandText = "select MedicineGroupID,MedicineGroupName from  IPD_MedicineGroup where status=1";
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
    public DataTable DropdownID4(string MedicineGroupID, string sub)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (MedicineGroupID == "0")
            theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1";
        else
            theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1 AND MedicineGroupID='" + MedicineGroupID + "' and SubGroupid='" + sub + "'";
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
    public bool CheckIfPurchaseMedicineExists(string PurchaseMedicineName, string PurchaseMedicineID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT COUNT(*) FROM MD_PurchaseMedicine WHERE PurchaseMedicineName='" + PurchaseMedicineName + "' AND PurchaseMedicineID<>" + PurchaseMedicineID;
        theCommand.CommandType = CommandType.Text;
        int count = (int)theCommand.ExecuteScalar();

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose(); 

        return count > 0;
    }

    public DataTable DropdownSubGroup(string group)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (group == "0")
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup";
        else
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup sub where sub.GroupID='" + group + "'";
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
    public bool InsertSaleMedicine(string PatientName, string PatientAddress, string CardNo, string DoctorName, string SalesReturnBillNo, string SaleDate, string Total, string Discount, string MCode, string MedicineGroupID, string MedicineID, string BatchNo, string ExpiryDate, string Qty, string UnitPrice, string TotalPrice, string LoginUser)
    {
        int effectedRows = 0;
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
                        theCommand.CommandText = "SELECT * FROM MD_MedicineSaleReturn WHERE SalesReturnBillNo='" + SalesReturnBillNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows)
                            theReader.Close();
                        else
                        {
                            theCommand.CommandText = "INSERT INTO MD_MedicineSaleReturn(PatientName,PatientAddress, CardNo, DoctorName, SalesReturnBillNo,SaleDate,Total,Discount) VALUES ('" + PatientName + "','" + PatientAddress + "','" + CardNo + "', '" + DoctorName + "', '" + SalesReturnBillNo + "','" + SaleDate + "', '" + Total + "','" + Discount + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }            // Execute insert query.

                        theCommand.CommandText = "INSERT INTO MD_MedicineReturnDtls(ReturnBillNoID,MCode,MedicineGroupID,MedicineID,BatchNo,ExpiryDate,Qty,PricePerUnit,TotalPrice,CreatedBy, status) VALUES ('" + SalesReturnBillNo + "','" + MCode + "', '" + MedicineGroupID + "', '" + MedicineID + "', '" + BatchNo + "', '" + ExpiryDate + "', '" + Qty + "', '" + UnitPrice + "', '" + TotalPrice + "', '" + LoginUser + "', 1)";
                        theCommand.Transaction = tran as SqlTransaction;
                        effectedRows = theCommand.ExecuteNonQuery();
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

    public bool DeleteMEdDtls(string id)
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
                        theCommand.CommandText = "delete dbo.MD_MedicineSaleDtls where ID='" + id + "'";
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

    public DataTable GenerateSaleId()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_MD_GenerateSaleReturnBillNo";
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


    public DataTable GetSaleMedicineDetails(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,pm.SaleDate,103) purdate,mm.MedicineName,CONVERT(varchar,md.ExpiryDate,103) exdate from dbo.MD_MedicineSaleReturn pm,IPD_MedicineMaster mm,dbo.MD_MedicineReturnDtls md where pm.SalesReturnBillNo=md.ReturnBillNoID and mm.MedicineID=md.MedicineID and pm.SalesReturnBillNo='" + id + "'";
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