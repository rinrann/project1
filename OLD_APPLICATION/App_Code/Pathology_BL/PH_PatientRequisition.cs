using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for PH_PatientRequisition123444
/// </summary>
public class PH_PatientRequisition
{
    public PH_PatientRequisition(string con)
    {
        conString = con;
    }



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;


    public DataTable GenerateRequisitionNo(string compcode, string yearcode)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateRequisitionNo '" + compcode + "','" + yearcode + "'";
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

    public DataTable checkInvoice(string compcode, string yearcode, string regno, string reqno)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select ReceiptNo from PatientBillDtls where compcode='" + compcode + "' and yearcode='" + yearcode + "' and RegNo='" + regno + "' and ReqNo='" + reqno + "'";
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

    public DataTable GenerateReceiptNo(string compcode, string yearcode, string billtype)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateReceiptNo '" + compcode + "','" + yearcode + "','" + billtype + "'";
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

    public string GetBookCode(string compcode, string yearcode, string PaymentMode)
    {
        string bookCode = "";
        DataTable bookTable;
        if (PaymentMode == "C")
        {
            theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='C'";
        }
        else
        {
            theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='B'";
        }


        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (PaymentMode == "C")
        {
            theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='C'";
        }
        else
        {
            theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='B'";
        }
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        bookTable = new DataTable();
        theAdapter.Fill(bookTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        bookCode = bookTable.Rows[0]["bkcode"].ToString();
        return bookCode;
    }

    public string GenerateVoucherNo(string compcode, string yearcode, string bookCode)
    {
        string vchno = "";
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','" + bookCode + "','I','R','Y',''";
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

        return custTable.Rows[0][0].ToString();
    }

    public DataTable DropdownDoc(string compcode)
    {
        DataTable hospitalTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.GN_DoctorMaster where compcode='" + compcode + "' and status=1";
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

    public String GetReqNoRule(string compcode, string yearcode)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select isnull(ReqNoRule,'S')ReqNoRule from parms where compcode='" + compcode + "' and yearcode='" + yearcode + "'";
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


        return custTable.Rows[0]["ReqNoRule"].ToString();
    }
    public DataTable GenerateRegNo(string compcode)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_GenerateRegNO '" + compcode + "'";
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
    public DataTable TestName(string compcode)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_TestMaster where compcode='" + compcode + "'";
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
    public DataTable GetTestResult(string compcode, string ReqNo)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        string lsSql = "select RowId,RequisitionID,TestCode,TestName,result,convert(varchar,TReqDate,103)TReqDate from PH_RequisitionTestMap where compcode='" + compcode + "' and RequisitionID='" + ReqNo + "' order by TestCode"; 
        theCommand.CommandText = lsSql;
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

    public DataTable GridFillforTest(string compcode, string yearcode, string TestDate, string ReqType, string name, string phno)
    {
        DataTable custTable;
        String lsSql = "";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;


        /*theCommand.CommandText = "select LedgerID from Ac_ledger where LedgerType='P' and LedgerFK";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);
        string ledgerid = dt.Rows[0]["LedgerID"].ToString();*/

        lsSql = "select p.*,CONVERT(varchar,p.TestDate,103) tdate,CONVERT(varchar,p.DeliveryDate,103) ddate,AdAmt as adv_amt,isNull(VchNo,'')VchNo  from PH_PatientReq p where  p.Status=1 and compcode='" + compcode + "' and yearcode='" + yearcode + "' and ReqType='" + ReqType + "'";
     
        if (name != "")
        {
            lsSql = lsSql + " And p.PatientName like'" + name + "%'";
        }

        if (phno != "")
        {
            lsSql = lsSql + " And RIGHT(p.Ph1,10) ='" + phno + "'";
        }
        if (TestDate != "")
        {
            lsSql = lsSql + " And convert(Date,P.TestDate,103) ='" + TestDate + "'";
        }

        lsSql = lsSql + " order by p.TestDate desc";

        theCommand.CommandText = lsSql;
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


    public DataTable GridFill(string compcode, string yearcode, string RegNo, string ReqType, string name, string phno, string regdate)
    {
        DataTable custTable;
        String lsSql = "";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;


        /*theCommand.CommandText = "select LedgerID from Ac_ledger where LedgerType='P' and LedgerFK";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        DataTable dt = new DataTable();
        theAdapter.Fill(dt);
        string ledgerid = dt.Rows[0]["LedgerID"].ToString();*/

        lsSql = "select p.*,CONVERT(varchar,p.TestDate,103) tdate,CONVERT(varchar,p.DeliveryDate,103) ddate,AdAmt as adv_amt,isNull(VchNo,'')VchNo  from PH_PatientReq p where  p.Status=1 and compcode='" + compcode + "' and yearcode='" + yearcode + "' and ReqType='" + ReqType + "'";
        if (RegNo != "")
        {
            lsSql = lsSql + " And p.RegistrationNo='" + RegNo + "'";
        }

        if (name != "")
        {
            lsSql = lsSql + " And p.PName like'" + name + "'";
        }

        if (phno != "")
        {
            lsSql = lsSql + " And RIGHT(p.PhNo1,10) ='" + phno + "'";
        }
        if (regdate != "")
        {
            lsSql = lsSql + " And convert(Date,P.TestDate,103) ='" + regdate + "'";
        }

        lsSql = lsSql + " order by p.TestDate desc";

        theCommand.CommandText = lsSql;
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
    public DataTable GridFillFetch(string compcode, string code)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "exec sp_PatientDetails '" + compcode + "'," + code + ",null,null,null";
        theCommand.CommandText = "select * from OPD_PatientRegistration where compcode='" + compcode + "' and PatientRegNo='" + code + "'";
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

    public DataTable GetDocName(string compcode, string reg)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "Select p.DocId underDoctor,d.doc_name from OPD_PatientRegistration p,GN_DoctorMaster d where d.compcode=p.compcode and d.doc_id=p.DocId and p.compcode='" + compcode + "' and p.PatientRegNo='" + reg + "'/* and p.Checked='1'*/";
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

    public DataTable PatientDtls(string compcode, string code)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();


        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pd.* from GN_PatientReg pd where pd.PatientReg='" + code + "' and pd.compcode='" + compcode + "'";
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

    public DataTable GetRequisitionForReport(string compcode, string yearcode, string requisitionno)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select p.RequisitionNo,p.Age+' yrs' Age,p.RegistrationNo,dbo.fn_PROPER_CASE(p.PatientName) PatientName,dbo.fn_PROPER_CASE(case isNull(p.ReferalName,'Self') when 'Self' then 'Self' else dbo.fn_GetDoctor(p.compcode,p.ReferalName) end) ReferalName,dbo.fn_PROPER_CASE(p.Address) Address ,p.Ph1,p.AdAmt ,CONVERT(varchar,p.TestDate,103) TDate,CONVERT(varchar,p.DeliveryDate,103) delDate,dbo.fn_GetGenderName(P.compcode,p.RegistrationNo) as Gender,dbo.fn_GetDoctor(p.compcode,p.ReferalName),isNull(p.DocId,'') UnderDoc,dbo.fn_GetDoctorDetails(p.compcode,p.DocId,'N') UnderDocName,dbo.fn_GetDoctorDetails(p.compcode,p.DocId,'R') DocRegNo,p.ReqType from PH_PatientReq p  where p.Status=1 and p.RequisitionNo='" + requisitionno + "' and p.compcode='" + compcode + "'/* and p.yearcode='" + yearcode + "'*/";
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


    public DataTable GetRequisitionDtls(string compcode, string yearcode, string val, string LedgerId)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_GetRequisitionDtls '" + compcode + "','" + yearcode + "','" + val + "','" + LedgerId + "'";
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

    public DataTable GetMapDtls(string compcode, string val)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select map.Type  from PH_RequisitionTestMap map where map.RequisitionID='" + val + "' and map.compcode='" + compcode + "'";
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
    public DataTable GetTestDtls(string compcode, string val)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select t.TestName,t.Cost from PH_TestMaster t, PH_RequisitionTestMap rm where t.compcode=rm.compcode and rm.TestCode=t.TestId and rm.RequisitionID='" + val + "' and t.compcode='" + compcode + "'";
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

    public DataTable GetRadioDtls(string val)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select name.Name TestName,sub.Charges Cost from dbo.PH_USGNameMaster name,dbo.PH_USGSubGrMaster sub,PH_PatientReq pr,PH_RequisitionTestMap map where pr.RequisitionNo=map.RequisitionID and map.TestCode=name.ID and name.SubGroupCode=sub.SubGrID and pr.RequisitionNo='" + val + "'";
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

    public DataTable getTestGroup(string compcode, string testtype)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        string lsSql = "";
        if (testtype == "C")
        {
            lsSql = "select ProfileName TestName,ProfileCode from PH_ProfileMaster where compcode='" + compcode + "' and isNull(TestType,'')='" + testtype + "'";
        }
        else
        {
            lsSql = "select ProfileName TestName,ProfileCode from PH_ProfileMaster where compcode='" + compcode + "'  and isNull(TestType,'')='" + testtype + "'";
        }
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = lsSql;
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

    public DataTable getTest(string compcode, string grpcode, string testcode = "")
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        string lsSql = "";
        if (testcode == "")
        {
            lsSql = "select TestId,TestName,Cost from PH_TestMaster where compcode='" + compcode + "' and GroupCode='" + grpcode + "'";
        }
        else
        {
            lsSql = "select TestId,TestName,Cost from PH_TestMaster where compcode='" + compcode + "' and GroupCode='" + grpcode + "' and TestId='" + testcode + "'";
        }
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = lsSql;
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

    public DataTable GetTestGroupDtls(string compcode, string val)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pm.ProfileName TestName,pm.Price Cost from dbo.PH_ProfileMaster pm,dbo.PH_RequisitionTestMap rmap  where pm.compcode=map.compcode and pm.ProfileCode=rmap.TestCode and rmap.RequisitionID='" + val + "' and pm.compcode='" + compcode + "'";
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
    public DataTable GetXRayDtls(string compcode, string val)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select xm.TestName,xm.TestCost Cost from dbo.PH_TestXRAYMaster xm,PH_RequisitionTestMap map  where xm.compcode=map.compcode and xm.TestId=map.TestCode and map.RequisitionID='" + val + "' and xm.compcode='" + compcode + "'";
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
    public DataTable GetTotal(string compcode, string val)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select SUM(t.cost) Total from PH_TestMaster t, PH_RequisitionTestMap rm where t.compcode=rm.compcode and rm.TestCode=t.TestId and rm.RequisitionID='" + val + "' and t.compcode='" + compcode + "'";
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

    public DataTable GetRadioTotal(string val)
    {
        DataTable custTable;
        //if (fromdate == "")
        //    fromdate = "null";
        //if (todate == "")
        //    todate = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select SUM(sub.Charges) total from dbo.PH_RequisitionRadioMap map,dbo.PH_USGNameMaster name,dbo.PH_USGSubGrMaster sub where sub.SubGrID=name.SubGroupCode and map.NameCode=name.ID and map.RequisitionID='" + val + "'";
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

    public DataTable GetTestCost(string compcode, string testid)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select t.Cost,TestName from dbo.PH_TestMaster t where t.TestId='" + testid + "' and t.compcode='" + compcode + "'";
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
    public DataTable GetRequisitionNo(string compcode, string yearcode, string RegistrationNo)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select isnull(Max(p.RequisitionNo),'0') RequisitionNo from PH_PatientReq p,GN_PatientReg pr where p.compcode=pr.compcode and p.RegistrationNo=pr.PatientReg and p.TestDate>=pr.AdmissionDate and p.RegistrationNo='" + RegistrationNo + "' and Status=1 and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "'";
        theCommand.CommandText = "select isnull(Max(p.RequisitionNo),'0') RequisitionNo from PH_PatientReq p,OPD_PatientRegistration pr where p.compcode=pr.compcode and p.RegistrationNo=pr.PatientRegNo /*and p.TestDate>=pr.AdmissionDate*/ and p.RegistrationNo='" + RegistrationNo + "' and pr.Status=1 and p.compcode='" + compcode + "' and p.yearcode='" + yearcode + "'";
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

    public bool InsertRequisition(string compcode, string yearcode, string Time, string Age, string due, string regno, string reqno, string name, string referal, string address, string address2, string ph1, string ph2, string date1, string deldate, string adamt, string created, string createdate, string paymentMode, string BankName, string ChequeNo, string chqdt, string docid, string DeptCode, string reqType, string discAmt, string payableAmt, string doctype = "", string ReportingFees = "")
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

            if (ReportingFees == "")
            {
                ReportingFees = "0";
            }
            //if (due == "")
            //{
            //    due = "0";
            //}
            //string fullpay = "0";
            //if (Convert.ToDecimal(due) > 0)
            //{
            //    fullpay = "0";
            //}
            //else
            //{
            //    fullpay = "1";
            //}
            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                // transactional code...
                try
                {
                    string LedgerId = "";
                    theCommand.CommandText = "select LedgerID from AC_Ledger al where LedgerFK='" + regno + "' and ActiveStatus=1 and compcode='" + compcode + "'";
                    theCommand.Transaction = tran as SqlTransaction;
                    SqlDataReader theReader = theCommand.ExecuteReader();
                    theReader.Read();
                    if (theReader.HasRows)
                    {
                        LedgerId = theReader[0].ToString();
                    }
                    theReader.Close();

                    //string bookCode = "";
                    //if (paymentMode == "C")
                    //{
                    //    theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='C'";
                    //}
                    //else
                    //{
                    //    theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='B'";
                    //}
                    //theCommand.Transaction = tran as SqlTransaction;
                    //SqlDataReader theReader2 = theCommand.ExecuteReader();
                    //theReader2.Read();
                    //bookCode = theReader2[0].ToString();
                    //theReader2.Close();


                    //theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','" + bookCode + "','I','R','Y',''";
                    //theCommand.Transaction = tran as SqlTransaction;
                    //SqlDataReader theReader3 = theCommand.ExecuteReader();
                    //theReader3.Read();
                    //string Vchno = theReader3[0].ToString();
                    //theReader3.Close();


                    using (theCommand = theConnection.CreateCommand())
                    {

                        theCommand.CommandText = "INSERT INTO PH_PatientReq(compcode,yearcode,LedgerId,Time,Age,Address2,DueAmt,RegistrationNo,RequisitionNo,PatientName,ReferalName,Address,Ph1,Ph2,TestDate,DeliveryDate,AdAmt,CreatedBy,Status,user01,logdt01,FullPayment,PaymentMode,Chq_CardNo,Bank_CardHolderName,VchNo,DocId,DeptCode,ReqType,doctype,PayableAmt,DiscountAmt,ReportingFees) VALUES('" + compcode + "','" + yearcode + "','" + LedgerId + "',convert(varchar(5),getdate(),108),'" + Age + "','" + address2 + "','" + due + "','" + regno + "','" + reqno + "','" + name + "','" + referal + "','" + address + "','" + ph1 + "','" + ph2 + "','" + date1 + "','" + deldate + "','0','" + created + "',1,'" + created + "',getdate(),'0','" + paymentMode + "','" + ChequeNo + "','" + BankName + "','','" + docid + "','" + DeptCode + "','" + reqType + "','" + doctype.Trim() + "','" + payableAmt + "','" + discAmt + "','" + ReportingFees + "')";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        //if (adamt != "0")
                        //{
                        //    //if (theReader.HasRows == true)
                        //    //{
                        //        theCommand.CommandText = "exec sp_ACCOUNT_GenerateReceiptNo '"+compcode+"'";
                        //        theCommand.Transaction = tran as SqlTransaction;
                        //        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        //        theReader1.Read();
                        //        string receiptno = theReader1[0].ToString();
                        //        theReader1.Close();

                        //        theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,LadgerId,Credit,TrunsactionDate,Reason,ReceiptNo,PaymentType,Status,EntryBy) VALUES('" + compcode + "','" + yearcode + "','" + LedgerId + "','" + adamt + "','" + createdate + "','PATHOLOGY CHARGE','" + receiptno + "',5,1,'" + created + "')";
                        //        theCommand.Transaction = tran as SqlTransaction;
                        //        theCommand.ExecuteNonQuery(); // Execute insert query.  



                        //        theCommand.CommandText = "exec Dsp_CollectionAccEffect '" + compcode + "','" + yearcode + "','" + created + "','" + Vchno + "','" + date1 + "','" + bookCode + "','" + LedgerId + "','" + adamt + "',1";
                        //        theCommand.Transaction = tran as SqlTransaction;
                        //        theCommand.ExecuteNonQuery(); // Execute insert query. 

                        //        theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','" + bookCode + "','U','R','Y',''";
                        //        theCommand.Transaction = tran as SqlTransaction;
                        //        SqlDataReader theReader4 = theCommand.ExecuteReader();
                        //        theReader4.Read();
                        //        theReader4.Close();
                        //    //}
                        //}
                        tran.Commit();
                    }
                }
                catch (Exception ex)
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

    public DataTable GenerateTestReqno(string compcode)
    {
        DataTable custTable;

        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_Generate_Test_RequisitionNo '" + compcode + "'";
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

    public bool InsertRequisitionTestMap(string compcode, string Type, string RequisitionID, string TestCode, string TestReqNo, string TReqDate, string TDeliveryDate, string TReqTime, string Remarks, string user, string consultant, string refdoc, string testcost, string testname)
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
            theCommand.CommandText = "INSERT INTO PH_RequisitionTestMap(compcode,Type,RequisitionID,TestCode,TestReqNo,TReqDate,TReqTime,TDeliveryDate,Remarks,user01,logdt01,consultant,ReferDoc,cost,testname) VALUES('" + compcode + "','" + Type + "','" + RequisitionID + "','" + TestCode + "','" + TestReqNo + "','" + TReqDate + "','" + TReqTime + "','" + TDeliveryDate + "','" + Remarks + "','" + user + "',getdate(),'" + consultant + "','" + refdoc + "','" + testcost + "','" + testname + "')";
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

    public bool UpdateChargeDetails(string compcode, string yearcode, string regno, string type, string date, string amount)
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

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "select LedgerID from AC_Ledger l where l.LedgerFK='" + regno + "' and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader_Ledger = theCommand.ExecuteReader();
                        theReader_Ledger.Read();

                        string LedgerId = theReader_Ledger[0].ToString().Trim();
                        theReader_Ledger.Close();
                        theCommand.CommandText = "select * from OPD_ChargeDetails cd where cd.PatientReg='" + regno + "' and  cd.IssueDate='" + date + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader5 = theCommand.ExecuteReader();
                        theReader5.Read();

                        if (theReader5.HasRows == false)
                        {

                            theReader5.Close();
                            theCommand.CommandText = "insert into OPD_ChargeDetails (compcode,yearcode,LedgerId,RegnFees,DoctorFees,Status,PatientReg,IssueDate) values ('" + compcode + "','" + yearcode + "','" + LedgerId + "','0','0',1,'" + regno + "',CONVERT(varchar,'" + date + "',103))";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            theReader5.Close();
                        }

                        if (type == "U")
                        {
                            lsSql = "update OPD_ChargeDetails set USGCharge='" + amount + "' where compcode='" + compcode + "'  and yearcode='" + yearcode + "'  and IssueDate='" + date + "'";
                        }
                        else if (type == "X")
                        {
                            lsSql = "update OPD_ChargeDetails set InvestigationCharge='" + amount + "' where compcode='" + compcode + "'  and yearcode='" + yearcode + "' and IssueDate='" + date + "'";
                        }
                        else if (type == "P")
                        {
                            lsSql = "update OPD_ChargeDetails set InvestigationCharge='" + amount + "' where compcode='" + compcode + "'  and yearcode='" + yearcode + "' and IssueDate='" + date + "'";
                        }
                        else
                        {
                            lsSql = "update OPD_ChargeDetails set InvestigationCharge='" + amount + "' where compcode='" + compcode + "'  and yearcode='" + yearcode + "' and IssueDate='" + date + "'";
                        }
                        theCommand.CommandText = lsSql;
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
                catch (Exception ex)
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


    public bool UpdateRequisition(string compcode, string yearcode, string regno, string Age, string reqno, string name, string referal, string address, string Address2, string ph1, string ph2, string date1, string deldate, string adamt, string createdate, string user, string paymentMode, string BankName, string ChequeNo, string chqdt, string vchno, string reqCancel, string cancelReason, string due, string docid, string DeptCode, string time, string discAmt, string payableAmt, string doctype = "", string ReportingFees = "0")
    {
        try
        {
            if (ReportingFees == "")
            {
                ReportingFees = "0";
            }
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

            theCommand.CommandText = "update PH_PatientReq set Age='" + Age + "', PatientName='" + name + "',Address2='" + Address2 + "', ReferalName='" + referal + "', Address='" + address + "', Ph1='" + ph1 + "', Ph2='" + ph2 + "', TestDate='" + date1 + "', DeliveryDate='" + deldate + "', user02='" + user + "',logdt02=getdate(),PaymentMode='" + paymentMode + "',Chq_CardNo='" + ChequeNo + "',Bank_CardHolderName='" + BankName + "',CancelFlag='" + reqCancel + "',CancelReason='" + cancelReason + "',DueAmt='" + due + "',DocId='" + docid + "',DeptCode='" + DeptCode + "',Time='" + time + "',doctype='" + doctype + "',PayableAmt='" + payableAmt + "',DiscountAmt='" + discAmt + "',ReportingFees='" + ReportingFees + "' where RequisitionNo = '" + reqno + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.



            //   sql = "select LedgerID from AC_Ledger al where LedgerFK='" + regno + "' and ActiveStatus=1";
            if (Convert.ToDecimal(adamt) > 0)
            {
                string sql;
                string ledgerid;
                SqlDataAdapter da;
                DataTable dt = new DataTable();
                sql = "select LedgerID from AC_Ledger al where LedgerFK='" + regno + "' and ActiveStatus=1 and compcode='" + compcode + "'";
                da = new SqlDataAdapter(sql, theConnection);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ledgerid = dt.Rows[0][0].ToString();
                }
                else
                {
                    sql = "exec sp_GenerateLedgerId '" + compcode + "','P'";
                    da = new SqlDataAdapter(sql, theConnection);
                    da.Fill(dt);
                    ledgerid = dt.Rows[0][0].ToString();
                }

                //theCommand.CommandText = "exec sp_ACCOUNT_GenerateReceiptNo '" + compcode + "'";
                ////theCommand.Transaction = tran as SqlTransaction;
                //SqlDataReader theReader1 = theCommand.ExecuteReader();
                //theReader1.Read();
                //string receiptno = theReader1[0].ToString();
                //theReader1.Close();
                //theCommand.CommandText = "INSERT INTO AC_Transaction(compcode,yearcode,LadgerId,Credit,Debit,TrunsactionDate,Reason,ReceiptNo,PaymentType,Status) VALUES('" + compcode + "','" + yearcode + "','" + ledgerid + "','" + adamt + "',0.00,'" + createdate + "','PATHOLOGY CHARGE','" + receiptno + "',1,1)";
                //theCommand.CommandType = CommandType.Text;
                //theCommand.ExecuteNonQuery(); // Execute insert query.


                //string bookCode = "";
                //if (paymentMode == "C")
                //{
                //    theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='C'";
                //}
                //else
                //{
                //    theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='B'";
                //}
                ////theCommand.Transaction = tran as SqlTransaction;
                //SqlDataReader theReader2 = theCommand.ExecuteReader();
                //theReader2.Read();
                //bookCode = theReader2[0].ToString();
                //theReader2.Close();



                //theCommand.CommandText = "exec Dsp_CollectionAccEffect '" + compcode + "','" + yearcode + "','" + user + "','" + vchno + "','" + date1 + "','" + bookCode + "','" + ledgerid + "','" + adamt + "',1";
                //theCommand.CommandType = CommandType.Text;
                //theCommand.ExecuteNonQuery(); // Execute insert query. 
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

    public bool DeleteRequisition(string compcode, string yearcode, string id)
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
            theCommand.CommandText = "delete from PH_RequisitionTestMap  where RequisitionID='" + id + "' and compcode='" + compcode + "'";
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


    public bool DeleteReq(string compcode, string yearcode, string id, string vchno = "")
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
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "delete from PH_RequisitionTestMap  where RequisitionID='" + id + "' and compcode='" + compcode + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.


            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "delete from PH_PatientReq  where RequisitionNo='" + id + "' and compcode='" + compcode + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.


            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "delete from inp  where and compcode='" + compcode + "' and yearcode='" + yearcode + "' and vchno='" + vchno + "'";
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
    public bool DeleteReg(string compcode, string id)
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
            theCommand.CommandText = "delete from GN_PatientReg WHERE PatientReg='" + id + "' and compcode='" + compcode + "'";
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

    public DataTable GridFillDue(string compcode, string yearcode, string type, string pname)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        string lsSql = "";
        if (type == "1")
        {
            lsSql = "select p.*,case p.ReferalName when 'self' then 'Self' else dbo.fn_GetDoctorDetails(p.compcode,p.ReferalName,'N') end as RefDocName,CONVERT(varchar,p.TestDate,103) tdate,Convert(varchar(10),CONVERT(date,p.TestDate,103)) billdate,CONVERT(varchar,p.DeliveryDate,103) ddate,AdAmt as adv_amt,isNull(VchNo,'')VchNo,0 total  from PH_PatientReq p where  p.Status=1 and compcode='" + compcode + "' /*and yearcode='" + yearcode + "'*/ and isNull(DueAmt,0)>0  ";
            if (pname != "")
            {
                lsSql = lsSql + " and p.PatientName like '%" + pname + "%'";
            }
            lsSql = lsSql + " order by p.TestDate";
        }
        else
        {
            lsSql = "Select S.VchNo,S.RegistrationNo,S.RequisitionNo,CONVERT(varchar,S.CollectDate,103) tdate,R.PName PatientName,dbo.fn_GetDoctor(R.compcode,R.DocId) RefDocName,sum(S.PatientBillAmt)total,isnull(sum(AdAmt),0)AdAmt, isnull(sum(DueAmt),0)DueAmt from PH_SampleCollect S,OPD_PatientRegistration R where S.CompCode=R.COMPCODE and S.YearCode=R.YEARCODE and S.RegistrationNo=R.PatientRegNo /*and S.compcode='" + compcode + "'*/ and S.yearcode='" + yearcode + "' and isnull(S.fullpayment,0) = 0 group by S.RequisitionNo,S.RegistrationNo,R.PName ,dbo.fn_GetDoctor(R.compcode,R.DocId),S.VchNo";
        }

        theCommand.CommandText = lsSql;
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

    public DataTable GridtestDtls(string compcode, string yearcode, string Reqno)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select SampleCode TestId,dbo.Fnc_SampleName(compcode,SampleCode) + ' (' + Unit +' Unit)' TestName,PatientBillAmt Cost from PH_SampleCollect where compcode='" + compcode + "' and yearcode='" + yearcode + "' and RequisitionNo='" + Reqno + "'";
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

    public string gettotalbill(string compcode, string yearcode, string srl)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select PatientBillAmt  from PH_SampleCollect where compcode='" + compcode + "' and yearcode='" + yearcode + "' and SrlNo=" + srl + "";
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


        return custTable.Rows[0][0].ToString();
    }




    public bool UpdateRequisitionBill(string compcode, string yearcode, string RegNo, string ReqNo, string totpaidAmt, string DueAmt, string PaymentMode, string BankName, string ChequeNo, string transactionId, string VchDate, string User, string type, string payableAmt, string PayType, string ReqType, string billdate, string userId, string invoiceType, string invoiceNo, string Vchno, string bookCode, string discount, string updInvNo = "0")
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

            if (DueAmt == "")
            {
                DueAmt = "0";
            }
            string fullpay = "1";
            if (Convert.ToDecimal(DueAmt) > 0)
            {
                fullpay = "0";
            }
            else
            {
                fullpay = "1";
            }
            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                // transactional code...
                try
                {
                    string LedgerId = "";
                    string billtype = "";
                    theCommand.CommandText = "select LedgerID from AC_Ledger al where LedgerFK='" + RegNo + "' and ActiveStatus=1 and compcode='" + compcode + "'";
                    theCommand.Transaction = tran as SqlTransaction;
                    SqlDataReader theReader = theCommand.ExecuteReader();
                    theReader.Read();
                    LedgerId = theReader[0].ToString();
                    theReader.Close();

                    //string bookCode = "";
                    //if (PaymentMode == "C")
                    //{
                    //    theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='C'";
                    //}
                    //else
                    //{
                    //    theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='B'";
                    //}
                    //theCommand.Transaction = tran as SqlTransaction;
                    //SqlDataReader theReader2 = theCommand.ExecuteReader();
                    //theReader2.Read();
                    //bookCode = theReader2[0].ToString();
                    //theReader2.Close();

                    //theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','" + bookCode + "','I','R','Y',''";
                    //theCommand.Transaction = tran as SqlTransaction;
                    //SqlDataReader theReader3 = theCommand.ExecuteReader();
                    //theReader3.Read();
                    //string Vchno = theReader3[0].ToString();
                    //theReader3.Close();

                    int lastinvno = Convert.ToInt32(invoiceNo.Substring(invoiceNo.Length - 4));
                    //int lastrcptno = Convert.ToInt32(ReceptNo.Substring(ReceptNo.Length - 4));

                    if (type == "1")
                    {
                        billtype = "R";
                    }
                    else
                    {
                        billtype = "S";
                    }
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand = new SqlCommand();
                        theCommand.Connection = theConnection;
                        if (type == "1")
                            theCommand.CommandText = "update PH_PatientReq set AdAmt='" + totpaidAmt + "',DueAmt='" + DueAmt + "',FullPayment='" + fullpay + "',PaymentMode='" + PaymentMode + "',Chq_CardNo='" + ChequeNo + "',Bank_CardHolderName='" + BankName + "',TransactionId='" + transactionId + "',Cashier='" + User + "',PaymentDate=GETDATE(),DiscountAmt='" + discount + "' where RequisitionNo = '" + ReqNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        else
                            theCommand.CommandText = "update PH_SampleCollect set AdAmt=PatientBillAmt,DueAmt=0,FullPayment='" + fullpay + "',PaymentMode='" + PaymentMode + "',Chq_CardNo='" + ChequeNo + "',Bank_CardHolderName='" + BankName + "',TransactionId='" + transactionId + "',Cashier='" + User + "',PaymentDate=GETDATE() where RequisitionNo = '" + ReqNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "Insert into PatientBillDtls (CompCode,YearCode,VchNo,RegNo,ReqNo,BillType,BillAmt,BillDate,CreatedBy,PaymentMode,Chq_CardNo,Bank_CardHolderName,TransactionId,Cashier,PaymentDate,PayType,ReceiptNo,InvoiceType,MoneyReceiptNo) values('" + compcode + "','" + yearcode + "','" + Vchno + "','" + RegNo + "','" + ReqNo + "','" + billtype + "','" + payableAmt + "','" + billdate + "','" + User + "','" + PaymentMode + "','" + ChequeNo + "','" + BankName + "','" + transactionId + "','" + User + "',GETDATE(),'" + PayType + "','" + invoiceNo + "','" + invoiceType + "','" + invoiceNo + "')";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update OPD_BillTypeMaster set LastSerial=" + lastinvno + " where compcode='" + compcode + "' and yearcode='" + yearcode + "' and BillTypeId='" + ReqType + "' and Bill_Month=CONVERT(varchar(2),GETDATE(),101) ";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.


                        //theCommand.CommandText = "update OPD_BillTypeMaster set LastSerial=" + lastrcptno + " where compcode='" + compcode + "' and yearcode='" + yearcode + "' and BillTypeId='RCP' and Bill_Month=CONVERT(varchar(2),GETDATE(),101) ";
                        //theCommand.Transaction = tran as SqlTransaction;
                        //theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "exec Dsp_CollectionAccEffect '" + compcode + "','" + yearcode + "','" + userId + "','" + Vchno + "','" + VchDate + "','" + bookCode + "','" + LedgerId + "','" + payableAmt + "',1,'" + ReqNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','" + bookCode + "','U','R','Y',''";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader4 = theCommand.ExecuteReader();
                        theReader4.Read();
                        theReader4.Close();
                    }
                    tran.Commit();
                }
                catch (Exception ex)
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


    public bool UpdateAdvanceBill(string compcode, string yearcode, string Vchno, string RegNo, string receiptNo, double adjAmt, int status, string reqno)
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
            theCommand.CommandText = "Update OPD_AdvanceBill set Status=" + status + ",AdjustedAmt=isNull(AdjustedAmt,0)+" + adjAmt + ",adjustdate=getdate() where compcode='" + compcode + "' /*and yearcode='" + yearcode + "'*/ and RegNo='" + RegNo + "' and ReceiptNo='" + receiptNo + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.

            theCommand.CommandText = "Insert into ReqWiseAdvAdjust(CompCode,YearCode,RegNo,ReqNo,ReceiptNo,RefVchno,AdjustAmt,adjustdate) values('" + compcode + "','" + yearcode + "','" + RegNo + "','" + reqno + "','" + receiptNo + "','" + Vchno + "'," + adjAmt + ",getdate())";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();
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
    public DataTable GridFillBillDetls(string compcode, string yearcode, string regno, string name, string phno, string regdate)
    {
        DataTable custTable;
        string lsSql = "";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.

        lsSql = "select P.VchNo,P.RegNo,P.ReceiptNo,P.ReqNo,P.BillType,Case P.BillType when 'R' then 'Requisition' else 'Sample' end BillTypeName,P.BillAmt,Convert(varchar,P.BillDate,103)BillDate,R.PName as PatientName,P.CancelRequest Cancel,P.CancelRemarks,isNull(P.CancelRequestStatus,0) ReqSts,case isNull(P.CancelRequestStatus,0) when 0 then '' when 1 then 'Approved' when 2 then 'Reject' else 'Pending' end ReqStatus,isNull(RefundStatus,0) RefStatus,case isNull(RefundStatus,0) when 0 then '' else 'Done' end RefundStatus from PatientBillDtls P,OPD_PatientRegistration R  where R.CompCode=P.CompCode and R.PatientRegNo=P.RegNo and P.compcode='" + compcode + "' and P.yearcode='" + yearcode + "'";
        if (regno != "")
        {
            lsSql = lsSql + " And p.RegNo='" + regno + "'";
        }

        if (name != "")
        {
            lsSql = lsSql + " And R.PName like '%" + name + "%'";
        }

        if (phno != "")
        {
            lsSql = lsSql + " And RIGHT(R.PhNo1,10) ='" + phno + "'";
        }
        if (regdate != "")
        {
            lsSql = lsSql + " And convert(Date,P.BillDate,103) ='" + regdate + "'";
        }
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = lsSql;
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

    public bool CancelBillDetails(string compcode, string yearcode, string VchNo, string RegNo, string ReqNo, string Reason, string BillAmt, string User, string type)
    {
        try
        {
            Decimal amt;
            if (BillAmt == "")
            {
                BillAmt = "0";
            }
            amt = Convert.ToDecimal(BillAmt);
            // Connection.
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                // transactional code...
                try
                {

                    //string bookCode = "";
                    //if (PaymentMode == "C")
                    //{
                    //    theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='C'";
                    //}
                    //else
                    //{
                    //    theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='B'";
                    //}
                    //theCommand.Transaction = tran as SqlTransaction;
                    //SqlDataReader theReader2 = theCommand.ExecuteReader();
                    //theReader2.Read();
                    //bookCode = theReader2[0].ToString();
                    //theReader2.Close();

                    //theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','" + bookCode + "','I','R','Y',''";
                    //theCommand.Transaction = tran as SqlTransaction;
                    //SqlDataReader theReader3 = theCommand.ExecuteReader();
                    //theReader3.Read();
                    //string Vchno = theReader3[0].ToString();
                    //theReader3.Close();


                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand = new SqlCommand();
                        theCommand.Connection = theConnection;
                        //if (type == "R")
                        //    theCommand.CommandText = "update PH_PatientReq set AdAmt=AdAmt-" + amt + ",DueAmt=DueAmt+" + amt + ",FullPayment=0 where RequisitionNo = '" + ReqNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        //else
                        //    theCommand.CommandText = "update PH_SampleCollect set AdAmt=AdAmt-" + amt + ",DueAmt=DueAmt+" + amt + ",FullPayment=0 where RequisitionNo = '" + ReqNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        //theCommand.Transaction = tran as SqlTransaction;
                        //theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update PatientBillDtls set CancelRequest='1',CancelRemarks='" + Reason + "',CancelRequestBy='" + User + "',CancelRequestStatus=3 where compcode='" + compcode + "' and yearcode='" + yearcode + "' and Vchno='" + VchNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.


                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();

        }
    }

    public bool ApproveBillDetails(string compcode, string yearcode, string VchNo, string Status)
    {
        try
        {
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand = new SqlCommand();
                        theCommand.Connection = theConnection;
                        theCommand.CommandText = "update PatientBillDtls set CancelRequestStatus=" + Status + " where compcode='" + compcode + "' and yearcode='" + yearcode + "' and Vchno='" + VchNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();

        }
    }


    public bool UpdateBillDetails(string compcode, string yearcode, string VchNo, string paymode)
    {
        try
        {
            theConnection = new SqlConnection();
            if (conString != "")
            {
                theConnection.ConnectionString = conString;
                theConnection.Open();
            }
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand = new SqlCommand();
                        theCommand.Connection = theConnection;
                        theCommand.CommandText = "update PatientBillDtls set PaymentMode='" + paymode + "' where compcode='" + compcode + "' and yearcode='" + yearcode + "' and Vchno='" + VchNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();

        }
    }
    public bool RefundRequisitionBill(string compcode, string yearcode, string RegNo, string ReqNo, string VchNo, string PaymentMode, string CardHolderName, string CardNo, string TransactionId, string BillAmt, string cancelDate, string User)
    {
        Decimal amt;
        if (BillAmt == "")
        {
            BillAmt = "0";
        }
        amt = Convert.ToDecimal(BillAmt);
        // Connection.
        theConnection = new SqlConnection();
        if (conString != "")
        {
            theConnection.ConnectionString = conString;
            theConnection.Open();
        }
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        try
        {
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                // transactional code...
                try
                {


                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand = new SqlCommand();
                        theCommand.Connection = theConnection;
                        //if (type == "R")
                        //    theCommand.CommandText = "update PH_PatientReq set AdAmt=AdAmt-" + amt + ",DueAmt=DueAmt+" + amt + ",FullPayment=0 where RequisitionNo = '" + ReqNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        //else
                        //theCommand.CommandText = "update PH_SampleCollect set AdAmt=AdAmt-" + amt + ",DueAmt=DueAmt+" + amt + ",FullPayment=0 where RequisitionNo = '" + ReqNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        theCommand.CommandText = "update PH_PatientReq set AdAmt=AdAmt-" + amt + ",DueAmt=DueAmt+" + amt + ",FullPayment=0 where RequisitionNo = '" + ReqNo + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update PatientBillDtls set Cancel='Y',CancelBy='" + User + "',RefundMode='" + PaymentMode + "',RefundStatus=1,RefundCardNo='" + CardNo + "',RefundTransactionId='" + TransactionId + "',RefundCardHolderName='" + CardHolderName + "',RefundDate=getdate() where compcode='" + compcode + "' and yearcode='" + yearcode + "' and Vchno='" + VchNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.


                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();

        }

    }

    public DataTable GridFillCanceledBillDetls(string compcode, string yearcode, string regno, string pname, string paydate)
    {
        DataTable custTable;
        string lssql = "";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        lssql = "select P.VchNo,P.RegNo,P.ReqNo,P.ReceiptNo,P.BillType,Case P.BillType when 'R' then 'Requisition' else 'Sample' end BillTypeName,P.BillAmt,Convert(varchar,P.PaymentDate,103)BillDate,R.PName as PatientName,P.CancelRequest Cancel,P.CancelRemarks,isNull(P.CancelRequestStatus,0) ReqSts,case isNull(P.CancelRequestStatus,0) when 0 then '' when 1 then 'Approved' when 2 then 'Reject' else 'Pending' end ReqStatus,case isNull(RefundStatus,0) when 0 then '' else 'Done' end RefundStatus,isnull(P.cancel,'N')cncl from PatientBillDtls P,OPD_PatientRegistration R  where R.CompCode=P.CompCode and R.PatientRegNo=P.RegNo and P.compcode='" + compcode + "' and P.yearcode='" + yearcode + "' and isnull(CancelRequest,0)=1";
        if (regno != "")
        {
            lssql = lssql + " And P.RegNo='" + regno + "'";
        }
        if (pname != "")
        {
            lssql = lssql + " And R.PName like '" + pname + "%'";
        }
        if (paydate != "")
        {
            lssql = lssql + " And convert(date,P.PaymentDate,103)='" + paydate + "'";
        }
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = lssql;
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

    public DataTable GridFillBillDetls(string compcode, string yearcode, string regno, string pname, string paydate)
    {
        DataTable custTable;
        string lssql = "";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        lssql = "select P.VchNo,P.ReceiptNo,P.RegNo,P.ReqNo,P.BillType,Case P.BillType when 'R' then 'Requisition' else 'Sample' end BillTypeName,P.BillAmt,Convert(varchar,P.PaymentDate,103)BillDate,R.PName as PatientName,P.PaymentMode from PatientBillDtls P,OPD_PatientRegistration R  where R.CompCode=P.CompCode and R.PatientRegNo=P.RegNo and P.compcode='" + compcode + "' and P.yearcode='" + yearcode + "' and isnull(Cancel,'N')='N'";
        if (regno != "")
        {
            lssql = lssql + " And P.RegNo='" + regno + "'";
        }
        if (pname != "")
        {
            lssql = lssql + " And R.PName like '" + pname + "%'";
        }
        if (paydate != "")
        {
            lssql = lssql + " And convert(date,P.PaymentDate,103)='" + paydate + "'";
        }
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = lssql;
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

    public string userStatus(string compcode, string username)
    {
        DataTable custTable;
        string status = "0";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select isNull(AdminUser,0) from GN_UserDetails where compcode='" + compcode + "' and userid='" + username + "'";
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
        if (custTable.Rows.Count > 0)
        {
            status = custTable.Rows[0][0].ToString();
        }
        else
        {
            status = "0";
        }
        return status;
    }

    public bool UpdateRequisitionTestMap(string compcode, string ReqNo, string testreqno, string testid, string remarks, string docid)
    {

        // Connection.
        theConnection = new SqlConnection();
        if (conString != "")
        {
            theConnection.ConnectionString = conString;
            theConnection.Open();
        }
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        try
        {
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                // transactional code...
                try
                {


                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand = new SqlCommand();
                        theCommand.Connection = theConnection;
                        theCommand.CommandText = "update PH_RequisitionTestMap set Remarks='" + remarks + "',consultant='" + docid + "' where compcode='" + compcode + "' and RequisitionID = '" + ReqNo + "' and TestCode='" + testid + "' and TestReqNo='" + testreqno + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.



                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();

        }
    }

    public DataTable getPrescriptionDtls(string compcode, string yearcode, string prepid)
    {
        DataTable custTable;
        DataSet ds;
        ds = new DataSet();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select PrescriptionId,RowID,GroupID,MedicineId,Dose,Duration from OPD_PrescriptionMapping where compcode='" + compcode + "' and yearcode='" + yearcode + "' and PrescriptionId='" + prepid + "'";
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
    public DataTable getInjectionDtls(string compcode, string yearcode, string docno, string PatientRegNo)
    {
        DataTable custTable;
        DataSet ds;
        ds = new DataSet();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "Select Docno,Docdate,PatientRegNo,Header,NoofDays,convert(varchar,Dates,103)Dates,MedicineId,Sig,Adv,Footer,AdvtobeFollowed from patientinjection where compcode='" + compcode + "' and yearcode='" + yearcode + "' and Docno='" + docno + "' and PatientRegNo='" + PatientRegNo + "'";
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


    public DataTable getInjectionDocno(string compcode, string yearcode)
    {
        DataTable custTable;
        DataSet ds;
        ds = new DataSet();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select isnull(max(right(Docno,9)),0)+1 from patientinjection where compcode='" + compcode + "' and yearcode='" + yearcode + "'";
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

    public void InsertInjection_1(string compcode, string yearcode, string Docdt, string Docno, string PatientRegNo, string Header, string NoofDays, string Dates, string MedicineId, string Sig, string Adv, string Footer, string AdvtobeFollowed, string username)
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

            theCommand.CommandText = "Insert into PatientInjection(Compcode,Yearcode,Docno,Docdate,PatientRegNo,Header,NoofDays,Dates,MedicineId,Sig,Adv,Footer,AdvtobeFollowed,User01,Logdt01) " +
                " values('" + compcode + "','" + yearcode + "','" + Docno + "','" + Docdt + "','" + PatientRegNo + "','" + Header + "','" + NoofDays + "','" + Dates + "'," + MedicineId + ",'" + Sig + "','" + Adv + "','" + Footer + "','" + AdvtobeFollowed + "','" + username + "',getdate())";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();
        }
        catch
        {

        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();

        }

    }
    public void InsertInjection_2(string compcode, string yearcode, string Docno, string PatientRegNo, string Items, int Checked, string Remarks, string Qty, string username)
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

            theCommand.CommandText = "Insert into itemstobringforinjection(Compcode,Yearcode,Docno,PatientRegNo,Items,Checked,Remarks,Qty,User01,Logdt01) " +
                " values('" + compcode + "','" + yearcode + "','" + Docno + "','" + PatientRegNo + "','" + Items + "'," + Checked + ",'" + Remarks + "','" + Qty + "','" + username + "',getdate())";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();
        }
        catch
        {

        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();

        }

    }

    public DataTable GetAllDoc(string compcode, string yearcode)
    {
        DataTable custTable;
        DataSet ds;
        ds = new DataSet();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select distinct Docno,PatientRegNo,convert(varchar,docdate,103) docdate,convert(varchar(10),docdate,120) docdt  from patientinjection where compcode='" + compcode + "' and yearcode='" + yearcode + "' order by docno";
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

    public void UpdateResult(string compcode, string RequisitionID, string RowId, string TestCode, string result)
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

            theCommand.CommandText = "Update PH_RequisitionTestMap set result='" + result + "' where compcode='" + compcode + "' and RequisitionID='" + RequisitionID + "'and RowId='" + RowId + "' and TestCode='" + TestCode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();
        }
        catch
        {

        }
        finally
        {
            theConnection.Dispose();
            theCommand.Dispose();
        }

    }

    public DataSet GetTestReportsResult(string compcode,string yearcode, string reqid)
    {
        DataSet ds = new DataSet();
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec Dsp_PHTestResult @a_Company='" + compcode + "',@a_YearCode='" + yearcode + "',@a_ReqNo='" + reqid + "'";
        theCommand.CommandType = CommandType.Text;
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;
        theAdapter.Fill(ds); // Fill data into data table.
        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        return ds;
    }
}