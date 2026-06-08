using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for ExportToExcel
/// </summary>
public class ExportToExcel
{
    public ExportToExcel(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable MedicineTable;


    public DataTable GetAllMedicine( string company,string group,string subgroup)
    {
        if (company == "0" || company == "")
            company = "null";

        if (group == "0" || group == "")
            group = "null";

        if (subgroup == "0" || subgroup == "")
            subgroup = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_Medicine_ExportExcel " + company + "," + group + "," + subgroup + " ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    }


    public DataTable DropdownMedicine(string compcode,string MCode=null, string SubGroupid=null)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (MCode == null && SubGroupid == null)
        {
            theCommand.CommandText = "select * from  IPD_MedicineMaster where status=1 and compcode='"+ compcode+"'";
        }
        else
        {
            theCommand.CommandText = "select * from  IPD_MedicineMaster where status=1 and compcode='" + compcode + "' and SubGroupid='" + SubGroupid + "' and MCode='" + MCode + "'";
        } 
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    }


    public DataTable DropdownManufacturer(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  PH_ManufactureMaster where status=1 and compcode='" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    }

    public DataTable DropdownSubGroup(string compcode,string gr)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (gr == "0")
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where status=1 and compcode='"+ compcode+"'";
        else
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where status=1 and compcode='" + compcode + "' and GroupID='" + gr + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    }

    public DataTable DropdownGroup(string compcpde)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select MedicineGroupID,MedicineGroupName from  IPD_MedicineGroup where status=1 and compcode='"+ compcpde+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    }
    public DataTable OpnMedicineList(string compcode, string yearcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select b.BATCHNO,b.ICODE,m.MedicineID,m.MedicineName,Right(convert(varchar,b.EXPDATE,103),7)EXPDATE,isNull(b.OPSTOCK,0) OPSTOCK,b.MRP from  BATDETL b,IPD_MedicineMaster m where b.compcode=m.compcode and ltrim(rtrim(b.icode))=ltrim(rtrim(m.ICODE)) and b.compcode='" + compcode + "' and b.yearcode='" + yearcode + "' /*and isnull(b.OPSTOCK,0)!=0*/";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    } 
}