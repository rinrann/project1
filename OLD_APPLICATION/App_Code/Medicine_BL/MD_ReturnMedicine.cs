using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for MD_ReturnMedicine
/// </summary>
public class MD_ReturnMedicine
{
    public MD_ReturnMedicine(string con)
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
        //theCommand.CommandText = "exec sp_MD_GenerateReturnId '"+ compcode +"','"+ yearcode+"'";
        theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','IC','I','P','Y',NULL";
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
    public bool UpdatePurchaseMedicine(string compcode, string yearcode, string ReturnMedicineID, string SCode, string ReturnDate,  string Total, string Discount, string lessperc, string taxperc)
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
                        theCommand.CommandText = "UPDATE INVHEAD  SET SlCode = '" + SCode + "', docdt = '" + ReturnDate + "',BILLVALUE='" + Total + "',GVALUE='" + Total + "' WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "' and docno='" + ReturnMedicineID + "' and types='Q' and bkcode='IC'";
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
        theCommand.CommandText = "select MCode,MName from  PH_ManufactureMaster where compcode='"+ compcode+"' and status=1";
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
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup where status=1 and compcode='"+ compcode+"'";
        else
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup sub where status=1 and compcode='"+ compcode+"' and sub.GroupID='" + group + "'";
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

    public DataTable DropdownID4(string compcode,string MedicineGroupID, string sub)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (MedicineGroupID == "0")
            theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where status=1 and compcode='"+ compcode +"'";
        else
            theCommand.CommandText = "select mm.MedicineID,mm.MedicineName from  IPD_MedicineMaster mm where status=1 and compcode='"+ compcode+"' and mm.MedicineGroupID='" + MedicineGroupID + "' and mm.SubGroupid='" + sub + "'";
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

    public DataTable getBatchId(string compcode,string yearcode,string mcode,string med,string supplier)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select BatchNo from MD_PurchaseMedicineDetails where compcode='" + compcode + "' and yearcode='" + yearcode + "' and MedicineId='" + med + "' and PurchaseMedicineID in (select PurchaseMedicineID from MD_PurchaseMedicine where compcode='" + compcode + "' and yearcode='" + yearcode + "' and SCode='"+ supplier +"')";
        theCommand.CommandText = "select BatchNo from INVDETL md, IPD_MedicineMaster mm where mm.compcode=md.compcode and md.ICODE=mm.ICODE and md.types='P' and md.compcode='" + compcode + "' and md.yearcode='" + yearcode + "' and mm.MedicineId='" + med + "'";
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
        theCommand.CommandText = "select SCode,SName from  PH_SuppilierMaster where status=1";
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

    public bool InsertPurchaseMedicine(int i,string compcode, string yearcode, string SubGrp, string Discount, string totalpc, string ReturnMedicineID, string SCode, string ReturnDate, string BillNo, string MCode, string MedicineGroupID, string MedicineID, string BatchNo, string ExpiryDate, string Qty, string UnitPrice, string TotalPrice, string LoginUser, string trenddiscount, string tax, string lessperc, string taxperc)
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
                        theCommand.CommandText = "SELECT * FROM INVHEAD  WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "' and types='Q' and docno='" + ReturnMedicineID + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();


