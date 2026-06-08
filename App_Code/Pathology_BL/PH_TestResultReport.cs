using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_TestResultReport12345
/// </summary>
public class PH_TestResultReport
{
	public PH_TestResultReport(string con)
	{
        conString = con;
	}



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetTestmaster(string compcode, string yearcode, string CustID, string code)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select tm.TestId,tm.TestName,tm.TestType,tm.NormalRange FROM  dbo.PH_DepartmentMaster d,PH_TestMaster tm,PH_PatientReq pr,PH_RequisitionTestMap map "+
            "where d.compcode=tm.compcode and d.compcode=pr.compcode and d.compcode=map.compcode and d.DeptCode=tm.DepertmentID and tm.TestId=map.TestCode and "+
            "map.RequisitionID=pr.RequisitionNo and tm.TestId='" + code + "' and d.compcode='"+compcode+"' and pr.yearcode='"+yearcode+"' and pr.RegistrationNo='" + CustID + "'  "+
            "union select tm.TestId,tm.TestName,tm.TestType,tm.NormalRange from PH_DepartmentMaster d,PH_TestMaster tm,PH_ProfileMapping pmap,PH_RequisitionTestMap map,PH_PatientReq pr "+
            "where d.compcode=tm.compcode and d.compcode=pr.compcode and d.compcode=map.compcode and d.compcode=pmap.compcode and d.DeptCode=tm.DepertmentID and tm.TestId=pmap.TestId and " +
            "pmap.ProfileCode=map.TestCode and map.RequisitionID=pr.RequisitionNo and d.compcode='" + compcode + "' and pr.yearcode='" + yearcode + "' and pr.RegistrationNo='" + CustID + "'";
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

