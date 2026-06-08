using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_ReagentMaster5554
/// </summary>
public class PH_ReagentMaster
{
	public PH_ReagentMaster(string con)
	{
        conString = con;
	}



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;

    public DataTable DropdownUnit(string cocode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from GN_UnitMaster where compcode='"+cocode+"' and status=1";
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

    public DataTable GridAgent(string cocode,string reagent)
    {
        if (reagent == "")
        {
            reagent = "'%%'";
        }
        else
        {
            reagent = "'" + reagent + "%'";
        }
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from dbo.PH_ReagentMaster r,GN_UnitMaster u where r.compcode=u.compcode and u.UnitId=r.Unit and r.Status=1 and r.compcode='"+cocode+"' and rname like "+reagent+"";
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
    public DataTable GenerateReagentCode(string cocode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_PH_GenerateReagent '" + cocode + "'";
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

    public bool InsertReagent(string rcode, string rname, string Unit, string TestPerUnit, string MinStock, string craetedby, string cocode, string yearcode, string TestPerUnittxt)
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
            theCommand.CommandText = "INSERT INTO PH_ReagentMaster(compcode,RCode,RName,Unit,TestPerUnit,MinStock,CreatedBy,Status,User01,Logdt01,TestPerUnitText) VALUES('" + cocode + "','" + rcode + "','" + rname + "','" + Unit + "','" + TestPerUnit + "','" + MinStock + "','" + craetedby + "',1,'" + craetedby + "',GETDATE(),'" + TestPerUnittxt+ "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            theCommand.CommandText = "INSERT INTO ITEMMAST(COMPCODE,ICODE,INAME,ITYPE,IUNIT,I2CONVRT,TAG,USER01,LOGDT01,TestPerUnitText) VALUES ('" + cocode + "','" + rcode + "','" + rname + "','G'," + Unit + ",'" + TestPerUnit + "', 1,'" + craetedby + "',getdate(),'" + TestPerUnittxt+ "')";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery();
            theCommand.CommandText = "insert into ITEMMST1(COMPCODE,yearcode,Icode,tag,user01,logdt01) values('" + cocode + "','" + yearcode + "','" + rcode + "',1,'" + craetedby + "',getdate())";
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

    public bool UpdateReaent(string rcode, string rname, string Unit, string TestPerUnit, string MinStock, string cocode, string TestPerUnitText)
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

            theCommand.CommandText = "update PH_ReagentMaster set RName='" + rname + "', Unit='" + Unit + "', TestPerUnit='" + TestPerUnit + "',MinStock='" + MinStock + "',TestPerUnitText='" + TestPerUnitText + "' where RCode ='" + rcode + "'and compcode='" + cocode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.

            theCommand.CommandText = "Update ITEMMAST set ConversionFactor='" + TestPerUnit + "', iname='" + rname + "', UnitId=" + Unit + ",TestPerUnitText='" + TestPerUnitText + "'  where ltrim(rtrim(icode)) = ltrim(rtrim('" + rcode + "')) and compcode='" + cocode + "'";
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

    public bool DeleteReagent(string id, string cocode, string yearcode)
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
            theCommand.CommandText = "delete PH_ReagentMaster WHERE RCode='" + id + "'and compcode='" + cocode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute insert query.
            theCommand.CommandText = "delete ITEMMAST  WHERE ltrim(rtrim(icode)) = ltrim(rtrim('" + id + "')) and compcode='" + cocode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute Delete query.  
            theCommand.CommandText = "delete ITEMMST1  WHERE ltrim(rtrim(icode)) = ltrim(rtrim('" + id + "')) and compcode='" + cocode + "' and yearcode='" + yearcode + "'";
            theCommand.CommandType = CommandType.Text;
            theCommand.ExecuteNonQuery(); // Execute Delete query.  
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