using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Discharge
/// </summary>
public class Discharge
{
	public Discharge(string constring)
	{
        conString = constring;
	}

  


 
    private string conString;
    private SqlConnection theConnection;
    private SqlCommand theCommand;
    private SqlDataAdapter theAdapter;

 
    public DataTable  DischargeBill(string regno)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT *,dbo.AmountToWords(Total-Discount-DueAmount) words FROM IPD_PatientBillDtls WHERE PatientRegNo='" + regno + "' ";
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
    public DataTable Ddiscount(string regno, string adate)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;
        string[] a=adate.Split('/');
        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT 'Discount',isnull(sum(isnull(credit,0)),0) amt FROM AC_Transaction WHERE LadgerId='" + regno + "' and trunsactionDate>='" + a[2]+a[1]+a[0] + "' and reason like 'Discount Amount%' " +
            "union SELECT 'Due',isnull(sum(isnull(debit,0)),0) amt FROM AC_Transaction WHERE LadgerId='" + regno + "' and trunsactionDate>='" + a[2] + a[1] + a[0] + "' and reason like 'Due Amount%' ";
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
    public DataTable AmtToWords(string amt)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select dbo.AmountToWords('" + Convert.ToDecimal(amt) + "') words ";
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
    public DataTable GetBillDtls(string regno)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandTimeout = 0;
        theCommand.CommandText = "select * from IPD_PatientBillDtls where PatientRegNo='" + regno + "'";
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

