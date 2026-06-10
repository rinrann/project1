using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PatientHistory
/// </summary>
public class PatientHistory
{
	public PatientHistory(string con)
	{
        conString = con;
	}

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetPatientDtls( string CompCode,string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select PatientRegNo as PatientReg,PName patient_name,age,GuadianName guardian_name,pr.SpouseName,pr.Sex,pr.Address vill_city, PhNo1,convert(varchar(10),pr.AppointMentDate,103)+' '+ convert(varchar(5),pr.AppointmentTime,108) as RegistrationDate,Gen.SexName from  OPD_PatientRegistration pr,gn_gender Gen where Gen.CompCode=pr.CompCode and Gen.ID=pr.Sex and pr.compcode='" + CompCode + "' and pr.PatientRegNo='" + regno + "'";
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

    public DataTable GetHistory(string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select *,CONVERT(varchar,Date,103) Date1 from dbo.OPD_PatientHistoryMapping where PatientReg='" + reg + "'";
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

    public DataTable GetVaccine(string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select map.*,vm.Name,CONVERT(varchar,Date,103) Date1 from dbo.OPD_PatientVaccineMap map,dbo.OPD_VaccinationMaster vm where vm.ID=map.VacineID and PatientReg='" + reg + "'";
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

    public DataTable GetInvestigation(string compcode,string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "select *,CONVERT(varchar,Date,103) Date1 from dbo.OPD_PatientInvestigationMap where PatientReg='" + reg + "'";
        string lsSql = "select Req.RequisitionNo,case isNull(Req.ReferalName,'Self') when 'Self' then 'Self' else dbo.fn_GetDoctor(Req.compcode,Req.ReferalName) end as ReferalName,Convert(varchar(10),Req.TestDate,103) as ReqDate,Convert(varchar(5),Req.Time,108)Time,dbo.Fnc_ReqWiseTotBill(Req.CompCode,Req.RegistrationNo,Req.RequisitionNo)PaidAmt,Req.DueAmt,isNull(Req.DiscountAmt,0) as DiscountAmt," +
            "ReqMap.TestCode,dbo.fn_GetDoctor(ReqMap.compcode,ReqMap.consultant)consultant,TestMast.TestName,TestMast.Cost,TestGrp.ProfileName " +
            "from PH_PatientReq Req,PH_RequisitionTestMap ReqMap,PH_TestMaster TestMast,PH_ProfileMaster TestGrp "+
            "where TestGrp.compcode=TestMast.CompCode and TestGrp.ProfileCode=TestMast.GroupCode and TestMast.compcode=ReqMap.compcode and TestMast.TestId=ReqMap.TestCode "+
            "and ReqMap.compcode=Req.compcode and ReqMap.RequisitionID=Req.RequisitionNo and "+
            "Req.compcode='" + compcode + "' and Req.RegistrationNo='" + reg + "' order by Req.TestDate asc,Req.Time asc";
        theCommand.CommandText = lsSql;
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

    public DataTable GetClinicalFinding(string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select *,CONVERT(varchar,Date,103) Date1 from dbo.OPD_PatientBodyMap map,OPD_Complain c where c.RowId=map.Complain and PatientReg='" + reg + "'";
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

    public DataTable GetPrescriptiondtls(string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select map.RowID,CONVERT(varchar,op.Date,103) Date1,map.PrescriptionId,map.Dose,mm.MedicineName,ms.SubGrName,mg.MedicineGroupName from dbo.OPD_PrescriptionMapping map,dbo.IPD_MedicineGroup mg,dbo.IPD_MedicineMaster mm,dbo.IPD_MedicineSubGroup ms,OPD_Prescription op where op.PrescriptionID=map.PrescriptionId and map.GroupID=mg.MedicineGroupID and map.MedicineId=mm.MedicineID and map.SubGroupId=ms.ID and op.Date in (select  top 4 op.Date from OPD_Prescription op where  op.PatientReg='" + reg + "' order by op.Date desc)";
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

    public DataTable GetHospitalNote(string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select  *  from dbo.OPD_PatientNote n  where n.PatientReg='" + reg + "'";
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


    public DataTable GetReportDetails(string compcode, string docid, string fromdate, string todate, string option)
    {
        DataTable dt = new DataTable();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandTimeout = 600;
        theCommand.CommandText = "exec Dsp_DocWiseMISRpt '" + HttpContext.Current.Session["CoCode"].ToString() + "','" + docid + "','" + fromdate + "','" + todate + "'," + option + "";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }


    public DataTable GetInvoiceReportDetails(string compcode, string fromdate, string todate)
    {
        DataTable dt = new DataTable();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_InvoiceDetails '" + compcode + "','" + fromdate + "','" + todate + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }


    public DataTable GetIVFPaymentDetls(string compcode, string fromdate, string todate)
    {
        DataTable dt = new DataTable();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_PackageTestPaymentStatus '" + compcode + "','" + fromdate + "','" + todate + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(dt); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return dt;
    }

    public DataTable GetPatientQueue(string compcode, string date,string docid)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "select B.RegNo ,convert(varchar(10),B.BillDate,103) as DispBillDate,convert(varchar(5),B.BillDate,108) as DispBillTime,P.PatientName,P.Docid,dbo.fn_GetDoctorDetails(P.compcode,p.Docid,'N') as DocName from PatientBillDtls B,PH_PatientReq P where P.compcode=B.compcode and P.RegistrationNo=B.RegNo and P.RequisitionNo=B.ReqNo and B.compcode='" + compcode + "' and B.BillDate='" + date + "' and isNull(B.Cancel,'N')='N' order by B.BillDate asc";
        theCommand.CommandText = "exec Dsp_PatinetQueueList '" + HttpContext.Current.Session["CoCode"].ToString() + "','" + date + "'";
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

    public DataTable GetPatientInvoiceList(string compcode, string regno,string ptname,string phno,string invdate)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        string strsql = "select ROW_NUMBER() OVER (ORDER BY P.BillDate ASC) AS SrNo, P.VchNo,P.ReceiptNo,P.MoneyReceiptNo,P.RegNo,R.PatientName,P.ReqNo,R.ReqType as BillType,Case R.ReqType when 'OPD' then 'OPD' else 'Diagnosis' end BillTypeName,P.BillAmt,Convert(varchar,P.BillDate,103)BillDate,Convert(varchar,P.PaymentDate,103)PaymentDate,isNull((R.PayableAmt-R.DiscountAmt-P.BillAmt-dbo.Fnc_ReqWisePrevTotBill(p.CompCode,p.RegNo,p.ReqNo,p.PaymentDate)-dbo.Fnc_VchnoWiseTotAdvance(p.CompCode,p.RegNo,p.ReqNo)),0) as DueAmt,Case P.PaymentMode when 'C' then 'Cash' when 'R' then 'Card' when 'E' then 'e-Wallet' else 'Net Banking' End as PayMode,R.PatientName,P.CancelRequest Cancel,P.CancelRemarks,isNull(P.CancelRequestStatus,0) ReqSts,case isNull(P.CancelRequestStatus,0) when 0 then '' when 1 then 'Approved' when 2 then 'Reject' else 'Pending' end ReqStatus,case isNull(RefundStatus,0) when 0 then '' else 'Done' end RefundStatus,isnull(P.cancel,'N')cncl,Case when isNull(P.Cancel,'N')='Y' then 'Cancelled' when isNull(P.CancelRequest,0)=1 then 'Requested for cancelation' else '' end as BillStatus from PatientBillDtls P,PH_PatientReq R  where R.CompCode=P.CompCode and R.RegistrationNo=P.RegNo and R.RequisitionNo=P.ReqNo and P.compcode='" + compcode + "' /*and isnull(P.cancel,'N')='N'*/";

        if (regno != "")
        {
            strsql = strsql + " and P.RegNo='" + regno + "'";
        }

        if (ptname != "")
        {
            strsql = strsql + " and R.PatientName like'%" + ptname + "%'";
        }
        if (phno != "")
        {
            strsql = strsql + " and R.Ph1 ='" + phno + "'";
        }

        if (invdate != "")
        {
            strsql = strsql + " and convert(date,P.BillDate,103) ='" + invdate + "'";
        }

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = strsql;
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


        return custTable;
    }

}