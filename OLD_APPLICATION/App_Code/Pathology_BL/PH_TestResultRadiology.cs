using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_TestResultRadiology12345
/// </summary>
public class PH_TestResultRadiology
{
	public PH_TestResultRadiology(string con)
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
    public DataTable GenerateRadiologyCode(string compcode,string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "sp_PH_GenerateRadiologyCode '" + compcode + "','" + yearcode + "'";
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
        theCommand.CommandText = "select * from dbo.GN_DoctorMaster where compcode='"+compcode+"' and status=1";
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

    public DataTable GetPatientDetails(string compcode, string yearcode, string CustID)
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
        theCommand.CommandText = "select pr.RegistrationNo,pr.RequisitionNo,pr.PatientName,pr.Address,map.TestCode,name.TestName from PH_PatientReq pr,/*PH_USGNameMaster*/PH_TestXRAYMaster name,PH_RequisitionTestMap map where pr.compcode=name.compcode and pr.compcode=map.compcode and pr.RequisitionNo=map.RequisitionID and map.TestCode=name.TestId and pr.RegistrationNo='" + CustID + "' and pr.compcode='" + compcode + "' and pr.yearcode='" + yearcode + "'";
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


    public DataTable GetRadiology(string compcode, string yearcode, string Reg, string code)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select name.ID as id123,*,head.HeaderContent hc from PH_USGHeaderMaster head,PH_USGSubGrMaster sub,PH_PatientReq req,"+
            "PH_USGNameMaster name,PH_RequisitionTestMap map where head.compcode=sub.compcode andhead.compcode=req.compcode and head.compcode=name.compcode "+
            "and head.compcode=map.compcode and sub.SubGrID=head.SubGrCode and req.RequisitionNo=map.RequisitionID and map.TestCode=name.ID and " +
            "name.SubGroupCode=sub.SubGrId and name.ID='" + code + "' and req.RegistrationNo='" + Reg + "' and req.compcode='"+compcode+"' and req.yearcode='"+yearcode+"'";
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


    public DataTable GetRadiologyParameter(string compcode, string yearcode, string Reg, string code)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,null Value from PH_PatientReq req,PH_USGSubGrMapping submap,PH_RequisitionTestMap map,PH_USGNameMaster name where req.compcode=submap.compcode and req.compcode=map.compcode and req.compcode=name.compcode and submap.SubGrId=name.SubGroupCode and name.ID=map.TestCode  and req.RequisitionNo=map.RequisitionID and req.RegistrationNo='" + Reg + "' and name.ID='" + code + "' and req.compcode='"+compcode+"' and req.yearcode='"+yearcode+"'";
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



    public DataTable GetUSGCode(string compcode, string yearcode, string Reg)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select map.TestCode from PH_PatientReq pr,PH_RequisitionTestMap map where pr.compcode=map.compcode and pr.RequisitionNo=map.RequisitionID and map.Type='U' and pr.RegistrationNo='" + Reg + "' and pr.compcode='"+compcode+"' and pr.yearcode='"+yearcode+"'";
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

    public DataTable GetUSGResultCode(string compcode, string yearcode, string Reg)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_USGTestResult ur,PH_PatientReq pr where ur.compcode=pr.compcode and ur.yearcode=pr.yearcode and ur.ReqNo=pr.RequisitionNo and ur.RegNo='" + Reg + "' and ur.compcode='"+compcode+"' and ur.yearcode='"+yearcode+"' and ur.Ttype='U'";
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

    public DataTable GetUSGheaderResultCode(string compcode, string yearcode, string Reg, string code)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,hr.HeaderContent hc from PH_USGHeaderResult hr,PH_USGTestResult ur,dbo.PH_USGHeaderMaster hm where "+
            "hr.compcode=ur.compcode and hr.yearcode=ur.yearcode and hr.compcode=hm.compcode and hr.RowId=ur.RowId and hm.ID=hr.HeaderId and ur.USGNameId='" + code + "'" +
            "and ur.RegNo='" + Reg + "' and hr.ContentType=ur.Ttype and hr.ContentType='U' and hr.compcode='" + compcode + "' and hr.yearcode='"+yearcode+"'";
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

    public DataTable GetRadioResultParameter(string compcode, string yearcode, string Reg, string code)
    {
        DataTable custTable;


        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_USGParameterResult pr,PH_USGTestResult ur,dbo.PH_USGSubGrMapping submap where pr.compcode=ur.compcode and pr.compcode=submap.compcode and pr.yearcode=ur.yearcode and pr.RowId=ur.RowId and submap.Id=pr.ParameterId and ur.RegNo='" + Reg + "'  and  ur.USGNameId='" + code + "' and ur.compcode='" + compcode + "' and ur.yearcode='" + yearcode + "' and ur.Ttype='U'";
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


    public bool InsertRadiology(string compcode, string yearcode, string RowId, string USGNameId, string RegNo, string ReqNo, string ConsultantDoc, string CheckedDoc, string Remarks, string CreatedBy)
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
            theCommand.CommandText = "INSERT INTO PH_USGTestResult(compcode,yearcode,RowId,Ttype,USGNameId,RegNo,ReqNo,ConsultantDoc,CheckedDoc,Remarks,CreatedBy,Status,user01,logdt01) VALUES('" + compcode + "','" + yearcode + "','" + RowId + "','U','" + USGNameId + "','" + RegNo + "','" + ReqNo + "','" + ConsultantDoc + "','" + CheckedDoc + "','" + Remarks + "','" + CreatedBy + "',1,'" + CreatedBy + "',getdate())";
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
    public bool InsertHeader(string compcode, string yearcode, string RowId, string HeaderId, string HeaderContent, string CreatedBy)
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
            theCommand.CommandText = "INSERT INTO PH_USGHeaderResult(compcode,yearcode,RowId,HeaderId,HeaderContent,ContentType,user01,logdt01) VALUES('" + compcode + "','" + yearcode + "','" + RowId + "','" + HeaderId + "','" + HeaderContent + "','U','" + CreatedBy + "',getdate())";
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

    public bool InsertParameter(string compcode, string yearcode, string RowId, string ParameterId, string Value, string CreatedBy)
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
            theCommand.CommandText = "INSERT INTO PH_USGParameterResult(compcode,yearcode,RowId,ParameterId,Value,user01,logdt01) VALUES('" + compcode + "','" + yearcode + "','" + RowId + "','" + ParameterId + "','" + Value + "','" + CreatedBy + "',getdate())";
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




    public bool UpdateRadiology(string compcode, string yearcode, string RowId, string ConsultantDoc, string CheckedDoc, string Remarks, string CreatedBy)
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

            // Command.    RowId
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "update PH_USGTestResult SET ConsultantDoc='" + ConsultantDoc + "',CheckedDoc='" + CheckedDoc + "',Remarks='" + Remarks + "',user02='" + CreatedBy + "',logdt02=getdate() " +
                "where RowId='" + RowId + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "' ";
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
    public bool UpdateHeader(string compcode, string yearcode, string RowId, string HeaderId, string HeaderContent, string CreatedBy)
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
            theCommand.CommandText = "update PH_USGHeaderResult SET HeaderContent='" + HeaderContent + "',user02='" + CreatedBy + "',logdt02=getdate() where ID='" + HeaderId + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "' ";
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

    public bool UpdateParameter(string compcode, string yearcode, string RowId, string ParameterId, string Value, string CreatedBy)
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
            theCommand.CommandText = "update PH_USGParameterResult SET Value='" + Value + "',user02='" + CreatedBy + "',logdt02=getdate() where ID='" + ParameterId + "' and compcode='" + compcode + "' and yearcode='" + yearcode + "'";
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