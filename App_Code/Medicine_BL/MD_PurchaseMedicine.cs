using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for MD_PurchaseMedicine123
/// </summary>
public class MD_PurchaseMedicine
{
    public MD_PurchaseMedicine(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable PurchaseMedicineTable;


    public DataTable GetPurchaseMedicineID(string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_MD_Generatepurchaseid '" + compcode + "','" + yearcode + "'";
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


    public int GetPurchasePricePerUnit(string compcode, string medicineID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("SELECT PricePerUnit FROM IPD_MedicineMaster WHERE compcode='"+ compcode +"' MedicineID = " + medicineID, theConnection);
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



    public bool UpdatePurchaseMedicine(string compcode, string yearcode, string PurchaseMedicineID, string SCode, string PurchaseDate, string BillNo, string Total)
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

                        theCommand.CommandText = "UPDATE MD_PurchaseMedicine SET  SCode = '" + SCode + "', PurchaseDate = '" + PurchaseDate + "', BillNo= '" + BillNo + "',Total='" + Total + "' WHERE PurchaseMedicineID='" + PurchaseMedicineID + "' and compcode='"+ compcode +"' and yearcode='"+ yearcode +"'";
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
        theCommand.CommandText = "select MCode,MName from  PH_ManufactureMaster where status=1 and compcode='"+ compcode +"' order by MName ";
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

    public DataTable DropdownID3(string compcode, string MCode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select distinct gr.* from IPD_MedicineGroup  gr,IPD_MedicineMaster mm where gr.MedicineGroupID=mm.MedicineGroupID and gr.compcode=mm.compcode and gr.status=1 and mm.status=1 and mm.compcode='"+ compcode +"' and mm.MCode='" + MCode + "'";
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

    public DataTable DropdownID4(string compcode,string sub)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (sub == "0")
            theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1 and compcode='"+ compcode +"' order by MedicineName";
        else
            theCommand.CommandText = "select mm.MedicineID,mm.MedicineName from  IPD_MedicineMaster mm where status=1 and compcode='"+ compcode +"' and mm.SubGroupid='" + sub + "'  order by MedicineName";
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

    public DataTable DropdownMedicine(string compcode,string type)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1 and compcode='"+ compcode +"' and itype='"+type+"' order by MedicineName";
        
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

    public DataTable Dropdown_Reagent(string cocode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select * from  PH_ReagentMaster where status=1";
        theCommand.CommandText = "select ltrim(rtrim(icode))icode,iname from  ITEMMAST where compcode='" + cocode + "' and itype='G' and tag=1";
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


        return PurchaseMedicineTable;
    }

    public DataTable DropdownState()
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from state order by names";
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
        theCommand.CommandText = "select SCode,SName from  PH_SuppilierMaster where status=1 and compcode='"+ compcode +"' order by SName";
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
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup where status=1 and compcode='"+ compcode +"' order by SubGrName";
        else
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup sub where status=1 and compcode='"+ compcode +"' and sub.GroupID='" + group + "' order by SubGrName";
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

    public bool CheckIfPurchaseMedicineExists(string compcode, string yearcode, string PurchaseMedicineName, string PurchaseMedicineID)
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

    public bool InsertPurchaseMedicine(string compcode, string yearcode, string subgroup, string Discount, string totalpc, string PurchaseMedicineID, string SCode, string PurchaseDate, string BillNo, string MCode, string MedicineGroupID, string MedicineID, string BatchNo, string ExpiryDate, string Qty, string UnitPrice, string TotalPrice, string LoginUser, string SellPricePerUnit, string freeqty, string trenddisc, string tax, string totqty, string costprice, string lessperc, string taxperc, string lespersingle, string taxpersingle, string itype, string fromloc, string toloc, string gsttype,string HSNCode,string CGSTRt,string CGSTAmt,string SGSTRt,string SGSTAmt,string IGSTRt,string IGSTAmt,string Billvalue,string GSTAmt,string convFact,string netAmt,string rounfOffAmt)
    {
        if (SellPricePerUnit == "")
            SellPricePerUnit = "0";
        else
            SellPricePerUnit = "'" + SellPricePerUnit + "'";
        if (freeqty == "")
            freeqty = "0";
        if (trenddisc == "")
            trenddisc = "0";
        if (tax == "")
            tax = "0";
        if (lessperc == "")
            lessperc = "0";
        if (taxperc == "")
            taxperc = "0";
        if (costprice == "")
            costprice = "0";
        if (totqty == "")
            totqty = Qty;

        if (Discount == "")
        {
            Discount = "0";
        }
        if (lespersingle == "")
        {
            lespersingle = "0";
        }
        else
        {
            lespersingle = "'" + lespersingle + "'";
        }

        if (taxpersingle == "")
        {
            taxpersingle = "0";
        }
        else
        {
            taxpersingle = "'" + taxpersingle + "'";
        }
        if (convFact == "")
        {
            convFact = "1";
        }

        if (rounfOffAmt == "")
        {
            rounfOffAmt = "0";
        }

        if (netAmt == "")
        {
            netAmt = "0";
        }
        int effectedRows = 0;
        Decimal sellQty = (Convert.ToDecimal(totqty) + Convert.ToDecimal(freeqty)) * Convert.ToDecimal(convFact);
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


                        theCommand.CommandText = "SELECT * FROM MD_PurchaseMedicine WHERE PurchaseMedicineID='" + PurchaseMedicineID + "' and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows)
                            theReader.Close();
                        else
                        {
                            theReader.Close();
                            //theCommand.CommandText = "INSERT INTO MD_PurchaseMedicine(compcode,yearcode,PurchaseMedicineID,Total, SCode, PurchaseDate, BillNo, CreatedBy,Discount, status,TaxPercent,LessPercent,I2CONVRT,TotalSellQty) VALUES ('" + compcode + "','" + yearcode + "','" + PurchaseMedicineID + "','" + totalpc + "','" + SCode + "', '" + PurchaseDate + "', '" + BillNo + "', '" + LoginUser + "','" + Discount + "', 1,'" + taxperc + "','" + lessperc + "','"+ con +"')";
                            theCommand.CommandText = "INSERT INTO MD_PurchaseMedicine(compcode,yearcode,PurchaseMedicineID,Total, SCode, PurchaseDate, BillNo, CreatedBy,Discount, status,FromLoc,ToLoc,GSTType,NetAmt,RoundOffAmt) VALUES ('" + compcode + "','" + yearcode + "','" + PurchaseMedicineID + "','" + totalpc + "','" + SCode + "', '" + PurchaseDate + "', '" + BillNo + "', '" + LoginUser + "','" + Discount + "', 1,'" + fromloc + "','" + toloc + "','" + gsttype + "','"+ netAmt +"','"+ rounfOffAmt +"')";
                            theCommand.Transaction = tran as SqlTransaction;
                            effectedRows = theCommand.ExecuteNonQuery();
                        }
                       // string tdate = ExpiryDate.Substring(6, 4) + "/" + ExpiryDate.Substring(3, 2) + "/" + ExpiryDate.Substring(0, 2);
                        string tdate = ExpiryDate;
                        theCommand.CommandText = "INSERT INTO MD_PurchaseMedicineDetails(compcode,yearcode,EntryType,MedicineSubGrp,PurchaseMedicineID,MCode,MedicineGroupID,MedicineID,BatchNo,ExpiryDate,Qty,PricePerUnit,TotalPrice,CreatedBy, status,SellPricePerUnit,FQty,TrendDiscount,STax,CostPrice,TotalQty,lesspercent,taxpercent,HSNCode,CGSTRt,CGSTAmt,SGSTRt,SGSTAmt,IGSTRt,IGSTAmt,Billvalue,GSTAmt,I2CONVRT,TotalSellQty) VALUES ('" + compcode + "','" + yearcode + "',1,'" + subgroup + "',(SELECT PurchaseMedicineID FROM MD_PurchaseMedicine WHERE PurchaseMedicineID='" + PurchaseMedicineID + "'),'" + MCode + "', '" + MedicineGroupID + "', '" + MedicineID + "', '" + BatchNo + "', '" + tdate + "', '" + Qty + "', '" + UnitPrice + "', '" + TotalPrice + "', '" + LoginUser + "', 1," + SellPricePerUnit + ",'" + freeqty + "','" + trenddisc + "','" + tax + "','" + costprice + "','" + totqty + "'," + lespersingle + "," + taxpersingle + ",'" + HSNCode + "','" + CGSTRt + "','" + CGSTAmt + "','" + SGSTRt + "','" + SGSTAmt + "','" + IGSTRt + "','" + IGSTAmt + "','" + Billvalue + "','" + GSTAmt + "','" + convFact + "','" + sellQty + "')";
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
    public bool InsUpdInv(string compcode, string yearcode, string id, string type, string user,string itype)
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
                        theCommand.CommandText = "exec sp_PopulateInvHeadDetl '" + compcode + "','" + yearcode + "','" + id + "','" + type + "','" + user + "'";
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
                        theCommand.CommandText = "delete dbo.MD_PurchaseMedicineDetails where PurchaseMedicineID='" + id + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
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

    public DataTable GetPurchaseMedicineDetails(string compcode, string yearcode, string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select mm.MedicineName, pm.PurchaseMedicineID,pm.SCode,pm.Total,md.TotalPrice,pm.BillNo,CONVERT(varchar,pm.PurchaseDate,103) purdate, CONVERT(varchar,md.ExpiryDate,103) exdate,md.MCode,md.MedicineGroupID,md.MedicineSubGrp,md.MedicineID, md.BatchNo,md.Qty,md.PricePerUnit,md.SellPricePerUnit,pm.TaxPercent,pm.LessPercent,md.FQty,md.TrendDiscount,md.STax,md.CostPrice,md.TotalQty,mfg.MName,gr.MedicineGroupName,sgr.SubGrName,ps.SName,md.lesspercent singleLess,md.taxpercent singleTax from  dbo.MD_PurchaseMedicine pm ,dbo.MD_PurchaseMedicineDetails md,IPD_MedicineMaster mm,IPD_MedicineGroup gr,IPD_MedicineSubGroup sgr,PH_ManufactureMaster mfg,PH_SuppilierMaster ps where  mm.MedicineID=md.MedicineID and  pm.PurchaseMedicineID=md.PurchaseMedicineID and mm.status=1 and mfg.MCode=md.MCode and gr.MedicineGroupID=md.MedicineGroupID and sgr.ID=md.MedicineSubGrp and pm.SCode=ps.SCode AND pm.PurchaseMedicineID='" + id + "' and pm.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "'";
        theCommand.CommandText = "select mm.MedicineName, pm.CHNO PurchaseMedicineID,pm.SLCODE SCode,isNull(pm.GVALUE,0) grossvalue,isNull(pm.ROFFAMT,0) as roundOff,pm.BILLVALUE Total,pmd.TotalPrice,ppm.BillNo,CONVERT(varchar,pm.DOCDT,103) purdate, CONVERT(varchar,md.EXPDATE,103) exdate,pmd.SellPricePerUnit,mm.MCode,mm.MedicineGroupID,mm.SubGroupid MedicineSubGrp,mm.MedicineID,mm.itype, md.BATCHNO BatchNo,pmd.Qty Qty,pmd.PricePerUnit,pmd.CostPrice,md.SELLAMOUNT SellPricePerUnit,ppm.TaxPercent,ppm.LessPercent,pmd.FQty,pmd.TrendDiscount,pmd.STax,md.IRATE,md.IQTY TotalQty,mfg.MName,ps.SName,pmd.lesspercent singleLess,pmd.taxpercent singleTax,ppm.FromLoc,ppm.toloc,ppm.GSTType,pmd.HSNCode,pmd.CGSTRt,pmd.CGSTAmt,pmd.SGSTRt,pmd.SGSTAmt,pmd.IGSTRt,pmd.IGSTAmt,pmd.Billvalue,pmd.GSTAmt from INVHEAD pm,INVDETL md,dbo.MD_PurchaseMedicine ppm ,dbo.MD_PurchaseMedicineDetails pmd,IPD_MedicineMaster mm,PH_ManufactureMaster mfg,PH_SuppilierMaster ps where pm.COMPCODE=md.COMPCODE and pm.YEARCODE=md.YEARCODE and pm.DOCNO=md.DOCNO and mm.COMPCODE=md.COMPCODE and mfg.COMPCODE=mm.COMPCODE and ps.COMPCODE=mm.COMPCODE and pmd.COMPCODE=md.COMPCODE and pmd.YEARCODE=md.YEARCODE and  ppm.COMPCODE=md.COMPCODE and ppm.YEARCODE=md.YEARCODE and pmd.PurchaseMedicineID=ppm.PurchaseMedicineID  and ppm.PurchaseMedicineID=pm.CHNO and mm.ICODE=md.ICODE and  pmd.MedicineID=mm.MedicineID and  pm.DOCNO=md.DOCNO and mm.status=1 and mfg.MCode=mm.MCode and  pm.SLCODE=ps.SCode AND  md.TYPES=pm.TYPES and pm.TYPES='P' and pm.CHNO='" + id + "' and pm.compcode='" + compcode + "' and  pm.yearcode='" + yearcode + "'";
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

    public DataTable GetPurchaseMedicineSellPriceDetails(string compcode, string yearcode, string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,pm.PurchaseDate,103) purdate,CONVERT(varchar,md.ExpiryDate,103) exdate from dbo.MD_PurchaseMedicine pm ,dbo.MD_PurchaseMedicineDetails md,IPD_MedicineMaster mm where pm.compcode=md.compcode and pm.compcode=mm.compcode and pm.yearcode=md.yearcode and mm.MedicineID=md.MedicineID and mm.status=1 and pm.PurchaseMedicineID=md.PurchaseMedicineID and pm.PurchaseMedicineID='" + id + "' and pm.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "'";
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


    public DataTable DropdownGrp(string compcode,string med)
    {
        DataTable GrpTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select gr.MedicineGroupID,gr.MedicineGroupName,mm.MedicineGroupID MedGrp,mm.MedicineID from IPD_MedicineGroup gr,IPD_MedicineMaster mm where gr.compcode=mm.compcode and gr.MedicineGroupID=mm.MedicineGroupID and gr.status=1 and mm.status=1 and mm.MedicineID='"+med+"' and mm.compcode='"+ compcode +"'";
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

    public DataTable DropdownSubGrp(string compcode,string med)
    {
        DataTable SubGrpTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select sgr.ID,sgr.SubGrName,mm.SubGroupid MedGrp,mm.MedicineID from IPD_MedicineSubGroup sgr,IPD_MedicineMaster mm where sgr.compcode=mm.compcode and sgr.ID=mm.SubGroupid and mm.MedicineID='"+med+"' and sgr.compcode='"+ compcode +"' and sgr.status='1' and mm.status='1'";
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

    public DataTable DropdownMfg(string compcode, string med)
    {
        DataTable MfgTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select mfg.MCode,mfg.MName,mm.MedicineID,mm.MCode from PH_ManufactureMaster mfg,IPD_MedicineMaster mm where mfg.compcode=mm.compcode and mfg.MCode=mm.MCode and mm.MedicineID='"+med+"' and mfg.compcode='"+ compcode +"' and mm.status=1 and mfg.Status='1'";
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

    public DataTable GetMedGrpByID(string compcode,string MGrp)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineGroupID,MedicineGroupName from IPD_MedicineGroup where compcode='"+ compcode+"' and status=1 and MedicineGroupID='"+MGrp+"'";
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

    public DataTable GetMedSubGrpByID(string compcode, string MSubGrp)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ID,SubGrName from IPD_MedicineSubGroup where compcode='"+ compcode +"' and status=1 and ID='" + MSubGrp + "'";
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

    public DataTable GetMedMfgByMCode(string compcode, string Mfgcd)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MCode,MName from  PH_ManufactureMaster where compcode='"+ compcode+"' and status=1 and MCode='"+Mfgcd+"'";
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
    public DataTable getmedicineDetailInfo(string compcode, string yearcode,string medid)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select mm.MedicineID,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mf.MName,mg.MedicineGroupName,msg.SubGrName,CONVERT(varchar,pm.PurchaseDate,103) purchaseDate,pmd.BatchNo,CONVERT(varchar,pmd.ExpiryDate,103) ExpiryDate,pm.SCode,pm.TaxPercent,pm.LessPercent,pmd.PricePerUnit from IPD_MedicineMaster mm,IPD_MedicineGroup mg,IPD_MedicineSubGroup msg,PH_ManufactureMaster mf,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where mf.MCode=mm.MCode and mg.MedicineGroupID=mm.MedicineGroupID and msg.Id=mm.SubGroupid and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where MedicineID=mm.MedicineID) and mm.MedicineId='" + medid + "'";
        //theCommand.CommandText = "select mm.MedicineID,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,pm.SCode,mm.itype from IPD_MedicineMaster mm,MD_PurchaseMedicine pm,MD_PurchaseMedicineDetails pmd where pm.compcode=pmd.compcode and pm.yearcode=pmd.yearcode and pm.PurchaseMedicineID=pmd.PurchaseMedicineID and pmd.compcode=mm.compcode and pmd.MedicineID=mm.MedicineID and pmd.RowID=(select MAX(RowID) from MD_PurchaseMedicineDetails where MedicineID=mm.MedicineID) and mm.MedicineId='" + medid + "' and pm.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "'";
        theCommand.CommandText = "select mm.MedicineID,mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mm.itype from  IPD_MedicineMaster mm where mm.compcode='" + compcode + "' and mm.MedicineId='" + medid + "'";
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

    public DataTable GetMedGrpSubGrpMfgBymedID(string compcode, string medid)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select mm.MedicineGroupID,gr.MedicineGroupName,mm.SubGroupid,sgr.SubGrName,mm.MCode,ph.MName from IPD_MedicineMaster mm,IPD_MedicineGroup gr,IPD_MedicineSubGroup sgr, PH_ManufactureMaster ph where gr.compcode=mm.compcode and sgr.compcode=sgr.compcode and ph.compcode=mm.compcode and ph.MCode=mm.MCode and gr.MedicineGroupID=mm.MedicineGroupID and sgr.ID=mm.SubGroupid and mm.compcode='" + compcode + "' and mm.status=1 and mm.MedicineID='" + medid + "'";
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
    public DataTable getTaxDetails(string compcode, string yearcode, string MedicineId, string ManufacturerId)
    {
        // Connection.
        string str;
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        str = "select HSNCode,isNull(CGSTRate,0)CGSTRate,isNull(SGSTRate,0)SGSTRate,isNull(IGSTRate,0)IGSTRate,isNull(ConversionFactor,1)ConversionFactor,isNull(ApplPurWithoutGst,0)ApplPurWithoutGst from IPD_MedicineMaster md where md.compcode='" + compcode + "' /*and md.yearcode='" + yearcode + "'*/ and  md.medicineid='" + MedicineId + "' and MCode='" + ManufacturerId + "'";


        theCommand.CommandText = str;
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BatchTable = new DataTable();
        theAdapter.Fill(BatchTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BatchTable;
    }
}