    public bool InsertPatientBilldtls(string compcode,string LedgerId, string otAttendence,string otConsumable, string AnesthesiaConsumable,string AnesthesiaMedicine, string BillNo, string PatientRegNo, string BillDate, string BedCharge, string DoctorVisit, string Medicine, string Consumable, string SeviceDtls, string Pathology, string XRay, string USG, string OTCharges, string SisterAya, string Ambulance, string Instrument)
    {
        double Total;
        string Discount;
        string Due;
        try
        {
            if (otAttendence == "")
                otAttendence = "0.00";

            if (otConsumable == "")
                otConsumable = "0.00";

            if (AnesthesiaConsumable == "")
                AnesthesiaConsumable = "0.00";

            if (AnesthesiaMedicine == "")
                AnesthesiaMedicine = "0.00";



            if (BedCharge == "")
                BedCharge = "0.00";

            if (DoctorVisit == "")
                DoctorVisit = "0.00";

            if (Medicine == "")
                Medicine = "0.00";

            if (Consumable == "")
                Consumable = "0.00";

            if (SeviceDtls == "")
                SeviceDtls = "0.00";

            if (Pathology == "")
                Pathology = "0.00";

            if (XRay == "")
                XRay = "0.00";

            if (USG == "")
                USG = "0.00";

            if (OTCharges == "")
                OTCharges = "0.00";


            if (SisterAya == "")
                SisterAya = "0.00";


            if (Ambulance == "")
                Ambulance = "0.00";


            if (Instrument == "")
                Instrument = "0.00";

            Total = Convert.ToDouble(otAttendence) + Convert.ToDouble(otConsumable) + Convert.ToDouble(AnesthesiaConsumable) + Convert.ToDouble(AnesthesiaMedicine) + Convert.ToDouble(BedCharge) + Convert.ToDouble(DoctorVisit) + Convert.ToDouble(Medicine) + Convert.ToDouble(Consumable) + Convert.ToDouble(SeviceDtls) + Convert.ToDouble(Pathology) + Convert.ToDouble(XRay) + Convert.ToDouble(USG) + Convert.ToDouble(OTCharges) + Convert.ToDouble(SisterAya) + Convert.ToDouble(Ambulance) + Convert.ToDouble(Instrument);

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
                        //theCommand.CommandText = "select LedgerID from AC_Ledger al where LedgerFK='" + PatientRegNo + "' and ActiveStatus=1";
                        //theCommand.Transaction = tran as SqlTransaction;
                        //SqlDataReader theReader = theCommand.ExecuteReader();
                        //theReader.Read();
                        //LedgerId = theReader[0].ToString();
                        //theReader.Close();


                        theCommand.CommandText = "SELECT (CASE WHEN SUM(at.Credit) is null THEN 0 ELSE SUM(at.Credit) END) Discount FROM AC_Transaction at  WHERE at.LadgerId='" + LedgerId + "' and compcode='"+compcode+"' AND at.PaymentType=3";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader1 = theCommand.ExecuteReader();
                        theReader1.Read();
                        Discount = theReader1[0].ToString();
                        theReader1.Close();


                        theCommand.CommandText = "SELECT (CASE WHEN SUM(at.Debit) is null THEN 0 ELSE SUM(at.Debit) END) Discount FROM AC_Transaction at  WHERE at.LadgerId='" + LedgerId + "' AND at.PaymentType=2 and compcode='"+compcode+"'";
                        theCommand.Transaction = tran as SqlTransaction;
                        SqlDataReader theReader2 = theCommand.ExecuteReader();
                        theReader2.Read();
                        Due = theReader2[0].ToString();
                        theReader2.Close();


                        theCommand.CommandText = "delete IPD_PatientBillDtls where LedgerId ='" + LedgerId + "' and compcode='" + compcode + "'";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "insert into  IPD_PatientBillDtls(compcode,DueAmount,OTAttendenceCharge,OTConsumableharge,AnesthesiaMedicine,AnesthesiaConsumable,LedgerId,BillNo,PatientRegNo,BillDate,BedCharge,DoctorVisit,Medicine,Consumable,SeviceDtls,Pathology,XRay,USG,OTCharges,SisterAya,Ambulance,Instrument,Discount,Total)  values('" + compcode + "','" + Due + "','" + otAttendence + "','" + otConsumable + "','" + AnesthesiaMedicine + "','" + AnesthesiaConsumable + "','" + LedgerId + "','" + BillNo + "','" + PatientRegNo + "','" + BillDate + "','" + BedCharge + "','" + DoctorVisit + "','" + Medicine + "','" + Consumable + "','" + SeviceDtls + "','" + Pathology + "','" + XRay + "','" + USG + "','" + OTCharges + "','" + SisterAya + "','" + Ambulance + "','" + Instrument + "','" + Discount + "','" + Total + "')";
                        theCommand.Transaction = tran as SqlTransaction;
                        theCommand.ExecuteNonQuery(); // Execute insert query.

                        theCommand.CommandText = "UPDATE AC_Ledger SET ActiveStatus=0 WHERE LedgerID='" + LedgerId + "' and compcode='" + compcode + "'";
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

    public DataTable PatientChecking(string regno,string compcode)
    {
        DataTable custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "select *  from IPD_PatientBillDtls where PatientRegNo='" + regno + "' and compcode='" + compcode + "'";
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

    public DataSet PatientForReport(string regno,string cocode)
    {
        DataSet custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) adate from GN_DoctorMaster dm,dbo.GN_PatientReg pr,IPD_BedAllocation ba,IPD_BedMaster bm where pr.underDoctor=dm.doc_id and bm.BedNo=ba.BedNo and pr.PatientReg=ba.PatientReg and  ba.PatientReg='" + regno + "' and ba.FromTime=( select MAX(FromTime) from IPD_BedAllocation WHERE PatientReg='" + regno + "' and ba.FromDate=(select MAX(FromDate) from IPD_BedAllocation WHERE PatientReg='" + regno + "'))";
        //theCommand.CommandText = "exec  sp_GetPatientdetails_ForReport " + regno + ",'" + cocode + "'"; 
        theCommand.CommandText = "exec  Dsp_GetOPDPatientDetails '" + cocode + "','" + regno + "'"; 
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
           custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataTable PatientDetailsForRequisition(string regno, string cocode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) adate from GN_DoctorMaster dm,dbo.GN_PatientReg pr,IPD_BedAllocation ba,IPD_BedMaster bm where pr.underDoctor=dm.doc_id and bm.BedNo=ba.BedNo and pr.PatientReg=ba.PatientReg and  ba.PatientReg='" + regno + "' and ba.FromTime=( select MAX(FromTime) from IPD_BedAllocation WHERE PatientReg='" + regno + "' and ba.FromDate=(select MAX(FromDate) from IPD_BedAllocation WHERE PatientReg='" + regno + "'))";
        //theCommand.CommandText = "exec  sp_GetPatientdetails_ForReport " + regno + ",'" + cocode + "'"; 
        theCommand.CommandText = "exec  Dsp_GetOPDPatientDetails '" + cocode + "','" + regno + "'";
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



    public DataSet GetDoctors(string compcode)
    {
        DataSet docTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) adate from GN_DoctorMaster dm,dbo.GN_PatientReg pr,IPD_BedAllocation ba,IPD_BedMaster bm where pr.underDoctor=dm.doc_id and bm.BedNo=ba.BedNo and pr.PatientReg=ba.PatientReg and  ba.PatientReg='" + regno + "' and ba.FromTime=( select MAX(FromTime) from IPD_BedAllocation WHERE PatientReg='" + regno + "' and ba.FromDate=(select MAX(FromDate) from IPD_BedAllocation WHERE PatientReg='" + regno + "'))";
        theCommand.CommandText = "select DrRegNo as doc_id,doc_name,Address,Qualification from GN_DoctorMaster where compcode='"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        docTable = new DataSet();
        theAdapter.Fill(docTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return docTable;
    }
    public DataTable GetPayments(string compcode,string yearcode,string LedgerId)
    {
        DataTable docTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        //theCommand.CommandText = "select *,CONVERT(varchar,pr.AdmissionDate,103) adate from GN_DoctorMaster dm,dbo.GN_PatientReg pr,IPD_BedAllocation ba,IPD_BedMaster bm where pr.underDoctor=dm.doc_id and bm.BedNo=ba.BedNo and pr.PatientReg=ba.PatientReg and  ba.PatientReg='" + regno + "' and ba.FromTime=( select MAX(FromTime) from IPD_BedAllocation WHERE PatientReg='" + regno + "' and ba.FromDate=(select MAX(FromDate) from IPD_BedAllocation WHERE PatientReg='" + regno + "'))";
        theCommand.CommandText = "select dbo.Fnc_GetPatientPayment('" + compcode + "','" + yearcode + "','" + LedgerId + "',1)pay_amt,dbo.Fnc_GetPatientPayment('" + compcode + "','" + yearcode + "','" + LedgerId + "',2)due_amt,dbo.Fnc_GetPatientPayment('" + compcode + "','" + yearcode + "','" + LedgerId + "',3)discount_amt";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        docTable = new DataTable();
        theAdapter.Fill(docTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return docTable;
    }
    public string CommisionCalRule(string cocode,string yearcode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT isnull(CommCalcRule,'A')CommCalcRule FROM parms fm WHERE compcode='" + cocode + "' and yearcode='"+yearcode+"'";
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
        return custTable.Rows[0]["CommCalcRule"].ToString();
    }
    public DataTable doctorCommision(string docid,string cocode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT isnull(Commission_Per,0)Commission_Per,isnull(Commission_Rs,0)Commission_Rs FROM GN_DoctorMaster fm WHERE doc_id='" + docid + "' and compcode='" + cocode + "' union SELECT isnull(Commission_Per,0)Commission_Per,isnull(Commission_Rs,0)Commission_Rs FROM GN_QuackMaster fm WHERE quackid='" + docid + "' and compcode='" + cocode + "'";
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
    public DataTable GetReportDetails(string id,string compcode)
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "SELECT fm.ID,fm.FormName,fm.FormContext FROM IPD_FormMaster fm WHERE fm.ID='" + id + "' and compcode='"+compcode+"'";
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


    public DataTable GenerateBillNo()
    {
        DataTable custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "EXEC sp_IPD_GenerateBillNo";
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



    public DataSet SisterAyaBill(string regno, string compcode)
    {
        DataSet custTable;

        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_SisterAyaBill " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet OTAttendenceBill(string regno,string compcode)
    {
        DataSet custTable;

        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec  sp_IPD_OT_AttendenceCharge_Bill   " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();
        return custTable;
    }


    public DataSet OTConsumableBill(string regno, string compcode)
    {
        DataSet custTable;

        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_OTConsumableBill " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet AmbulanceBill(string LedgerId, string compcode)
    {
        DataSet custTable;

        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_AmbulanceBill " + LedgerId + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }


    public DataSet OTInstrumentBill(string regno, string compcode)
    {
        DataSet custTable;

        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_OTInstrumentBill " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet BedBill(string regno, string compcode)
    {
        DataSet custTable; 
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_Bed_Bill " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet DoctorVisitBill(string regno,string compcode)
    {
        DataSet custTable; 
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_DoctorVisitBill " + regno + ",'"+compcode+"'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet MedicineBill(string regno, string compcode)
    {
        DataSet custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_IPD_MedicineBill " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet ServiceBill(string regno, string compcode)
    {
        DataSet custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_IPD_ServiceBill " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet ConsumableBill(string regno, string compcode)
    {
        DataSet custTable;

        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;

        theCommand.CommandText = "exec sp_IPD_ConsumableBill " + regno + ",'" + compcode + "' ";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet OTRequisitionBill(string regno, string compcode)
    {
        DataSet custTable; 
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_OTRequisitionBill " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet pathologybill(string regno, string compcode)
    {
        DataSet custTable; 
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_PathologyBill " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet xraybill(string regno, string compcode)
    {
        DataSet custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_XRayBill " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }

    public DataSet AnesthesiaCons(string regno, string compcode)
    {
        DataSet custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec [sp_IPD_Anesthesia_ConsumableBill] " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }


    public DataSet AnesthesiaMed(string regno, string compcode)
    {
        DataSet custTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec [sp_IPD_Anesthesia_MedicineBill] " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }


    public DataSet USGBill(string regno, string compcode)
    {
        DataSet custTable; 
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        // Command.
        theCommand = new SqlCommand();
        theCommand.Connection = theConnection;
        theCommand.CommandText = "exec sp_IPD_USGBill " + regno + ",'" + compcode + "'";
        theCommand.CommandType = CommandType.Text;

        // Adapter.
        theAdapter = new SqlDataAdapter();
        theAdapter.SelectCommand = theCommand;

        // Datatable.
        custTable = new DataSet();
        theAdapter.Fill(custTable); // Fill data into data table.

        // Clean up.
        theConnection.Dispose();
        theCommand.Dispose();
        theAdapter.Dispose();

        return custTable;
    }


    public DataTable GetPatientDetails_Duplicate(string RegNo)
    {
        DataTable hospitalTable;
        // Connection.
        theConnection = new SqlConnection();
        theConnection.ConnectionString = conString;

        hospitalTable = new DataTable();
        string sql;
        sql = "select CONVERT(varchar,pa.AppDate,103) AppDate1,bm.Charges,s.ShiftName ,pd.PatientReg,pd.patient_name,pd.vill_city,pd.PhNo1,pa.AdvAmount,pd.DialyserNo,pd.Startdate from IPD_BedMaster bm, dbo.DC_PatientAppointment pa,DC_ShiftDtls s,dbo.GN_PatientReg pd where  pa.ShiftId=s.ShiftID and pd.PatientReg=pa.PatientReg and bm.BedNo=pd.bedallocation  and pd.PatientReg='" + RegNo + "' and pa.AppDate=(select MAX(pa.AppDate) from DC_PatientAppointment pa where pa.PatientReg='" + RegNo + "')";
        SqlDataAdapter da = new SqlDataAdapter(sql, theConnection);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
            hospitalTable = dt;
        else
        {
            hospitalTable = new DataTable();
            string sql1;
            sql1 = "select CONVERT(varchar,pa.AppDate,103) AppDate1,bm.Charges,s.ShiftName ,pd.PatientReg,pd.patient_name,pd.vill_city,pd.PhNo1,pa.AdvAmount,pd.DialyserNo,pd.Startdate from IPD_BedMaster bm, dbo.DC_PatientAppointment pa,DC_ShiftDtls s,dbo.GN_PatientReg pd where  pa.ShiftId=s.ShiftID and pd.PatientReg=pa.PatientReg and bm.BedNo=pd.bedallocation  and pd.PatientReg='" + RegNo + "' and pa.AppDate=(select top (1) pa.AppDate from DC_PatientAppointment pa where pa.PatientReg='" + RegNo + "')";
            SqlDataAdapter da1 = new SqlDataAdapter(sql1, theConnection);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            hospitalTable = dt1;
        }


        // Clean up.
        theConnection.Dispose();
        da.Dispose();
        return hospitalTable;
    }
}