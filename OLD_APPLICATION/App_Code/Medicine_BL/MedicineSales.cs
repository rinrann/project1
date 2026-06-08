using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for MedicineSales
/// </summary>
public class MedicineSales
{
    public MedicineSales(string con)
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
    public DataTable DropdownSubGroup(string compcode,string group)
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
            theCommand.CommandText = "select * from dbo.IPD_MedicineSubGroup sub where sub.status=1 and sub.compcode='"+ compcode+"' and sub.GroupID='" + group + "'";
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


    public int GetPurchasePricePerUnit(string compcode, string yearcode, string medicineID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        //theAdapter = new SqlDataAdapter("SELECT PricePerUnit FROM /*IPD_MedicineMaster*/MD_PurchaseMedicineDetails WHERE compcode='"+ compcode+"' and yearcode='"+ yearcode+"' and MedicineID = " + medicineID, theConnection);
        theAdapter = new SqlDataAdapter("SELECT SELLAMOUNT PricePerUnit FROM INVDETL WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "' and icode='" + medicineID + "'", theConnection);
        
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
    public DataTable DropdownID1(string compcode, string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select PurchaseMedicineID,PurchaseMedicineName from  MD_PurchaseMedicine where status=1 and compcode='"+ compcode+"' and yearcode='"+ yearcode+"'";
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
    public DataTable DropdownBatchNo(string compcode,string yearcode,string MedicineId, string ManufacturerId,string type,string billno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "EXEC  sp_MD_FetchBatchNo " + MedicineId + "";
        //theCommand.CommandText = "select  md.BatchNo,CONVERT(varchar,md.ExpiryDate,103) ExDate  from MD_PurchaseMedicineDetails md where md.compcode='"+ compcode+"' and md.yearcode='"+ yearcode+"' and md.status=1 and md.MedicineID= '" + MedicineId + "' /*and md.Mcode='" + ManufacturerId + "' and dbo.Fnc_MD_AvailablePurchaseStock('"+ compcode+"','"+ yearcode+"','" + MedicineId + "','" + ManufacturerId + "',md.BatchNo)>0*/";
        theCommand.CommandText = "select  md.BatchNo,CONVERT(varchar,md.ExpDate,103) ExDate,md.BatchNo+' ('+CONVERT(varchar,md.ExpDate,103)+')' BatchExDate  from BATDETL md where md.compcode='" + compcode + "' /*and md.yearcode='" + yearcode + "'*/ and md.icode='" + MedicineId + "' and isnull(CURSTOCK,0)>0";
        
        /*if (type == "I")
        {
            theCommand.CommandText = "select  md.BatchNo,CONVERT(varchar,md.ExpiryDate,103) ExDate  from MD_PurchaseMedicineDetails md where md.compcode='"+ compcode+"' and md.yearcode='"+ yearcode+"' and md.status=1 and md.MedicineID= '" + MedicineId + "' and md.Mcode='" + ManufacturerId + "' and dbo.Fnc_MD_AvailablePurchaseStock('"+ compcode+"','"+ yearcode+"','" + MedicineId + "','" + ManufacturerId + "',md.BatchNo)>0";
        }
        else
        {
            theCommand.CommandText = "select  md.BatchNo,CONVERT(varchar,md.ExpiryDate,103) ExDate  from MD_MedicineSaleDtls md where md.compcode='" + compcode + "' and md.yearcode='" + yearcode + "' and md.MedicineID= '" + MedicineId + "' and md.Mcode='" + ManufacturerId + "' and salebillnoId='" + billno + "'";
        }*/
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
    /*public DataTable ExpiryDate(string compcode, string yearcode, string MedicineId, string ManufacturerId, string BatchNo)
    {
        // Connection.
        string str;
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "EXEC  sp_MD_FetchBatchNo " + MedicineId + "";
        str = "select  md.BatchNo,CONVERT(varchar,md.ExpiryDate,103) ExDate  from MD_PurchaseMedicineDetails md where md.compcode='" + compcode + "' and md.yearcode='" + yearcode + "' and md.status=1 and md.MedicineID= '" + MedicineId + "'";
        if (ManufacturerId != "")
        {
            str = str + " and md.Mcode='" + ManufacturerId + "'";
        }
        if (BatchNo != "")
        {
            str = str + " and BatchNo='" + BatchNo + "'";
        }
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
    }*/
    public bool UpdatePurchaseMedicine(string compcode, string yearcode, string SalesBillNo, string PatientName, string PatientAddress, string CardNo, string DoctorName, string Total, string SaleDate, string Discount, string WingsId)
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
                        theCommand.CommandText = "UPDATE MD_MedicineSales SET WingsID='"+WingsId+"',PatientName = '" + PatientName + "',DoctorName='" + DoctorName + "',SaleDate='" + SaleDate + "',/*Discount='" + Discount + "',*/ PatientAddress = '" + PatientAddress + "', CardNo= '" + CardNo + "',Total='" + Total + "' WHERE SalesBillNo='" + SalesBillNo + "' and compcode='"+ compcode+"' and yearcode='"+ yearcode+"'";
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


    public bool UpdatePurchaseMedicineNew(string compcode, string yearcode, string SalesBillNo, string PatientName, string PatientAddress, string CardNo, string DoctorName, string Total, string SaleDate, string Discount)
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
                        theCommand.CommandText = "UPDATE MD_MedicineSales SET PatientName = '" + PatientName + "',DoctorName='" + DoctorName + "',SaleDate='" + SaleDate + "',/*Discount='" + Discount + "',*/ PatientAddress = '" + PatientAddress + "', CardNo= '" + CardNo + "',Total='" + Total + "' WHERE SalesBillNo='" + SalesBillNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
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


    public string SalesParameter(string compcode, string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select isnull(MedicineIssue,'W') from parms where compcode='"+compcode+"' and yearcode='"+ yearcode+"'";
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


        return PurchaseMedicineTable.Rows[0][0].ToString().Trim();
    }
    public DataTable TxtBoxList(string compcode,string type)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_txtboxforMedicineSales '"+compcode+"','"+type+"'";
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
    /*public DataTable DropdownID2(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MCode,MName from  PH_ManufactureMaster where status=1 and compcode='" + compcode + "'";
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
        theCommand.CommandText = "select MedicineGroupID,MedicineGroupName from  IPD_MedicineGroup where status=1 and compcode='"+ compcode+"'";
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
    }*/
    public DataTable getMedicineList(string compcode,string MedicineGroupID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where compcode='" + compcode  + "' and status=1 AND MedicineGroupID='" + MedicineGroupID + "'";

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

    public DataTable getInjectionsList(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where compcode='" + compcode + "' and status=1 AND itype='I'";

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
    /*public DataTable DropdownID5(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  GN_DoctorMaster where status=1 and compcode='"+ compcode+"'";
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
        theCommand.CommandText = "SELECT COUNT(*) FROM MD_PurchaseMedicine WHERE compcode='"+ compcode+"' and yearcode='"+ yearcode+"' and PurchaseMedicineName='" + PurchaseMedicineName + "' AND PurchaseMedicineID<>" + PurchaseMedicineID;
        theCommand.CommandType = CommandType.Text;
        int count = (int)theCommand.ExecuteScalar();

        // Clean up.
        theConnection.Dispose();
        theAdapter.Dispose();

        return count > 0;
    }*/
    public bool InsertSaleMedicine(int i, string compcode, string yearcode, string SubGroup, string PatientName, string WingsId, string PatientAddress, string CardNo, string DoctorName, string SalesBillNo, string SaleDate, string Total, string Discount, string MCode, string MedicineGroupID, string MedicineID, string BatchNo, string ExpiryDate, string Qty, string UnitPrice, string TotalPrice, string LoginUser, string rcvdt, string receiveby, string issueby)
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

                        //theCommand.CommandText = "SELECT * FROM MD_MedicineSales WHERE SalesBillNo='" + SalesBillNo + "' and compcode='"+ compcode+"' and yearcode='"+ yearcode+"'";
                        theCommand.CommandText = "SELECT * FROM INVHEAD WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "' and docno='" + SalesBillNo + "' and bkcode='IC'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows)
                        {
                            theReader.Close();
                            theCommand.CommandText = "update INVHEAD set issue_to='" + WingsId + "',GVALUE='" + Total + "',BILLVALUE='" + Total + "', DOCDT='" + SaleDate + "', USER02='" + LoginUser + "',logdt02=getdate(),slcode='" + PatientName + "' where compcode='" + compcode + "' and yearcode='" + yearcode + "' and docno='" + SalesBillNo + "' and bkcode='IC'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            theReader.Close();//SLCODE,'" + PatientName + "',
                            //theCommand.CommandText = "INSERT INTO MD_MedicineSales(compcode,yearcode,WingsID,PatientName,PatientAddress, CardNo, DoctorName, SalesBillNo,SaleDate,Total/*,Discount*/) VALUES ('" + compcode + "','" + yearcode + "','" + WingsId + "','" + PatientName + "','" + PatientAddress + "','" + CardNo + "', '" + DoctorName + "', '" + SalesBillNo + "','" + SaleDate + "', '" + Total + "'/*,'" + Discount + "'*/)";
                            theCommand.CommandText = "INSERT INTO INVHEAD(compcode,yearcode,TYPES,docno,GVALUE,BILLVALUE, issue_to, DOCDT,BKCODE, REMARKS, USER01,tag,logdt01,ReceiveBy,ReceiveDate,Issueby,slcode) VALUES ('" + compcode + "','" + yearcode + "','I','" + SalesBillNo + "','" + Total + "','" + Total + "'," + WingsId + ", '" + SaleDate + "','IC', 'Issue Medicine', '" + LoginUser + "', 1,getdate(),'" + receiveby + "','" + rcvdt + "','" + issueby + "','" + PatientName + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                            theCommand.CommandText = "update DOCSERIAL set LASTNO='" + SalesBillNo + "' where ltrim(rtrim(COMPCODE))=ltrim(rtrim('" + compcode + "')) and YEARCODE='" + yearcode + "' and AUTOCODE='IC' and BOOKCODE='IC'";
                            theCommand.Transaction = tran as SqlTransaction;

                            effectedRows = theCommand.ExecuteNonQuery();
                        }// Execute insert query.

                        //theCommand.CommandText = "INSERT INTO MD_MedicineSaleDtls(compcode,yearcode,MedicineSubGrp,SaleBillNoID,MCode,MedicineGroupID,MedicineID,BatchNo,ExpiryDate,Qty,PricePerUnit,TotalPrice,CreatedBy, status) VALUES ('" + compcode + "','" + yearcode + "','" + SubGroup + "','" + SalesBillNo + "','" + MCode + "', '" + MedicineGroupID + "', '" + MedicineID + "', '" + BatchNo + "', '" + ExpiryDate + "', '" + Qty + "', '" + UnitPrice + "', '" + TotalPrice + "', '" + LoginUser + "', 1)";
                        //theCommand.CommandText = "INSERT INTO INVDETL(compcode,yearcode,TYPES,Docno,srlno,icode,BatchNo,EXPDATE,IQTY,ITRCD,IRATE,IAMOUNT,user01, tag,logdt01) VALUES ('" + compcode + "','" + yearcode + "','I','" + SalesBillNo + "'," + i + ", dbo.Fnc_GetMedicineIcode('" + compcode + "','" + MedicineID + "'), '" + BatchNo + "', '" + ExpiryDate + "', '" + Qty + "','O', '" + UnitPrice + "', '" + TotalPrice + "', '" + LoginUser + "', 1,getdate())";
                        theCommand.CommandText = "INSERT INTO INVDETL(compcode,yearcode,TYPES,Docno,srlno,icode,BatchNo,EXPDATE,IQTY,ITRCD,IRATE,IAMOUNT,user01, tag,logdt01) VALUES ('" + compcode + "','" + yearcode + "','I','" + SalesBillNo + "'," + i + ", '" + MedicineID + "', '" + BatchNo + "', '" + ExpiryDate + "', '" + Qty + "','O', '" + UnitPrice + "', '" + TotalPrice + "', '" + LoginUser + "', 1,getdate())";
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


    public bool InsertSaleMedicineNew(int i, string compcode, string yearcode, string SubGroup, string PatientName, string PatientAddress, string CardNo, string DoctorName, string SalesBillNo, string SaleDate, string Total, string Discount, string MCode, string MedicineGroupID, string MedicineID, string BatchNo, string ExpiryDate, string Qty, string UnitPrice, string TotalPrice, string LoginUser, string rcvdt, string receiveby, string issueby,string PackSize, string discAmt,string TaxableAmt, string HsnCode, string CgstRt, string CgstAmt, string SgstRtt, string SgstAmt,string totgval,string pushChrg,string sellingUnit,string BillNo,string roundOff,string cashamt,string cardamt,string eWalletamt,string netBankamt,string itemType,string type,string extraChrgDes1,string extraChrgAmt1,string extraChrgDes2,string extraChrgAmt2,string cancel)
    {
        int effectedRows = 0;
        if (UnitPrice == "")
        {
            UnitPrice = "0";
        }
        if (PackSize == "")
        {
            PackSize = "1";
        }
        if (discAmt == "")
        {
            discAmt = "0";
        }
        if (TaxableAmt == "")
        {
            TaxableAmt = "0";
        }
        if (CgstRt == "")
        {
            CgstRt = "0";
        }
        if (CgstAmt == "")
        {
            CgstAmt = "0";
        }
        if (SgstRtt == "")
        {
            SgstRtt = "0";
        }
        if (SgstAmt == "")
        {
            SgstAmt = "0";
        }

        if(pushChrg=="")
        {
            pushChrg = "0";
        }

        if (cashamt == "")
        {
            cashamt = "0";
        }
        if (cardamt == "")
        {
            cardamt = "0";
        }
        if (eWalletamt == "")
        {
            eWalletamt = "0";
        }
        if (netBankamt == "")
        {
            netBankamt = "0";
        }

        if (cashamt == "0" && cardamt == "0" && eWalletamt == "0" && netBankamt == "0")
        {
            cashamt = Total;
        }
        string expdt = "";

        if (ExpiryDate == "")
        {
            expdt = "null";
        }
        else
        {
            expdt = "'"+ExpiryDate+"'";
        }

        int lastinvno = Convert.ToInt32(BillNo.Substring(BillNo.Length - 4));
        Qty = (Convert.ToDecimal(Qty) / Convert.ToDecimal(PackSize)).ToString();
        string grossamt = (Convert.ToDecimal(UnitPrice) * Convert.ToDecimal(Qty)).ToString();
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

                        //theCommand.CommandText = "SELECT * FROM MD_MedicineSales WHERE SalesBillNo='" + SalesBillNo + "' and compcode='"+ compcode+"' and yearcode='"+ yearcode+"'";
                        theCommand.CommandText = "SELECT * FROM INVHEAD WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "' and docno='" + SalesBillNo + "' and bkcode='IC'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows)
                        {
                            theReader.Close();
                            theCommand.CommandText = "update INVHEAD set GVALUE='" + totgval + "',BILLVALUE='" + Total + "',ROFFAMT='" + roundOff + "', DOCDT='" + SaleDate + "', USER02='" + LoginUser + "',logdt02=getdate(),slcode='" + PatientName + "',CFILLER10='" + DoctorName + "',ReceiveBy='" + receiveby + "',ReceiveDate='" + rcvdt + "',Issueby='" + issueby + "',MISCPM01='P',MISCAMT01='" + pushChrg + "',cash_amt='" + cashamt + "',card_amt='" + cardamt + "',ewallet_amt='" + eWalletamt + "',netbank_amt='" + netBankamt + "',CFILLER09='" + type + "',ExtraChargeDesc1='" + extraChrgDes1 + "',ExtraChargeAmt1='" + extraChrgAmt1 + "',ExtraChargeDesc2='" + extraChrgDes2 + "',ExtraChargeAmt2='" + extraChrgAmt2 + "',CancelFlag='"+ cancel +"' where compcode='" + compcode + "' and yearcode='" + yearcode + "' and docno='" + SalesBillNo + "' and bkcode='IC'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            theReader.Close();//SLCODE,'" + PatientName + "',
                            //theCommand.CommandText = "INSERT INTO MD_MedicineSales(compcode,yearcode,WingsID,PatientName,PatientAddress, CardNo, DoctorName, SalesBillNo,SaleDate,Total/*,Discount*/) VALUES ('" + compcode + "','" + yearcode + "','" + WingsId + "','" + PatientName + "','" + PatientAddress + "','" + CardNo + "', '" + DoctorName + "', '" + SalesBillNo + "','" + SaleDate + "', '" + Total + "'/*,'" + Discount + "'*/)";
                            theCommand.CommandText = "INSERT INTO INVHEAD(compcode,yearcode,TYPES,docno,REFBILLNO,GVALUE,BILLVALUE, DOCDT,BKCODE, REMARKS, USER01,tag,logdt01,ReceiveBy,ReceiveDate,Issueby,slcode,MISCPM01,MISCAMT01,CFILLER10,ROFFAMT,cash_amt,card_amt,ewallet_amt,netbank_amt,CFILLER09,ExtraChargeDesc1,ExtraChargeAmt1,ExtraChargeDesc2,ExtraChargeAmt2) VALUES ('" + compcode + "','" + yearcode + "','I','" + SalesBillNo + "','" + BillNo + "','" + totgval + "','" + Total + "','" + SaleDate + "','IC', 'Issue Medicine', '" + LoginUser + "', 1,getdate(),'" + receiveby + "','" + rcvdt + "','" + issueby + "','" + PatientName + "','P','" + pushChrg + "','" + DoctorName + "','" + roundOff + "','" + cashamt + "','" + cardamt + "','" + eWalletamt + "','" + netBankamt + "','" + type + "','"+extraChrgDes1+"','"+ extraChrgAmt1+"','"+ extraChrgDes2 +"','"+ extraChrgAmt2 +"')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();

                            theCommand.CommandText = "update OPD_BillTypeMaster set LastSerial=" + lastinvno + " where compcode='" + compcode + "' and yearcode='" + yearcode + "' and BillTypeId='PHR' and Bill_Month=CONVERT(varchar(2),GETDATE(),101) ";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.


                            theCommand.CommandText = "update DOCSERIAL set LASTNO='" + SalesBillNo + "' where ltrim(rtrim(COMPCODE))=ltrim(rtrim('" + compcode + "')) and YEARCODE='" + yearcode + "' and AUTOCODE='IC' and BOOKCODE='IC'";
                            theCommand.Transaction = tran as SqlTransaction;

                            effectedRows = theCommand.ExecuteNonQuery();
                        }// Execute insert query.

                        //theCommand.CommandText = "INSERT INTO MD_MedicineSaleDtls(compcode,yearcode,MedicineSubGrp,SaleBillNoID,MCode,MedicineGroupID,MedicineID,BatchNo,ExpiryDate,Qty,PricePerUnit,TotalPrice,CreatedBy, status) VALUES ('" + compcode + "','" + yearcode + "','" + SubGroup + "','" + SalesBillNo + "','" + MCode + "', '" + MedicineGroupID + "', '" + MedicineID + "', '" + BatchNo + "', '" + ExpiryDate + "', '" + Qty + "', '" + UnitPrice + "', '" + TotalPrice + "', '" + LoginUser + "', 1)";
                        //theCommand.CommandText = "INSERT INTO INVDETL(compcode,yearcode,TYPES,Docno,Docdt,srlno,icode,BatchNo,EXPDATE,IQTY,ITRCD,IRATE,IAMOUNT,user01, tag,logdt01) VALUES ('" + compcode + "','" + yearcode + "','I','" + SalesBillNo + "'," + i + ", dbo.Fnc_GetMedicineIcode('" + compcode + "','" + MedicineID + "'), '" + BatchNo + "', '" + ExpiryDate + "', '" + Qty + "','O', '" + UnitPrice + "', '" + TotalPrice + "', '" + LoginUser + "', 1,getdate())";
                        theCommand.CommandText = "INSERT INTO INVDETL(compcode,yearcode,TYPES,Docno,Docdt,srlno,icode,BatchNo,EXPDATE,IQTY,ITRCD,IRATE,IAMOUNT,SELLAMOUNT,DISCAMT,PACKQTY,HSNCODE,CGST_RATE,CGST_AMT,SGST_RATE,SGST_AMT,IAMOUNTWITHVAT,user01, tag,logdt01,sellingUnit,cfiller10) VALUES ('" + compcode + "','" + yearcode + "','I','" + SalesBillNo + "','" + SaleDate + "'," + i + ", '" + MedicineID + "', '" + BatchNo + "', " + expdt + ", '" + Qty + "','O', '" + UnitPrice + "', '" + grossamt + "','" + TaxableAmt + "','" + discAmt + "','" + PackSize + "','" + HsnCode + "','" + CgstRt + "','" + CgstAmt + "','" + SgstRtt + "','" + SgstAmt + "','" + TotalPrice + "', '" + LoginUser + "', 1,getdate(),'" + sellingUnit + "','" + itemType + "')";
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

    public bool InsertUpdateSaleMedicineHead(string compcode, string yearcode, string PatientName, string PatientAddress, string CardNo, string DoctorName, string SalesBillNo, string SaleDate, string Total, string Discount, string LoginUser, string rcvdt, string receiveby, string issueby, string totgval, string pushChrg, string BillNo, string roundOff, string cashamt, string cardamt, string eWalletamt, string netBankamt, string itemType, string type, string extraChrgDes1, string extraChrgAmt1, string extraChrgDes2, string extraChrgAmt2, string cancel)
    {
        int effectedRows = 0;
        

        if (pushChrg == "")
        {
            pushChrg = "0";
        }

        if (cashamt == "")
        {
            cashamt = "0";
        }
        if (cardamt == "")
        {
            cardamt = "0";
        }
        if (eWalletamt == "")
        {
            eWalletamt = "0";
        }
        if (netBankamt == "")
        {
            netBankamt = "0";
        }

        if (cashamt == "0" && cardamt == "0" && eWalletamt == "0" && netBankamt == "0")
        {
            cashamt = Total;
        }
        string expdt = "";

        
        int lastinvno = Convert.ToInt32(BillNo.Substring(BillNo.Length - 4));
        
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

                        //theCommand.CommandText = "SELECT * FROM MD_MedicineSales WHERE SalesBillNo='" + SalesBillNo + "' and compcode='"+ compcode+"' and yearcode='"+ yearcode+"'";
                        theCommand.CommandText = "SELECT * FROM INVHEAD WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "' and docno='" + SalesBillNo + "' and bkcode='IC'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows)
                        {
                            theReader.Close();
                            theCommand.CommandText = "update INVHEAD set GVALUE='" + totgval + "',BILLVALUE='" + Total + "',ROFFAMT='" + roundOff + "', DOCDT='" + SaleDate + "', USER02='" + LoginUser + "',logdt02=getdate(),slcode='" + PatientName + "',CFILLER10='" + DoctorName + "',ReceiveBy='" + receiveby + "',ReceiveDate='" + rcvdt + "',Issueby='" + issueby + "',MISCPM01='P',MISCAMT01='" + pushChrg + "',cash_amt='" + cashamt + "',card_amt='" + cardamt + "',ewallet_amt='" + eWalletamt + "',netbank_amt='" + netBankamt + "',CFILLER09='" + type + "',ExtraChargeDesc1='" + extraChrgDes1 + "',ExtraChargeAmt1='" + extraChrgAmt1 + "',ExtraChargeDesc2='" + extraChrgDes2 + "',ExtraChargeAmt2='" + extraChrgAmt2 + "',CancelFlag='" + cancel + "' where compcode='" + compcode + "' and yearcode='" + yearcode + "' and docno='" + SalesBillNo + "' and bkcode='IC'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            theReader.Close();//SLCODE,'" + PatientName + "',
                            //theCommand.CommandText = "INSERT INTO MD_MedicineSales(compcode,yearcode,WingsID,PatientName,PatientAddress, CardNo, DoctorName, SalesBillNo,SaleDate,Total/*,Discount*/) VALUES ('" + compcode + "','" + yearcode + "','" + WingsId + "','" + PatientName + "','" + PatientAddress + "','" + CardNo + "', '" + DoctorName + "', '" + SalesBillNo + "','" + SaleDate + "', '" + Total + "'/*,'" + Discount + "'*/)";
                            theCommand.CommandText = "INSERT INTO INVHEAD(compcode,yearcode,TYPES,docno,REFBILLNO,GVALUE,BILLVALUE, DOCDT,BKCODE, REMARKS, USER01,tag,logdt01,ReceiveBy,ReceiveDate,Issueby,slcode,MISCPM01,MISCAMT01,CFILLER10,ROFFAMT,cash_amt,card_amt,ewallet_amt,netbank_amt,CFILLER09,ExtraChargeDesc1,ExtraChargeAmt1,ExtraChargeDesc2,ExtraChargeAmt2) VALUES ('" + compcode + "','" + yearcode + "','I','" + SalesBillNo + "','" + BillNo + "','" + totgval + "','" + Total + "','" + SaleDate + "','IC', 'Issue Medicine', '" + LoginUser + "', 1,getdate(),'" + receiveby + "','" + rcvdt + "','" + issueby + "','" + PatientName + "','P','" + pushChrg + "','" + DoctorName + "','" + roundOff + "','" + cashamt + "','" + cardamt + "','" + eWalletamt + "','" + netBankamt + "','" + type + "','" + extraChrgDes1 + "','" + extraChrgAmt1 + "','" + extraChrgDes2 + "','" + extraChrgAmt2 + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();

                            theCommand.CommandText = "update OPD_BillTypeMaster set LastSerial=" + lastinvno + " where compcode='" + compcode + "' and yearcode='" + yearcode + "' and BillTypeId='PHR' and Bill_Month=CONVERT(varchar(2),GETDATE(),101) ";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.


                            theCommand.CommandText = "update DOCSERIAL set LASTNO='" + SalesBillNo + "' where ltrim(rtrim(COMPCODE))=ltrim(rtrim('" + compcode + "')) and YEARCODE='" + yearcode + "' and AUTOCODE='IC' and BOOKCODE='IC'";
                            theCommand.Transaction = tran as SqlTransaction;

                            effectedRows = theCommand.ExecuteNonQuery();
                        }// Execute insert query.

                        
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
                        theCommand.CommandText = "exec sp_PopulateInvHeadDetl_fromIssue '" + compcode + "','" + yearcode + "','" + id + "','" + type + "','" + user + "'";
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
                        theCommand.CommandText = "delete dbo.INVDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and docno='" + id + "'";
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

    public DataTable GenerateSaleId(string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "exec sp_MD_GenerateSaleBillNo '" + compcode + "','" + yearcode + "'";
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


    public string getPatientLedgerId(string compcode, string yearcode, string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select LedgerID from AC_Ledger where compcode='" + compcode + "' and LedgerFK='"+ regno +"'";
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


        return PurchaseMedicineTable.Rows[0][0].ToString();
    }

    public DataTable gettaxinvoicehead(string compcode, string yearcode, string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select dbo.FNC_P_GETPatientNamefromLedgerId(COMPCODE,SLCODE,'N')patient,dbo.AmountToWords(billvalue)amtword, DOCNO,REFBILLNO,convert(varchar,DOCDT,103)DOCDT,isnull(MISCAMT01,0) pushchrg,isNull(ROFFAMT,0)ROFFAMT,isNull(cash_amt,0)cash_amt,isNull(card_amt,0)card_amt,isNull(ewallet_amt,0)ewallet_amt,isNull(netbank_amt,0)netbank_amt,isNull(ExtraChargeDesc1,'')ExtraChargeDesc1,isNull(ExtraChargeDesc2,'')ExtraChargeDesc2,isNull(ExtraChargeAmt1,0)ExtraChargeAmt1,isNull(ExtraChargeAmt2,0)ExtraChargeAmt2,isNull(CancelFlag,0)CancelFlag,* from INVHEAD I where I.COMPCODE='" + compcode + "' and I.YEARCODE='" + yearcode + "' and docno='" + id + "'";
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

    public DataTable GetSaleMedicineDetails(string compcode, string yearcode, string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select pm.docno,pm.docdt,isnull(pm.slcode,'0')Patient_Name,isnull(pm.issue_to,0)Wings_ID,dbo.Fnc_getFloor('" + compcode + "',isnull(pm.issue_to,0)) FloorId,CONVERT(varchar,pm.docdt,103) purdate,md.icode,CONVERT(varchar,md.expdate,103) exdate,md.BatchNo,md.Iqty Qty,md.irate PricePerUnit,md.iamount TotalPrice,pm.ReceiveBy,pm.Issueby,CONVERT(varchar,pm.ReceiveDate,103) Rcvdate from dbo.INVHEAD pm,dbo.INVDETL md where pm.compcode=md.compcode and pm.yearcode=md.yearcode and pm.docno=md.docno and pm.TYPES=md.TYPES and pm.docno='" + id + "'and pm.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "'";
        theCommand.CommandText = "select SRLNO,I.Icode,M.MedicineName,BATCHNO,CONVERT(varchar(10),I.EXPDATE,120) Expdt,RIGHT(CONVERT(VARCHAR(10), I.EXPDATE, 103) , 7) ExpYearMonth,isNull(I.IRATE,0)IRATE,I.HSNCODE,IQTY,DISCAMT,SELLAMOUNT,SGST_RATE,CGST_RATE,SGST_AMT,CGST_AMT,IAMOUNTWITHVAT,isNull(PACKQTY,'1')PACKQTY,I.sellingUnit,M.UnitId Unit1,dbo.fn_GetUnitName(I.compcode,M.UnitId) Unit1Name,M.SellingUnit Unit2,dbo.fn_GetUnitName(I.compcode,M.SellingUnit) Unit2Name,dbo.fn_GetUnitName(I.compcode,I.sellingUnit) sellUnitName,isNull(Cfiller10,'M') itype from INVDETL I,IPD_MedicineMaster M where I.COMPCODE=M.compcode and I.ICODE=M.ICODE and I.COMPCODE='" + compcode + "' and I.YEARCODE='" + yearcode + "' and I.DOCNO='" + id + "'";
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

    public DataTable GetSaleMedicineHead(string compcode, string yearcode, string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select DocNo,REFBILLNO,Slcode,convert(varchar(10),DOCDT,120)DOCDATE,dbo.FNC_P_GETPatientNamefromLedgerId(compcode,slcode,'R') RegNo,dbo.FNC_P_GETPatientNamefromLedgerId(compcode,slcode,'N') PatientName,isNull(MISCAMT01,0) PushingChrg,isNull(GVALUE,0)GVALUE,isNull(BILLVALUE,0)BILLVALUE,isNull(ROFFAMT,0)ROFFAMT,isNull(CFILLER10,'') DocName,isNull(ReceiveBy,'') ReceiveBy,isNull(Issueby,'')Issueby,isNull(PAYMODE,'')PAYMODE,convert(varchar(10),ReceiveDate,120) ReceiveDate,isNull(cash_amt,0)cash_amt,isNull(card_amt,0)card_amt,isNull(ewallet_amt,0)ewallet_amt,isNull(netbank_amt,0)netbank_amt,isnull(CFILLER09,'')CFILLER09,isNull(ExtraChargeDesc1,'')ExtraChargeDesc1,isNull(ExtraChargeDesc2,'')ExtraChargeDesc2,isNull(ExtraChargeAmt1,0) ExtraChargeAmt1,isnull(ExtraChargeAmt2,0)ExtraChargeAmt2,isNull(CancelFlag,0)CancelFlag from INVHEAD where COMPCODE='" + compcode + "' and YEARCODE='" + yearcode + "' and DOCNO='" + id + "'";
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
    public DataTable AvailableStock(string compcode, string yearcode, string id, string Mfgid, string Batchno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select dbo.Fnc_MD_AvailablePurchaseStock('"+ compcode+"','"+ yearcode+"','" + id + "','" + Mfgid + "','" + Batchno + "') as AvailQty";
        //theCommand.CommandText = "select curstock as AvailQty from BATDETL where compcode='" + compcode + "' and yearcode='" + yearcode + "' and icode=dbo.Fnc_GetMedicineIcode(compcode,'"+id+"') and ltrim(rtrim(batchno))='" + Batchno.Trim() + "'";
        theCommand.CommandText = "select curstock as AvailQty,convert(varchar(10),EXPDATE,120) EXPDATE from BATDETL where compcode='" + compcode + "' /*and yearcode='" + yearcode + "'*/ and ltrim(rtrim(icode))=ltrim(rtrim('" + id.Trim() + "')) and ltrim(rtrim(batchno))='" + Batchno.Trim() + "'";
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
    public DataTable GridPopup(string compcode, string yearcode, string invoiceid, string fromdate, string todate, string workstation)
    {

        if (fromdate == "" || fromdate == "null")
            fromdate = "null";
        if (fromdate != "null")
            fromdate = "'" + fromdate + "'";

        if (todate == "" || todate == "null")
        {
            todate = "null";
        }
        else
        {
            todate = "'" + todate + "'";
        }
        if (workstation == "" || workstation == "null")
        {
            workstation = "'%%'";
        }
        else
        {
            workstation = "'" + workstation + "%'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (fromdate == "null" && todate == "null")
        {
            theCommand.CommandText = "select docno salesbillno,REFBILLNO,CONVERT(varchar,docdt,103)SaleDate,dbo.FNC_P_GETPatientNamefromLedgerId(compcode,slcode,'N')IssueTo from INVHEAD where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ltrim(rtrim(bkcode))='IC' and REFBILLNO like '" + invoiceid + "%'  order by docdt desc,REFBILLNO desc";
        }
        else if (fromdate == "null" && todate != "null")
        {
            theCommand.CommandText = "select docno salesbillno,REFBILLNO,CONVERT(varchar,docdt,103)SaleDate,dbo.FNC_P_GETPatientNamefromLedgerId(compcode,slcode,'N')IssueTo from INVHEAD where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ltrim(rtrim(bkcode))='IC' and REFBILLNO like '" + invoiceid + "%' and cast(docdt as date)<=cast(" + todate + " as date)  order by docdt desc,REFBILLNO desc";
        }
        else if (fromdate != "null" && todate == "null")
        {
            theCommand.CommandText = "select docno salesbillno,REFBILLNO,CONVERT(varchar,docdt,103)SaleDate,dbo.FNC_P_GETPatientNamefromLedgerId(compcode,slcode,'N')IssueTo from INVHEAD where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ltrim(rtrim(bkcode))='IC' and REFBILLNO like '" + invoiceid + "%' and cast(docdt as date)>=cast(" + fromdate + " as date)  order by docdt desc,REFBILLNO desc";
        }
        else
        {
            theCommand.CommandText = "select docno salesbillno,REFBILLNO,CONVERT(varchar,docdt,103)SaleDate,dbo.FNC_P_GETPatientNamefromLedgerId(compcode,slcode,'N')IssueTo from INVHEAD where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ltrim(rtrim(bkcode))='IC' and REFBILLNO like '" + invoiceid + "%' and cast(docdt as date)>=cast(" + fromdate + " as date) and  cast(docdt as date)<=cast(" + todate + " as date)  order by docdt desc,REFBILLNO desc";
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
        theCommand.CommandText = "select FloorId,FloorName from IPD_FloorMaster where compcode='"+compcode+"' and status='1'";
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

    public DataTable getWorkStation(string compcode,string floor)
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select WingsID ,WingsName,WorkStation from IPD_WingsMaster where compcode='"+compcode+"' and FloorId='" + floor + "' and WorkStation!= 'null' and status='1'";
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

    public DataTable DropdownMedicine(string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        /*theCommand.CommandText = "select cast(MedicineID as char)MedicineID,MedicineName from  IPD_MedicineMaster where compcode='" + compcode + "' and status=1 " +
            "union select ltrim(rtrim(icode))icode,iname from  ITEMMAST where compcode='" + compcode + "' and itype='G' and tag=1 order by MedicineName";*/
        theCommand.CommandText = "select ltrim(rtrim(icode))MedicineID,iname MedicineName from  ITEMMAST where compcode='" + compcode + "' and itype in ('M','G','C') and tag=1 order by MedicineName";
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

    public DataTable DropdownStaff(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select '0' EmployeeID,'Select Staff' EmployeeName union select LedgerID,LedgerName  from AC_Ledger where COMPCODE='" + compcode + "' and isnull(ledgertype,'')='S'";
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

    public string MedicineExist(string compcode, string med)
    {
        DataTable MfgTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select MedicineID,HSNCode,isNull(CGSTRate,0)CGSTRate,isNull(SGSTRate,0) SGSTRate from IPD_MedicineMaster where compcode='" + compcode + "' and dbo.Fnc_GetMedicineIcode(compcode,MedicineID)=ltrim(rtrim('" + med + "'))";
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

        return MfgTable.Rows.Count.ToString();
    }
    public DataTable DropdownMfg(string compcode,string yearcode,string med)
    {
        DataTable MfgTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select mfg.MCode,mfg.MName,mm.MedicineID,mm.MCode from PH_ManufactureMaster mfg,IPD_MedicineMaster mm where mfg.compcode=mm.compcode and mfg.MCode=mm.MCode and mm.compcode='" + compcode + "' and dbo.Fnc_GetMedicineIcode(mm.compcode, mm.MedicineID)=ltrim(rtrim('" + med + "')) and mfg.Status='1';";
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

    public DataTable DropdownGrp(string compcode, string yearcode, string med)
    {
        DataTable GrpTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select gr.MedicineGroupID,gr.MedicineGroupName,mm.MedicineGroupID MedGrp,mm.MedicineID from IPD_MedicineGroup gr,IPD_MedicineMaster mm where gr.compcode=mm.compcode and gr.MedicineGroupID=mm.MedicineGroupID and mm.compcode='" + compcode + "' and dbo.Fnc_GetMedicineIcode(mm.compcode,mm.MedicineID)=ltrim(rtrim('" + med + "'))";
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


    public DataTable DropdownMedicineGrp(string compcode)
    {
        DataTable GrpTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select '0'as MedicineGroupID,'--Select--' as MedicineGroupName Union Select gr.MedicineGroupID,gr.MedicineGroupName from IPD_MedicineGroup gr where gr.compcode='" + compcode + "'";
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


    public DataTable DropdownMedicineDose(string compcode)
    {
        DataTable GrpTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select '0'as ID,'--Select--' as DoseName Union Select ID,DoseName from MD_DoseMaster  where compcode='" + compcode + "'";
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

    public DataTable DropdownMedicineDuration(string compcode)
    {
        DataTable GrpTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select '0'as DurationId,'--Select--' as DurationName Union Select DurationId,DurationName from IPD_DurationMaster  where compcode='" + compcode + "'";
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
    public DataTable DropdownSubGrp(string compcode, string yearcode, string med)
    {
        DataTable SubGrpTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select sgr.ID,sgr.SubGrName,mm.SubGroupid MedGrp,mm.MedicineID from IPD_MedicineSubGroup sgr,IPD_MedicineMaster mm where sgr.compcode=mm.compcode and sgr.ID=mm.SubGroupid and mm.compcode='" + compcode + "' and dbo.Fnc_GetMedicineIcode(mm.compcode,mm.MedicineID)=ltrim(rtrim('" + med + "')) and sgr.status='1'";
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

    public DataTable ExpiryDateBatchWise(string compcode, string yearcode, string MedicineId, string ManufacturerId, string BatchNo)
    {
        // Connection.
        string str;
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "EXEC  sp_MD_FetchBatchNo " + MedicineId + "";
        //str = "select  md.BatchNo,CONVERT(varchar,md.ExpiryDate,103) ExDate  from MD_PurchaseMedicineDetails md where md.compcode='" + compcode + "' and md.yearcode='" + yearcode + "' and md.status=1 and md.MedicineID= '" + MedicineId + "'";
        str = "select md.BatchNo,CONVERT(varchar(10),md.ExpDate,120) ExDate,RIGHT(CONVERT(VARCHAR(10), md.ExpDate, 103) , 7) ExpYearMonth,isNull(MRP,0)MRP,isNull(PackSize,0)PackSize  from BATDETL md where md.compcode='" + compcode + "' /*and md.yearcode='" + yearcode + "'*/ and  md.icode='" + MedicineId + "'";
        
        if (BatchNo != "")
        {
            str = str + " and BatchNo='" + BatchNo + "'";
        }
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


    public DataTable getTaxDetails(string compcode, string yearcode, string MedicineId, string ManufacturerId)
    {
        // Connection.
        string str;
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        str = "select HSNCode,isNull(CGSTRate,0)CGSTRate,isNull(SGSTRate,0)SGSTRate,md.UnitId,um.UnitName,itype from IPD_MedicineMaster md,GN_UnitMaster um where um.compcode=md.compcode and um.UnitId=md.UnitId and md.compcode='" + compcode + "' /*and md.yearcode='" + yearcode + "'*/ and  md.icode='" + MedicineId + "' and MCode='" + ManufacturerId + "'";

        
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

    /*public DataTable GetMedGrpByID(string compcode,string MGrp)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineGroupID,MedicineGroupName from IPD_MedicineGroup where compcode='"+compcode+"' and MedicineGroupID='" + MGrp + "'";
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

    public DataTable GetMedSubGrpByID(string compcode,string MSubGrp)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ID,SubGrName from IPD_MedicineSubGroup where compcode='"+compcode+"' and ID='" + MSubGrp + "'";
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

    public DataTable GetMedMfgByMCode(string compcode, string MedId)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select mm.MCode,ph.MName from  IPD_MedicineMaster mm,PH_ManufactureMaster ph where mm.compcode=ph.compcode and ph.MCode=mm.MCode and mm.MedicineId='" + MedId + "' and mm.compcode='" + compcode + "'";
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
    }*/

    public DataTable getBatchId(string compcode,string yearcode,string mcode, string med,string docno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
       // theCommand.CommandText = "select BatchNo from MD_PurchaseMedicineDetails where MedicineId='" + med + "'";
        theCommand.CommandText = "Select BatchNo from INVDETL where compcode='"+compcode+"' and yearcode='"+yearcode+"' and docno='"+docno+"'";
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
    public DataTable DropdownMedicine(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select MedicineID,MedicineName from  IPD_MedicineMaster where compcode='" + compcode + "' and status=1 order by MedicineName";
        theCommand.CommandText = "select ltrim(rtrim(icode)) MedicineID,iname MedicineName from  itemmast where compcode='" + compcode + "' and itype in('M','G','C') and tag=1 order by MedicineName";

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
        theCommand.CommandText = "select mm.MedicineGroupID,gr.MedicineGroupName,mm.SubGroupid,sgr.SubGrName,mm.MCode,ph.MName from IPD_MedicineMaster mm,IPD_MedicineGroup gr,IPD_MedicineSubGroup sgr, PH_ManufactureMaster ph where gr.compcode=mm.compcode and sgr.compcode=sgr.compcode and ph.compcode=mm.compcode and ph.MCode=mm.MCode and gr.MedicineGroupID=mm.MedicineGroupID and sgr.ID=mm.SubGroupid and mm.compcode='" + compcode + "' and mm.status=1 and dbo.Fnc_GetMedicineIcode(mm.compcode,mm.MedicineID)=ltrim(rtrim('" + medid + "'))";
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

    public string getUnitDetls(string compcode,string medId,string Unit)
    {
        String convertionFact = "";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select UnitId,SellingUnit,isNull(ConversionFactor,1)ConversionFactor from IPD_MedicineMaster where compcode='" + compcode + "' and ICode='" + medId + "'";
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

        if (PurchaseMedicineTable.Rows[0]["UnitId"].ToString().Trim() == Unit)
        {
            convertionFact = "1";
        }
        else
        {
            convertionFact = PurchaseMedicineTable.Rows[0]["ConversionFactor"].ToString().Trim();
        }
        return convertionFact;
    }

    public DataTable getMedicineUnits(string compcode,string medId)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select UnitId,dbo.fn_GetUnitName(compcode,UnitId) PurUnitName,SellingUnit,dbo.fn_GetUnitName(compcode,SellingUnit) sellUnitName from IPD_MedicineMaster where compcode='" + compcode + "' and ICode='" + medId + "'";
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
    public bool InsertUpdatePerscriptionHead(string compcode, string YearCode, string PrepId, string reg, string Date, string DocId, string user, string comment, string flag)
    {
        try
        {
            string lsSql = "";
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
                    if (flag == "I")
                    {
                        lsSql = "Insert into OPD_Prescription(CompCode,YearCode,PatientReg,PrescriptionID,DoctorId,Date,Comment,User01,logdt01) values('" + compcode + "','" + YearCode + "','" + reg + "','" + PrepId + "','" + DocId + "','" + Date + "','" + comment + "','" + user + "',getdate())";
                    }
                    else
                    {
                        lsSql = "Update OPD_Prescription set DoctorId='" + DocId + "',Date='" + Date + "',Comment='" + comment + "' where compcode='" + compcode + "' and yearcode='" + YearCode + "' and PrescriptionID='"+ PrepId +"'";
                    }
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = lsSql;
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

    public bool InsertperscriptionMaping(string compcode, string YearCode, string PrepId,string MedGrpId, string MedcineId, string Dose, string Duration)
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
                        theCommand.CommandText = "Insert into OPD_PrescriptionMapping(CompCode,YearCode,PrescriptionID,GroupID,MedicineId,Dose,Duration,status) values('" + compcode + "','" + YearCode + "','" + PrepId + "','" + MedGrpId + "','" + MedcineId + "','" + Dose + "','" + Duration + "','1')";
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

    public bool DeletePrescriptionDetl(string compcode,string yearcode,string prepId)
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
                        theCommand.CommandText = "delete OPD_PrescriptionMapping where compcode='" + compcode + "' and yearcode='" + yearcode + "' and PrescriptionID='" + prepId + "'";
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

    public bool DeletePrescription(string compcode, string yearcode, string prepId)
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
                        theCommand.CommandText = "delete OPD_Prescription where compcode='" + compcode + "' and yearcode='" + yearcode + "' and PrescriptionID='" + prepId + "' delete OPD_PrescriptionMapping where compcode='" + compcode + "' and yearcode='" + yearcode + "' and PrescriptionID='" + prepId + "'";
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

    private DataTable PrescriptionTable;

    public DataTable BindGrid(string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select PrescriptionID,convert(varchar(10),Date,120) as PrescriptionDate,convert(varchar(10),Date,103) as prepdate,PatientReg,PName,DoctorId,doc_name,Comment " +  
                        "from OPD_Prescription ,OPD_PatientRegistration,GN_DoctorMaster  where OPD_Prescription.compcode=OPD_PatientRegistration.compcode and " +
                        "OPD_PatientRegistration.PatientRegNo=OPD_Prescription.PatientReg and GN_DoctorMaster.compcode=OPD_Prescription.compcode and " +
                        "GN_DoctorMaster.doc_id=OPD_Prescription.DoctorId and OPD_Prescription.compcode ='"+ compcode +"' and OPD_Prescription.yearcode='"+ yearcode +"'";

        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTable = new DataTable();
        theAdapter.Fill(PrescriptionTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return PrescriptionTable;
    }

  
   
}
