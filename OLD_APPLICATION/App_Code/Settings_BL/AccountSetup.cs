using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AccountSetup
/// </summary>
public class AccountSetup
{
    public AccountSetup(string con)
	{
        conString = con;
	}
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;

    public DataTable getJournal(string compcode)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select BkCode,BkName from bkcodes where BkType='J' and compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;


        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataTable getRecord(string compcode, string yearcode)
    {
        DataTable recTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select CFILLER01,Patient_GL,Doctor_GL,P_BedCharges_GL,P_ConsCharges_GL,P_DocCharges_GL,P_MedicineCharges_GL,P_PathCharges_GL,P_UsgCharges_GL,P_XrayCharges_GL,P_ServicesCharges_GL,P_SisAyaCharges_GL,P_AmbulanceCharges_GL,P_OTCharges_GL,P_AnthesCharges_GL,P_OthrCharges_GL,P_OTConsCharges_GL,P_OTInsCharges_GL,P_OTAttCharges_GL,P_OTAnthesConsCharges_GL,SurgeonCharge_GL,DocVisitCharge_GL,DocAnesthesisCharge_GL,DocRefCharge_GL,P_DialysisCharges_GL,P_DialysisFees_GL,P_OPDFees_GL,P_DispFees_GL  from PCPARMS1 where compcode='" + compcode + "' and yearcode='" + yearcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;


        // Datatable.
        recTable = new DataTable();
        theAdapter.Fill(recTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return recTable;
    }

    public DataTable getGL(string compcode)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select GLCODE,GLNAME from glmast where compcode='GFC'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;


        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public bool Update(string compcode, string yearcode, string ddljrnl, string patient, string doctor, string bed, string cons, string docvisit, string medicin, string patho, string service,string sister, string usg, string xray, string ambulance, string otchrg, string anesthesia, string other,string otconschrg,string otinschrg,string otattchrg,string anesthesiacons,string flag,string surgeon,string visit,string docanesthesis,string refdoc, string dialysis,string opdfees,string disposal,string user, string date)
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
                        theCommand.CommandText = "Update PCPARMS1 set CFILLER01='" + ddljrnl + "',Patient_GL='" + patient + "',Doctor_GL='" + doctor + "',P_BedCharges_GL='" + bed + "',P_ConsCharges_GL='" + cons + "',P_DocCharges_GL='" + docvisit + "',P_MedicineCharges_GL='" + medicin + "',P_PathCharges_GL='" + patho + "',P_UsgCharges_GL='" + usg + "',P_XrayCharges_GL='" + xray + "',P_ServicesCharges_GL='" + service + "',P_SisAyaCharges_GL='" + sister + "',P_AmbulanceCharges_GL='" + ambulance + "',P_OTCharges_GL='" + otchrg + "',P_AnthesCharges_GL='" + anesthesia + "',P_OthrCharges_GL='" + other + "',P_OTConsCharges_GL='" + otconschrg + "',P_OTInsCharges_GL='" + otinschrg + "',P_OTAttCharges_GL='" + otattchrg + "',P_OTAnthesConsCharges_GL='" + anesthesiacons + "',SurgeonCharge_GL='" + surgeon + "',DocVisitCharge_GL='" + visit + "',DocAnesthesisCharge_GL='" + docanesthesis + "',DocRefCharge_GL='" + refdoc + "',P_DialysisFees_GL='" + dialysis + "',P_OPDFees_GL='" + opdfees + "',P_DispFees_GL='"+disposal+"' where compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute update query. 
                        theCommand.Transaction = tran as SqlTransaction;

                        theCommand.CommandText = "Update parms set LinkAccount='" + flag + "' where compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute update query. 
                        theCommand.Transaction = tran as SqlTransaction;
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

    public Int32 getParmsData(string compcode,string yearcode)
    {
        Int32 retVal;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select LinkAccount from parms where compcode='"+compcode+"' and yearcode='"+yearcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        DataTable custTable;
        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        retVal = Convert.ToInt32(custTable.Rows[0]["LinkAccount"].ToString());
        return retVal;
    }
}