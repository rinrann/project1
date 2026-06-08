using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Prescriptemplateopd123
/// </summary>
public class Prescriptemplateopd
{
	public Prescriptemplateopd(string con)
	{
        conString = con;
	}



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable PrescriptionTemplateTable;

    public int GetPrescrpTemID()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theAdapter = new SqlDataAdapter("SELECT ISNULL(max(PrescrpTemID),0)+1 as PrescrpTemID FROM IPD_PrescriptionTmplate Where status =1", theConnection);
        DataSet ds = new DataSet();
        theAdapter.Fill(ds);
        int PrescrpTemID = 0;
        if (ds.Tables[0].Rows[0]["PrescrpTemID"] == DBNull.Value)
        {
            PrescrpTemID = 1;
        }

        else
        {
            PrescrpTemID = Convert.ToInt32(ds.Tables[0].Rows[0]["PrescrpTemID"]) + 1;
        }
        return PrescrpTemID;
    }


    public DataTable GridPopup(string name)
    {
        
        if (name == "")
            name = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        // theCommand.CommandText = "select * from patient_data where patient_name like '" + PatientName + "%' and Patient_Id like '" + PatientId + "%'";
        theCommand.CommandText = "exec sp_OPD_TemplateDtls " + name + "";
        theCommand.CommandType = CommandType.Text;


        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
       DataTable  hospitalTable = new DataTable();
        theAdapter.Fill(hospitalTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return hospitalTable;
    }

    public DataTable DropdownID1()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select PrescrpTemID,PrescrpTemName from  IPD_PrescriptionTmplate where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return PrescriptionTemplateTable;
    }
    public DataTable DropdownID2(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineGroupID,MedicineGroupName from  IPD_MedicineGroup where status=1 and compcode='" + compcode + "' order by MedicineGroupName ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return PrescriptionTemplateTable;
    }

    public DataTable DropdownMedicine(string value,string CompCode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  IPD_MedicineMaster where compcode='" + CompCode + "' and status=1 and MedicineGroupID='" + value + "'  order by MedicineName";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return PrescriptionTemplateTable;
    }

    public DataTable DropdownsUB(string MedicineGroupID,string CompCode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.  
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (MedicineGroupID == "0")
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where compcode='"+ CompCode +"' and status=1  order by SubGrName ";
        else
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where compcode='" + CompCode + "' and status=1 AND GroupID='" + MedicineGroupID + "' order by SubGrName ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();


        return PrescriptionTemplateTable;
    }
    public DataTable DropdownID4(string CompCode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select RouteID,RouteName from  IPD_MedicineRoute where compcode='" + CompCode + "' and Status=1  order by RouteName";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return PrescriptionTemplateTable;
    }

    public DataTable DropdownTempalteGroup(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  IPD_PrescriptionTmplateGroup where compcode='" + compcode + "'  order by PrescriptionGroupName";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return PrescriptionTemplateTable;
    }

    public DataTable DropdownDuration(string compcode)
    {
        // Connection.
        DataTable BedTable;
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandTimeout = 0;
        theCommand.CommandText = "select d.DurationId,d.DurationName+' Days'  DurationName from  IPD_DurationMaster d where d.status=1 and compcode='" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }

    public DataTable DropdownDose(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select d.ID,d.DoseName+' Hourly'  DoseName from  MD_DoseMaster d where d.compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        PrescriptionTemplateTable = new DataTable();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return PrescriptionTemplateTable;
    }
    public bool CheckIfTemplateExists(string tempName, string prescrpTemID)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT COUNT(*) FROM IPD_PrescriptionTmplate WHERE PrescrpTemName='" + tempName + "' AND PrescrpTemID<>" + prescrpTemID;
        theCommand.CommandType = CommandType.Text;
        int count = (int)theCommand.ExecuteScalar();
        return count > 0;
    }
    public bool Insert_Update_PrescriptionDetails(int mode,string id, string PrescrpTemName, string CreatedDate, string LoginUser, string PrescriptionGrId,string cocode)
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
            if(mode==1)
                theCommand.CommandText = "INSERT INTO IPD_PrescriptionTmplate(compcode,PrescrpTemName, CreatedBy,CreatedDate, status,PrescriptionGrId) VALUES ('" + cocode + "','" + PrescrpTemName + "', '" + LoginUser + "','" + CreatedDate + "', 1,'" + PrescriptionGrId + "')";
            else
                theCommand.CommandText = "update IPD_PrescriptionTmplate set PrescrpTemName='" + PrescrpTemName + "', PrescriptionGrId='" + PrescriptionGrId + "'  where PrescrpTemID='" + id + "'and compcode='" + cocode + "'";
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

    public bool InsertPrescriptionMapping(string sub,string PrescrpTemID, string name, string MedicineGroupID, string MedicineID, string RouteID, string DailyDose, string CreatedBy,string cocode,string duration)
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
            if(PrescrpTemID=="")
                theCommand.CommandText = "INSERT INTO IPD_PrescriptionTmplateMapping(compcode,SubGroupid,PrescrpTemID,MedicineGroupID,MedicineID,RouteID,DailyDose,CreatedBy, status,Duration) VALUES ('" + cocode + "','" + sub + "',(select MAX(pt.PrescrpTemID) FROM IPD_PrescriptionTmplate pt), '" + MedicineGroupID + "', '" + MedicineID + "', '" + RouteID + "', '" + DailyDose + "',  '" + CreatedBy + "', 1,'"+duration+"')";
            else
                theCommand.CommandText = "INSERT INTO IPD_PrescriptionTmplateMapping(compcode,SubGroupid,PrescrpTemID,MedicineGroupID,MedicineID,RouteID,DailyDose,CreatedBy, status,Duration) VALUES ('" + cocode + "','" + sub + "','" + PrescrpTemID + "', '" + MedicineGroupID + "', '" + MedicineID + "', '" + RouteID + "', '" + DailyDose + "',  '" + CreatedBy + "', 1,'"+duration+"')";
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

    public bool DeleteMap(string id,string cocode)
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
            theCommand.CommandText = "delete from IPD_PrescriptionTmplateMapping where PrescrpTemID='" + id + "'and compcode='" + cocode + "'";
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

    public DataSet GetPrescriptionTemplateDetails(string id,string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        theConnection.Open();
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "sp_IPD_PrescriptionTemplate_Details  " + id + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataSet PrescriptionTemplateTable = new  DataSet();
        theAdapter.Fill(PrescriptionTemplateTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        

        return PrescriptionTemplateTable;
    }
}