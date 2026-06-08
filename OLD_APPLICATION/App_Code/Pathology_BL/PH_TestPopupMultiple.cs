using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for PH_TestPopupMultiple1245
/// </summary>
public class PH_TestPopupMultiple
{
    public PH_TestPopupMultiple(string con)
    {
        conString = con;
    }



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


    public DataTable GridFill(string compcode, string name, string code, string dept, string testGrp)
    {
        string tname, tcode, tdept;
        if (name == "")
            tname = "null";
        else
            tname = name.ToString();

        if (code == "")
            tcode = "null";
        else
            tcode = code.ToString();

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.CommandTimeout = 0;
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_TestDetails '" + compcode + "'," + tcode + "," + tname + "," + dept + "," + testGrp + "";

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

    public DataTable GetExistTestDetails(string compcode, string yearcode, string ReqNo, string curtest)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        
            if (curtest == "")
            {
                //theCommand.CommandText = " select map.Remarks,CONVERT(varchar,map.TReqDate,103) Date,CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate, tm.TestId,map.TestReqNo,map.TReqTime Time,tm.TestName,tm.Cost,map.consultant,dbo.fn_GetDoctor(map.compcode,map.consultant) as consultantname,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'Q') DocQualification,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'T') DocType,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'R') DocRegNo,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestMaster tm where tm.compcode=p.compcode and map.compcode=p.compcode and tm.TestId=map.TestCode and map.RequisitionID= p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all  select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,x.TestId,map.TestReqNo,map.TReqTime Time,x.TestName,x.TestCost,map.consultant,dbo.fn_GetDoctor(map.compcode,map.consultant) as consultantname,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestXRAYMaster x  where x.compcode=p.compcode and map.compcode=p.compcode and x.TestId=map.TestCode and map.RequisitionID=p.RequisitionNo and p.Status=1 and p.RequisitionNo= '" + ReqNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all   select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,usg.ID,map.TestReqNo,map.TReqTime Time,usg.Name,sub.Charges,map.consultant,dbo.fn_GetDoctor(map.compcode,map.consultant) as consultantname,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'Q') DocQualification,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'T') DocType,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'R') DocRegNo,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p, PH_RequisitionTestMap map,PH_USGNameMaster usg,PH_USGSubGrMaster sub where sub.compcode=usg.compcode and usg.compcode=p.compcode and map.compcode=p.compcode and  usg.ID=map.TestCode and  map.RequisitionID=p.RequisitionNo and p.Status=1 and usg.SubGroupCode=sub.SubGrID and  p.RequisitionNo='" + ReqNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all   select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,pr.ProfileCode,map.TestReqNo,map.TReqTime Time,pr.ProfileName,pr.Price,map.consultant,dbo.fn_GetDoctor(map.compcode,map.consultant) as consultantname,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'Q') DocQualification,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'T') DocType,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'R') DocRegNo,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_ProfileMaster pr where pr.compcode=p.compcode and map.compcode=p.compcode and pr.ProfileCode= map.TestCode and map.RequisitionID=p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "'";
                theCommand.CommandText = " select ROW_NUMBER() OVER(ORDER BY map.RequisitionID ASC) AS Srl,map.Remarks,CONVERT(varchar,map.TReqDate,103) Date,CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate, tm.TestId,map.TestReqNo,map.TReqTime Time,tm.TestName,map.Cost,map.consultant,'Dr. '+dbo.fn_GetDoctor(map.compcode,map.consultant) as consultantname,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'Q') DocQualification,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'T') DocType,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'R') DocRegNo,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt,isNull(tm.TestType,'')TestType from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestMaster tm where tm.compcode=p.compcode and map.compcode=p.compcode and tm.TestId=map.TestCode and map.RequisitionID= p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and p.compcode='" + compcode + "' /*and p.yearcode='" + yearcode + "'*/";
            }
            else
            {
                //theCommand.CommandText = " select map.Remarks,CONVERT(varchar,map.TReqDate,103) Date,CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate, tm.TestId,map.TestReqNo,map.TReqTime Time,tm.TestName,tm.Cost,map.consultant,dbo.fn_GetDoctor(map.compcode,map.consultant) as consultantname,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'Q') DocQualification,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'T') DocType,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'R') DocRegNo,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestMaster tm where tm.compcode=p.compcode and map.compcode=p.compcode and tm.TestId=map.TestCode and map.RequisitionID= p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and map.TestreqNo in(" + curtest + ") and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all  select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,x.TestId,map.TestReqNo,map.TReqTime Time,x.TestName,x.TestCost,map.consultant,dbo.fn_GetDoctor(map.compcode,map.consultant) as consultantname,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestXRAYMaster x  where x.compcode=p.compcode and map.compcode=p.compcode and x.TestId=map.TestCode and map.RequisitionID=p.RequisitionNo and p.Status=1 and p.RequisitionNo= '" + ReqNo + "' and map.TestreqNo in(" + curtest + ") and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all   select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,usg.ID,map.TestReqNo,map.TReqTime Time,usg.Name,sub.Charges,map.consultant,dbo.fn_GetDoctor(map.compcode,map.consultant) as consultantname,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'Q') DocQualification,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'T') DocType,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'R') DocRegNo,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p, PH_RequisitionTestMap map,PH_USGNameMaster usg,PH_USGSubGrMaster sub where sub.compcode=usg.compcode and usg.compcode=p.compcode and map.compcode=p.compcode and  usg.ID=map.TestCode and  map.RequisitionID=p.RequisitionNo and p.Status=1 and usg.SubGroupCode=sub.SubGrID and  p.RequisitionNo='" + ReqNo + "' and map.TestreqNo in(" + curtest + ") and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all   select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,pr.ProfileCode,map.TestReqNo,map.TReqTime Time,pr.ProfileName,pr.Price,map.consultant,dbo.fn_GetDoctor(map.compcode,map.consultant) as consultantname,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'Q') DocQualification,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'T') DocType,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'R') DocRegNo,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_ProfileMaster pr where pr.compcode=p.compcode and map.compcode=p.compcode and pr.ProfileCode= map.TestCode and map.RequisitionID=p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and map.TestreqNo in(" + curtest + ") and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "'";
                theCommand.CommandText = " select ROW_NUMBER() OVER(ORDER BY map.RequisitionID ASC) AS Srl,map.Remarks,CONVERT(varchar,map.TReqDate,103) Date,CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate, tm.TestId,map.TestReqNo,map.TReqTime Time,tm.TestName,map.Cost,map.consultant,'Dr. '+dbo.fn_GetDoctor(map.compcode,map.consultant) as consultantname,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'Q') DocQualification,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'T') DocType,dbo.fn_GetDoctorDetails(map.compcode,map.consultant,'R') DocRegNo,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt,isNull(tm.TestType,'')TestType from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestMaster tm where tm.compcode=p.compcode and map.compcode=p.compcode and tm.TestId=map.TestCode and map.RequisitionID= p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and map.TestreqNo in(" + curtest + ") and p.compcode='" + compcode + "' /*and p.yearcode='" + yearcode + "'*/";
                
            }
        
        //theCommand.CommandText = "select  map.TestCode TestId,tm.Cost,map.Remarks,CONVERT(VARCHAR,map.TReqDate,103) Date,map.TestReqNo,map.TReqTime Time,tm.TestName from PH_RequisitionTestMap map,dbo.PH_PatientReq pr,PH_TestMaster tm where tm.TestId=map.TestCode and pr.RequisitionNo=map.RequisitionID and pr.Status=1 and pr.RequisitionNo='" + ReqNo + "'";

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

    public DataTable GetExistSampleDetails(string compcode, string yearcode, string ReqNo, string curtest)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (curtest == "")
            theCommand.CommandText = "select CONVERT(varchar,p.CollectDate,103) Date,CONVERT(varchar,p.DeliveryDate,103) DeliveryDate, p.SampleCode,p.RegistrationNo,dbo.Fnc_SampleName(p.CompCode,p.SampleCode)+ ' ('+unit +' Unit)' TestName,p.PatientBillAmt from PH_SampleCollect p where p.RequisitionNo='" + ReqNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "'";
        else theCommand.CommandText = "select CONVERT(varchar,p.CollectDate,103) Date,CONVERT(varchar,p.DeliveryDate,103) DeliveryDate, p.SampleCode,p.RegistrationNo,dbo.Fnc_SampleName(p.CompCode,p.SampleCode)+ ' ('+unit +' Unit)' TestName,p.PatientBillAmt from PH_SampleCollect p where p.RequisitionNo='" + ReqNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' and SrlNo=" + curtest + "";

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
    public DataTable GetExistSampleDetails(string compcode, string yearcode, string ReqNo, string curtest, string type = "1")
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "1")
        {
            if (curtest == "")
            {
                theCommand.CommandText = " select map.Remarks,CONVERT(varchar,map.TReqDate,103) Date,CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate, tm.TestId,map.TestReqNo,map.TReqTime Time,tm.TestName,tm.Cost,map.consultant,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestMaster tm where tm.compcode=p.compcode and map.compcode=p.compcode and tm.TestId=map.TestCode and map.RequisitionID= p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all  select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,x.TestId,map.TestReqNo,map.TReqTime Time,x.TestName,x.TestCost,map.consultant,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestXRAYMaster x  where x.compcode=p.compcode and map.compcode=p.compcode and x.TestId=map.TestCode and map.RequisitionID=p.RequisitionNo and p.Status=1 and p.RequisitionNo= '" + ReqNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all   select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,usg.ID,map.TestReqNo,map.TReqTime Time,usg.Name,sub.Charges,map.consultant,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p, PH_RequisitionTestMap map,PH_USGNameMaster usg,PH_USGSubGrMaster sub where sub.compcode=usg.compcode and usg.compcode=p.compcode and map.compcode=p.compcode and  usg.ID=map.TestCode and  map.RequisitionID=p.RequisitionNo and p.Status=1 and usg.SubGroupCode=sub.SubGrID and  p.RequisitionNo='" + ReqNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all   select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,pr.ProfileCode,map.TestReqNo,map.TReqTime Time,pr.ProfileName,pr.Price,map.consultant,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_ProfileMaster pr where pr.compcode=p.compcode and map.compcode=p.compcode and pr.ProfileCode= map.TestCode and map.RequisitionID=p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "'";
            }
            else
            {
                theCommand.CommandText = " select map.Remarks,CONVERT(varchar,map.TReqDate,103) Date,CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate, tm.TestId,map.TestReqNo,map.TReqTime Time,tm.TestName,tm.Cost,map.consultant,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestMaster tm where tm.compcode=p.compcode and map.compcode=p.compcode and tm.TestId=map.TestCode and map.RequisitionID= p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and map.TestreqNo in(" + curtest + ") and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all  select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,x.TestId,map.TestReqNo,map.TReqTime Time,x.TestName,x.TestCost,map.consultant,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestXRAYMaster x  where x.compcode=p.compcode and map.compcode=p.compcode and x.TestId=map.TestCode and map.RequisitionID=p.RequisitionNo and p.Status=1 and p.RequisitionNo= '" + ReqNo + "' and map.TestreqNo in(" + curtest + ") and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all   select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,usg.ID,map.TestReqNo,map.TReqTime Time,usg.Name,sub.Charges,map.consultant,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p, PH_RequisitionTestMap map,PH_USGNameMaster usg,PH_USGSubGrMaster sub where sub.compcode=usg.compcode and usg.compcode=p.compcode and map.compcode=p.compcode and  usg.ID=map.TestCode and  map.RequisitionID=p.RequisitionNo and p.Status=1 and usg.SubGroupCode=sub.SubGrID and  p.RequisitionNo='" + ReqNo + "' and map.TestreqNo in(" + curtest + ") and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "' union  all   select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,pr.ProfileCode,map.TestReqNo,map.TReqTime Time,pr.ProfileName,pr.Price,map.consultant,convert(varchar,p.TestDate,103)TestDt,convert(varchar,p.DeliveryDate,103)DeliveryDt from PH_PatientReq p,PH_RequisitionTestMap map,PH_ProfileMaster pr where pr.compcode=p.compcode and map.compcode=p.compcode and pr.ProfileCode= map.TestCode and map.RequisitionID=p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and map.TestreqNo in(" + curtest + ") and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "'";
                //theCommand.CommandText = " select map.Remarks,CONVERT(varchar,map.TReqDate,103) Date,CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate, tm.TestId,map.TestReqNo,map.TReqTime Time,tm.TestName,tm.Cost from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestMaster tm where tm.TestId=map.TestCode and map.RequisitionID= p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and map.TestreqNo in(" + curtest + ") union  all  select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,x.TestId,map.TestReqNo,map.TReqTime Time,x.TestName,x.TestCost from PH_PatientReq p,PH_RequisitionTestMap map,PH_TestXRAYMaster x  where x.TestId=map.TestCode and map.RequisitionID=p.RequisitionNo and p.Status=1 and p.RequisitionNo= '" + ReqNo + "' and map.TestreqNo in(" + curtest + ") union  all   select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,usg.ID,map.TestReqNo,map.TReqTime Time,usg.Name,sub.Charges from PH_PatientReq p, PH_RequisitionTestMap map,PH_USGNameMaster usg,PH_USGSubGrMaster sub where usg.ID=map.TestCode and  map.RequisitionID=p.RequisitionNo and p.Status=1 and usg.SubGroupCode=sub.SubGrID and  p.RequisitionNo='" + ReqNo + "' and map.TestreqNo in(" + curtest + ") union  all   select map.Remarks,CONVERT(varchar,map.TReqDate,103),CONVERT(varchar,map.TDeliveryDate,103) DeliveryDate,pr.ProfileCode,map.TestReqNo,map.TReqTime Time,pr.ProfileName,pr.Price from PH_PatientReq p,PH_RequisitionTestMap map,PH_ProfileMaster pr where pr.ProfileCode=  map.TestCode and map.RequisitionID=p.RequisitionNo and p.Status=1 and p.RequisitionNo='" + ReqNo + "' and map.TestreqNo in(" + curtest + ")";
            }
        }
        else
            theCommand.CommandText = "select CONVERT(varchar,p.CollectDate,103) Date,CONVERT(varchar,p.DeliveryDate,103) DeliveryDate, p.SampleCode,p.RegistrationNo,dbo.Fnc_SampleName(p.CompCode,p.SampleCode) TestName,p.PatientBillAmt from PH_SampleCollect p where p.RequisitionNo='" + ReqNo + "' and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "'";
        //theCommand.CommandText = "select  map.TestCode TestId,tm.Cost,map.Remarks,CONVERT(VARCHAR,map.TReqDate,103) Date,map.TestReqNo,map.TReqTime Time,tm.TestName from PH_RequisitionTestMap map,dbo.PH_PatientReq pr,PH_TestMaster tm where tm.TestId=map.TestCode and pr.RequisitionNo=map.RequisitionID and pr.Status=1 and pr.RequisitionNo='" + ReqNo + "'";

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
    public DataTable GetExistTestCostDetails(string compcode, string yearcode, string ReqNo, string type = "1")
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "1")
            theCommand.CommandText = "select AdAmt paid,DueAmt due,case when PaymentMode='C' then 'Cash' when  PaymentMode='E' then 'e-Wallet' when  PaymentMode='R' then 'Card' else 'Other' end as mode,dbo.AmountToWords(AdAmt) num2word,case when isnull(Bank_CardHolderName,'')='' then '' else 'Bank Name ' + Bank_CardHolderName end as Bank_CardHolderName,case when isnull(Chq_CardNo,'')='' then '' else 'Chq No '+ Chq_CardNo end as Chq_CardNo,isNull(TransactionId,'')TransactionId from PH_PatientReq where compcode='" + compcode + "' /*and yearcode='" + yearcode + "'*/ and RequisitionNo='" + ReqNo + "' ";
        else
            theCommand.CommandText = "Select isnull(sum(AdAmt),0)paid, isnull(sum(DueAmt),0)due,case when PaymentMode='C' then 'Cash' when  PaymentMode='E' then 'e-Wallet' when  PaymentMode='R' then 'Card' else 'Other' end as mode,dbo.AmountToWords(AdAmt) num2word,case when isnull(Bank_CardHolderName,'')='' then '' else 'Bank Name ' + Bank_CardHolderName end as Bank_CardHolderName,case when isnull(Chq_CardNo,'')='' then '' else 'Chq/Card No '+ Chq_CardNo end as Chq_CardNo,isNull(TransactionId,'')TransactionId  from PH_SampleCollect S where S.compcode='" + compcode + "' and /*S.yearcode='" + yearcode + "'  and*/ RequisitionNo='" + ReqNo + "' group by  S.RequisitionNo ,S.PaymentMode,dbo.AmountToWords(AdAmt),Bank_CardHolderName,Chq_CardNo,CardExpYrMnth ";

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

    public DataTable GetBillDetails(string compcode, string yearcode, string ReceiptNo, string type = "1")
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "1")
        {
            theCommand.CommandText = "Select B.ReceiptNo,B.MoneyReceiptNo,isNull(B.InvoiceType,'I')InvoiceType,isNull(R.PayableAmt,0)PayableAmt,isNull(DiscountAmt,0) DiscountAmt,isnull(B.BillAmt,0)paid, isNull(R.DueAmt,0)TotAmt,dbo.Fnc_ReqWiseTotBill(B.CompCode,B.RegNo,B.ReqNo) TotPaidAmt,(isNull(R.PayableAmt,0)-dbo.Fnc_ReqWiseTotBill(B.CompCode,B.RegNo,B.ReqNo)) DueAmt,isNull(dbo.Fnc_ReqWisePrevTotBill(B.CompCode,B.RegNo,B.ReqNo,B.PaymentDate),0) PrevBillAmt,case when B.PaymentMode='C' then 'Cash' when  B.PaymentMode='E' then 'e-Wallet' when  B.PaymentMode='R' then 'Card' else 'Other' end as mode,dbo.AmountToWords(B.BillAmt) num2word,case when isnull(B.Bank_CardHolderName,'')='' then '' else 'Card Holder Name ' + B.Bank_CardHolderName end as Bank_CardHolderName,case when isnull(B.Chq_CardNo,'')='' then '' else 'Chq/Card No '+ B.Chq_CardNo end as Chq_CardNo,isNull(B.TransactionId,'') TransactionId,Convert(varchar(10),B.PaymentDate,103) as BillDate,substring(convert(varchar(19), B.PaymentDate, 100), len(convert(varchar(19), B.PaymentDate, 100)) - 6, 7) as BillTime from PatientBillDtls B,PH_PatientReq R where R.compcode=B.compcode /*and R.Yearcode=B.yearcode*/ and R.RequisitionNo=B.ReqNo and B.compcode='" + compcode + "' and B.yearcode='" + yearcode + "' and B.ReceiptNo='" + ReceiptNo + "' ";
        }
        else
        {
            theCommand.CommandText = "Select B.ReceiptNo,B.MoneyReceiptNo,isNull(B.InvoiceType,'I')InvoiceType,isNull(S.PayableAmt,0)PayableAmt,isNull(DiscountAmt,0) DiscountAmt,isnull(B.BillAmt,0)paid, isNull(S.DueAmt,0)TotAmt,dbo.Fnc_ReqWiseTotBill(B.CompCode,B.RegNo,B.ReqNo) TotPaidAmt,(isNull(S.PayableAmt,0)-dbo.Fnc_ReqWiseTotBill(B.CompCode,B.RegNo,B.ReqNo)) DueAmt,isNull(dbo.Fnc_ReqWisePrevTotBill(B.CompCode,B.RegNo,B.ReqNo,B.PaymentDate),0) PrevBillAmt,case when B.PaymentMode='C' then 'Cash' when  B.PaymentMode='E' then 'e-Wallet' when  B.PaymentMode='R' then 'Card' else 'Other' end as mode,dbo.AmountToWords(B.BillAmt) num2word,case when isnull(B.Bank_CardHolderName,'')='' then '' else 'Card Holder Name ' + B.Bank_CardHolderName end as Bank_CardHolderName,case when isnull(B.Chq_CardNo,'')='' then '' else 'Chq/Card No '+ B.Chq_CardNo end as Chq_CardNo,isNull(B.TransactionId,'') TransactionId,Convert(varchar(10),B.PaymentDate,103) as BillDate,substring(convert(varchar(19), B.PaymentDate, 100), len(convert(varchar(19), B.PaymentDate, 100)) - 6, 7) as BillTime from  PatientBillDtls B,PH_SampleCollect S where S.compcode=B.compcode /*and S.Yearcode=B.yearcode*/ and S.RequisitionNo=B.ReqNo and B.compcode='" + compcode + "' and B.yearcode='" + yearcode + "' and B.ReceiptNo='" + ReceiptNo + "'";
        }

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

    public DataTable GetPaymentDetails(string compcode, string yearcode, string RecNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select ReceiptNo,MoneyReceiptNo,isnull(B.BillAmt,0)paidAmt,case when B.PaymentMode='C' then 'Cash' when  B.PaymentMode='E' then 'e-Wallet' when  B.PaymentMode='R' then 'Card' else 'Other' end as mode,Convert(varchar(10),B.PaymentDate,103) as PayDate,substring(convert(varchar(19), B.PaymentDate, 100), len(convert(varchar(19), B.PaymentDate, 100)) - 6, 7) as PayTime,Vchno from PatientBillDtls B where B.compcode='" + compcode + "' and B.yearcode='" + yearcode + "' and B.ReqNo='" + RecNo + "' order by B.PaymentDate";

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


    public DataTable GetAdvanceDetails(string compcode, string yearcode, string Vchno,string ReqNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "Select isNull(sum(isnull(AdvAmount,0)),0)AdvAmt from OPD_AdvanceBill  where compcode='" + compcode + "' and yearcode='" + yearcode + "' and RefVchNo='" + Vchno + "'";
        theCommand.CommandText = "Select isNull(sum(isnull(AdjustAmt,0)),0)AdvAmt from ReqWiseAdvAdjust where compcode='" + compcode + "' and yearcode='" + yearcode + "' and ReqNo='" + ReqNo + "'";
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


    public DataTable GetInvoiceDetails(string compcode, string yearcode, string ReceiptNo, string type = "1")
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (type == "1")
        {
            theCommand.CommandText = "Select B.ReceiptNo,B.MoneyReceiptNo,isNull(B.InvoiceType,'I')InvoiceType,isNull(R.PayableAmt,0)PayableAmt,isNull(DiscountAmt,0) DiscountAmt,isnull(B.BillAmt,0)paid, isNull(R.DueAmt,0)TotAmt,dbo.Fnc_ReqWiseTotBill(B.CompCode,B.RegNo,B.ReqNo) TotPaidAmt,(isNull(R.PayableAmt,0)-dbo.Fnc_ReqWiseTotBill(B.CompCode,B.RegNo,B.ReqNo)) DueAmt,isNull(dbo.Fnc_ReqWisePrevTotBill(B.CompCode,B.RegNo,B.ReqNo,B.PaymentDate),0) PrevBillAmt,case when B.PaymentMode='C' then 'Cash' when  B.PaymentMode='E' then 'e-Wallet' when  B.PaymentMode='R' then 'Card' else 'Other' end as mode,dbo.AmountToWords(B.BillAmt) num2word,case when isnull(B.Bank_CardHolderName,'')='' then '' else 'Card Holder Name ' + B.Bank_CardHolderName end as Bank_CardHolderName,case when isnull(B.Chq_CardNo,'')='' then '' else 'Chq/Card No '+ B.Chq_CardNo end as Chq_CardNo,isNull(B.TransactionId,'') TransactionId,Convert(varchar(10),B.PaymentDate,103) as BillDate,substring(convert(varchar(19), B.PaymentDate, 100), len(convert(varchar(19), B.PaymentDate, 100)) - 6, 7) as BillTime from PatientBillDtls B,PH_PatientReq R where R.compcode=B.compcode and R.Yearcode=B.yearcode and R.RequisitionNo=B.ReqNo and B.compcode='" + compcode + "' and B.yearcode='" + yearcode + "' and B.ReceiptNo='" + ReceiptNo + "' ";
        }
        else
        {
            theCommand.CommandText = "Select B.ReceiptNo,B.MoneyReceiptNo,isNull(B.InvoiceType,'I')InvoiceType,isNull(S.PayableAmt,0)PayableAmt,isNull(DiscountAmt,0) DiscountAmt,isnull(B.BillAmt,0)paid, isNull(S.DueAmt,0)TotAmt,dbo.Fnc_ReqWiseTotBill(B.CompCode,B.RegNo,B.ReqNo) TotPaidAmt,(isNull(S.PayableAmt,0)-dbo.Fnc_ReqWiseTotBill(B.CompCode,B.RegNo,B.ReqNo)) DueAmt,isNull(dbo.Fnc_ReqWisePrevTotBill(B.CompCode,B.RegNo,B.ReqNo,B.PaymentDate),0) PrevBillAmt,case when B.PaymentMode='C' then 'Cash' when  B.PaymentMode='E' then 'e-Wallet' when  B.PaymentMode='R' then 'Card' else 'Other' end as mode,dbo.AmountToWords(B.BillAmt) num2word,case when isnull(B.Bank_CardHolderName,'')='' then '' else 'Card Holder Name ' + B.Bank_CardHolderName end as Bank_CardHolderName,case when isnull(B.Chq_CardNo,'')='' then '' else 'Chq/Card No '+ B.Chq_CardNo end as Chq_CardNo,isNull(B.TransactionId,'') TransactionId,Convert(varchar(10),B.PaymentDate,103) as BillDate,substring(convert(varchar(19), B.PaymentDate, 100), len(convert(varchar(19), B.PaymentDate, 100)) - 6, 7) as BillTime from  PatientBillDtls B,PH_SampleCollect S where S.compcode=B.compcode and S.Yearcode=B.yearcode and S.RequisitionNo=B.ReqNo and B.compcode='" + compcode + "' and B.yearcode='" + yearcode + "' and B.ReceiptNo='" + ReceiptNo + "'";
        }

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


    public string GetExistTestDetailsDept(string compcode, string Testid)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select dbo.Fnc_InvestigationDeptName(G.compcode,G.DepartmentID)deptnm from ph_testmaster T,PH_ProfileMaster G where T.COMPCODE=G.COMPCODE and T.groupcode=G.ProfileCode and T.compcode='" + compcode + "' and T.TestId='" + Testid + "' order by G.DepartmentID,GroupCode";

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
        if (hospitalTable.Rows.Count > 0)
            return hospitalTable.Rows[0][0].ToString().Trim();
        else return "";
    }

    public DataTable GetconsultantDoc(string compcode, string code)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        string lsSql = " select doc_id as consullt,doc_name as docname from gn_doctormaster where compcode='" + compcode + "'";
            
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

    public DataTable Getconsultant(string compcode, string code)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        string lsSql = " select consullt_name1 as consullt,dbo.fn_GetDoctor(compcode,consullt_name1) as docname from PH_TestMaster where compcode='" + compcode + "' and TestId='" + code + "'" +
            "Union select consullt_name2 as consullt,dbo.fn_GetDoctor(compcode,consullt_name2) as docname from PH_TestMaster where compcode='" + compcode + "' and TestId='" + code + "'" +
            "Union select consullt_name3 as consullt,dbo.fn_GetDoctor(compcode,consullt_name3) as docname from PH_TestMaster where compcode='" + compcode + "' and TestId='" + code + "'";
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


    public DataTable DropDownFill(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_DepartmentMaster where Status=1 and compcode='" + compcode + "'";
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
}