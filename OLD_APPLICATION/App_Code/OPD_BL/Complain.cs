using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Complain
/// </summary>
public class Complain
{
	public Complain(string con)
	{
        conString = con;
	}


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


    public DataTable GridComplain(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select cl.RowId,cl.ComplainName,cl.MedicineGroupId,gr.MedicineGroupName,sub.SubGrName  from dbo.OPD_Complain cl,IPD_MedicineGroup gr,IPD_MedicineSubGroup sub where gr.compcode=cl.compcode and sub.compcode=gr.compcode and cl.MedicineGroupId=gr.MedicineGroupID and cl.MedicineSubGrId=sub.ID and cl.compcode='"+compcode+"' union all select cl.RowId,cl.ComplainName,null,null,null  from dbo.OPD_Complain cl where cl.MedicineGroupId is null and cl.MedicineSubGrId is null and cl.compcode='"+compcode+"' order by ComplainName";
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
        theAdapter.Dispose();

        return hospitalTable;
    }

    public DataTable GridPopup(string reg, string name, string ph, string address)
    {
        if (reg == "")
            reg = "null";
        if (name == "")
            name = "null";
        if (ph == "")
            ph = "null";
        if (address == "")
            address = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_PH_PatientDetails  " + reg + "," + name + "," + ph + "," + address + "";
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
        theAdapter.Dispose();

        return hospitalTable;
    }
    public bool InsertComplain(string name, string MedicineGroupId, string MedicineSubGrId,string cocode)
    {
        if (MedicineGroupId == "0")
            MedicineGroupId = "null";
        else
            MedicineGroupId = "'" + MedicineGroupId + "'";

        if (MedicineSubGrId == "0")
            MedicineSubGrId = "null";
        else
            MedicineSubGrId = "'" + MedicineSubGrId + "'";

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
            theCommand.CommandText = "INSERT INTO OPD_Complain(compcode,ComplainName,MedicineGroupId,MedicineSubGrId) VALUES('"+cocode+"','" + name + "'," + MedicineGroupId + "," + MedicineSubGrId + ")";
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

    public bool UpdateComplain(string id, string name ,string MedicineGroupId, string MedicineSubGrId,string cocode)
    {

        if (MedicineGroupId == "0")
            MedicineGroupId = "null";
        else
            MedicineGroupId = "'" + MedicineGroupId + "'";

        if (MedicineSubGrId == "0")
            MedicineSubGrId = "null";
        else
            MedicineSubGrId = "'" + MedicineSubGrId + "'";

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

            theCommand.CommandText = "update OPD_Complain set ComplainName='" + name + "',MedicineGroupId=" + MedicineGroupId + ",MedicineSubGrId=" + MedicineSubGrId + " where RowId = '" + id + "'and compcode='"+cocode+"'";
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

    public bool DeleteComplain(string id,string cocode)
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
            theCommand.CommandText = "delete OPD_Complain  WHERE RowId='" + id + "'and compcode='"+cocode+"'";
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