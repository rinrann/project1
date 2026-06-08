using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for MD_Adjustment
/// </summary>
public class MD_Adjustment
{
    public MD_Adjustment(string con)
	{
        conString = con;
	}
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable PurchaseMedicineTable;


    public DataTable GetAdjustmentID(string compcode, string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "exec sp_MD_GenerateAdjId '" + compcode + "','" + yearcode + "'";
        theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','AS','I','P','Y',NULL";
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
        theCommand.CommandText = "select * from  MD_ReturnMedicine where status=1";
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
    public bool UpdatePurchaseMedicine(string compcode, string yearcode, string docno, string SCode, string Date, string reason, string Total, string Discount, string lessperc, string taxperc)
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
            theCommand.CommandType = CommandType.Text;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        // Discount='" + Discount + "', 
                        theCommand.CommandText = "UPDATE MD_Adjustment  SET SlCode = '" + SCode + "', DOCDT = '" + Date + "', REMARKS= '" + reason + "',GVALUE='" + Total + "',BILLVALUE='" + Total + "' WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "' and ltrim(rtrim(docno))=ltrim(rtrim('" + docno + "'))";
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
    public DataTable DropdownID2(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MCode,MName from  PH_ManufactureMaster where compcode='" + compcode + "' and status=1";
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
    public DataTable DropdownID3(string compcode)
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

    public DataTable DropdownSubGroup(string compcode, string group)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (group == "0")
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup where status=1 and compcode='" + compcode + "'";
        else
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup sub where status=1 and compcode='" + compcode + "' and sub.GroupID='" + group + "'";
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

    public DataTable DropdownID4(string compcode, string MedicineGroupID, string sub)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (MedicineGroupID == "0")
            theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1 and compcode='" + compcode + "'";
        else
            theCommand.CommandText = "select mm.MedicineID,mm.MedicineName from  IPD_MedicineMaster mm where status=1 and compcode='" + compcode + "' and mm.MedicineGroupID='" + MedicineGroupID + "' and mm.SubGroupid='" + sub + "'";
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

    public DataTable getBatchId(string compcode, string yearcode, string mcode, string med, string supplier)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select BatchNo from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and MedicineId='" + med + "' and PurchaseMedicineID in (select PurchaseMedicineID from MD_PurchaseMedicine where compcode='" + compcode + "' and yearcode='" + yearcode + "' and SCode='" + supplier + "')";
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
    public DataTable DropdownID5(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select SCode,SName from  PH_SuppilierMaster where compcode='"+ compcode +"' and status=1";
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

    public bool InsertPurchaseMedicine(int i,string compcode, string yearcode, string SubGrp, string Discount, string totalpc, string docno, string SCode, string Date, string reason, string MCode, string MedicineGroupID, string MedicineID, string BatchNo, string ExpiryDate, string Qty,string trcd, string UnitPrice, string TotalPrice, string LoginUser, string trenddiscount, string tax, string lessperc, string taxperc)
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
                        theCommand.CommandText = "SELECT * FROM MD_Adjustment  WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "' and docno='" + docno + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();


                        if (theReader.HasRows)
                            theReader.Close();
                        else
                        {
                            theReader.Close();
                            theCommand.CommandText = "INSERT INTO MD_Adjustment(compcode,yearcode,TYPES,docno,GVALUE,BILLVALUE, SLCODE, DOCDT,BKCODE, REMARKS, USER01,tag,logdt01) VALUES ('" + compcode + "','" + yearcode + "','I','" + docno + "','" + totalpc + "','" + totalpc + "','" + SCode + "', '" + Date + "','AS', '" + reason + "', '" + LoginUser + "', 1,getdate())";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                            theCommand.CommandText = "update DOCSERIAL set LASTNO='" + docno + "' where ltrim(rtrim(COMPCODE))=ltrim(rtrim('" + compcode + "')) and YEARCODE='" + yearcode + "' and AUTOCODE='AS' and BOOKCODE='AS'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();

                        }// Execute insert query.

                        theCommand.CommandText = "INSERT INTO MD_AdjustmentDtls(compcode,yearcode,TYPES,Docno,srlno,icode,MedicineID,BatchNo,ExpiryDate,IQTY,ITRCD,IRATE,IAMOUNT,user01, tag,logdt01) VALUES ('" + compcode + "','" + yearcode + "','I','" + docno + "'," + i + ", dbo.Fnc_GetMedicineIcode('" + compcode + "','" + MedicineID + "'), '" + MedicineID + "', '" + BatchNo + "', '" + ExpiryDate + "', '" + Qty + "','" + trcd + "', '" + UnitPrice + "', '" + TotalPrice + "', '" + LoginUser + "', 1,getdate())";
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
    public bool InsUpdInv(string compcode, string yearcode, string id, string type, string user)
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
                        theCommand.CommandText = "exec sp_PopulateInvHeadDetl_fromAdj '" + compcode + "','" + yearcode + "','" + id + "','" + type + "','" + user + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return true;

                }
                catch
                {
                    tran.Rollback();
                    return false;
                    throw;
                }
            }
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
    public bool DeleteMEdDtls(string compcode, string yearcode, string id)
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
                        theCommand.CommandText = "delete dbo.MD_AdjustmentDtls where ltrim(rtrim(docno))=ltrim(rtrim('" + id + "'))";
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
    public DataTable GetPurchaseMedicineDetails(string compcode, string yearcode,string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pm.DOCNO,CONVERT(varchar,pm.docdt,103) purdate,pm.REMARKS,pm.SLCODE,md.MedicineID,md.ITRCD,md.IQTY,md.IRATE,md.IAMOUNT,CONVERT(varchar,md.ExpiryDate,103) exdate,md.batchno,mm.itype from dbo.MD_Adjustment pm ,dbo.MD_AdjustmentDtls  md,IPD_MedicineMaster mm where mm.compcode=md.compcode and mm.MedicineID=md.MedicineID and pm.compcode=md.compcode and pm.yearcode=md.yearcode and pm.docno=md.docno and pm.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "' and pm.docno='" + id + "'";
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

    public DataTable getPurMedicineRec(string mcode, string med, string batch)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select CONVERT(varchar,ExpiryDate,103) expiredate,PricePerUnit unitprice from dbo.MD_PurchaseMedicineDetails  where MCode='" + mcode + "' and MedicineID='" + med + "' and BatchNo='" + batch + "'";
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

    public DataTable DropdownMfg(string med)
    {
        DataTable MfgTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select mfg.MCode,mfg.MName,mm.MedicineID,mm.MCode from PH_ManufactureMaster mfg,IPD_MedicineMaster mm where mfg.MCode=mm.MCode and mm.MedicineID='" + med + "' and mfg.Status='1';";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MfgTable = new DataTable();
        theAdapter.Fill(MfgTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MfgTable;
    }

    public DataTable GetMedGrpByID(string MGrp)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineGroupID,MedicineGroupName from IPD_MedicineGroup where MedicineGroupID='" + MGrp + "'";
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

    public DataTable DropdownMedicine(string compcode,string itype)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1 and compcode='" + compcode + "' and itype='"+itype+"' order by MedicineName";

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

    public DataTable DropdownGrp(string med)
    {
        DataTable GrpTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select gr.MedicineGroupID,gr.MedicineGroupName,mm.MedicineGroupID MedGrp,mm.MedicineID from IPD_MedicineGroup gr,IPD_MedicineMaster mm where gr.MedicineGroupID=mm.MedicineGroupID and mm.MedicineID='" + med + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        GrpTable = new DataTable();
        theAdapter.Fill(GrpTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return GrpTable;
    }

    public DataTable DropdownSubGrp(string med)
    {
        DataTable SubGrpTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select sgr.ID,sgr.SubGrName,mm.SubGroupid MedGrp,mm.MedicineID from IPD_MedicineSubGroup sgr,IPD_MedicineMaster mm where sgr.ID=mm.SubGroupid and mm.MedicineID='" + med + "' and sgr.status='1'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        SubGrpTable = new DataTable();
        theAdapter.Fill(SubGrpTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return SubGrpTable;
    }

    public DataTable GetMedSubGrpByID(string MSubGrp)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ID,SubGrName from IPD_MedicineSubGroup where ID='" + MSubGrp + "'";
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

    public DataTable GetMedMfgByMCode(string Mfgcd)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MCode,MName from  PH_ManufactureMaster where  MCode='" + Mfgcd + "'";
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
    public DataTable GridPopup(string compcode, string yearcode, string invoiceid, string scode, string date, string total)
    {
        /*if (invoiceid == "")
            invoiceid = "null";
        if (scode == "")
            scode = "null";
        if (total == "")
            total = "null";*/
        if (date != "null")
            date = "'" + date + "'";

        string str = "";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        str = "select a.docno,convert(varchar,a.docdt,103)docdt,s.SName,a.billvalue from MD_Adjustment a,PH_SuppilierMaster s where a.compcode=s.compcode and a.slcode=s.scode and a.compcode='" + compcode + "' and a.yearcode='" + yearcode + "' and a.docno like '" + invoiceid + "%' and a.slcode like '" + scode + "%'";
        if (date == "null" || date == "")
        {
            str =str+ "";
        }
        else
        {
            str =str+ " and date="+ date+"";
        }
        if (total != "")
        {
            str =str+ " and billvalue=" + total + "";
        }
        theCommand.CommandText = str;
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