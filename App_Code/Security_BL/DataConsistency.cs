using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DataConsistency
/// </summary>
public class DataConsistency
{
	public DataConsistency(string con)
	{
        conString = con;
	}
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    //private DataTable ImplantTable;

    public bool process(string cocode,string yearcode,string dt,string user)
    {
        int EffectRows = 0;
        try
        {
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }
            //exec sp_PopulateDoctorLedger @createDate datetime,@createUser varchar(50)

            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;

            theCommand.CommandText = "Exec  sp_PopulateDoctorLedger  '" + cocode + "','" + dt + "','" + user + "'";
            theCommand.CommandType = CommandType.Text;
            EffectRows = theCommand.ExecuteNonQuery();

            theCommand.CommandText = "Exec  sp_PopulateslmastFrmAC_ledger  '" + cocode + "','" + yearcode + "','" + dt + "','" + user + "'";
            theCommand.CommandType = CommandType.Text;
            EffectRows = theCommand.ExecuteNonQuery();

            theCommand.CommandText = "Exec  Dsp_PopulateItemastFrmMedicine  '" + cocode + "','" + yearcode + "','" + user + "'";
            theCommand.CommandType = CommandType.Text;
            EffectRows = theCommand.ExecuteNonQuery();
            
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