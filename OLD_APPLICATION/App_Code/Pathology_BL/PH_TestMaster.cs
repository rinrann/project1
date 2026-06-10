using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for PH_TestMaster12345
/// </summary>
public class PH_TestMaster
{
    public PH_TestMaster(string con)
    {
        conString = con;
    }



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable GetTestCode(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateTestCode '" + compcode + "'";
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

    public DataTable GetXRayCode(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateXRayCode '" + compcode + "'";
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


    public DataTable GetDetails(string code)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select tm.TestId,tm.TestName,tm.Cost,tm.DepertmentID,tm.TestType,tm.NormalRange,null TemplateId,null Template from PH_TestMaster tm where tm.TestId='" + code + "' union select x.TestId,x.TestName,x.TestCost,x.DepartmentId,null,null,x.TemplateId,r.Template from PH_TestXRAYMaster x,PH_ReportTemplate r where x.TemplateId=r.TemplateID and x.TestId='" + code + "'";
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
    public DataTable GetMulDetails(string code)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_TestMasterMultiple where TestId='" + code + "'";
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
    public DataTable GetComplexDetails(string code)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,c.Subheading cs,c.NormalRange cn,m.SubHeading ms,m.NormalRange mn from PH_TestMasterComplex c,PH_TestMasterMultiple m where m.MultipleId=c.MultipleId and c.TestId='" + code + "'";
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

    public DataTable GetComplexDetails1(string code)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.PH_TestMasterComplex td where td.MultipleId='" + code + "'";
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

    public DataTable GetMultipleDetails(string code)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.PH_TestMasterMultiple tm where tm.TestId='" + code + "'";
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

    public DataTable GetDuplexDetails1(string code)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.PH_TestMasterDuplex td where td.ComplexId='" + code + "'";
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

    public DataTable GetDuplexDetails(string code)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select d.*,d.SubHeading ds,d.NormalRange dn,c.Subheading cs,c.NormalRange cn,m.SubHeading ms,m.NormalRange mn from PH_TestMasterComplex c,PH_TestMasterMultiple m,PH_TestMasterDuplex d where m.MultipleId=c.MultipleId and d.ComplexId=c.ComplexId and d.TestId='" + code + "'";
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

    public DataTable DropdownDepartment(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_DepartmentMaster d where d.Status=1 and compcode='" + compcode + "' and d.DeptCode != 'D0002'";
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

    public DataTable DropdownTestGroup(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select ProfileCode,ProfileName from  PH_ProfileMaster d where d.Status=1 and compcode='" + compcode + "'";
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

    public DataTable DropdownTemplate(string id, string compcode)
    {
        if (id == "")
            id = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_ReportTemplate '" + compcode + "'," + id + "";
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
    public DataTable GenerateMulCode()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateMultipleTestCode";
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

    public DataTable GenerateDuplexCode()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateDuplexTestCode";
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
    public DataTable GenerateComplexCode()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateComplexTestCode";
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
    public DataTable getvaluefromMultiple(string code)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from PH_TestMasterMultiple where TestId='" + code + "'";
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

    public bool InsertDialysisCharge(string dept, string testcode, string type, string name, string cost, string normal, string created, string cocode)
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
            theCommand.CommandText = "INSERT INTO PH_TestMaster(compcode,DepertmentID,TestId,TestName,Cost,TestType,NormalRange,Status,CreatedBy) VALUES('" + cocode + "','" + dept + "','" + testcode + "','" + name + "'," + cost + ",'" + type + "','" + normal + "',1,'" + created + "')";
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

    public bool InsertMultipleTest(string code, string mulcode, string subhead, string normal, string compcode)
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
            theCommand.CommandText = "INSERT INTO PH_TestMasterMultiple(CompCode,TestId,MultipleId,SubHeading,NormalRange) VALUES('" + compcode + "','" + code + "','" + mulcode + "','" + subhead + "','" + normal + "')";
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
    public bool InsertXRay(string dept, string TestCode, string testname, string testcost, string TemplatID, string createdby, string compcode)
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
            if (testcost == "")
            {
                testcost = "0.00";
            }
            // Command.
            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "INSERT INTO PH_TestXRAYMaster(compcode,DepartmentId,TestId,TestName,TestCost,TemplateId,Status,CreatedBy,User01,Logdt01) VALUES('" + compcode + "','" + dept + "','" + TestCode + "','" + testname + "','" + testcost + "','" + TemplatID + "',1,'" + createdby + "','" + createdby + "',GETDATE())";
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

    public bool UpdateXray(string dept, string TestCode, string testname, string testcost, string TemplatID, string compcode, string user)
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
            theCommand.CommandText = "update PH_TestXRAYMaster set DepartmentId='" + dept + "', TestName='" + testname + "', TestCost='" + testcost + "',TemplateId='" + TemplatID + "',User02='" + user + "',Logdt02=GETDATE() where compcode='" + compcode + "' and TestId = '" + TestCode + "'";
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

    public bool InsertcomplexTest(string code, string mulcode, string testcode, string subhead, string normal)
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
            theCommand.CommandText = "INSERT INTO PH_TestMasterComplex(ComplexId,MultipleId,TestId,Subheading,NormalRange) VALUES('" + code + "','" + mulcode + "','" + testcode + "','" + subhead + "','" + normal + "')";
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

    public bool InsertduplexTest(string dcode, string testcode, string mulcode, string comcode, string subhead, string normal)
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
            theCommand.CommandText = "INSERT INTO PH_TestMasterDuplex(DuplexId,TestId,MultipleId,ComplexId,SubHeading,NormalRange) VALUES('" + dcode + "','" + testcode + "','" + mulcode + "','" + comcode + "','" + subhead + "','" + normal + "')";
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

    public bool UpdateSimpleTest(string TestId, string TestName, string Cost, string NormalRange, string dept, string cocode)
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
            theCommand.CommandText = "update PH_TestMaster set DepertmentID='" + dept + "', TestName='" + TestName + "', Cost='" + Cost + "',NormalRange='" + NormalRange + "' where TestId = '" + TestId + "'and compcode='" + cocode + "'";
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
    public bool UpdateMultipleTest(string MultipleId, string SubHeading, string NormalRange)
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
            theCommand.CommandText = "update PH_TestMasterMultiple set SubHeading='" + SubHeading + "', NormalRange='" + NormalRange + "' where MultipleId = '" + MultipleId + "'";
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
    public bool UpdateComplexTest(string ComplexId, string Subheading, string NormalRange)
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
            theCommand.CommandText = "update PH_TestMasterComplex set  NormalRange='" + NormalRange + "',Subheading='" + Subheading + "' where ComplexId = '" + ComplexId + "'";
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

    public bool UpdateDuplexTest(string DuplexId, string Subheading, string NormalRange)
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
            theCommand.CommandText = "update PH_TestMasterDuplex set  NormalRange='" + NormalRange + "',SubHeading='" + Subheading + "' where DuplexId = '" + DuplexId + "'";
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

    public bool DeleteTestMasterMultiple(string testid, string cocode)
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
            theCommand.CommandText = "delete from PH_TestMasterMultiple  WHERE TestId='" + testid + "'and compcode='" + cocode + "'";
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

    public bool DeleteTestMasterComplex(string testid)
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
            theCommand.CommandText = "delete from PH_TestMasterComplex  WHERE TestId='" + testid + "'";
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

    public DataTable getDeptCode(string compcode, string grpcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select DepartmentID from PH_ProfileMaster where compcode='" + compcode + "' and ProfileCode='" + grpcode + "'";
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

    public bool DeleteTestMasterDuplex(string testid)
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
            theCommand.CommandText = "delete from PH_TestMasterDuplex  WHERE TestId='" + testid + "'";
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


    public DataTable getTestDetails(string CompCode, string GroupCode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select TestId,TestName,isNull(Cost,0)Cost,GroupCode,isNull(LabChrg,0)LabChrg,IsNull(SingleChrg,0)SingleChrg,isNull(TwinsChrg,0)TwinsChrg,isNull(ConsultantChrg,0)ConsultantChrg,isNull(CompanyChrg,0)CompanyChrg,isNull(OtCharge,0)OtCharge,isNull(MedicinesChrg,0)MedicinesChrg,isNull(BiopsyChrg,0)BiopsyChrg,isNull(IVFLAbChrg,0)IVFLAbChrg,isNull(OfferRate1,0)OfferRate1,isNull(OfferRate2,0) OfferRate2,isNull(OfferRate3,0) OfferRate3,isNull(consullt_name1,'')consullt_name1,isNull(consullt_name2,'')consullt_name2,isNull(consullt_name3,'')consullt_name3,dbo.fn_GetDoctor(compcode,consullt_name1) as docname1,dbo.fn_GetDoctor(compcode,consullt_name2) as docname2,dbo.fn_GetDoctor(compcode,consullt_name3) as docname3,* from PH_TestMaster where compcode='" + CompCode + "' and GroupCode='" + GroupCode + "'";
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

    public bool InsertInvestigation(string compcode, string deptcode, string grpcode, string testcode, string TestName, string Inr, string Cons, string comp, string single, string twins, string labcost, string otcost, string medcost, string biocost, string ivfcost, string user, string Rate1, string Rate2, string Rate3, string consult1, string consult2, string consult3, string NormalRange, string Unit, string Method, string Details)
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
            theCommand.CommandText = "Insert into PH_TestMaster(COMPCODE,DepertmentID,GroupCode,TestId,TestName,Cost,LabChrg,SingleChrg,TwinsChrg,ConsultantChrg,CompanyChrg,OtCharge,MedicinesChrg,BiopsyChrg,IVFLAbChrg,CreatedBy,status,OfferRate1,OfferRate2,OfferRate3,consullt_name1,consullt_name2,consullt_name3,NormalRange,Method,Unit,Details) values('" + compcode + "','" + deptcode + "','" + grpcode + "','" + testcode + "','" + TestName + "','" + Inr + "','" + labcost + "','" + single + "','" + twins + "','" + Cons + "','" + comp + "','" + otcost + "','" + medcost + "','" + biocost + "','" + ivfcost + "','" + user + "','1','" + Rate1 + "','" + Rate2 + "','" + Rate3 + "','" + consult1 + "','" + consult2 + "','" + consult3 + "','" + NormalRange + "','" + Method + "','" + Unit + "','" + Details + "')";
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


    public bool UpdateInvestigation(string compcode, string deptcode, string grpcode, string testcode, string TestName, string Inr, string Cons, string comp, string single, string twins, string labcost, string otcost, string medcost, string biocost, string ivfcost, string user, string Rate1, string Rate2, string Rate3, string consult1, string consult2, string consult3, string NormalRange, string Unit, string Method, string Details)
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
            theCommand.CommandText = "Update ph_testmaster set TestName='" + TestName + "',Cost='" + Inr + "',LabChrg='" + labcost + "',SingleChrg='" + single + "',TwinsChrg='" + twins + "',ConsultantChrg='" + Cons + "',CompanyChrg='" + comp + "',OtCharge='" + otcost + "',MedicinesChrg='" + medcost + "',BiopsyChrg='" + biocost + "',IVFLAbChrg='" + ivfcost + "',OfferRate1='" + Rate1 + "',OfferRate2='" + Rate2 + "',OfferRate3='" + Rate3 + "',consullt_name1='" + consult1 + "',consullt_name2='" + consult2 + "',consullt_name3='" + consult3 + "', NormalRange= '" + NormalRange + "', Unit= '" + Unit + "', Method= '" + Method + "',Details='" + Details + "' where compcode='" + compcode + "' and TestId='" + testcode + "' and GroupCode='" + grpcode + "'";
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

    public DataTable getUserDetails(string compcode, string user)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select isNull(AdminUser,'0') AdminUser from GN_UserDetails where compcode='" + compcode + "' and userid='" + user + "'";
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


    public DataTable getInvestigationGroupList(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select dbo.Fnc_InvestigationDeptName(compcode, DepertmentID)deptnm,DepertmentID,dbo.Fnc_InvestigationGroupName(compcode,groupcode)grpnm,GroupCode,TestName,cost,consullt_name1,consullt_name2,consullt_name3,ConsultantChrg,companychrg from ph_testmaster where compcode='" + compcode + "' order by DepertmentID , GroupCode,TestName";
        theCommand.CommandText = "select ProfileCode GroupCd,ProfileName grpnm,DepartmentID,dbo.Fnc_InvestigationDeptName(compcode,DepartmentID)deptnm from PH_ProfileMaster where compcode='" + compcode + "' order by DepartmentID,ProfileCode";
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

    public DataTable getInvestigationList(string compcode, string grpcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select dbo.Fnc_InvestigationDeptName(compcode, DepertmentID)deptnm,DepertmentID,dbo.Fnc_InvestigationGroupName(compcode,groupcode)grpnm,GroupCode,TestName,cost,consullt_name1,consullt_name2,consullt_name3,ConsultantChrg,companychrg from ph_testmaster where compcode='" + compcode + "' order by DepertmentID , GroupCode,TestName";
        //theCommand.CommandText = "select G.DepartmentID DepertmentID,dbo.Fnc_InvestigationDeptName(G.compcode,G.DepartmentID)deptnm, T.groupcode GroupCd,dbo.Fnc_InvestigationGroupName(T.compcode,T.groupcode)grpnm,* from ph_testmaster T,PH_ProfileMaster G where T.COMPCODE=G.COMPCODE and T.groupcode=G.ProfileCode and T.compcode='" + compcode + "' order by G.DepartmentID,GroupCode";
        theCommand.CommandText = "select dbo.fn_GetDoctor(compcode,consullt_name1) as consullt_name1,dbo.fn_GetDoctor(compcode,consullt_name2) as consullt_name2,dbo.fn_GetDoctor(compcode,consullt_name3) as consullt_name3,* from ph_testmaster where compcode='" + compcode + "' and groupcode='" + grpcode + "'";
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

    public DataTable getInvestigationGrpCost(string compcode, string grpcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        //theCommand.CommandText = "select dbo.Fnc_InvestigationDeptName(compcode, DepertmentID)deptnm,DepertmentID,dbo.Fnc_InvestigationGroupName(compcode,groupcode)grpnm,GroupCode,TestName,cost,consullt_name1,consullt_name2,consullt_name3,ConsultantChrg,companychrg from ph_testmaster where compcode='" + compcode + "' order by DepertmentID , GroupCode,TestName";
        //theCommand.CommandText = "select G.DepartmentID DepertmentID,dbo.Fnc_InvestigationDeptName(G.compcode,G.DepartmentID)deptnm, T.groupcode GroupCd,dbo.Fnc_InvestigationGroupName(T.compcode,T.groupcode)grpnm,* from ph_testmaster T,PH_ProfileMaster G where T.COMPCODE=G.COMPCODE and T.groupcode=G.ProfileCode and T.compcode='" + compcode + "' order by G.DepartmentID,GroupCode";
        theCommand.CommandText = "select sum(isNull(cost,0)) cost,sum(isNull(LabChrg,0))LabChrg,Sum(IsNull(SingleChrg,0))SingleChrg,sum(isNull(TwinsChrg,0))TwinsChrg,sum(isNull(ConsultantChrg,0))ConsultantChrg,sum(isNull(CompanyChrg,0))CompanyChrg,sum(isNull(OtCharge,0))OtCharge,sum(isNull(MedicinesChrg,0))MedicinesChrg,sum(isNull(BiopsyChrg,0)) BiopsyChrg,sum(isNull(IVFLAbChrg,0)) IVFLAbChrg from ph_testmaster where compcode='" + compcode + "' and groupcode='" + grpcode + "'";
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

    public DataTable DropdownDoctor(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select doc_id,doc_name from GN_DoctorMaster where compcode='" + compcode + "'";
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