    public DataTable GetTestmaster1(string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select tm.TestId,tm.TestName,tm.TestType,tm.NormalRange FROM  dbo.PH_DepartmentMaster d,PH_TestMaster tm,PH_PatientReq pr,PH_RequisitionTestMap map  where d.DeptCode=tm.DepertmentID and tm.TestId=map.TestCode and map.RequisitionID=pr.RequisitionNo and  pr.RegistrationNo='" + CustID + "'  union    select tm.TestId,tm.TestName,tm.TestType,tm.NormalRange from PH_DepartmentMaster d,PH_TestMaster tm,PH_ProfileMapping pmap,PH_RequisitionTestMap map,PH_PatientReq pr where  d.DeptCode=tm.DepertmentID and tm.TestId=pmap.TestId and pmap.ProfileCode=map.TestCode and map.RequisitionID=pr.RequisitionNo and pr.RegistrationNo='" + CustID + "'";
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

    public DataTable DropDownfill(string compcode, string yearcode, string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_RequisitionSlip  '"+compcode+"','"+yearcode+"'," + CustID + "";
        //"+CustID+"";
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

    public DataTable GetTestMap(string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_RequisitionTestMap m where m.RequisitionID='" + CustID + "'";
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

    public DataTable Getxrayreport(string compcode,string yearcode,string CustID, string code)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select * from dbo.PH_TestXRAYResult x,dbo.PH_TestXRAYMaster xm,PH_DepartmentMaster d where xm.DepartmentId=d.DeptCode and x.TestId=xm.TestId and x.TestId='" + code + "'  and x.RegNo='" + CustID + "'";
        theCommand.CommandText = "select * from dbo.PH_USGTestResult x,dbo.PH_USGHeaderResult h,dbo.PH_TestXRAYMaster xm,PH_DepartmentMaster d "+
            "where x.compcode=h.compcode and x.compcode=xm.compcode and x.compcode=d.compcode and x.yearcode=h.yearcode and xm.DepartmentId=d.DeptCode "+
            "and x.USGNameId=xm.TestId and x.USGNameId='" + code + "'  and x.RegNo='" + CustID + "' and x.compcode='"+compcode+"' and x.yearcode='"+yearcode+"'";
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

    public DataTable GetUsgResult(string compcode, string yearcode, string CustID, string code)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,head.HeaderContent hc from PH_USGTestResult ur,dbo.PH_USGNameMaster name,PH_USGHeaderResult head,PH_USGHeaderMaster h "+
            "where ur.compcode=name.compcode and ur.compcode=head.compcode and ur.compcode=h.compcode and ur.yearcode=head.yearcode and h.ID=head.HeaderId and "+
            "ur.USGNameId=name.id and head.RowId=ur.RowId and ur.USGNameId='" + code + "' and ur.RegNo='" + CustID + "' and ur.compcode='" + compcode + "' and ur.yearcode='" + yearcode + "'";
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

    public DataTable GetUGCparameter(string compcode, string yearcode, string CustID, string code)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_USGTestResult ur,PH_USGParameterResult pr,PH_USGSubGrMapping sub "+
            "where ur.compcode=pr.compcode and ur.compcode=sub.compcode and ur.yearcode=pr.yearcode and ur.compcode='"+compcode+"' and ur.yearcode='"+yearcode+"' and sub.Id=pr.ParameterId and ur.RowId=pr.RowId and ur.RegNo='" + CustID + "' and ur.USGNameId='" + code + "'";
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

    public DataTable GetHeaderDetails(string RequisitionNo)
    {
        DataTable custTable;
        if (RequisitionNo == "")
            RequisitionNo = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,rm.headercontent hc from PH_USGHeaderMaster head,dbo.PH_RadiologyMaster rm,GN_DoctorMaster d where head.ID=rm.Headerid and d.doc_id=rm.pathoid and rm.ReqId='" + RequisitionNo + "'";
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


    public DataTable GetRadiologyNameDetails(string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_RequisitionRadioMap map,PH_PatientReq pr,dbo.PH_USGNameMaster name where map.RequisitionID=pr.RequisitionNo and name.ID=map.NameCode and pr.RegistrationNo='" + CustID + "'";
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

    public DataTable GetParameterDetails(string RequisitionNo)
    {
        DataTable custTable;
        if (RequisitionNo == "")
            RequisitionNo = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.PH_USGSubGrMapping map,dbo.PH_RadioParameter rp where map.Id=rp.ParameterId and rp.ReqId='" + RequisitionNo + "'";
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


    public DataTable GetDoctorDetailsXray(string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_TestXRAYResult tr,dbo.GN_DoctorMaster dm where tr.ConsultantDoc=dm.doc_id and tr.RegNo='" + CustID + "'";
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

    public DataTable GetDoctorusg(string compcode, string yearcode, string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";
        

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        
        theCommand.CommandText = "select * from  PH_USGTestResult tr,dbo.GN_DoctorMaster dm where tr.compcode=dm.compcode and tr.ConsultantDoc=dm.doc_id and tr.RegNo='" + CustID + "' and tr.compcode='"+compcode+"' and tr.yearcode='"+yearcode+"'";
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


    public DataTable GetDoctors(string compcode, string yearcode, string CustID, string opt)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (opt == "P")
        {
            theCommand.CommandText = "select tr.*,(select doc_name from GN_DoctorMaster where compcode='" + compcode + "' and doc_id=tr.PatholigistId) pathologist,(select dt.TypeName from GN_DoctorType dt,GN_DoctorMaster dm where dt.COMPCODE=dm.COMPCODE and dm.COMPCODE=tr.COMPCODE and dt.DocTypeId=dm.DocTypeId and dm.doc_id=tr.PatholigistId) PathDesignation,(select QuackName from GN_QuackMaster where compcode='" + compcode + "' and QuackId=tr.CheckedBy and Type='T') CheckedByDoc/*,(select dt.TypeName from GN_DoctorType dt,GN_DoctorMaster dm where dt.COMPCODE=dm.COMPCODE and dm.COMPCODE=tr.COMPCODE and dt.DocTypeId=dm.DocTypeId and dm.doc_id=tr.CheckedBy) ChkDesignation*/  from PH_TestResult tr where tr.RegNo='" + CustID + "'";
        }
        else if (opt == "U")
        {
            theCommand.CommandText = "select tr.*,(select do_cname from GN_DoctorMaster where compcode='" + compcode + "' and doc_id=tr.ConsultantDoc) pathologist,(select dt.TypeName from GN_DoctorType dt,GN_DoctorMaster dm where dt.COMPCODE=dm.COMPCODE and dm.COMPCODE=tr.COMPCODE and dt.DocTypeId=dm.DocTypeId and dm.doc_id=tr.ConsultantDoc) PathDesignation,(select QuackName from GN_QuackMaster where compcode='" + compcode + "' and QuackId=tr.CheckedDoc and Type='T') CheckedByDoc/*,(select dt.TypeName from GN_DoctorType dt,GN_DoctorMaster dm where dt.COMPCODE=dm.COMPCODE and dm.COMPCODE=tr.COMPCODE and dt.DocTypeId=dm.DocTypeId and dm.doc_id=tr.CheckedDoc) ChkDesignation*/  from PH_USGTestResult tr where tr.RegNo='" + CustID + "'";
        }
        else if (opt == "R")
        {
            theCommand.CommandText = "select tr.*,(select doc_name from GN_DoctorMaster where compcode='" + compcode + "' and doc_id=tr.ConsultantDoc) pathologist,(select dt.TypeName from GN_DoctorType dt,GN_DoctorMaster dm where dt.COMPCODE=dm.COMPCODE and dm.COMPCODE=tr.COMPCODE and dt.DocTypeId=dm.DocTypeId and dm.doc_id=tr.ConsultantDoc) PathDesignation,(select QuackName from GN_QuackMaster where compcode='" + compcode + "' and QuackId=tr.CheckedDoc and Type='T') CheckedByDoc/*,(select dt.TypeName from GN_DoctorType dt,GN_DoctorMaster dm where dt.COMPCODE=dm.COMPCODE and dm.COMPCODE=tr.COMPCODE and dt.DocTypeId=dm.DocTypeId and dm.doc_id=tr.CheckedDoc) ChkDesignation*/  from PH_USGTestResult tr where tr.RegNo='" + CustID + "'";
        }
        else if (opt == "X")
        {
            theCommand.CommandText = "select tr.*,(select doc_name from GN_DoctorMaster where compcode='" + compcode + "' and doc_id=tr.ConsultantDoc) pathologist,(select dt.TypeName from GN_DoctorType dt,GN_DoctorMaster dm where dt.COMPCODE=dm.COMPCODE and dm.COMPCODE=tr.COMPCODE and dt.DocTypeId=dm.DocTypeId and dm.doc_id=tr.ConsultantDoc) PathDesignation,(select QuackName from GN_QuackMaster where compcode='" + compcode + "' and QuackId=tr.CheckedDoc and Type='T') CheckedByDoc/*,(select dt.TypeName from GN_DoctorType dt,GN_DoctorMaster dm where dt.COMPCODE=dm.COMPCODE and dm.COMPCODE=tr.COMPCODE and dt.DocTypeId=dm.DocTypeId and dm.doc_id=tr.CheckedDoc) ChkDesignation*/  from PH_TestXRAYResult tr where tr.RegNo='" + CustID + "'";
        }
        //theCommand.CommandText = "select * from  PH_USGTestResult tr,dbo.GN_DoctorMaster dm where tr.compcode=dm.compcode and tr.ConsultantDoc=dm.doc_id and tr.RegNo='" + CustID + "' and tr.compcode='" + compcode + "' and tr.yearcode='" + yearcode + "'";
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

    public DataTable GetDoctorDetailsPathology(string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_TestResult tr,dbo.GN_DoctorMaster dm where tr.PatholigistId=dm.doc_id and tr.RegNo='" + CustID + "'";
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


    public DataTable GetTestResult(string compcode, string yearcode, string CustID, string code)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select tr.* from PH_TestResult tr where tr.TestCode='" + code + "' and tr.RegNo='" + CustID + "'";
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

    public DataTable GetTestResult1(string CustID,string compcode)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select tr.*,(Select doc_name from GN_DoctorMaster where compcode='" + compcode + "' and doc_id=tr.PatholigistId) pathologist,(Select doc_name from GN_DoctorMaster where compcode='" + compcode + "' and doc_id=tr.CheckedBy) checkedby from PH_TestResult tr where tr.RegNo='" + CustID + "' and compcode='"+compcode+"' order by tr.TestCode";
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

    public DataTable GetTestmasterMul(string compcode, string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_TestMasterMultiple tr where tr.TestId='" + CustID + "' and tr.compcode='"+compcode+"'";
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

    public DataTable GetTestResultMul(string compcode, string yearcode, string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select tm.* from PH_TestResultMultiple tm where tm.ResultID='" + CustID + "' and tm.compcode='"+compcode+"' and tm.yearcode='"+yearcode+"'";
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


    public DataTable GetTestmasterComplex(string compcode,string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_TestMasterComplex tr where tr.MultipleId='" + CustID + "' and compcode='"+compcode+"'";
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

    public DataTable GetTestResultComplex(string compcode, string yearcode, string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_TestResultComplex tr where tr.RMultipleId='" + CustID + "' and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
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

    public DataTable GetTestmasterDuplex(string compcode, string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_TestMasterDuplex tr where tr.ComplexId='" + CustID + "' and compcode='" + compcode + "'";
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

    public DataTable GetTestResultDuplex(string compcode, string yearcode, string CustID)
    {
        DataTable custTable;
        if (CustID == "")
            CustID = "null";

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_TestResultDuplex tr where tr.RComplexId='" + CustID + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
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



    public DataTable GetPatientDetails(string compcode, string yearcode, string RegNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_PatientReq pr,GN_PatientReg reg where pr.compcode=reg.compcode and pr.RegistrationNo=reg.PatientReg and reg.PatientReg='" + RegNo + "' and pr.compcode='"+compcode+"' and pr.yearcode='"+yearcode+"'";
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


   

    public DataTable GetXrayDetails(string RegNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_TestResult tr,PH_TestXRAYResult x,PH_TestXRAYMaster xm where tr.ReqNo=x.ReqId and xm.TestCode=tr.TestCode and tr.RegNo='" + RegNo + "'";
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