using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


public class DCDischargedtls
{
    public DCDischargedtls(string con)
    {
        conString = con;
    }


    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    private DataTable hospitalTable;


    public DataTable GetPatientDtls(string compcode,string regno)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *,CONVERT(varchar,d.DischargeDate,103) DDate from dbo.GN_PatientReg pr,GN_DischargeDetail d,GN_District di where pr.compcode=d.compcode and pr.compcode=di.compcode and pr.District=di.ID and pr.PatientReg=d.PatientReg and pr.PatientReg='" + regno + "' and pr.compcode='"+compcode+"'";
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

    public DataTable GetPatient(string compcode,string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  *   from dbo.GN_PatientReg pr , IPD_BedMaster bm where pr.compcode=bm.compcode and bm.BedNo=pr.bedallocation and pr.PatientReg='" + reg + "' and pr.compcode='"+compcode+"'";
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

    public DataTable GridFill(string compcode,string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  *,CONVERT(varchar,DischargeDate,103) DDate  from dbo.GN_DischargeDetail dd,GN_PatientReg pr where pr.compcode=dd.compcode and pr.PatientReg=dd.PatientReg and dd.DischargeDate is not null and pr.PatientReg='" + reg + "' and pr.compcode='"+compcode+"'";
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

    public DataTable CheckDue(string reg)
    {
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select  * from GN_PatientReg pr where pr.IsDue=1 and  pr.PatientReg='" + reg + "'";
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

    public bool InsertDischarge(string compcode,string bedno, string PatientReg, string PatientName, string DischargeDate, string DischargeTime, string Comment, string IsDeathFlag, string DeathDate, string DeathTime)
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

            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;

            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        // Update Appointment 
                        theCommand.CommandText = "update PH_PatientReq set Status=0 where RegistrationNo='" + PatientReg + "' and compcode='"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        // Update Appointment
                        theCommand.CommandText = "update DC_PatientAppointment set Status=0 where PatientReg='" + PatientReg + "' and compcode='"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        // Update BedAllocation in PatientReg 
                        theCommand.CommandText = "update GN_PatientReg  set bedallocation=0 where PatientReg='" + PatientReg + "' and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        // Update PatientReg
                        theCommand.CommandText = "update GN_PatientReg set CheckStatus=0 where PatientReg='" + PatientReg + "' and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        //update Monitor
                        theCommand.CommandText = "update DC_PatientMonitor set Status=0 where PatientReg='" + PatientReg + "' and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        // update Charge Details
                        theCommand.CommandText = "update DC_ChargeDetails set Status=0 where PatientReg='" + PatientReg + "' and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();


                        // Insert Patient_data 
                        theCommand.CommandText = "update IPD_BedMaster set Allotted=0 where BedNo='" + bedno + "' and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.



                        theCommand.CommandText = "select LedgerID from  AC_Ledger where LedgerFK='" + PatientReg + "' and  ActiveStatus=1 and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        string LadgerId = theReader[0].ToString();
                        theReader.Close();

                        // Update Discharge Details
                        theCommand.CommandText = "update GN_DischargeDetail set  IsDeathFlag='" + IsDeathFlag + "', DeathDate='" + DeathDate + "', DeathTime='" + DeathTime + "', DischargeDate='" + DischargeDate + "', DischargeTime='" + DischargeTime + "',Remarks='" + Comment + "'   where LadgerId='" + LadgerId + "' and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        // Update Ledger 
                        theCommand.CommandText = "update  AC_Ledger set  ActiveStatus=0   where LedgerFK='" + PatientReg + "' and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();
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

    public bool UpdateDayOff(string compcode,string id, string DischargeDate, string DischargeTime, string Comment)
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
                        theCommand.CommandText = "update GN_DischargeDetail set DischargeDate='" + DischargeDate + "', DischargeTime='" + DischargeTime + "',Comment='" + Comment + "'  where RowId = '" + id + "' and compcode='" + compcode + "'";
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

    public bool DeleteDayOff(string compcode,string id)
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
                        theCommand.CommandText = "delete GN_DischargeDetail  WHERE RowId='" + id + "' and compcode='"+compcode+"'";
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