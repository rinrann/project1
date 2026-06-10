using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Bill_QuickBillRpt_DOS : System.Web.UI.Page
{
    System.IO.StreamWriter rdr;
    String Path = String.Empty;
    private Int32 LineNo = 0;
    public int pg = 1;
    //String details = String.Empty;
    public string details1 = string.Empty;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["RptTable"] = dt;
            //DataTable RptDt =(DataTable)Session["RptTable"];
            GenerateReport();
        }
    }
    public void GenerateReport()
    {
        DataSet Dt = (DataSet)Session["RptPatient"];
        DataTable RptDt = Dt.Tables[0];
        if (RptDt != null)
        {
            if (RptDt.Rows.Count > 0)
            {
                string dir = Server.MapPath("Print");
                Path = Server.MapPath("Print/QuickBillRpt_DOS.txt");

                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                    ////System.IO.File.Create(Path);
                    rdr = new System.IO.StreamWriter(Path, false);
                }
                else
                {

                    rdr = new System.IO.StreamWriter(Path, false);
                }
                GetReport();
                Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Report Generated successfully');", true);

                string command = String.Empty;

                command = "print " + Path;
                Response.ContentType = "application/txt";
                string filePath = "QuickBillRpt_DOS.txt";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                Response.TransmitFile(Path);
                Response.End();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Data found for Division');", true);
        }



    }
    public void GetReport()
    {
        Print_Header();
        HEADER_Detail();

    }
    public void Print_Header()
    {
        LineNo = 0;
        if (pg < 10)
        {
            rdr.WriteLine(AllowSpace(158) + "Pg : " + pg);
            LineNo += 1;
        }
        else
        {
            rdr.WriteLine(AllowSpace(124) + "Pg : " + GetRightFormatedText(pg.ToString(), 3));
            LineNo += 1;
        }
        rdr.WriteLine(AllowSpace(75) + " GFC Hospital ");
        rdr.WriteLine(AllowSpace(70) + "(REG NO : NH-315/G-70/2013)");
        rdr.WriteLine(AllowSpace(65) + "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        //rdr.WriteLine(AllowSpace(65) + "Ph :(03225)244400/244643  M:9434419825");
        //"Ph :(03225)244400/244643  M:9434419825"
        PrintLine();
    }
    public void PatientDetails_English(DataSet DsPatient)
    {
     //rdr.WriteLine(AllowSpace(27) + GetRightFormatedText(DsPatient.Tables[0].Rows[0]["patient_name"]);
        rdr.WriteLine(AllowSpace(30) + "Patient:" + GetFormatedText(DsPatient.Tables[0].Rows[0]["patient_name"].ToString(), 19)+","+ GetFormatedText(DsPatient.Tables[0].Rows[0]["age"].ToString(), 6)+","+ GetFormatedText(DsPatient.Tables[0].Rows[0]["SexName"].ToString(), 6)+","+ "C/O:"+ GetFormatedText(DsPatient.Tables[0].Rows[0]["HusbandName"].ToString(), 15) + "OF"+ GetFormatedText(DsPatient.Tables[0].Rows[0]["vill_city"].ToString(), 15)+"," + GetFormatedText(DsPatient.Tables[0].Rows[0]["po"].ToString(), 8)+"," + AllowSpace(0) + GetFormatedText(DsPatient.Tables[0].Rows[0]["DistrictName"].ToString(), 18) );
        rdr.WriteLine( AllowSpace(72) + "Ph No:" + AllowSpace(0) + GetFormatedText(DsPatient.Tables[0].Rows[0]["PhNo1"].ToString(), 12));
        rdr.WriteLine(AllowSpace(56) + "Diagnosis:" + GetFormatedText(DsPatient.Tables[0].Rows[0]["DiagnosisName"].ToString(),21) + "," + GetFormatedText(DsPatient.Tables[0].Rows[0]["doc_name"].ToString(), 19));
        rdr.WriteLine(AllowSpace(54) + "Regn No:" + GetFormatedText(DsPatient.Tables[0].Rows[0]["PatientReg"].ToString(), 13) + "," + "Bed No:" + GetFormatedText(DsPatient.Tables[0].Rows[0]["BedNoText"].ToString(), 9) + "," + "Adm:" + GetFormatedText(DsPatient.Tables[0].Rows[0]["adate"].ToString(), 10));
        PrintLine();
    }

   
    public void HEADER_Detail()
    {
        DataSet DsPatient = (DataSet)Session["RptPatient"];
        //DataTable RptPatient = Dt.Tables[0];
        DataTable DtBillno = (DataTable)Session["BillNo"];
        if (DsPatient.Tables[0].Rows.Count > 0)
        {
            if (LineNo >= 53)      //if (iLine >= 65)  if (iLine >= 67)
            {
                pg = pg + 1;
                LineNo = 0;
                Print_Header();


                // ============================== 07/05/14 ================
            }
            PatientDetails_English(DsPatient);
            DataSet DsBill = (DataSet)Session["Bill"];
            DataTable DtBill = (DataTable)Session["Dtbill"];
            DataTable DtTotalBill = (DataTable)Session["DtTotalbill"];
            decimal nettot = 0;
            if (DtBill.Rows.Count > 0)
            {
                rdr.WriteLine(AllowSpace(75) + " BED CHARGES DETAILS ");
                PrintLine();
                rdr.WriteLine(AllowSpace(4) + "Bed No" + AllowSpace(20) + "From Date" + AllowSpace(20) + "To Date" + AllowSpace(20) + "No of Days" + AllowSpace(20) + "Charge Per Day" + AllowSpace(20) + "Total Charges");
                for (int i = 0; i < DtBill.Rows.Count; i++)
                {
                    if (i + 1 < DtBill.Rows.Count && DtBill.Rows[i]["FromDate"].ToString() == DtBill.Rows[i + 1]["FromDate"].ToString() && (Convert.ToDecimal(DtBill.Rows[i]["DateDifference"]) < Convert.ToDecimal(DtBill.Rows[i + 1]["DateDifference"]) || Convert.ToDecimal(DtBill.Rows[i]["DateDifference"]) == Convert.ToDecimal(DtBill.Rows[i + 1]["DateDifference"])))
                    { }
                    else
                    {
                        rdr.WriteLine(AllowSpace(4) + GetFormatedText(DtBill.Rows[i]["BedNoText"].ToString(), 9) + AllowSpace(17) + GetFormatedText(DtBill.Rows[i]["FromDate"].ToString(), 10) + AllowSpace(19) + GetFormatedText(DtBill.Rows[i]["ToDate"].ToString(), 10) + AllowSpace(19) + GetFormatedText(DtBill.Rows[i]["DateDifference"].ToString(), 10) + AllowSpace(22) + GetRightFormatedText(DtBill.Rows[i]["Charges"].ToString(), 10) + AllowSpace(23) + GetRightFormatedText(DtBill.Rows[i]["TotalCharges"].ToString(), 10));
                       
                        //rdr.WriteLine(AllowSpace(0) + GetFormatedText(DtBill.Rows[i]["BedNoText"].ToString(), 9));
                        //rdr.WriteLine(AllowSpace(0) + GetFormatedText(DtBill.Rows[i]["BedNoText"].ToString(), 9));
                        //rdr.WriteLine(AllowSpace(0) + GetFormatedText(DtBill.Rows[i]["BedNoText"].ToString(), 9));
                    }
                }
                PrintLine();
                nettot = Convert.ToDecimal(DtTotalBill.Rows[0]["TotalBill"]) - Convert.ToDecimal(DtBill.Rows[0]["advamount"]);
                rdr.WriteLine(AllowSpace(75) + " Total : -" + AllowSpace(68) + GetRightFormatedText(DtTotalBill.Rows[0]["TotalBill"].ToString(), 10));
                PrintLine();

            }

            DataSet OTinstrumentbill = (DataSet)Session["OTInstrumntBill"];
            DataTable Instrment_0 = (DataTable)Session["Instrumnt_0"];
            DataTable TotalInstrument_1 = (DataTable)Session["Instrumnt_1"];

            if (Instrment_0.Rows.Count > 0)
            {
                rdr.WriteLine(AllowSpace(75) + " OT INSTRUMENT CHARGES DETAILS ");
                PrintLine();
                rdr.WriteLine(AllowSpace(4) + "Date" + AllowSpace(20) + "Instrument Name" + AllowSpace(20) + "Unit" + AllowSpace(20) + "Charges");

                for (int i = 0; i < Instrment_0.Rows.Count; i++)
                {
                    rdr.WriteLine(AllowSpace(4) + GetFormatedText(DtBill.Rows[i]["EntryDate1"].ToString(), 10) + AllowSpace(17) + GetFormatedText(DtBill.Rows[i]["InstrumentName"].ToString(), 10) + AllowSpace(19) + GetFormatedText(DtBill.Rows[i]["Unit"].ToString(), 10) + AllowSpace(19) + GetFormatedText(DtBill.Rows[i]["UsageCost"].ToString(), 10));
                }
                rdr.WriteLine(AllowSpace(75) + " Total : -" + AllowSpace(68) + GetRightFormatedText(TotalInstrument_1.Rows[0]["TotalBill"].ToString(), 10));
                PrintLine();
            }
        }


        DataSet OTAnesthisiaMbill = (DataSet)Session["OTAnsthisiaMBill"];
        DataTable AnesthisiaMedicine = (DataTable)Session["AnesthesiaMedicine_0"];
        DataTable TotalAnesthisiaMedicine = (DataTable)Session["AnesthesiaMedicine_1"];

        if (AnesthisiaMedicine.Rows.Count > 0)
        {
            rdr.WriteLine(AllowSpace(75) + " ANESTHESIA MEDICINE CHARGES DETAILS ");
                PrintLine();
                rdr.WriteLine(AllowSpace(4) + "Date" + AllowSpace(40) + "Medicine Name" + AllowSpace(30) + "Quantity" + AllowSpace(30) + "Rs. / Unit" + AllowSpace(20) + "Total");
                for (int i = 0; i < AnesthisiaMedicine.Rows.Count; i++)
                {

                    rdr.WriteLine(AllowSpace(4) + GetFormatedText(AnesthisiaMedicine.Rows[i]["IssueDate"].ToString(), 10) + AllowSpace(34) + GetFormatedText(AnesthisiaMedicine.Rows[i]["MedicineName"].ToString(), 28) + AllowSpace(22) + GetFormatedText(AnesthisiaMedicine.Rows[i]["BillQty"].ToString(), 10) + AllowSpace(21) + GetRightFormatedText(AnesthisiaMedicine.Rows[i]["SellPricePerUnit"].ToString(), 10) + AllowSpace(15) + GetRightFormatedText(AnesthisiaMedicine.Rows[i]["TotalCharge"].ToString(), 10));
                                              
                    }
                }
                PrintLine();
                //nettot = Convert.ToDecimal(TotalAnesthisiaMedicine.Rows[0]["TotalBill"]) - Convert.ToDecimal(DtBill.Rows[0]["advamount"]);
                rdr.WriteLine(AllowSpace(75) + " Total : -" + AllowSpace(69) + GetRightFormatedText(Convert.ToDecimal(TotalAnesthisiaMedicine.Rows[0]["Total"]).ToString("F2"), 10));
                PrintLine();




                DataSet dsAnesthisiaConsumableBill = (DataSet)Session["OTAnesthesiaConsumableBill"];
                DataTable dtanesthisiaConsumable_0 = (DataTable)Session["dtAnesthesiaConsumable_0"];
                DataTable TotalanesthisiaConsumable_1 = (DataTable)Session["dtAnesthesiaConsumable_1"];

                if (dtanesthisiaConsumable_0.Rows.Count > 0)
                {
                    rdr.WriteLine(AllowSpace(75) + " ANESTHESIA CONSUMABLE DETAILS ");
                    PrintLine();
                    rdr.WriteLine(AllowSpace(4) + "Date" + AllowSpace(40) + "Item Name" + AllowSpace(34) + "Quantity" + AllowSpace(30) + "Rs. / Unit" + AllowSpace(20) + "Total");

                    for (int i = 0; i < dtanesthisiaConsumable_0.Rows.Count; i++)
                {

                    rdr.WriteLine(AllowSpace(4) + GetFormatedText(dtanesthisiaConsumable_0.Rows[i]["IssueDate"].ToString(), 10) + AllowSpace(34) + GetFormatedText(dtanesthisiaConsumable_0.Rows[i]["ConItemName"].ToString(), 28) + AllowSpace(17) + GetRightFormatedText(dtanesthisiaConsumable_0.Rows[i]["BillQty"].ToString(), 6) + AllowSpace(30) + GetRightFormatedText(dtanesthisiaConsumable_0.Rows[i]["BillingPrice"].ToString(), 10) + AllowSpace(15) + GetRightFormatedText(dtanesthisiaConsumable_0.Rows[i]["TotalCharge"].ToString(), 10));
                                              
                    }
                }
                PrintLine();
                //nettot = Convert.ToDecimal(TotalAnesthisiaMedicine.Rows[0]["TotalBill"]) - Convert.ToDecimal(DtBill.Rows[0]["advamount"]);
                rdr.WriteLine(AllowSpace(75) + " Total : -" + AllowSpace(69) + GetRightFormatedText(Convert.ToDecimal(TotalanesthisiaConsumable_1.Rows[0]["Total"]).ToString("F2"), 10));
                PrintLine();




        
                DataSet OTAattendanceBill = (DataSet)Session["OTAttendenceBill"];
                DataTable OtaAttendance_0 = (DataTable)Session["otattendence_0"];
                DataTable OTAttendance_1 = (DataTable)Session["otattendence_1"];
                if (OtaAttendance_0.Rows.Count > 0)
                {
                    rdr.WriteLine(AllowSpace(75) + " OT ATTENDENCE FEES DETAILS ");
                    PrintLine();
                    rdr.WriteLine(AllowSpace(4) + "Date" + AllowSpace(40) + "Surgeon Charge" + AllowSpace(34) + "Doctor Charge" + AllowSpace(30) + "Anesthesis Charge" + AllowSpace(20) + "Assistant Chage");


                    rdr.WriteLine(AllowSpace(4) + GetFormatedText(OtaAttendance_0.Rows[0]["IssueDate"].ToString(), 10) + AllowSpace(34) + GetFormatedText(OtaAttendance_0.Rows[0]["SurgeonCharge"].ToString(), 28) + AllowSpace(17) + GetRightFormatedText(OtaAttendance_0.Rows[0]["DoctorCharge"].ToString(), 6) + AllowSpace(30) + GetRightFormatedText(OtaAttendance_0.Rows[0]["AnesthetistCharge"].ToString(), 10) + AllowSpace(15) + GetRightFormatedText(OtaAttendance_0.Rows[0]["AssistantCharge"].ToString(), 10));

                    }
                
                PrintLine();
                //nettot = Convert.ToDecimal(TotalAnesthisiaMedicine.Rows[0]["TotalBill"]) - Convert.ToDecimal(DtBill.Rows[0]["advamount"]);
                rdr.WriteLine(AllowSpace(75) + " Total : -" + AllowSpace(69) + GetRightFormatedText(Convert.ToDecimal(OTAttendance_1.Rows[0]["TotalBill"]).ToString("F2"), 10));
                PrintLine();
                }    
   
    
   
    private string GetFormatedText(string Cont, int Length)
    {
        int rLoc = Length - Cont.Length;
        if (rLoc < 0)
        {
            Cont = Cont.Substring(0, Length);
        }
        else
        {
            int nos;
            for (nos = 0; nos < rLoc; nos++)  //Length
                Cont = Cont + " ";
        }
        return (Cont);
    }
    public void SkipLine(int LineNos)
    {
        int i;
        for (i = 1; i <= LineNos; i++)
        {
            rdr.WriteLine("");
        }
    }
    private string AllowSpace(int Length)
    {
        string space = "";

        for (int nos = 0; nos < Length; nos++)
        {
            space += " ";
        }
        return space;
    }
    private string GetRightFormatedText(string Cont, int Length)
    {
        int rLoc = Length - Cont.Length;
        if (rLoc < 0)
        {
            Cont = Cont.Substring(0, Length);
        }
        else
        {
            for (int nos = 1; nos <= rLoc; nos++) // Length
                Cont = " " + Cont;
        }
        return (Cont);
    }
    private string GetCenterdFormatedText(string Cont, int Length)
    {
        int rLoc = Length - Cont.Length;
        if (rLoc < 0)
        {
            Cont = Cont.Substring(0, Length);
        }
        else
        {
            for (int nos = 0; nos < Length; nos++)
                Cont = " " + Cont;
        }
        return (Cont);
    }
    public void Close()
    {
        rdr.Close();
    }
    public void PrintLine()
    {
        int i;
        string Lstr = "";

        for (i = 1; i < 168; i++) //162=>LandScape  81=>Protat
        {
            Lstr = Lstr + "-";
        }

        rdr.WriteLine(Lstr);

    }
}