using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PH_SampleCollection
/// </summary>
public class PH_SampleCollection
{
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;

    public PH_SampleCollection(string constring)
	{
        conString = constring;
	}



    public DataTable GetPatientRequisitionList(string compcode, string regno)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT RequisitionNo FROM PH_PatientReq WHERE compcode='" + compcode + "' and RegistrationNo='" + regno + "' ";
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

    public DataTable getSampleList(string compcode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT LTrim(RTrim(SCode)) SCode,SName FROM PH_SpecimanMaster WHERE compcode='" + compcode + "' ";
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

    public DataTable getAgencyList(string compcode,string yearcode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT LTrim(RTrim(SlCode))SlCode,SlName FROM slmast WHERE compcode='" + compcode + "' and category='A' and glcode=(select creditor from parms where compcode='" + compcode + "' and yearcode='" + yearcode + "') ";
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


    public bool InsertSampleCollection(string compcode, string yearcode, string regno, string reqno, string samplecode, string unit, string agencycode, string TestDate, string DeliveryDate, string collectorName, string agencyAmt, string partyAmt, string user, string paymentMode, string BankName, string ChequeNo, string chqdt)
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

            //// Command.
            theCommand = new SqlCommand();
            // string s = Session["userName"];
            theCommand.Connection = theConnection;

            //string InsertQuery = "INSERT INTO PH_SampleCollect(CompCode,YearCode,RegistrationNo,RequisitionNo,SampleCode,Unit,AgencyCode,CollectorName,CollectDate,DeliveryDate,AgencyBillAmt,PatientBillAmt,user01,logdt01,PaymentMode,Chq_CardNo,Bank_CardHolderName,ChqDt_CardExpDt)";
            //InsertQuery += "Values('" + compcode + "','" + yearcode + "','" + regno + "','" + reqno + "','" + samplecode + "','" + unit + "','" + agencycode + "','" + collectorName + "',convert(date,'" + TestDate + "',103),convert(date,'" + DeliveryDate + "',103),'" + agencyAmt + "','" + partyAmt + "','" + user + "',getdate(),'" + paymentMode + "','" + ChequeNo + "','" + BankName + "','" + chqdt + "')";
            //theCommand.CommandText = InsertQuery; 
            //theCommand.CommandType = CommandType.Text;

            //theCommand.ExecuteNonQuery(); // Execute insert query.
            using (IDbTransaction tran = theConnection.BeginTransaction())
            {
                try
                {
                    string LedgerId = "";
                    theCommand.CommandText = "select LedgerID from AC_Ledger al where LedgerFK='" + regno + "' and ActiveStatus=1 and compcode='" + compcode + "'";
                    theCommand.Transaction = tran as SqlTransaction;
                    SqlDataReader theReader = theCommand.ExecuteReader();
                    theReader.Read();
                    LedgerId = theReader[0].ToString();
                    theReader.Close();

                    //string bookCode = "";
                    //if (paymentMode == "C")
                    //{
                    //    theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='C'";
                    //}
                    //else
                    //{
                    //    theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='B'";
                    //}
                    //theCommand.Transaction = tran as SqlTransaction;
                    //SqlDataReader theReader2 = theCommand.ExecuteReader();
                    //theReader2.Read();
                    //bookCode = theReader2[0].ToString();
                    //theReader2.Close();


                    //theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','" + bookCode + "','I','R','Y',''";
                    //theCommand.Transaction = tran as SqlTransaction;
                    //SqlDataReader theReader3 = theCommand.ExecuteReader();
                    //theReader3.Read();
                    //string Vchno = theReader3[0].ToString();
                    //theReader3.Close();

                    using (theCommand = theConnection.CreateCommand())
                    {
                        string InsertQuery = "INSERT INTO PH_SampleCollect(CompCode,YearCode,RegistrationNo,RequisitionNo,SampleCode,Unit,AgencyCode,CollectorName,CollectDate,DeliveryDate,AgencyBillAmt,PatientBillAmt,Dueamt,user01,logdt01,PaymentMode,Chq_CardNo,Bank_CardHolderName,ChqDt_CardExpDt,VchNo)";
                        InsertQuery += "Values('" + compcode + "','" + yearcode + "','" + regno + "','" + reqno + "','" + samplecode + "','" + unit + "','" + agencycode + "','" + collectorName + "',convert(date,'" + TestDate + "',103),convert(date,'" + DeliveryDate + "',103),'" + agencyAmt + "','" + partyAmt + "','" + partyAmt + "','" + user + "',getdate(),'" + paymentMode + "','" + ChequeNo + "','" + BankName + "','" + chqdt + "','')";
                        theCommand.CommandText = InsertQuery;
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        //if (Convert.ToDecimal(partyAmt) > 0)
                        //{
                            

                        //    theCommand.CommandText = "exec Dsp_CollectionAccEffect '" + compcode + "','" + yearcode + "','" + user + "','" + Vchno + "',convert(date,'" + TestDate + "',103),'" + bookCode + "','" + LedgerId + "','" + partyAmt + "',2";
                        //    theCommand.Transaction = tran as SqlTransaction;
                        //    theCommand.ExecuteNonQuery(); // Execute insert query. 

                        //    theCommand.CommandText = "exec Dsp_GenDocNo '" + compcode + "','" + yearcode + "','','" + bookCode + "','U','R','Y',''";
                        //    theCommand.Transaction = tran as SqlTransaction;
                        //    SqlDataReader theReader4 = theCommand.ExecuteReader();
                        //    theReader4.Read();
                        //    theReader4.Close();
                        //}
                    }
                    tran.Commit();
                }
                catch (Exception ex)
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


    public DataTable GridFill(string compcode,string yearcode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        string lsSql = "select SC.SrlNo,SC.RegistrationNo,SC.RequisitionNo,PR.PName,DM.doc_name,SM.SName,SC.Unit,Convert(varchar(10),SC.CollectDate,103) ColDate,Sl.SlName AgencyName,SC.VchNo  " +
            "from PH_SampleCollect SC,OPD_PatientRegistration PR,gn_doctormaster DM,PH_SpecimanMaster Sm,SlMast Sl "+
            "where PR.Compcode=SC.compcode and PR.YEARCODE=SC.Yearcode and PR.PatientRegNo=SC.RegistrationNo and "+
            "PR.COMPCODE=DM.COMPCODE and PR.DocId=Dm.doc_id and SC.CompCode=Sm.COMPCODE and Sm.SCode=SC.SampleCode and "+
            "Sl.compcode=SC.compcode and Sl.Slcode=SC.AgencyCode and Sl.glcode=(select creditor from parms where compcode='"+ compcode +"' and yearcode='"+ yearcode +"') "+
            "and SC.compcode='" + compcode + "' and Sc.YearCode='" + yearcode + "'";
        theCommand.CommandText = lsSql;
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

    public bool UpdateSampleCollection(string compcode, string yearcode, string srlno, string regno, string reqno, string samplecode, string unit, string agencycode, string TestDate, string DeliveryDate, string collectorName, string agencyAmt, string partyAmt, string user, string paymentMode, string BankName, string ChequeNo, string chqdt,string vchno,string reqCancel,string cancelReason)
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

            string UpdateQuery = "Update PH_SampleCollect set RequisitionNo='" + reqno + "',SampleCode='" + samplecode + "',Unit='" + unit + "',AgencyCode='" + agencycode + "',CollectorName='" + collectorName + "',CollectDate=convert(date,'" + TestDate + "',103),DeliveryDate=convert(date,'" + DeliveryDate + "',103),AgencyBillAmt='" + agencyAmt + "',PatientBillAmt='" + partyAmt + "',DueAmt='" + partyAmt + "',user02='" + user + "',logdt02=getdate(),PaymentMode='" + paymentMode + "',Chq_CardNo='" + ChequeNo + "',Bank_CardHolderName='" + BankName + "',ChqDt_CardExpDt='" + chqdt + "',CancelFlag='" + reqCancel + "',CancelReason='" + cancelReason + "' ";
            UpdateQuery = UpdateQuery + " where compcode='" + compcode + "' and yearcode='" + yearcode + "' and SrlNo='" + srlno + "'";
            theCommand.CommandText = UpdateQuery;
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.

            //if (Convert.ToDecimal(partyAmt) > 0)
            //{
            //    string sql;
            //    string ledgerid;
            //    SqlDataAdapter da;
            //    DataTable dt = new DataTable();
            //    sql = "select LedgerID from AC_Ledger al where LedgerFK='" + regno + "' and ActiveStatus=1 and compcode='" + compcode + "'";
            //    da = new SqlDataAdapter(sql, theConnection);
            //    da.Fill(dt);
            //    if (dt.Rows.Count > 0)
            //    {
            //        ledgerid = dt.Rows[0][0].ToString();
            //    }
            //    else
            //    {
            //        sql = "exec sp_GenerateLedgerId '" + compcode + "','P'";
            //        da = new SqlDataAdapter(sql, theConnection);
            //        da.Fill(dt);
            //        ledgerid = dt.Rows[0][0].ToString();
            //    }

            //    string bookCode = "";
            //    if (paymentMode == "C")
            //    {
            //        theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='C'";
            //    }
            //    else
            //    {
            //        theCommand.CommandText = "select bkcode from bkcodes where compcode='" + compcode + "' and bktype='B'";
            //    }
            //    //theCommand.Transaction = tran as SqlTransaction;
            //    SqlDataReader theReader2 = theCommand.ExecuteReader();
            //    theReader2.Read();
            //    bookCode = theReader2[0].ToString();
            //    theReader2.Close();



            //    theCommand.CommandText = "exec Dsp_CollectionAccEffect '" + compcode + "','" + yearcode + "','" + user + "','" + vchno + "','" + TestDate + "','" + bookCode + "','" + ledgerid + "','" + partyAmt + "',1";
            //    theCommand.CommandType = CommandType.Text;
            //    theCommand.ExecuteNonQuery(); // Execute insert query. 
            //}
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


    public DataTable getSampleCollectionDetls(string compcode, string yearcode, string srlno)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        string lsSql = "Select SrlNo,RegistrationNo,RequisitionNo,SampleCode,Unit,AgencyCode,CollectorName,convert(varchar(10),CollectDate,103) CollectDate,Convert(varchar(10),DeliveryDate,103) DeliveryDate,AgencyBillAmt,PatientBillAmt,isNull(VchNo,'') VchNo,isnull(PaymentMode,'C')PaymentMode,Chq_CardNo,case isNull(ChqDt_CardExpDt,'') when '' then '' else convert(varchar(10),isNull(ChqDt_CardExpDt,''),103) end as ChqDt_CardExpDt,Bank_CardHolderName,isNull(CancelFlag,'0') CancelFlag,CancelReason  from PH_SampleCollect where compcode='" + compcode + "' and yearcode='" + yearcode + "' and srlno=" + srlno + "";
        theCommand.CommandText = lsSql;
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


    public bool DeleteSampleCollection(string compcode,string yearcode,string srlno,string vchno)
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
            theCommand.CommandText = "delete from PH_SampleCollect  where compcode='" + compcode + "' abd yearcode='" + yearcode + "' and SrlNo='" + srlno + "'";
            theCommand.CommandType = CommandType.Text;

            theCommand.ExecuteNonQuery(); // Execute insert query.


            theCommand = new SqlCommand();
            theCommand.Connection = theConnection;
            theCommand.CommandText = "delete from inp  where and compcode='" + compcode + "' and yearcode='" + yearcode + "' and vchno='" + vchno + "'";
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