using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for RecallPatientClass
/// </summary>
public class RecallPatientClass
{
	private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;
    public RecallPatientClass(string ConnectionString)
    {
        conString = ConnectionString;
    }

    public DataTable  PatientDetails(string regno,string compcode)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select h.PatientReg,h.patient_name,h.vill_city,h.LedgerId,CONVERT(VARCHAR,h.AdmissionDate,103) adate,dbo.fn_splitOnlyNames(d.doc_name) doc_name  from GN_PatientReg_History h ,GN_DoctorMaster d where d.compcode=h.compcode and d.doc_id=h.underdoctor and h.PatientReg='" + regno + "' and h.compcode='"+compcode+"'";
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

    public bool RecallPatient(string RegNo, string LedgerId)
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
            string BedNo = "",allotted="";
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    // transactional code...
                    using (theCommand = theConnection.CreateCommand())
                    {
                        theCommand.CommandText = "update AC_Ledger set ActiveStatus=1  where LedgerID='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.


                        theCommand.CommandText = "select bedallocation from GN_PatientReg where  LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader = theCommand.ExecuteReader();
                        theReader.Read();
                        BedNo = theReader[0].ToString();
                        theReader.Close();


                        theCommand.CommandText = "select Allotted from IPD_BedMaster where  BedNo='" + BedNo + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        allotted = theReader1[0].ToString();
                        theReader1.Close();


                        // RecallFlag =1 means bedallotted change    recallflag=2 means bedallotted unchange 

                        if (allotted == "1")
                        {
                            //Update PatientReg Table
                            theCommand.CommandText = "UPDATE GN_PatientReg  Set CheckStatus =1,RecallFlag=2  where PatientReg='" + RegNo + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.

                            String cd1 = "update IPD_BedMaster set Allotted=1 where BedNo='" + BedNo + "'";
                            theCommand.CommandText = cd1;
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }
                        else
                        {
                            //Update PatientReg Table
                            theCommand.CommandText = "UPDATE GN_PatientReg  Set CheckStatus =1,RecallFlag=1  where PatientReg='" + RegNo + "'";
                            theCommand.Transaction = tran as SqlTransaction;
                            theCommand.ExecuteNonQuery(); // Execute insert query.
                        }

                        String cd2 = "update IPD_BedAllocation set Todate=null, ToTime=null where LedgerId='" + LedgerId + "'  and BedNo='" + BedNo + "'";
                        theCommand.CommandText = cd2;
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        //Update Biopsy Note
                        theCommand.CommandText = "update IPD_OTBiopsyNote  set Status =1 where LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.


                        theCommand.CommandText = "update IPD_OTOperationNote  set Status =1 where LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update OT_DeliveryNote  set Status =1 where LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update IPD_AnesthesisNote  set Status =1 where LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update OT_Consumable  set Status =1 where LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update OT_AttendenceCharges  set Status =1 where LedgerId='" + LedgerId + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "update OT_InstrumentCost  set Status =1 where LedgerId='" + LedgerId + "'";
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