                        if (theReader.HasRows)
                        {
                            theReader.Close();
                            /*theCommand.CommandText = "update INVHEAD set GVALUE='" + totalpc + "',BILLVALUE='" + totalpc + "', DOCDT='" + ReturnDate + "', USER02='" + LoginUser + "',logdt02=getdate() where compcode='" + compcode + "' and yearcode='" + yearcode + "' and docno='" + ReturnMedicineID + "' and bkcode='IC'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();*/
                        }
                        else
                        {
                            theReader.Close();
                            /*theCommand.CommandText = "INSERT INTO MD_ReturnMedicine(compcode,yearcode,ReturnMedicineID,Total, SCode, ReturnDate, BillNo, CreatedBy,status,LessPercent,TaxPercent) VALUES ('" + compcode + "','" + yearcode + "','" + ReturnMedicineID + "','" + totalpc + "','" + SCode + "', '" + ReturnDate + "', '" + BillNo + "', '" + LoginUser + "', 1,'" + lessperc + "','" + taxperc + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();*/

                            theCommand.CommandText = "select ISNULL(StkofStores,'') from PARMS where COMPCODE='"+compcode+"' and YEARCODE='"+yearcode+"'";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader1 = theCommand.ExecuteReader();
                            theReader1.Read();
                            string glcode = theReader1[0].ToString(); theReader1.Close();
                            theCommand.CommandText = "INSERT INTO INVHEAD(compcode,yearcode,TYPES,docno,GVALUE,BILLVALUE, glcode,SLCODE,DOCDT,BKCODE, REMARKS, USER01,tag,logdt01) VALUES ('" + compcode + "','" + yearcode + "','Q','" + ReturnMedicineID + "','" + totalpc + "','" + totalpc + "','" + glcode + "','" + SCode + "', '" + ReturnDate + "','IC', 'Return Medicine', '" + LoginUser + "', 1,getdate())";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                            theCommand.CommandText = "update DOCSERIAL set LASTNO='" + ReturnMedicineID + "' where ltrim(rtrim(COMPCODE))=ltrim(rtrim('" + compcode + "')) and YEARCODE='" + yearcode + "' and AUTOCODE='IC' and BOOKCODE='IC'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }// Execute insert query.

                        /*theCommand.CommandText = "INSERT INTO MD_ReturnMedicineDetails(compcode,yearcode,MedicineSubGrp,ReturnMedicineID,MCode,MedicineGroupID,MedicineID,BatchNo,ExpiryDate,Qty,PricePerUnit,TotalPrice,CreatedBy, status,TrendDiscount,Stax) VALUES ('" + compcode + "','" + yearcode + "','" + SubGrp + "',(SELECT ReturnMedicineID FROM MD_ReturnMedicine WHERE ReturnMedicineID='" + ReturnMedicineID + "'),'" + MCode + "', '" + MedicineGroupID + "', '" + MedicineID + "', '" + BatchNo + "', '" + ExpiryDate + "', '" + Qty + "', '" + UnitPrice + "', '" + TotalPrice + "', '" + LoginUser + "', 1,'" + trenddiscount + "','" + tax + "')";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();*/
                        theCommand.CommandText = "INSERT INTO INVDETL(compcode,yearcode,TYPES,Docno,srlno,icode,BatchNo,EXPDATE,IQTY,ITRCD,IRATE,IAMOUNT,BillNo,user01, tag,logdt01) VALUES ('" + compcode + "','" + yearcode + "','Q','" + ReturnMedicineID + "'," + i + ", dbo.Fnc_GetMedicineIcode('" + compcode + "','" + MedicineID + "'), '" + BatchNo + "', '" + ExpiryDate + "', '" + Qty + "','O', '" + UnitPrice + "', '" + TotalPrice + "','" + BillNo + "', '" + LoginUser + "', 1,getdate())";
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
                        theCommand.CommandText = "exec sp_PopulateInvHeadDetl_fromReturn '" + compcode + "','" + yearcode + "','" + id + "','" + type + "','" + user + "'";
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
    public bool DeleteMEdDtls(string compcode,string yearcode,string id)
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
                        theCommand.CommandText = "delete dbo.INVDETL where compcode='"+compcode+"' and yearcode='"+yearcode+"' and docno='" + id + "'";
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
    public DataTable GetPurchaseMedicineDetails(string compcode,string yearcode,string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select *,CONVERT(varchar,pm.docdt,103) purdate,CONVERT(varchar,md.EXPDATE,103) exdate,mm.MedicineName from dbo.INVHEAD pm ,dbo.INVDETL md,IPD_MedicineMaster mm where pm.compcode=md.compcode and pm.compcode=mm.compcode and pm.yearcode=md.yearcode and mm.MedicineID=md.MedicineID and mm.MedicineGroupID=md.MedicineGroupID  and pm.docno=md.docno and pm.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "' and pm.docno='" + id + "'";
        //theCommand.CommandText = "select pm.docno,pm.docdt,pm.chno,isnull(pm.slcode,'0')slcode,CONVERT(varchar,pm.docdt,103) purdate,mm.MedicineID,mm.MedicineName,CONVERT(varchar,md.expdate,103) exdate,md.BatchNo,md.Iqty Qty,md.irate PricePerUnit,md.iamount TotalPrice,mfg.MName,gr.MedicineGroupName,sgr.SubGrName from dbo.INVHEAD pm,dbo.IPD_MedicineMaster mm,dbo.INVDETL md,itemmast it,IPD_MedicineGroup gr,IPD_MedicineSubGroup sgr,PH_ManufactureMaster mfg  where pm.compcode=mm.compcode and pm.compcode=md.compcode and pm.yearcode=md.yearcode and pm.docno=md.docno and pm.TYPES=md.TYPES and dbo.Fnc_GetMedicineIcode(mm.compcode,mm.MedicineID)=md.icode and mfg.MCode=mm.MCode and gr.MedicineGroupID=mm.MedicineGroupID and sgr.ID=mm.SubGroupid and pm.docno='" + id + "'and pm.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "' and pm.TYPES='Q'";
        theCommand.CommandText = "select pm.docno,pm.docdt,md.billNo chno,isnull(pm.slcode,'0')slcode,(select SName from PH_SuppilierMaster where compcode='" + compcode + "' and SCode=pm.slcode) supplier,CONVERT(varchar,pm.docdt,103) retdate,(select CONVERT(varchar,pm.docdt,103) purdate from invhead,invdetl where invhead.compcode=invdetl.compcode and invhead.yearcode=invdetl.yearcode and invhead.docno=invdetl.docno and invdetl.icode=md.icode and invdetl.BatchNo=md.BatchNo and invdetl.types='P') purdate,(select SELLAMOUNT from invdetl,invhead where invhead.compcode=invdetl.compcode and invhead.yearcode=invdetl.yearcode and invhead.docno=invdetl.docno and invdetl.icode=md.icode and invdetl.BatchNo=md.BatchNo and invdetl.types='P') SELLAMOUNT,pm.BILLVALUE,CONVERT(varchar,md.expdate,103) exdate,md.BatchNo,md.Iqty Qty,md.irate PricePerUnit,md.iamount TotalPrice,mm.MedicineID,mm.itype,mm.MedicineName,mfg.MName,gr.MedicineGroupName,sgr.SubGrName,(select isNull(lesspercent,0) from MD_PurchaseMedicineDetails where COMPCODE='" + compcode + "' and YEARCODE='" + yearcode + "' and MedicineID=mm.MedicineID and BATCHNO=md.BATCHNO) lessper,(select isNull(taxpercent,0) from MD_PurchaseMedicineDetails where COMPCODE='" + compcode + "' and YEARCODE='" + yearcode + "' and MedicineID=mm.MedicineID and BATCHNO=md.BATCHNO) taxper from dbo.INVHEAD pm,dbo.INVDETL md,IPD_MedicineMaster mm,IPD_MedicineGroup gr,IPD_MedicineSubGroup sgr,PH_ManufactureMaster mfg where md.compcode=pm.compcode and md.yearcode=pm.yearcode and mm.compcode=pm.compcode and md.docno=pm.docno and md.TYPES=pm.TYPES and md.icode=dbo.Fnc_GetMedicineIcode(mm.compcode,mm.MedicineID)and mfg.COMPCODE=mm.COMPCODE and gr.COMPCODE=mm.COMPCODE and sgr.COMPCODE=mm.COMPCODE and mfg.MCode=mm.MCode and gr.MedicineGroupID=mm.MedicineGroupID and sgr.ID=mm.SubGroupid and pm.docno='" + id + "' and pm.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "' and pm.TYPES='Q'";
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

    public DataTable getPurMedicineRec(string mcode, string med, string batch,string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select CONVERT(varchar,md.ExpiryDate,103) expiredate,md.PricePerUnit unitprice,isnull(md.lesspercent,0) lesspercent,isnull(md.taxpercent,0) taxpercent,pm.SCode from dbo.MD_PurchaseMedicineDetails md,MD_PurchaseMedicine pm  where pm.compcode=md.compcode and pm.yearcode=md.yearcode and pm.PurchaseMedicineID=md.PurchaseMedicineID and md.MedicineID='" + med + "' and md.BatchNo='" + batch + "'";
        theCommand.CommandText = "select CONVERT(varchar,md.EXPDATE,103) expiredate,md.IRATE unitprice/*,isnull(md.lesspercent,0) lesspercent,isnull(md.taxpercent,0) taxpercent*/,pm.SLCODE SCode,pm.chno from dbo.INVDETL md,INVHEAD pm  where pm.compcode=md.compcode and pm.yearcode=md.yearcode and pm.DOCNO=md.DOCNO and /*md.MedicineID='" + med + "' and */md.BATCHNO='" + batch + "' and md.compcode='" + compcode + "' and md.yearcode='" + yearcode + "' and md.types='P'";
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

    public DataTable getPurMedicineRecdetl(string mcode, string med, string batch, string compcode, string yearcode,string sup)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select CONVERT(varchar,md.ExpiryDate,103) expiredate,md.PricePerUnit unitprice,isnull(md.lesspercent,0) lesspercent,isnull(md.taxpercent,0) taxpercent,pm.SCode from dbo.MD_PurchaseMedicineDetails md,MD_PurchaseMedicine pm  where pm.compcode=md.compcode and pm.yearcode=md.yearcode and pm.PurchaseMedicineID=md.PurchaseMedicineID and md.MedicineID='" + med + "' and md.BatchNo='" + batch + "'";
        theCommand.CommandText = "select CONVERT(varchar,md.EXPDATE,103) expiredate,md.IRATE unitprice,(select isNull(lesspercent,0) from MD_PurchaseMedicineDetails where COMPCODE='" + compcode + "' and YEARCODE='" + yearcode + "' and MedicineID=mm.MedicineID and BATCHNO=md.BATCHNO) lesspercent,(select isNull(taxpercent,0) from MD_PurchaseMedicineDetails where COMPCODE='" + compcode + "' and YEARCODE='" + yearcode + "' and MedicineID=mm.MedicineID and BATCHNO=md.BATCHNO) taxpercent,pm.SLCODE,(select BillNo from MD_PurchaseMedicine p,MD_PurchaseMedicineDetails d where p.compcode=d.compcode and p.yearcode=d.yearcode and p.PurchaseMedicineID=d.PurchaseMedicineID and p.COMPCODE='" + compcode + " ' and p.YEARCODE='" + yearcode + "' and d.MedicineID=mm.MedicineID and d.BatchNo=md.BATCHNO)chno,isnull(md.SELLAMOUNT,0) sellamt from dbo.INVDETL md,INVHEAD pm,IPD_MedicineMaster mm  where pm.compcode=md.compcode and pm.yearcode=md.yearcode and pm.DOCNO=md.DOCNO and md.Icode=mm.icode and mm.MedicineID='" + med + "' and md.BATCHNO='" + batch + "' and md.compcode='" + compcode + "' and md.yearcode='" + yearcode + "' and md.types='P' and pm.slcode='" + sup + "'";
        //theCommand.CommandText = "select pm.docno,pm.docdt,md.chno,isnull(pm.slcode,'0')slcode,(select SName from PH_SuppilierMaster where compcode='" + compcode + "' and SCode=pm.slcode) supplier,CONVERT(varchar,pm.docdt,103) purdate,pm.BILLVALUE,CONVERT(varchar,md.expdate,103) expiredate,md.BatchNo,md.Iqty Qty,md.irate PricePerUnit,md.iamount TotalPrice,mm.MedicineID,mm.MedicineName,mfg.MName,gr.MedicineGroupName,sgr.SubGrName,(select isNull(lesspercent,0) from MD_PurchaseMedicineDetails where COMPCODE='" + compcode + "' and YEARCODE='" + yearcode + "' and MedicineID=mm.MedicineID and BATCHNO=md.BATCHNO) lessper,(select isNull(taxpercent,0) from MD_PurchaseMedicineDetails where COMPCODE='" + compcode + "' and YEARCODE='" + yearcode + "' and MedicineID=mm.MedicineID and BATCHNO=md.BATCHNO) taxper from dbo.INVHEAD pm,dbo.INVDETL md,IPD_MedicineMaster mm,IPD_MedicineGroup gr,IPD_MedicineSubGroup sgr,PH_ManufactureMaster mfg where md.compcode=pm.compcode and md.yearcode=pm.yearcode and mm.compcode=pm.compcode and md.docno=pm.docno and md.TYPES=pm.TYPES and md.icode=dbo.Fnc_GetMedicineIcode(mm.compcode,mm.MedicineID)and mfg.COMPCODE=mm.COMPCODE and gr.COMPCODE=mm.COMPCODE and sgr.COMPCODE=mm.COMPCODE and mfg.MCode=mm.MCode and gr.MedicineGroupID=mm.MedicineGroupID and sgr.ID=mm.SubGroupid and pm.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "' and pm.TYPES='P' and md.MedicineID='" + med + "' and */md.BATCHNO='" + batch + "' and pm.slcode-'" + sup + "'";
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

    public DataTable DropdownMedicine(string supp,string compcode,string yearcode)
    {
        if (supp == null)
        {
            supp = "%%";
        }
        else
        {
            supp="'%"+supp+"%'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select mm.ICODE,mm.MedicineID,mm.MedicineName,pm.SCode,pm.PurchaseMedicineID,pmd.PurchaseMedicineID from  MD_PurchaseMedicine pm, MD_PurchaseMedicineDetails pmd,IPD_MedicineMaster mm where mm.compcode=pmd.compcode and pm.compcode=pmd.compcode and pm.yearcode=pmd.yearcode and mm.MedicineID=pmd.MedicineID and pmd.PurchaseMedicineID=pm.PurchaseMedicineID and pm.SCode like " + supp + " and pmd.compcode='" + compcode + "' and pmd.yearcode='"+yearcode+"' and mm.ICODE!='' order by MedicineName";
        theCommand.CommandText = "select distinct(mm.MedicineID),mm.icode,mm.MedicineName from IPD_MedicineMaster mm,INVHEAD pm,invdetl pmd where pm.COMPCODE=pmd.COMPCODE and pm.yearcode=pmd.yearcode and pmd.COMPCODE=mm.COMPCODE and pmd.ICODE=mm.ICODE and pmd.DOCNO=pm.DOCNO and pm.SLCODE like "+supp+" order by mm.MedicineName"; 
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

    public DataTable MedicineDropdown(string supp, string compcode, string yearcode,string itype)
    {
        if (supp == null)
        {
            supp = "%%";
        }
        else
        {
            supp = "'%" + supp + "%'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select mm.ICODE,mm.MedicineID,mm.MedicineName,pm.SCode,pm.PurchaseMedicineID,pmd.PurchaseMedicineID from  MD_PurchaseMedicine pm, MD_PurchaseMedicineDetails pmd,IPD_MedicineMaster mm where mm.compcode=pmd.compcode and pm.compcode=pmd.compcode and pm.yearcode=pmd.yearcode and mm.MedicineID=pmd.MedicineID and pmd.PurchaseMedicineID=pm.PurchaseMedicineID and pm.SCode like " + supp + " and pmd.compcode='" + compcode + "' and pmd.yearcode='" + yearcode + "' and mm.ICODE!='' order by MedicineName";
        theCommand.CommandText = "Select distinct(MedicineID),MedicineName from IPD_MedicineMaster where compcode='" + compcode + "' and itype='"+itype+"' order by MedicineName";
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
}