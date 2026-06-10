using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for ImportFromExcel
/// </summary>
public class ImportFromExcel
{
    public ImportFromExcel(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable MedicineTable;


    public DataTable GetIDDetails(string MedicineName)
    {

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select mm.MCode,mm.MedicineGroupID,mm.SubGroupid,mm.MedicineID from dbo.IPD_MedicineMaster mm where mm.MedicineName='" + MedicineName + "'";
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


    public DataTable GeneratePurchaseId(string compcode)
    {


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_MD_Generatepurchaseid '"+ compcode+"'";
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



    public bool InsertPurchaseMedicine(string compcode,string yearcode, string MedicineID, string BatchNo, string ExpiryDate, string Qty,string mrp, string LoginUser)
    {
        //if (ExpiryDate != "NULL")
        //    ExpiryDate = "'" + ExpiryDate + "'"; 
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
                        theCommand.CommandText = "SELECT * FROM BATDETL WHERE compcode='" + compcode + "' and yearcode='" + yearcode + "' and ICODE=dbo.Fnc_GetMedicineIcode('" + compcode + "','" + MedicineID + "') and batchno='" + BatchNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();

                        if (theReader.HasRows)
                        {
                            string opnstk = theReader["OPSTOCK"].ToString().Trim();
                            theReader.Close();
                            theCommand.CommandText = "UPDATE BATDETL set EXPDATE='" + ExpiryDate + "',OPSTOCK='" + Qty + "',CURSTOCK='" + Qty + "',MRP='" + mrp + "',USER02='" + LoginUser + "', LOGDT02=getdate() where compcode='" + compcode + "' and yearcode='" + yearcode + "' and batchno='" + BatchNo + "' and ltrim(rtrim(ICODE))=ltrim(rtrim(dbo.Fnc_GetMedicineIcode('" + compcode + "','" + MedicineID + "')))";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            theReader.Close();
                            theCommand.CommandText = "INSERT INTO BATDETL(COMPCODE,YEARCODE,BATCHNO,ICODE,EXPDATE,OPSTOCK,CURSTOCK,MRP,TAG, USER01, LOGDT01) VALUES ('" + compcode + "','" + yearcode + "','" + BatchNo + "',dbo.Fnc_GetMedicineIcode('" + compcode + "','" + MedicineID + "'),'" + ExpiryDate + "','" + Qty + "','" + Qty + "','"+mrp+"', 1,'" + LoginUser + "',getdate())";
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
}