using System;
using System.Data;
using System.Configuration;
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Services;
using System.Globalization;
using System.Collections.Generic;
//using Spire.Doc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;

public partial class Bill_DischargeBill : System.Web.UI.Page
{
    //Discharge thepatientbill = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BillGeneration thepatientbill = new BillGeneration(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thedischarge = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnyTimePayment anypayment = new AnyTimePayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Discharge Bill";

        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE BILL", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        { 
           RadioButtonList1.SelectedValue = "With Header";
           btnBack.Visible = false; btnPDF.Visible = false; cmdPrint.Visible = false;
           //showAccount();
        }
    }

    public void GetReport()
    {
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
        //showAccount();
    }

    public void showAccount()
    {
        string cocode = Session["CoCode"].ToString().Trim();
        //string curDate = DateTime.Now.ToString("yyyy-MM-dd");
        string yearcode = Session["YearCode"].ToString().Trim();
        DataTable accountStatus = anypayment.StatusLinkAccount(cocode, yearcode);
        if (accountStatus.Rows.Count > 0 && accountStatus.Rows[0]["LinkAccount"].ToString() == "1")
        { passJV.Visible = true; }
        else { passJV.Visible = false; }
    }
    public void Report_Header()
    {
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.Append("<td style='font-family:Verdana; font-size:Medium; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>Kushpata,Ghatal,Paschim Medinipur,WB,721212</td>");
         rpt.Append("</tr>");
         rpt.Append("</table>");
    }

    public void GetHearder_Detail()
    {
        ltrReport.Text = "";
        string cocode = Session["CoCode"].ToString().Trim();
        string Discount, DueAmount;
        //DataTable dtDischarge = thedischarge.DischargeBill(txtreg.Text);
        DataTable dt = thepatientbill.PatientDischargeDetails(txtreg.Text, cocode);
        DataSet dsDischarge = thepatientbill.TotalBillDtls(dt.Rows[0]["LedgerId"].ToString(), cocode);
        DataTable dtDischarge = dsDischarge.Tables[1];
        DataTable dtdiscount = thedischarge.Ddiscount(dt.Rows[0]["LedgerId"].ToString(), dt.Rows[0]["adate"].ToString());
        DataSet ds = thedischarge.PatientForReport(txtreg.Text, cocode);

        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-left:1px solid black;border-top:1px solid black;border-right:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td colspan='6' style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Patient:</b></u> {0}, {1}, {2}, <u><b> C/O:</b></u> {3} <u><b>of</b></u> {4}, {5}, {6}</td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"], ds.Tables[0].Rows[0]["HusbandName"], ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["DistrictName"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td colspan='6' style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Diagnosis:</b></u> {0}, <u><b>Under Doctor:</b></u> {1}</td>", ds.Tables[0].Rows[0]["DiagnosisName"], ds.Tables[0].Rows[0]["doc_name"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td colspan='6' style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Regn No:</b></u> {0}, <u><b>Bed No:</b></u> {1}, <u><b>Adm:</b></u> {2}</td>", ds.Tables[0].Rows[0]["PatientReg"], ds.Tables[0].Rows[0]["BedNoText"], ds.Tables[0].Rows[0]["adate"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");         
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>DISCHARGE BILL DETAILS</td>");
            rpt.Append("</tr >");


            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='3' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>PARTICULARS</td>");
            rpt.Append("<td colspan='3' style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>AMOUNT</td>");
            rpt.Append("</tr >");

            if (Convert.ToDecimal(dtDischarge.Rows[0]["BedCharge"]) > 0)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>Bed Charges</td>");
                rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["BedCharge"]);
                rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["DoctorVisit"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>Doctor Visit Charge</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["DoctorVisit"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["Medicine"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>Medicine Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["Medicine"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["Consumable"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>Consumable Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["Consumable"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["SeviceDtls"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>Service Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["SeviceDtls"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["Pathology"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>Pathology Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["Pathology"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["XRay"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>XRay Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["XRay"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["USG"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>USG Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["USG"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["OTCharges"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>Operation Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["OTCharges"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["SisterAya"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>SiaterAya Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["SisterAya"]);
               rpt.Append("</tr >");
            }
           /* if (Convert.ToDecimal(dtDischarge.Rows[0]["OTAttendenceCharge"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>OT Attendence Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["OTAttendenceCharge"]);
               rpt.Append("</tr >");
            }*/

            DataSet otattendence = thepatientbill.OTAttendenceBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtotattendence = otattendence.Tables[0];
            if (Convert.ToDecimal(dtDischarge.Rows[0]["OTConsumableharge"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>OT Consumable Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["OTConsumableharge"]);
               rpt.Append("</tr >");
            }

            if (dtotattendence.Rows.Count > 0 && Convert.ToDecimal(dtotattendence.Rows[0]["SurgeonCharge"]) > 0)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>OT Surgeon Charges</td>");
                rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtotattendence.Rows[0]["SurgeonCharge"]);
                rpt.Append("</tr >");
            }
            if (dtotattendence.Rows.Count > 0 && Convert.ToDecimal(dtotattendence.Rows[0]["DoctorCharge"]) > 0)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>OT Doctor Charges</td>");
                rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtotattendence.Rows[0]["SurgeonCharge"]);
                rpt.Append("</tr >");
            }
            if (dtotattendence.Rows.Count > 0 && Convert.ToDecimal(dtotattendence.Rows[0]["AnesthetistCharge"]) > 0)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>OT Anesthesis Charges</td>");
                rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtotattendence.Rows[0]["AnesthetistCharge"]);
                rpt.Append("</tr >");
            }
            if (dtotattendence.Rows.Count > 0 && Convert.ToDecimal(dtotattendence.Rows[0]["AssistantCharge"]) > 0)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>OT Assistant Charges</td>");
                rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtotattendence.Rows[0]["AssistantCharge"]);
                rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["Ambulance"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>Ambulance Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["Ambulance"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["Instrument"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>OT Instrument Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["Instrument"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["AnesthesiaMedicine"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>Anesthesia Medicine Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["AnesthesiaMedicine"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["AnesthesiaConsumable"]) > 0)
            {
               rpt.Append("<tr style='height:30px'>");
               rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>Anesthesia Consumable Charges</td>");
               rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["AnesthesiaConsumable"]);
               rpt.Append("</tr >");
            }
            if (Convert.ToDecimal(dtDischarge.Rows[0]["others"]) > 0)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='3'  style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>Other Charges</td>");
                rpt.AppendFormat("<td colspan='3'  style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtDischarge.Rows[0]["others"]);
                rpt.Append("</tr >");
            }

             

            //88888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='3' style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; padding-right:25px;font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
            rpt.AppendFormat("<td colspan='3' style='width: 8%;padding-right:100px;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}</td>", dsDischarge.Tables[0].Rows[0]["Total"]/*dtDischarge.Rows[0]["Total"]*/);
            rpt.Append("</tr >");
          //  DataTable dtcheck = thepatientbill.PatientChecking(txtreg.Text);
            if (dtdiscount.Rows.Count == 0) { Discount = "0.00"; DueAmount = "0.00"; }
            else
            {
                if (dtdiscount.Rows[0]["amt"] == string.Empty) { Discount = "0.00"; } else { Discount = dtdiscount.Rows[0]["amt"].ToString(); }
                if (dtdiscount.Rows[1]["amt"] == string.Empty) { DueAmount = "0.00"; } else { DueAmount = dtdiscount.Rows[1]["amt"].ToString(); }

            }
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='3' style='width: 8%;padding-left:100px;border-right: 1px solid black; font-family:Verdana;font-weight:bold; padding-right:25px; font-size:small; text-align:right'>Discount : -</td>");
            rpt.AppendFormat("<td colspan='3' style='width: 8%;padding-right:100px; font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>{0}</td>", Discount /*dtDischarge.Rows[0]["Discount"]*/);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='3' style='width: 8%;padding-left:100px;border-right: 1px solid black; font-family:Verdana;font-weight:bold; padding-right:25px; font-size:small; text-align:right'>Due Amount : -</td>");
            rpt.AppendFormat("<td colspan='3' style='width: 8%; padding-right:100px; font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>{0}</td>", DueAmount/*dtDischarge.Rows[0]["DueAmount"]*/);
            rpt.Append("</tr >");

            double paid;
            //paid = Convert.ToDouble(dtDischarge.Rows[0]["Total"]) - Convert.ToDouble(dtDischarge.Rows[0]["Discount"]) - Convert.ToDouble(dtDischarge.Rows[0]["DueAmount"]);
            paid = Convert.ToDouble(dsDischarge.Tables[0].Rows[0]["Total"]) - Convert.ToDouble(Discount) - Convert.ToDouble(DueAmount);
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='3' style='width: 8%;padding-left:100px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; padding-right:25px;font-weight:bold; font-size:small; text-align:right'>Paid Amount. : -</td>");
            rpt.AppendFormat("<td colspan='3' style='width: 8%; padding-right:100px; border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>{0}</td>", paid.ToString());
            rpt.Append("</tr >");
            DataTable dtamtwords = thedischarge.AmtToWords(paid.ToString());
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='3' style='width: 8%;border-right: 1px solid black; font-family:Verdana;font-weight:bold; padding-right:25px; font-size:small; text-align:right'>Net Amount Payable in Words : -</td>");
            rpt.AppendFormat("<td colspan='3' style='width: 8%; padding-left:100px; font-family:Verdana;font-weight:bold;font-size:small; text-align:center'>{0}</td>", dtamtwords.Rows[0]["words"]/*dtDischarge.Rows[0]["words"]*/);
            rpt.Append("</tr >");

            rpt.Append("</table>");
            
            
            rpt.Append("<table>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='3' style='width: 8%;font-family:Verdana;font-weight:bold; font-size:small; text-align:right'></td>");
            rpt.Append("<td colspan='3' style='width: 8%;font-family:Verdana;font-weight:bold; font-size:small; text-align:center'>Signature</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='3' style='width: 8%;font-family:Verdana;font-weight:bold; font-size:small; text-align:right'></td>");
            rpt.Append("<td colspan='3' style='width: 8%;font-family:Verdana;font-weight:bold; font-size:small; text-align:center'>Date</td>");
            rpt.Append("</tr >");
        }

        ltrReport.Visible = true;

    }
    protected void Button4_Click(object sender, EventArgs e)
    { 
            if (RadioButtonList1.SelectedValue == "With Header")
            {
                Report_Header();
                GetReport();
            }
            else
            {
                GetReport();

            }
            ltrReport.Text = rpt.ToString();
            if (ltrReport.Text != "")
            {
                btnBack.Visible = true;
                btnPDF.Visible = true;
                cmdPrint.Visible = true;
            }
            else
            {
                btnBack.Visible = false;
                btnPDF.Visible = false;
                cmdPrint.Visible = false;

            } 
    }
    //protected void Button6_Click(object sender, System.EventArgs e)
    //{
    //    if (RadioButtonList2.SelectedValue == "Duplicate Report")
    //    {
    //        txtreg.Text = "";
    //        if (RadioButtonList1.SelectedValue == "With Header")
    //        {
    //            Report_Header();
    //            GetReport_Duplicate();


    //        }
    //        else
    //        {
    //            GetReport_Duplicate();

    //        }
    //        ltrReport.Text = rpt.ToString();
    //        if (ltrReport.Text != "")
    //        {
    //            btnBack.Visible = true;
    //            btnPDF.Visible = true;
    //            cmdPrint.Visible = true;
    //        }
    //        else
    //        {
    //            btnBack.Visible = false;
    //            btnPDF.Visible = false;
    //            cmdPrint.Visible = false;

    //        }
    //    }
    //}
    //protected void RadioButtonList2_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    if (RadioButtonList2.SelectedValue == "Current Report")
    //    {
    //        Panel1.Visible = true;
    //        Panel2.Visible = false;
    //    }
    //    else
    //    {
    //        Panel2.Visible = true;
    //        Panel1.Visible = false;
    //    }
    //}

    public void PDF(string pdf)
    {
        string filename = "DischargeBill" + pdf;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename='" + filename + "'.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        mydiv.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(mydiv);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
    protected void btnPDF_Click(object sender, System.EventArgs e)
    {
        //if (RadioButtonList1.SelectedValue == "Current Report" || txtreg.Text != "")
        //{
        //    GetReport();
        //    ltrReport.Text = rpt.ToString();
        //    PDF(txtreg.Text);
           


        //}
        //else if (RadioButtonList1.SelectedValue == "Duplicate Report" || TextBox2.Text != "")
        //{
        //    GetReport_Duplicate();
        //    ltrReport.Text = rpt.ToString();
        //    PDF(TextBox2.Text);
           


        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select Proper Report !');", true);
        //}
    }
    protected void passJV_Click(object sender, EventArgs e)
    {
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        if (anypayment.PassJournal(cocode, yearcode, txtreg.Text, Session["userName"].ToString(), DateTime.Now.ToString("yyyy-MM-dd")) == true)
        {
            passJV.Enabled = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Journal Passed Successfully!');", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Processing!');", true);
        }
    }
}