using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Discharge123
/// </summary>
public class DischargeDtls
{
	public DischargeDtls(string con)
	{
        conString = con;
	}

    public string LedgerId { get; set; }
    public string DoctorLedgerId { get; set; }

    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;


    public DataTable OnlyName(string reg)
    {
        if (reg == "")
            reg = "null";
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select pr.patient_name,CONVERT(VARCHAR,pr.ProbableDischargeDate,103) ProbDisDate, bm.BedNoText,bm.BedNo,convert(varchar,pr.AdmissionDate,103) adate,pr.AdmissionTime,pr.ReferID,pr.ReferName from GN_PatientReg pr,IPD_BedAllocation ba ,IPD_BedMaster bm  where ba.BedNo=bm.BedNo and ba.PatientReg =pr.PatientReg and pr.PatientReg='" + reg + "' and ba.ToDate is null and ba.ToTime is null";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataTable GridFill(string reg,string compcode,string yearcode)
    {
        if (reg == "")
            reg = "null";
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "select *,CONVERT(varchar,dd.DischargeDate,103) ddate from dbo.GN_DischargeDetail dd,GN_PatientReg pr where pr.compcode=dd.compcode and pr.PatientReg=dd.PatientReg and dd.compcode='"+compcode+"' and dd.yearcode='"+yearcode+"' and  dd.PatientReg='" + reg + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataTable();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public bool  InsertDischarge(string IsDeathFlag,string DeathDate,string DeathTime, string BedNo, string PatientReg, string DischargeDate, string DischargeTime, string DischargeCondition,string admissiondate,string admissiontime,string RefDocid,string DocComm,string CreteUser,string cocode,string yearcode,string user)
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

            //string LedgerId = "";
            string RecallFlag = "";
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "select LedgerID from AC_Ledger where LedgerFK='" + PatientReg + "' and ActiveStatus=1 and compcode='" + cocode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        LedgerId = theReader[0].ToString();
                        theReader.Close();



                        theCommand.CommandText = "select * from GN_DischargeDetail where LadgerId='" + LedgerId + "' and compcode='" + cocode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader2 = theCommand.ExecuteReader();
                        theReader2.Read(); 
                  
                        if (theReader2.HasRows)
                        {
                            theReader2.Close();
                            theReader2.Close();
                            theCommand.CommandText = "update GN_DischargeDetail set IsDeathFlag='" + IsDeathFlag + "',DeathDate='" + DeathDate + "',DeathTime='" + DeathTime + "',DischargeDate='" + DischargeDate + "',DischargeTime='" + DischargeTime + "',Remarks='" + DischargeCondition + "',AdmissionDate='" + admissiondate + "',AdmissionTime='" + admissiontime + "',user02='"+user+"',logdt02=GETDATE() where LadgerId='" + LedgerId + "' and PatientReg='" + PatientReg + "' and compcode='" + cocode + "' and yearcode='"+yearcode+"'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            theReader2.Close();
                            theCommand.CommandText = "INSERT INTO GN_DischargeDetail(compcode,yearcode,LadgerId,IsDeathFlag,DeathDate,DeathTime,PatientReg,DischargeDate,DischargeTime,Remarks,AdmissionDate,AdmissionTime,user01,logdt01) VALUES('" + cocode + "','"+yearcode+"','" + LedgerId + "','" + IsDeathFlag + "','" + DeathDate + "','" + DeathTime + "','" + PatientReg + "','" + DischargeDate + "','" + DischargeTime + "','" + DischargeCondition + "','" + admissiondate + "','" + admissiontime + "','"+user+"',GETDATE())";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }


                        theCommand.CommandText = "select RecallFlag from GN_PatientReg where PatientReg='" + PatientReg + "' and compcode='" + cocode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        RecallFlag = theReader1[0].ToString();
                        theReader1.Close();

                        if (RecallFlag == "1" || RecallFlag=="")
                        {
                            String cd1 = "update IPD_BedMaster set Allotted=0 where BedNo='" + BedNo + "' and compcode='" + cocode + "'";
                            theCommand.CommandText = cd1;
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        if (Convert.ToDecimal(DocComm) > 0)
                        {
                            theCommand.CommandText = "select LedgerID from AC_Ledger where LedgerFK='" + RefDocid + "' and ActiveStatus=1 and compcode='" + cocode + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            SqlDataReader theReader3 = theCommand.ExecuteReader();
                            theReader3.Read();
                            DoctorLedgerId = theReader3[0].ToString();
                            theReader3.Close();

                            theCommand.CommandText = "insert into AC_Transaction (compcode,yearcode,LadgerId,Debit,TrunsactionDate,EntryBy,Reason,paymentfor,RegNo)values('" + cocode + "','" + yearcode + "','" + DoctorLedgerId + "','" + DocComm + "','" + DischargeDate + "','" + CreteUser + "','Refer Patient','" + LedgerId + "','" + PatientReg + "')";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery();
                        }
                        String cd2 = "update IPD_BedAllocation set Todate='" + DischargeDate + "' , ToTime='" + DischargeTime + "' where PatientReg='" + PatientReg + "'  and  Todate is null and ToTime is null and compcode='" + cocode + "'";
                        theCommand.CommandText = cd2;
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.


                        //Update PatientReg Table
                        theCommand.CommandText = "update GN_PatientReg  set CheckStatus =0 where PatientReg='" + PatientReg + "' and compcode='" + cocode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.


                        //Update Biopsy Note
                        theCommand.CommandText = "update IPD_OTBiopsyNote  set Status =0 where PatientRegId='" + PatientReg + "' and compcode='" + cocode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.


                        theCommand.CommandText = "update IPD_OTOperationNote  set Status =0 where PatientRegId='" + PatientReg + "' and compcode='" + cocode + "' and yearcode='"+yearcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update OT_DeliveryNote  set Status =0 where PatientReg='" + PatientReg + "' and compcode='" + cocode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update IPD_AnesthesisNote  set Status =0 where PatientRegId='" + PatientReg + "' and compcode='" + cocode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update OT_Consumable  set Status =0 where PatientRegNo='" + PatientReg + "' and compcode='" + cocode + "' and yearcode='"+yearcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update OT_AttendenceCharges  set Status =0 where PatientRegId='" + PatientReg + "' and compcode='" + cocode + "' and yearcode='"+yearcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update OT_InstrumentCost  set Status =0 where PatientReg='" + PatientReg + "' and compcode='" + cocode + "' and yearcode='" + yearcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update AC_Ledger  set ActiveStatus =0 where LedgerID='" + LedgerId + "' and LedgerFK='" + PatientReg + "' and compcode='" + cocode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery();

                        theCommand.CommandText = "exec sp_IPD_UpdatePrescriptionDischarge '" + LedgerId + "','" + DischargeDate + "','" + cocode + "'";
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

    public bool updateDischarge(string IsDeathFlag, string DeathDate, string DeathTime, string id, string PatientReg, string DischargeDate, string DischargeTime, string DischargeCondition, string RefDocid, string DocComm, string cocode, string yearcode)
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
                        using (  theCommand = theConnection.CreateCommand())
                        {
                            theCommand.CommandText = "update  GN_DischargeDetail set  IsDeathFlag='" + IsDeathFlag + "', DeathDate='" + DeathDate + "', DeathTime='" + DeathTime + "', DischargeDate='" + DischargeDate + "',DischargeTime='" + DischargeTime + "',Remarks='" + DischargeCondition + "' where RowId='" + id + "' and compcode='" + cocode + "'";
            theCommand.Transaction = tran as SqlTransaction;
            theCommand.ExecuteNonQuery(); // Execute insert query.

            theCommand.CommandText = "select LadgerID from GN_DischargeDetail where RowId='" + id + "' and compcode='" + cocode + "'";
            theCommand.Transaction = tran as SqlTransaction;
            SqlDataReader theReader = theCommand.ExecuteReader();
            theReader.Read();
            LedgerId = theReader[0].ToString();
            theReader.Close();

            theCommand.CommandText = "select LedgerID from AC_Ledger where LedgerFK='" + RefDocid + "' and ActiveStatus=1 and compcode='" + cocode + "'";
            theCommand.Transaction = tran as SqlTransaction;
            SqlDataReader theReader3 = theCommand.ExecuteReader();
            theReader3.Read();
            DoctorLedgerId = theReader3[0].ToString();
            theReader3.Close();

            theCommand.CommandText = "update  AC_Transaction set  Debit='" + DocComm + "' where LadgerId='" + DoctorLedgerId + "' and paymentfor='" + LedgerId + "' and reason like 'Refer Patient%' and compcode='" + cocode + "'";
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
            theConnection.Close();
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