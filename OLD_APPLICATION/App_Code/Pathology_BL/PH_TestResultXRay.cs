using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_TestResultXRay12345
/// </summary>
public class PH_TestResultXRay
{
	public PH_TestResultXRay(string con)
	{
        conString = con;
	}


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;
    public DataTable TestMastersubheading(string id)
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
        theCommand.CommandText = "select * from PH_TestMaster where TestId='" + id + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return custTable;
    }

    public DataTable MultipleTestMastersubheading(string id)
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
        theCommand.CommandText = "select pd.*,t.* from GN_PatientReg pd,PH_PatientReq pr,PH_TestMasterMultiple t where pd.PatientReg=pr.RegistrationNo and pr.TestCode=t.TestId and pd.PatientReg='" + id + "'";
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
    public DataTable GetMultiple(string id)
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
        theCommand.CommandText = "select t.* from PH_TestMasterMultiple t where t.TestId='" + id + "'";
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

    public DataTable Getcomplex(string id)
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
        theCommand.CommandText = "select * from PH_TestMasterComplex c  where c.MultipleId='" + id + "'";
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

    public DataTable Getduplex(string id)
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
        theCommand.CommandText = "select * from PH_TestMasterDuplex c where c.ComplexId='" + id + "'";
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

    public DataTable complexTestMastersubheading(string id)
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
        theCommand.CommandText = "select m.*,c.Subheading cs,c.NormalRange cn,m.SubHeading ms,m.NormalRange mn from PH_TestMasterComplex c,PH_TestMasterMultiple m where m.MultipleId=c.MultipleId and m.TestId='" + id + "'";
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
    public DataTable GenerateTestResult()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateTestResCode";
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
    public DataTable GenerateRadiologyCode()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "sp_PH_GenerateRadiologyCode";
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
    public DataTable GenerateTestResultMul()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateTestResMulCode";
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
    public DataTable GenerateTestResultCom()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateTestResComCode";
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
    public DataTable GenerateTestResultDup()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateTestResDupCode";
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

    public DataTable DropdownCity(int id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from CityMaster where Status=1 and State_ID=" + id + "";
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

    public DataTable GetAdv(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select AdAmt from dbo.PH_PatientReq where RequisitionNo='" + id + "'";
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

    public DataTable GetCost(string id)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select Cost from PH_TestMaster where TestId='" + id + "' ";
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

    public DataTable DropdownTemplate(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.PH_ReportTemplate rt where rt.compcode='"+compcode+"' and rt.Status=1";
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

    public DataTable DropdownTemplatePara(string para)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.PH_ReportTemplate rt where rt.Status=1 and rt.TemplateID='" + para + "'";
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

    public DataTable DropdownDoc(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.GN_DoctorMaster where status=1 and compcode='"+compcode+"'";
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

    public DataTable DropdownTech(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.GN_QuackMaster where compcode='" + compcode + "' and Type='T' and status=1";
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


    public DataTable GetPatientDetails(string CustID)
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
        theCommand.CommandText = "exec sp_PatientDetails '" + CustID + "',null,null,null";
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
    public DataTable GetXray(string compcode,string CustID)
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
        theCommand.CommandText = "select * from PH_RequisitionTestMap map,PH_TestXRAYMaster xm,dbo.PH_ReportTemplate rt  where map.compcode=xm.compcode and map.compcode=rt.compcode and map.TestCode=xm.TestId and xm.TemplateId=rt.TemplateID and map.compcode='"+compcode+"' and map.RequisitionID='" + CustID + "'";
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

    public DataTable GetReqmaster(string CustID)
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
        theCommand.CommandText = " select * from PH_PatientReq pr where pr.RegistrationNo='" + CustID + "'";
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
    public DataTable GetXRayResult(string compcode, string yearcode, string CustID)
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
        //theCommand.CommandText = "select * from dbo.PH_TestXRAYResult r,dbo.PH_TestXRAYMaster xm where xm.TestId=r.TestId and r.ReqNo='" + CustID + "'";
        theCommand.CommandText = "select * from dbo.PH_USGTestResult r,PH_USGHeaderResult h,dbo.PH_TestXRAYMaster xm where xm.compcode=h.compcode and xm.compcode=r.compcode and h.yearcode=r.yearcode and xm.TestId=r.USGNameId and r.Ttype=h.ContentType and xm.compcode='" + compcode + "' and r.yearcode='" + yearcode + "' and r.ReqNo='" + CustID + "' and r.Ttype='X'";
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
    public DataTable GetTestmasertXray(string CustID)
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
        theCommand.CommandText = " select * from dbo.PH_ReportTemplate rt,dbo.PH_TestXRAYMaster x,PH_PatientReq pr,PH_RequisitionTestMap map,PH_TestMaster tm where rt.TemplateID=x.TemplatID and x.TestCode=tm.TestId and pr.RequisitionNo=map.RequisitionID and  tm.TestId=map.TestCode  and pr.RegistrationNo='" + CustID + "'";
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

    public DataTable DropdownTemplate(string compcode, string id)
    {
        if (id == "")
            id = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_ReportTemplate '"+compcode+"'," + id + "";
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

    public DataTable Populate(string compcode, string yearcode, string CustID)
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
        theCommand.CommandText = "select pd.* from GN_PatientReg pd,PH_PatientReq pr where pd.compcode=pr.compcode and pd.PatientReg=pr.RegistrationNo and pd.PatientReg='" + CustID + "' and pd.compcode='"+compcode+"' and pr.yearcode='"+yearcode+"'";
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



    public bool Updatexray(string compcode, string yearcode, string Id, string TContent, string ConsultantDoc, string CheckedDoc, string Remarks, string CreatedBy)
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

            // Command.       TestId,RegNo,ReqNo,TContent,ConsultantDoc,CheckedDoc,Remarks,CreatedBy,Status
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            //theCommand.CommandText = "update PH_TestXRAYResult set TContent='" + TContent + "',ConsultantDoc='" + ConsultantDoc + "',CheckedDoc='" + CheckedDoc + "',Remarks='" + Remarks + "'  where RowId='" + Id + "'";
            theCommand.CommandText = "update PH_USGTestResult set ConsultantDoc='" + ConsultantDoc + "',CheckedDoc='" + CheckedDoc + "',Remarks=ltrim(rtrim('" + Remarks + "')),user02='" + CreatedBy + "',logdt02=getdate()  where ltrim(rtrim(RowId))=ltrim(rtrim('" + Id + "')) and compcode='" + compcode + "' and yearcode='" + yearcode + "' and Ttype='X'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();

            theCommand.Connection = theConnection;
            //theCommand.CommandText = "update PH_TestXRAYResult set TContent='" + TContent + "',ConsultantDoc='" + ConsultantDoc + "',CheckedDoc='" + CheckedDoc + "',Remarks='" + Remarks + "'  where RowId='" + Id + "'";
            theCommand.CommandText = "update PH_USGHeaderResult set HeaderContent=ltrim(rtrim('" + TContent + "')),user02='" + CreatedBy + "',logdt02=getdate() where ltrim(rtrim(RowId))=ltrim(rtrim('" + Id + "')) and compcode='" + compcode + "' and yearcode='" + yearcode + "' and ContentType='X'";
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


    public bool InsertXRay(string compcode, string yearcode, string RowId, string TestId, string RegNo, string ReqNo, string TContent, string ConsultantDoc, string CheckedDoc, string Remarks, string CreatedBy)
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
            //theCommand.CommandText = "INSERT INTO PH_TestXRAYResult(TestId,RegNo,ReqNo,TContent,ConsultantDoc,CheckedDoc,Remarks,CreatedBy,Status) VALUES('" + TestId + "','" + RegNo + "','" + ReqNo + "','" + TContent + "','" + ConsultantDoc + "','" + CheckedDoc + "','" + Remarks + "','" + CreatedBy + "',1)";
            theCommand.CommandText = "INSERT INTO PH_USGTestResult(compcode,yearcode,RowId,Ttype,USGNameId,RegNo,ReqNo,ConsultantDoc,CheckedDoc,Remarks,CreatedBy,Status,user01,logdt01) VALUES('" + compcode + "','" + yearcode + "','" + RowId + "','X','" + TestId + "','" + RegNo + "','" + ReqNo + "','" + ConsultantDoc + "','" + CheckedDoc + "','" + Remarks + "','" + CreatedBy + "',1,'" + CreatedBy + "',getdate())";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();

            theCommand.Connection = theConnection;
            theCommand.CommandText = "INSERT INTO PH_USGHeaderResult(compcode,yearcode,RowId,HeaderId,HeaderContent,ContentType,user01,logdt01) VALUES('" + compcode + "','" + yearcode + "','" + RowId + "',NULL,'" + TContent + "','X','" + CreatedBy + "',getdate())";
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