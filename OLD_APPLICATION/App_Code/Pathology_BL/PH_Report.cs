using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
 
public class PH_Report
{
    public PH_Report(string con)
	{
        conString = con;
	}
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataSet ds = new DataSet();
    private DataTable dt = new DataTable();

    public DataSet GetCollectionRegister(string fromdt, string todt, string grp)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_InvestGroupWiseCollecReg_Rpt '" + HttpContext.Current.Session["CoCode"].ToString() + "','" + HttpContext.Current.Session["YearCode"].ToString() + "','" + fromdt + "','" + todt + "','" + grp + "'";
        theCommand.CommandType = CommandType.Text; 
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;          
        theAdapter.Fill(ds); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return ds;
    }

    public DataSet GetServiceCollectionRegister(string fromdt, string todt,string servGroup, string srvices)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_InvestWiseCollecReg_Rpt '" + HttpContext.Current.Session["CoCode"].ToString() + "','" + HttpContext.Current.Session["YearCode"].ToString() + "','" + fromdt + "','" + todt + "','" + srvices + "','" + servGroup + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(ds); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return ds;
    }

    public DataTable GetStockMovementRegister(string fromdt, string todt, string grp)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_A_StockLedger_BatchWise '" + HttpContext.Current.Session["CoCode"].ToString() + "','" + HttpContext.Current.Session["YearCode"].ToString() + "','" + HttpContext.Current.Session["username"].ToString() + "' ,'" + fromdt + "','" + todt + "','Y','" + grp + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }
    public DataTable GetSaleRegister(string fromdt, string todt, string RegNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_PharmaSalesRegister '" + HttpContext.Current.Session["CoCode"].ToString() + "','" + HttpContext.Current.Session["YearCode"].ToString() + "','" + RegNo + "' ,'" + fromdt + "','" + todt + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }

    public DataTable GetPurchaseRegister(string fromdt, string todt, string supplier, string item)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_PharmaPurchaseRegister '" + HttpContext.Current.Session["CoCode"].ToString() + "','" + HttpContext.Current.Session["YearCode"].ToString() + "','" + supplier + "','" + item + "' ,'" + fromdt + "','" + todt + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }
    public DataTable GetdropdownSuppilier()
    { 
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select '' scode,'All' SName union select scode,SName from PH_SuppilierMaster where COMPCODE='" + HttpContext.Current.Session["CoCode"].ToString() + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }
    public DataTable GetdropdownItems()
    { 
        // Connection.
        dt.Clear();
        theAdapter.Dispose();
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
       
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select '' MedicineID,'All' MedicineName union select MedicineID,MedicineName from IPD_MedicineMaster where COMPCODE='" + HttpContext.Current.Session["CoCode"].ToString() + "' and isnull(itype,'')='M'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
 

        return dt;
    }

    public DataTable GetStockValuationRegister(string fromdt, string todt, string grp, string itemwise, string withvalue)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_Stock @a_CompCode='" + HttpContext.Current.Session["CoCode"].ToString() + "',@a_YearCode='" + HttpContext.Current.Session["YearCode"].ToString() + "',@a_UserName='" + HttpContext.Current.Session["username"].ToString() + "' ,@a_Date01='" + fromdt + "',@a_Date02='" + todt + "',@a_AllItems='Y',@a_Itemgrpcodes='" + grp + "',@a_ItemWise=" + itemwise + ",@a_WithValue=" + withvalue + "";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }


    public DataTable GetSamplecollection(DateTime fromdt, DateTime todt, string sample,string all)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if(all=="Y")
            theCommand.CommandText = "exec Dsp_LabTestSampleCollecReg_Rpt '" + HttpContext.Current.Session["CoCode"].ToString() + "','" + HttpContext.Current.Session["YearCode"].ToString() + "' ,'" + fromdt.ToString("yyyy/MM/dd") + "','" + todt.ToString("yyyy/MM/dd") + "','Y'";
        else
            theCommand.CommandText = "exec Dsp_A_StockLedger '" + HttpContext.Current.Session["CoCode"].ToString() + "','" + HttpContext.Current.Session["YearCode"].ToString() + "','" + HttpContext.Current.Session["username"].ToString() + "' ,'" + fromdt.ToString("yyyy/MM/dd") + "','" + todt.ToString("yyyy/MM/dd") + "','N','" + sample + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }

    public DataTable GetPatientRegcollection(DateTime fromdt, DateTime todt)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_PatientRegistrationCollecReg_Rpt '" + HttpContext.Current.Session["CoCode"].ToString() + "','" + HttpContext.Current.Session["YearCode"].ToString() + "' ,'" + fromdt.ToString("yyyy/MM/dd") + "','" + todt.ToString("yyyy/MM/dd") + "','Y'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }

    public DataTable getsampleddl()
    {
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select SCode,SName from PH_SpecimanMaster where compcode='" + HttpContext.Current.Session["CoCode"].ToString() + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }


    public DataTable GetDateWiseCollection(string compcode,string yearcode,string fromdate,string todate,string paymode,string type,string userId)
    {
        DataTable dt = new DataTable();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_DateWiseCollection @a_CompCode='" + compcode + "',@a_YearCode='" + yearcode + "',@a_FromDate='" + fromdate + "' ,@a_ToDate='" + todate + "',@a_PayMode='" + paymode + "',@a_Type='"+ type +"',@a_User='"+ userId +"'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }


    public DataTable GetDateWiseRefund(string compcode, string yearcode, string fromdate, string todate, string paymode)
    {
        DataTable dt=new DataTable();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (paymode == "C")
        {
            theCommand.CommandText = "select isNull(sum(BillAmt),0) as RefAmt from PatientBillDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and convert(date,PaymentDate,103) between '" + fromdate + "' and '" + todate + "' and PaymentMode='C' and isNull(Cancel,'N')='Y'";
        }
        else
        {
            theCommand.CommandText = "select isNull(sum(BillAmt),0) as RefAmt from PatientBillDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and convert(date,PaymentDate,103) between '" + fromdate + "' and '" + todate + "' and PaymentMode<>'C' and isNull(Cancel,'N')='Y'";
        }
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }

    public DataTable GetRegList(string CompCode, string YearCode, string FrmDt, string ToDt)
    {
        DataTable dt = new DataTable();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        string lsSql = "select PatientRegNo,PName,PhNo1,Address,District,CONVERT(varchar(10),appointmentdate,103)RegDate,CONVERT(varchar(10),DOB,103) BirthDate,"+
        "PresentState,dbo.Fnc_StateName(Reg.CompCode,Reg.PresentState) StateName,PresentCountry,GN_Country.CountryName,dbo.fn_DistrictName(convert(varchar,Reg.District)) DistrictName " +
        "from OPD_PatientRegistration Reg left join GN_Country on GN_Country.COMPCODE=Reg.compcode and GN_Country.CountryId=Reg.PresentCountry where Reg.compcode='" + CompCode + "'";
        if(FrmDt!="" && ToDt!="")
        {
            lsSql = lsSql + " and CONVERT(date,appointmentdate,103) between '" + FrmDt + "' and '" + ToDt + "'";
        }
        else if (FrmDt != "")
        {
            lsSql = lsSql + " and CONVERT(date,appointmentdate,103) >= " + FrmDt + "'";
        }
        else if (ToDt != "")
        {
            lsSql = lsSql + " and CONVERT(date,appointmentdate,103) <= " + ToDt + "'";
        }
        theCommand.CommandText = lsSql;
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }
}