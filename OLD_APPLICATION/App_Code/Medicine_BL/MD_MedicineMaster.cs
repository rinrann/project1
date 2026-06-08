using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for MD_MedicineMaster123
/// </summary>
public class MD_MedicineMaster
{
	public MD_MedicineMaster(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable MedicineTable;


    public DataTable GetAllMedicine(string compcode,string group,string subgroup,string medicine)
    {
        if (group == "" || group == "0")
            group = "null";
        else
            group = "'" + group + "'";

        if (subgroup == "" || subgroup == "0")
            subgroup = "null";
        else
            subgroup = "'" + subgroup + "'";

        if (medicine == "")
            medicine = "null";
        else
            medicine = "'" + medicine + "'";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = " exec MD_GetAllMedicine '" + compcode + "'," + group + "," + subgroup + "," + medicine + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        

        return MedicineTable;
    }
    public int GetMedicineID()
    {
        //Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        //Command.
        theAdapter = new SqlDataAdapter("SELECT max(MedicineID) as MedicineID FROM IPD_MedicineMaster Where Status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int MedicineID = 0;
        if (ds.Tables[0].Rows[0]["MedicineID"] == DBNull.Value)
        {
            MedicineID = 1;
        }
        else
        {
            MedicineID = Convert.ToInt32(ds.Tables[0].Rows[0]["MedicineID"]) + 1;
        }

        // Clean up.
        theConnection.Dispose(); 
        theAdapter.Dispose();

        return MedicineID;
    }
    public DataTable DropdownID1(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MCode,MName from  PH_ManufactureMaster where status=1 and compcode='"+compcode+"' order by MName";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        

        return MedicineTable;
    }

    public DataTable DropdownSubGroup(string gr, string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (gr == "0")
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where status=1 and compcode='" + compcode + "' order by SubGrName";
        else
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where status=1 and compcode='" + compcode + "' and GroupID='" + gr + "' order by SubGrName";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        

        return MedicineTable;
    }

    public DataTable DropdownID2(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineGroupID,MedicineGroupName from  IPD_MedicineGroup where status=1 and compcode='" + compcode + "' order by MedicineGroupName";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        

        return MedicineTable;
    }

    public DataTable DropdownID3(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select UnitId,UnitName from  GN_UnitMaster where compcode='" + compcode + "' order by UnitName";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    }

    public DataTable DropdownPotency(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  MD_PotencyMaster where compcode='" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    }

    public DataTable DropdownSyring(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT c.ConItemID,c.ConItemName FROM dbo.IPD_ConsumableItems   c  WHERE c.ConGrId=25  AND c.status=1 and compcode='" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    }

    public bool InsertMedicineMaster(string injection, string SellingUnit, string ConversionFactor, string StockAlert, string SubGroupid, string MedicineName, string MfgCode, string MedicineGroupID, string UnitId, string LoginUser, string cocode, string yearcode, string itype, string genericname, string hsnno, string igstrate, string cgstrate, string sgstrate, string purgst)
    {
        if (injection == "")
            injection = "null";
        else
            injection = "'" + injection + "'";

        if (SubGroupid == "0")
            SubGroupid = "null";
        else
            SubGroupid = "'" + SubGroupid + "'";


        if (UnitId == "0")
            UnitId = "null";
        else
            UnitId = "'" + UnitId + "'";

        if (ConversionFactor == "")
            ConversionFactor = "1";

        if (StockAlert == "")
            StockAlert = "0.00";


        if (igstrate == "")
            igstrate = "0.00";

        if (cgstrate == "")
            cgstrate = "0.00";

        if (sgstrate == "")
            sgstrate = "0.00";

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
                        theCommand.CommandText = "select * from IPD_MedicineMaster where SubGroupid=" + SubGroupid + "  and MedicineName='" + MedicineName + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows)
                        {
                            theReader.Close();
                            return false;
                        }
                        else
                        {
                            theReader.Close();

                            theCommand.CommandText = "select dbo.Fnc_CreateItemcode ('" + cocode + "','" + MedicineName + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader1 = theCommand.ExecuteReader();
                            theReader1.Read();
                            string itemcode = theReader1[0].ToString();
                            theReader1.Close();

                            theCommand.CommandText = "INSERT INTO IPD_MedicineMaster(compcode,ICODE,ConsumableInjectionId,SellingUnit,ConversionFactor,StockAlert,SubGroupid,MedicineName, MCode, MedicineGroupID,  UnitId,  CreatedBy,Status,itype,GenericName,HSNCode,IGSTRate,CGSTRate,SGSTRate,ApplPurWithoutGst) VALUES ('" + cocode + "','" + itemcode + "'," + injection + ",'" + SellingUnit + "','" + ConversionFactor + "','" + StockAlert + "'," + SubGroupid + ",'" + MedicineName + "', '" + MfgCode + "', '" + MedicineGroupID + "', " + UnitId + ",  '" + LoginUser + "', 1,'" + itype + "','" + genericname + "','" + hsnno + "'," + igstrate + "," + cgstrate + "," + sgstrate + ",'" + purgst + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                            theCommand.CommandText = "INSERT INTO ITEMMAST(COMPCODE,ICODE,INAME,ITYPE,IUNIT,I2CONVRT,IUNIT2,TAG,USER01,LOGDT01) VALUES ('" + cocode + "','" + itemcode + "','" + MedicineName + "','" + itype + "'," + UnitId + ",'" + ConversionFactor + "','" + SellingUnit + "', 1,'" + LoginUser + "',getdate())";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                            theCommand.CommandText = "insert into ITEMMST1(COMPCODE,yearcode,Icode,tag,user01,logdt01) values('" + cocode + "','" + yearcode + "','" + itemcode + "',1,'" + LoginUser + "',getdate())";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
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
    public bool UpdateMedicineMaster(string ConsumableInjectionId, string SellingUnit, string ConversionFactor, string StockAlert, string sub, string MedicineID, string MedicineName, string MfgCode, string MedicineGroupID, string UnitId, string cocode, string yearcode, string icode, string itype, string genericname, string hsnno, string igstrate, string cgstrate, string sgstrate, string purgst)
    {
        if (ConsumableInjectionId == "")
            ConsumableInjectionId = "null";
        else
            ConsumableInjectionId = "'" + ConsumableInjectionId + "'";

        if (sub == "0")
            sub = "null";
        else
            sub = "'" + sub + "'";


        if (UnitId == "0")
            UnitId = "null";
        else
            UnitId = "'" + UnitId + "'";

        if (ConversionFactor == "")
            ConversionFactor = "1";

        if (StockAlert == "")
            StockAlert = "0.00";

        if (igstrate == "")
            igstrate = "0.00";

        if (cgstrate == "")
            cgstrate = "0.00";

        if (sgstrate == "")
            sgstrate = "0.00";

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
                        theCommand.CommandText = "select * from IPD_MedicineMaster where SubGroupid=" + sub + "  and MedicineName='" + MedicineName + "' and  MedicineID != '" + MedicineID + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows)
                        {
                            theReader.Close();
                            return false;
                        }
                        else
                        {
                            theReader.Close();
                            theCommand.CommandText = "Update IPD_MedicineMaster set ConsumableInjectionId=" + ConsumableInjectionId + ", SellingUnit='" + SellingUnit + "',ConversionFactor='" + ConversionFactor + "',StockAlert='" + StockAlert + "', SubGroupid=" + sub + ",  MedicineName='" + MedicineName + "', MCode='" + MfgCode + "', MedicineGroupID='" + MedicineGroupID + "', UnitId=" + UnitId + ",GenericName='" + genericname + "',HSNCode='" + hsnno + "',IGSTRate=" + igstrate + ",CGSTRate=" + cgstrate + ",SGSTRate=" + sgstrate + ",ApplPurWithoutGst='" + purgst + "'  where MedicineID = '" + MedicineID + "' and compcode='" + cocode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute update query. 

                            theCommand.CommandText = "Update ITEMMAST set  iname='" + MedicineName + "',ITYPE='" + itype + "',IUNIT=" + UnitId + ",I2CONVRT='" + ConversionFactor + "',IUNIT2='" + SellingUnit + "'  where icode = '" + icode.Trim() + "' and compcode='" + cocode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
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
    public bool DeleteMedicineMaster(int MedicineID, string cocode, string yearcode, string icode)
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
                        theCommand.CommandText = "delete IPD_MedicineMaster  WHERE MedicineID = '" + MedicineID + "'and compcode='" + cocode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute Delete query.  
                        theCommand.CommandText = "delete ITEMMAST  WHERE icode = '" + icode + "' and compcode='" + cocode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute Delete query.  
                        theCommand.CommandText = "delete ITEMMST1  WHERE icode = '" + icode + "' and compcode='" + cocode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute Delete query.  
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