using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for ToDoTask
/// </summary>
public class ToDoTask
{
    public ToDoTask(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable BedTable;


    public DataTable DropdownTask(string compcode)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select * from  IPD_ToDoTaskMaster  where Status=1 and compcode='"+compcode+"'";
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

    public DataTable Getonlypat(string compcode,string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select pr.PatientReg,pr.patient_name,CONVERT(varchar,pr.AdmissionDate,103) adate,bm.BedNoText  from IPD_BedMaster bm,IPD_BedAllocation ba,GN_PatientReg pr where bm.compcode=ba.compcode and bm.compcode=pr.compcode and pr.PatientReg=ba.PatientReg and ba.BedNo=bm.BedNo and pr.compcode='"+compcode+"' and  pr.PatientReg='" + reg + "' and ba.ToDate is null and ba.ToTime is null";

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
    public DataTable GridFill(string compcode,string RegNo)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(VARCHAR,t1.ToDoDate,103) Date1 from  IPD_ToDoTask t1,IPD_ToDoTaskMaster t2 where t1.compcode=t2.compcode and t1.ToDoTaskId=t2.TaskId and t1.compcode='"+compcode+"' AND t1.PatientReg='" + RegNo + "'";
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


    public bool Patienttask_insert_Update_Delete(int mode, string RowId, string PatientReg, string ToDoDate, string ToDoTaskId,string compcode,string user)
    {
        string LedgerId = "";
        int effectedRows = 0;
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
            string prvsts = string.Empty;
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "SELECT al.LedgerID FROM AC_Ledger al WHERE al.compcode='"+compcode+"' and al.LedgerFK='" + PatientReg + "' AND al.ActiveStatus=1";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();

                        if (mode == 1)
                            theCommand.CommandText = "INSERT INTO IPD_ToDoTask(compcode,LedgerId,PatientReg,ToDoDate,ToDoTaskId, Status,CompletedFlag,user01,logdt01) VALUES ('"+compcode+"','" + LedgerId + "','" + PatientReg + "','" + ToDoDate + "', '" + ToDoTaskId + "',1,'N','"+user+"',GETDATE())";
                        if (mode == 2)
                            theCommand.CommandText = "Update IPD_ToDoTask set LedgerId='" + LedgerId + "', ToDoDate='" + ToDoDate + "', ToDoTaskId='" + ToDoTaskId + "',user02='"+compcode+"',logdt02=GETDATE()  where compcode='"+compcode+"' and RowId = '" + RowId + "'";
                        if (mode == 3)
                            theCommand.CommandText = "delete IPD_ToDoTask where compcode='" + compcode + "' and RowId = '" + RowId + "'";
                        if (mode == 4)
                        {
                            theCommand.CommandText = "select CompletedFlag from IPD_ToDoTask where RowId = '" + RowId + "'";
                            theReader = theCommand.ExecuteReader();                           
                            if (theReader.Read())
                            {
                                prvsts = theReader[0].ToString();
                            }
                            else
                            { prvsts = "N"; }
                            theReader.Close();
                            if (prvsts == "Y")
                            {
                                theCommand.CommandText = "Update IPD_ToDoTask set CompletedFlag='N',user02='" + compcode + "',logdt02=GETDATE() where compcode='" + compcode + "' and RowId = '" + RowId + "'";
                            }
                            else
                            { theCommand.CommandText = "Update IPD_ToDoTask set CompletedFlag='Y',user02='" + compcode + "',logdt02=GETDATE() where compcode='" + compcode + "' and RowId = '" + RowId + "'"; }
                        }
                        theCommand.Transaction = tran as SqlTransaction;
                        effectedRows = theCommand.ExecuteNonQuery(); // Execute insert query.

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