using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for HospitalInfoClass
/// </summary>
public class HospitalInfoClass
{
    public string constring;
    public SqlConnection theConnection;
    public SqlCommand theCommand;
    public SqlDataAdapter theAdapter;
    public DataTable hospitalinfo;

	public HospitalInfoClass(string con)
	{
        constring = con;
	}

    public DataTable GridFill(string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = constring;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select Hid,InstitutionName,Catagory,Address,Email,Ph1,Ph2,Fax,website,icu,dialysis,nicu, generalward,cabin,delux,hdu,Duplex,TotalBed,lisenceno,CONVERT(varchar,validitydate,103)validity,Rmo,isnull(MedicineIssue,'W') MedicineIssue,RegNoPrifix,isnull(CommCalcRule,'0')CommCalcRule,isnull(ReqNoRule,'0')ReqNoRule from Hospitalinformation where compcode='" + compcode + "' and yearcode='" + yearcode + "' ";
        theCommand.CommandText = "select Hid,InstitutionName,Catagory,Address,Email,Ph1,Ph2,Fax,website,icu,dialysis,nicu, generalward,cabin,delux,hdu,Duplex,TotalBed,lisenceno,CONVERT(varchar,validitydate,103)validity,Rmo,isnull(MedicineIssue,'W') MedicineIssue,RegNoPrifix,isnull(CommCalcRule,'0')CommCalcRule,isnull(ReqNoRule,'0')ReqNoRule from Hospitalinformation where compcode='" + compcode + "' and yearcode='" + yearcode + "' ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalinfo = new DataTable();
        theAdapter.Fill(hospitalinfo); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalinfo;
    }

    public DataTable FillHospitalDetails(string compcode, string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = constring;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select Hid,InstitutionName,Catagory,Address,Email,Ph1,Ph2,Fax,website,icu,dialysis,nicu, generalward,cabin,delux,hdu,Duplex,TotalBed,lisenceno,CONVERT(varchar,validitydate,103)validity,Rmo,isnull(MedicineIssue,'W') MedicineIssue,RegNoPrifix,isnull(CommCalcRule,'0')CommCalcRule,isnull(ReqNoRule,'0')ReqNoRule from Hospitalinformation where compcode='" + compcode + "' and yearcode='" + yearcode + "' ";
        theCommand.CommandText = "select coname,Catagory,Addr,Email,Phone,Ph2,Fax,web,logopath,logopath2,icu,dialysis,nicu, generalward,cabin,delux,hdu,Duplex,TotalBed,lisenceno,HosRegnNo,CONVERT(varchar,validitydate,103)validity,Rmo,isnull(MedicineIssue,'W') MedicineIssue,RegNoPrifix,isnull(CommCalcRule,'0')CommCalcRule,isnull(ReqNoRule,'0')ReqNoRule from parms where compcode='" + compcode + "' and yearcode='" + yearcode + "' ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        hospitalinfo = new DataTable();
        theAdapter.Fill(hospitalinfo); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return hospitalinfo;
    }
    public bool HospitalInfo_Insert_Update_Delete(string Mode, string Hid, string InstitutionName, string Catagory, string Address, string Email, string Ph1, string Ph2, string Fax, string website, string icu, string dialysis, string nicu, string generalward, string cabin, string delux, string hdu, string duplex, string TotalBed, string lisenceno, string Hosrgno, string validitydate, string Rmo, string ddlMedcineIssue, string prefix, string CommCalRule, string ReqNoRule, string logo, string logo2, string compcode, string yearcode, string user)
    {

        if (Ph2 == "")
            Ph2 = "null";
        else
            Ph2 = "'" + Ph2 + "'";


        int EffectRows = 0;
        try
        {
            theConnection = new SqlConnection();
            if (constring != "")
            {
                theConnection.ConnectionString = constring;
                theConnection.Open();
            }
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "Exec  sp_HospitalDetails  '" + Mode + "','" + Hid + "','" + InstitutionName + "','" + Catagory + "','" + Address + "','" + Email + "','" + Ph1 + "'," + Ph2 + ",'" + Fax + "','" + website + "','" + icu + "','" + dialysis + "','" + nicu + "','" + generalward + "','" + cabin + "','" + delux + "','" + hdu + "','" + duplex + "','" + TotalBed + "','" + lisenceno + "','" + Hosrgno + "','" + validitydate + "','" + Rmo + "','" + ddlMedcineIssue + "','" + prefix + "','" + CommCalRule + "','" + ReqNoRule + "','" + logo + "','" + logo2 + "','" + compcode + "','" + yearcode + "','" + user + "'";
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
            theConnection.Close();
        }

    }
}