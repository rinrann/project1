using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_ReagentEntry6565
/// </summary>
public class PH_ReagentEntry
{
	public PH_ReagentEntry(string con)
	{
        conString = con;
	}


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


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
        hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return hospitalTable;
    }

    public DataTable Generatecode()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GenerateReagentCode";
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
        

        return hospitalTable;
    }

    public DataTable DropdownManufacturer()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_ManufactureMaster where status=1";
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
        

        return hospitalTable;
    }

    public DataTable grid()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_ReagentEntry";
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
        

        return hospitalTable;
    }

    public DataTable DropdownCompany()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_CompanyMaster where status=1";
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
        

        return hospitalTable;
    }

    public DataTable DropdownSupplier(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_SuppilierMaster where status=1 and compcode='"+compcode+"'";
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
        

        return hospitalTable;
    }

    public DataTable GetReagentDtls(string compcode,string yearcode,string docno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        /*if (date != "")
            theCommand.CommandText = "exec sp_ReagentDtlsDetails '" + compcode + "','" + yearcode + "','" + docno + "'";
        else
            theCommand.CommandText = "exec sp_ReagentDtlsDetails null";*/

        theCommand.CommandText = "exec sp_ReagentDtlsDetails '" + compcode + "','" + yearcode + "','" + docno + "'";
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
        

        return hospitalTable;
    }
    public DataTable GetDocNo(string compcode, string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "exec sp_MD_GenerateReturnId '"+ compcode +"','"+ yearcode+"'";
        theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','RC','I','P','Y',NULL";
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
    public bool InsertReagentEntry(string compcode, string yearcode,int srl,string ID, string ReagentID, string BatchNo,string SupplierId, string Quantity, string BonusQty, string Price, string TotalPrice,string Total, string ExpiryDate, string CreatedBy, string Date)
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
            int effectedRows = 0;
            // Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            /*theCommand.CommandText = "INSERT INTO PH_ReagentEntry(ID,ReagentID,BatchNo,CompanyId,SupplierId,ManufacturerId,Quantity,BonusQty,Price,TotalPrice,ExpiryDate,CreatedBy,Date) VALUES('" + ID + "','" + ReagentID + "','" + BatchNo + "','" + CompanyId + "','" + SupplierId + "','" + ManufacturerId + "','" + Quantity + "','" + BonusQty + "','" + Price + "','" + TotalPrice + "','" + ExpiryDate + "','" + CreatedBy + "','" + Date + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();*/ // Execute insert query.
            theCommand.CommandText = "SELECT * FROM INVHEAD WHERE DOCNO='" + ID + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
            theCommand.CommandType = CommandType.Text;
            SqlDataReader theReader = theCommand.ExecuteReader();
            theReader.Read();

            if (theReader.HasRows)
                theReader.Close();
            else
            {
                theReader.Close();

                theCommand.CommandText = "SELECT ISNULL(StkofStores,'') from PARMS WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                theCommand.CommandType = CommandType.Text;
                SqlDataReader theReader1 = theCommand.ExecuteReader();
                theReader1.Read();
                string glcode = theReader1[0].ToString();
                theReader1.Close();

                theCommand.CommandText = "INSERT INTO INVHEAD(COMPCODE,YEARCODE,TYPES,DOCNO,DOCDT,BKCODE,SLCODE,GVALUE,BILLVALUE,CASHTRAN,REMARKS,tag,user01,logdt01,GLCODE) "+
                    "VALUES ('" + compcode + "','" + yearcode + "','P','" + ID + "','" + Date + "', 'RC', '" + SupplierId + "', '" + Total + "','" + Total + "',0,'Purchase Reagent',1,'" + CreatedBy + "',getdate(),'" + glcode + "')";
                theCommand.CommandType = CommandType.Text;
                effectedRows = theCommand.ExecuteNonQuery();

                theCommand.CommandText = "update DOCSERIAL set LASTNO='" + ID + "' where COMPCODE='" + compcode + "' and YEARCODE='" + yearcode + "' and AUTOCODE='RC' and BOOKCODE='RC'";
                theCommand.CommandType = CommandType.Text;
                effectedRows = theCommand.ExecuteNonQuery();
            }
            // string tdate = ExpiryDate.Substring(6, 4) + "/" + ExpiryDate.Substring(3, 2) + "/" + ExpiryDate.Substring(0, 2);
            string tdate = ExpiryDate;
            theCommand.CommandText = "INSERT INTO INVDETL(COMPCODE,YEARCODE,TYPES,DOCNO,SRLNO,BATCHNO,EXPDATE,ICODE,IQTY,ITRCD,IRATE,IAMOUNT,tag,user01,logdt01) "+
                "VALUES ('" + compcode + "','" + yearcode + "','P','" + ID + "','" + srl + "', '" + BatchNo + "', '" + ExpiryDate + "', '" + ReagentID + "', '" + Quantity + "', 'I', '" + Price + "', '" + TotalPrice + "', 1,'" + CreatedBy + "',getdate())";
            theCommand.CommandType = CommandType.Text;
            effectedRows = theCommand.ExecuteNonQuery();
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


    public bool InsertReagentEntryMap(string ReagentID, string date)
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
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "INSERT INTO PH_ReagentEntryMap(ReagentEntryCode,Date)  VALUES('" + ReagentID + "','" + date + "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
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

    public bool UpdateReagentEntryMap(string compcode, string yearcode,string Docno, string date,string Total)
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
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "update INVHEAD set docdt='" + date + "',GVALUE='" + Total + "',BILLVALUE='" + Total + "' where ltrim(rtrim(docno))=ltrim(rtrim('" + Docno + "')) and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
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
    public bool DeleteDetail(string compcode, string yearcode,string Docno)
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
            // string s = Session["userName"];
            theCommand.Connection = theConnection;
            theCommand.CommandText = "delete from INVDETL where docno='" + Docno + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
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
    public bool UpdateReagentEntry(string ID, string ReagentID, string BatchNo, string CompanyId, string SupplierId, string ManufacturerId, string Quantity, string BonusQty, string Price, string TotalPrice, string ExpiryDate, string dat)
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

            theCommand.CommandText = "update PH_ReagentEntry set Date='" + dat + "',ReagentID='" + ReagentID + "', BatchNo='" + BatchNo + "', CompanyId='" + CompanyId + "', SupplierId='" + SupplierId + "',ManufacturerId='" + ManufacturerId + "',Quantity='" + Quantity + "',BonusQty='" + BonusQty + "',Price='" + Price + "',TotalPrice='" + TotalPrice + "',ExpiryDate='" + ExpiryDate + "'  where ID = '" + ID + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.
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