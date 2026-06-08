using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for MedicineStockReport
/// </summary>
public class MedicineStockReport
{
	public MedicineStockReport(string con)
	{
        conString = con;
	}



    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable PurchaseMedicineTable;



    public DataTable DropdownSubGroup(string gr)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if (gr == "0")
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where status=1";
        else
            theCommand.CommandText = "select * from  IPD_MedicineSubGroup where status=1 and GroupID='" + gr + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
       DataTable MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        

        return MedicineTable;
    }

    public DataTable DropdownGroup()
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  IPD_MedicineGroup where status=1";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
      DataTable   MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    }

    public DataTable DropdownMedicine(string Group,string sub)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        if(Group=="0" || sub=="0")
            theCommand.CommandText = "select  * from dbo.IPD_MedicineMaster mm where mm.status=1";
        else
        theCommand.CommandText = "select  * from dbo.IPD_MedicineMaster mm where mm.status=1 and mm.MedicineGroupID='" + Group + "' and mm.SubGroupid='" + sub + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable MedicineTable = new DataTable();
        theAdapter.Fill(MedicineTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return MedicineTable;
    }


     
}