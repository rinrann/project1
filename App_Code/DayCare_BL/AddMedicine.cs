using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AddMedicine
/// </summary>
public class DC_AddMedicine
{
    
    public DC_AddMedicine(string con)
    {
        conString = con;
    }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;

    public DataTable GetAllPatientMedicine(string compcode, string yearcode, string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();


        string sql;
        sql = "select pa.AppDate from DC_PatientAppointment pa where pa.PatientReg='" + reg + "' and pa.status=1 and compcode='"+compcode+"'";
        SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
        DataTable dt = new DataTable();
        da.Fill(dt);
        string date = dt.Rows[0][0].ToString();

        theCommand.Connection = theConnection;

        
        theCommand.CommandText = "select pm.RowID,*,CONVERT(VARCHAR,pm.ExpirDate,103) ExDate,pm.PatientReg,pm.AdviceBy,pr.patient_name,mm.MedicineName,mg.MedicineGroupName,pm.Quantity,CONVERT(varchar,pr.AdmissionDate,103) adate,CONVERT(varchar,pm.IssueDate,103) isdate " +
           "from GN_PatientReg pr,IPD_MedicineMaster mm,dbo.IPD_MedicineGroup mg,dbo.DC_PatientMedicine pm " +
           "where pr.compcode=mm.compcode and pr.compcode=mg.compcode and pr.compcode=pm.compcode and pr.PatientReg=pm.PatientReg and " +
           "pm.MedicineId=mm.MedicineID and mm.MedicineGroupID=mg.MedicineGroupID and pr.compcode='" + compcode + "' and pm.yearcode='" + yearcode + "' and pr.PatientReg='" + reg + "' and  pm.IssueDate>='" + Convert.ToDateTime( date).ToString("yyyyMMdd") + "'";
       
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataTable BedTable = new DataTable();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        da.Dispose();
        return BedTable;
    }
     
    public DataSet GetMedicine_Bill(string compcode,string yearcode,string reg)
    {
        if (reg == "")
            reg = "null";
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand(); 

        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec [sp_DC_MedicineBill] '"+compcode+"','"+yearcode+"'," + reg + "";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        DataSet BedTable = new  DataSet();
        theAdapter.Fill(BedTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return BedTable;
    }



    public bool InsertAddedicine(string compcode, string yearcode, string PatientReg, string MedicineGrpId, string MedicineId, string IssueDate, string AdviceBy, string MedicineSubGrId, string BillQty, string BatchNo, string ExpirDate)
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
                        theCommand.CommandText = "INSERT INTO DC_PatientMedicine(compcode,yearcode,PatientReg,MedicineGrpId,MedicineId,IssueDate,AdviceBy,MedicineSubGrId,BillQty,BatchNo,ExpirDate) VALUES('" + compcode + "','" + yearcode + "','" + PatientReg + "','" + MedicineGrpId + "','" + MedicineId + "','" + IssueDate + "','" + AdviceBy + "','" + MedicineSubGrId + "','" + BillQty + "','" + BatchNo + "','" + ExpirDate + "')";
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

    public bool UpdateAddedicine(string compcode, string yearcode, string RowID, string MedicineGrpId, string MedicineId, string IssueDate, string AdviceBy, string MedicineSubGrId, string BillQty, string BatchNo, string ExpirDate)
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
                        theCommand.CommandText = "update DC_PatientMedicine set MedicineGrpId='" + MedicineGrpId + "',MedicineId='" + MedicineId + "',IssueDate='" + IssueDate + "',AdviceBy='" + AdviceBy + "',MedicineSubGrId='" + MedicineSubGrId + "',BillQty='" + BillQty + "',BatchNo='" + BatchNo + "',ExpirDate='" + ExpirDate + "'  where RowID ='" + RowID + "' and compcode='"+compcode+"' and yearcode='"+yearcode+"'";
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


    public bool DeleteAddmedicine(string RowID)
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

                        theCommand.CommandText = "delete from DC_PatientMedicine where RowID = '" + RowID + "'";
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