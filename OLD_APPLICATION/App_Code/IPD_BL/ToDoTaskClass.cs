using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ToDoTaskClass
/// </summary>
public class ToDoTaskClass
{
    public ToDoTaskClass(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable ToDoTask;
    private DataTable ToDoTask1;
    private DataTable UpdateGrid;


    public DataTable GridFill(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select pr.PatientReg,pr.patient_name,pr.vill_city as Address from GN_PatientReg pr,IPD_ToDoTask tk where tk.compcode=pr.compcode and pr.PatientReg =tk.PatientReg and pr.compcode='"+compcode+"' group by pr.PatientReg,pr.patient_name,pr.vill_city";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        ToDoTask = new DataTable();
        theAdapter.Fill(ToDoTask); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return ToDoTask;
    }


    public DataTable GridFill1(string RegNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  tdt.RowId,tdtm.TaskName,CONVERT(varchar,tdt.ToDoDate,103) ToDate,tdt.TaskBy,tdt.TaskTime,CONVERT(varchar,tdt.TaskDate,103) TaskDate from IPD_ToDoTask tdt,IPD_ToDoTaskMaster tdtm,GN_PatientReg pr where tdt.ToDoTaskId=tdtm.TaskId and tdt.PatientReg =pr.PatientReg and pr.PatientReg='" + RegNo + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        ToDoTask1 = new DataTable();
        theAdapter.Fill(ToDoTask1); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return ToDoTask1;
    }


    public bool Patienttask_Entry(string Id, string TaskBy, string TaskTime, string TaskDate,string compcode,string user)
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
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "Update IPD_ToDoTask set TaskBy='" + TaskBy + "', TaskTime='" + TaskTime + "',TaskDate='" + TaskDate + "',user02='"+compcode+"',logdt02=GETDATE()  where RowId = '" + Id + "' and compcode='"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.
                    }
                    tran.Commit();
                }
                catch
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
}