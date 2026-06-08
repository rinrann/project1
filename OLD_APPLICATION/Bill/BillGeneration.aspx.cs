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

public partial class Bill_BillGeneration : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BillGeneration thepatientbill = new BillGeneration(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thedischarge = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AnyTimePayment anypayment = new AnyTimePayment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");


    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Discharge Bill Details";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DISCHARGE BILL DETAILS", checkAccessType.ViewAction) == false)
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
    public void GetReport()
    {
        //Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
        //showAccount();
    }

    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Verdana; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Verdana; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Verdana; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Verdana; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }


    public void PatientDetails_English(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 style='border-left:1px solid black;border-right:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Patient:</b></u> {0}, {1}, {2},<u><b>C/O:</b></u> {3} <u><b>of</b></u> {4},{5},{6}, <u><b><br/>Ph No:</b></u>{7}</td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"], ds.Tables[0].Rows[0]["HusbandName"], ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["DistrictName"], ds.Tables[0].Rows[0]["PhNo1"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Diagnosis:</b></u> {0}, <u><b>Under Doctor:</b></u> {1}</td>", ds.Tables[0].Rows[0]["DiagnosisName"], ds.Tables[0].Rows[0]["doc_name"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Verdana; font-size:small;text-align:center'><u><b>Regn No:</b></u> {0}, <u><b>Bed No:</b></u> {1}, <u><b>Adm:</b></u> {2}</td>", ds.Tables[0].Rows[0]["PatientReg"], ds.Tables[0].Rows[0]["BedNoText"], ds.Tables[0].Rows[0]["adate"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
        }
    }

    public void GetHearder_Detail()
    {
        ltrReport.Text = "";
        string cocode = Session["CoCode"].ToString().Trim();
        DataSet ds = thedischarge.PatientForReport(txtreg.Text, cocode);

        DataTable dt = thepatientbill.PatientDischargeDetails(txtreg.Text, Session["CoCode"].ToString().Trim());
        DataTable billno = thepatientbill.GetBillDtls(txtreg.Text);
        if (dt.Rows.Count > 0)
        {
            PatientDetails_English(ds);


            DataSet bill = thepatientbill.BedBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtbill = bill.Tables[0];
            DataTable dtTotalbill = bill.Tables[1];

            rpt.Append("<table width='100%' cellspacing=0 style='border-top:1px solid black;border-bottom:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            if (dtbill.Rows.Count > 0)
            {

                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>BED CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td  style='width:15%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Bed No</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>From Date</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>To Date</td>");
                rpt.Append("<td style='width: 1%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>No of Days</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Charge Per Day</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Total Charges</td>");
                rpt.Append("</tr >");

                for (int i = 0; i < dtbill.Rows.Count; i++)
                {
                    if (i + 1 < dtbill.Rows.Count && dtbill.Rows[i]["FromDate"].ToString() == dtbill.Rows[i + 1]["FromDate"].ToString() && (Convert.ToDecimal(dtbill.Rows[i]["DateDifference"]) < Convert.ToDecimal(dtbill.Rows[i + 1]["DateDifference"]) || Convert.ToDecimal(dtbill.Rows[i]["DateDifference"]) == Convert.ToDecimal(dtbill.Rows[i + 1]["DateDifference"])))
                    { }
                    else
                    {
                        rpt.Append("<tr style='height:20px'>");
                        rpt.AppendFormat("<td  style='width: 15%;font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtbill.Rows[i]["BedNoText"]);
                        rpt.AppendFormat("<td  style='width: 8%;font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtbill.Rows[i]["FromDate"]);
                        rpt.AppendFormat("<td style='width: 8%;font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtbill.Rows[i]["ToDate"]);
                        rpt.AppendFormat("<td  style='width: 1%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtbill.Rows[i]["DateDifference"]);
                        rpt.AppendFormat("<td  style='width: 8%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtbill.Rows[i]["Charges"]);
                        rpt.AppendFormat("<td  style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtbill.Rows[i]["TotalCharges"]);
                        rpt.Append("</tr >");
                    }
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td  style='width: 8%;border-top: 1px solid black;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}</td>", dtTotalbill.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");


            }







            DataSet instrument = thepatientbill.OTInstrumentBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtinstrument = instrument.Tables[0];
            DataTable dttotalinstrument = instrument.Tables[1];
            if (dtinstrument.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>OT INSTRUMENT CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Date</td>");
                rpt.Append("<td  colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Instrument Name</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Unit</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Charges</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtinstrument.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtinstrument.Rows[i]["EntryDate1"]);
                    rpt.AppendFormat("<td  colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtinstrument.Rows[i]["InstrumentName"]);
                    rpt.AppendFormat("<td style='width: 8%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtinstrument.Rows[i]["Unit"]);
                    rpt.AppendFormat("<td  style='width: 8%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtinstrument.Rows[i]["UsageCost"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotalinstrument.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");
            }








            DataSet AnesthesiaMedicine = thepatientbill.OTAnesthesiaMedicineBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtAnesthesiaMedicine = AnesthesiaMedicine.Tables[0];
            DataTable dttotalAnesthesiaMedicine = AnesthesiaMedicine.Tables[1];
            if (dtAnesthesiaMedicine.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>ANESTHESIA MEDICINE CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Date</td>");
                rpt.Append("<td  colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Medicine Name</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Quantity</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Rs. / Unit</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Total</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtAnesthesiaMedicine.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td style='width: 8%;SchedularService font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtAnesthesiaMedicine.Rows[i]["IssueDate"]);
                    rpt.AppendFormat("<td  colspan='2' style='width: 8%;SchedularService font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtAnesthesiaMedicine.Rows[i]["MedicineName"]);
                    rpt.AppendFormat("<td style='width: 8%;SchedularService font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtAnesthesiaMedicine.Rows[i]["BillQty"]);
                    rpt.AppendFormat("<td style='width: 8%;SchedularService font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtAnesthesiaMedicine.Rows[i]["SellPricePerUnit"]);
                    rpt.AppendFormat("<td  style='width: 8%;SchedularService font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtAnesthesiaMedicine.Rows[i]["TotalCharge"]);
                    rpt.Append("</tr >");
                }
                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotalAnesthesiaMedicine.Rows[0]["Total"]);
                rpt.Append("</tr >");
            }










            DataSet dsAnesthesiaConsumable = thepatientbill.OTAnesthesiaConsumableBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtAnesthesiaConsumable = dsAnesthesiaConsumable.Tables[0];
            DataTable dttotalAnesthesiaConsumable = dsAnesthesiaConsumable.Tables[1];
            if (dtAnesthesiaConsumable.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>ANESTHESIA CONSUMABLE DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Date</td>");
                rpt.Append("<td  colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Item Name</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Quantity</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Rs. / Unit</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Total</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtAnesthesiaConsumable.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td style='width: 8%;font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtAnesthesiaConsumable.Rows[i]["IssueDate"]);
                    rpt.AppendFormat("<td  colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtAnesthesiaConsumable.Rows[i]["ConItemName"]);
                    rpt.AppendFormat("<td style='width: 8%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtAnesthesiaConsumable.Rows[i]["BillQty"]);
                    rpt.AppendFormat("<td  style='width: 8%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtAnesthesiaConsumable.Rows[i]["BillingPrice"]);
                    rpt.AppendFormat("<td  style='width: 8%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtAnesthesiaConsumable.Rows[i]["TotalCharge"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom:1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotalAnesthesiaConsumable.Rows[0]["Total"]);
                rpt.Append("</tr >");
            }












            DataSet otattendence = thepatientbill.OTAttendenceBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtotattendence = otattendence.Tables[0];
            DataTable dttotalotattendence = otattendence.Tables[1];
            if (dtotattendence.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>OT ATTENDENCE FEES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Date</td>");
                rpt.Append("<td  style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Surgeon Charge</td>");
                rpt.Append("<td   style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Doctor Charge</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Anesthesis Charge</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Assistant Chage</td>");

                rpt.Append("</tr >");

                rpt.Append("<tr style='height:30px'>");
                rpt.AppendFormat("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtotattendence.Rows[0]["IssueDate"]);
                rpt.AppendFormat("<td  style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtotattendence.Rows[0]["SurgeonCharge"]);
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtotattendence.Rows[0]["DoctorCharge"]);
                rpt.AppendFormat("<td  style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtotattendence.Rows[0]["AnesthetistCharge"]);
                rpt.AppendFormat("<td  style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtotattendence.Rows[0]["AssistantCharge"]);
                rpt.Append("</tr >");


                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotalotattendence.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");
            }








            DataSet ambulance = thepatientbill.AmbulanceBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtambulaance = ambulance.Tables[0];
            DataTable dttotalambulance = ambulance.Tables[1];
            if (dtambulaance.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>AMBULANCE CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>From Address</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>To Address</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Date</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Distance</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Charges</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtambulaance.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtambulaance.Rows[i]["FromAddress"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtambulaance.Rows[i]["ToAddress"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtambulaance.Rows[i]["DelDate"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtambulaance.Rows[i]["Kelometer"]);
                    rpt.AppendFormat("<td  style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtambulaance.Rows[i]["Charges"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotalambulance.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");
            }








            DataSet otconsumable = thepatientbill.OTConsumableBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtotconsumable = otconsumable.Tables[0];
            DataTable dttotalotconsumable = otconsumable.Tables[1];
            if (dtotconsumable.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>OT CONSUMABLE CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td  style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Date</td>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Consumable Name</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Quantity</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Price/Unit</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Total</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtotconsumable.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td  style='width: 8%;font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtotconsumable.Rows[i]["IssueDate"]);
                    rpt.AppendFormat("<td  colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtotconsumable.Rows[i]["ConItemName"]);
                    rpt.AppendFormat("<td  style='width: 8%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtotconsumable.Rows[i]["BillQty"]);
                    rpt.AppendFormat("<td style='width: 8%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtotconsumable.Rows[i]["BillingPrice"]);
                    rpt.AppendFormat("<td  style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtotconsumable.Rows[i]["Charges"]);

                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotalotconsumable.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");
            }









            DataSet sisteraya = thepatientbill.SisterAyaBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtsisteraya = sisteraya.Tables[0];
            DataTable dttotalsisteraya = sisteraya.Tables[1];
            if (dtsisteraya.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>SISTER / AYA CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Issue Date</td>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Type</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Shift</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Charges</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtsisteraya.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtsisteraya.Rows[i]["IssueDate"]);
                    rpt.AppendFormat("<td colspan='2' style='width: 8%; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtsisteraya.Rows[i]["TypeName"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtsisteraya.Rows[i]["ShiftName"]);
                    rpt.AppendFormat("<td  style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtsisteraya.Rows[i]["Charges"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; border-top: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotalsisteraya.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");

            }







            DataSet docvisit = thepatientbill.DoctorVisitBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtdocvisit = docvisit.Tables[0];
            DataTable dttotaldocvisit = docvisit.Tables[1];
            if (dtdocvisit.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>DOCTOR VISIT CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Doctor Name</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Visiting Date</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Visit Type</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>No of Visit</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Visit Charge</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Total Charge</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtdocvisit.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtdocvisit.Rows[i]["doc_name"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtdocvisit.Rows[i]["IssueDate"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtdocvisit.Rows[i]["Visit_Type"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtdocvisit.Rows[i]["NoofVisit"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtdocvisit.Rows[i]["visit_charges"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtdocvisit.Rows[i]["TotalCharge"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; border-top: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotaldocvisit.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");
            }

            //DataSet medicine = thepatientbill.MedicineBill(dt.Rows[0]["LedgerId"].ToString());
            DataSet medicine = thepatientbill.DischargeMedicineBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtmedicine = medicine.Tables[0];
            DataTable dttotalmedicine = medicine.Tables[1];
            if (dtmedicine.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;  font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>MEDICINE CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Medicine Name</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Issue Date</td>");
                rpt.Append("<td  style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Quantity</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Selling Price/Unit</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Total Price</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtmedicine.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtmedicine.Rows[i]["MedicineName"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtmedicine.Rows[i]["IssueDate"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtmedicine.Rows[i]["BillQty"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtmedicine.Rows[i]["SellPricePerUnit"]);
                    rpt.AppendFormat("<td  style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtmedicine.Rows[i]["TotalCharge"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black;  font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotalmedicine.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");

            }













            DataSet service = thepatientbill.ServiceBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtservice = service.Tables[0];
            DataTable dttotalservice = service.Tables[1];
            if (dtservice.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>SERVICE CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Service Name</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Issue Date</td>");
                rpt.Append("<td  style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Quantity</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Per Price</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Total Price</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtservice.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtservice.Rows[i]["ServiceTemplateName"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtservice.Rows[i]["IssueDate"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtservice.Rows[i]["Quantity"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtservice.Rows[i]["ServiceCharge"]);
                    rpt.AppendFormat("<td  style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtservice.Rows[i]["TotalCharge"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;border-top: 1px solid black;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotalservice.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");

            }









            DataSet consumable = thepatientbill.ConsumableBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtconsumable = consumable.Tables[0];
            DataTable dttotalconsumable = consumable.Tables[1];
            if (dtconsumable.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>CONSUMABLE CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Consumable Name</td>");
                rpt.Append("<td  colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Issue Date</td>");
                rpt.Append("<td  style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Quantity</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Price/Unit</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Total Price</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtconsumable.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtconsumable.Rows[i]["ConItemName"]);
                    rpt.AppendFormat("<td  colspan='2' style='width: 8%; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtconsumable.Rows[i]["IssueDate"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtconsumable.Rows[i]["BillQty"]);
                    rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtconsumable.Rows[i]["BillingPrice"]);
                    rpt.AppendFormat("<td  style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtconsumable.Rows[i]["TotalCharge"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotalconsumable.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");

            }





            DataSet ot = thepatientbill.OTRequisitionBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtot = ot.Tables[0];
            DataTable dttotalot = ot.Tables[1];
            if (dtot.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>OPERATION CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Operation Name</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Operation Date</td>");
                rpt.Append("<td  style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Start Time</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>End Time</td>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Operation Charge</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtot.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtot.Rows[i]["OperationName"]);
                    rpt.AppendFormat("<td style='width: 8%;font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtot.Rows[i]["OperationDate"]);
                    rpt.AppendFormat("<td style='width: 8%;font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtot.Rows[i]["StartTime"]);
                    rpt.AppendFormat("<td style='width: 8%;font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtot.Rows[i]["EndTime"]);
                    rpt.AppendFormat("<td  style='width: 8%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtot.Rows[i]["OperationCost"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='5' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td  style='width: 8%;border-bottom: 1px solid black; border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttotalot.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");

            }









            DataSet pathology = thepatientbill.pathologybill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtpathology = pathology.Tables[0];
            DataTable dttpathology = pathology.Tables[1];
            if (dtpathology.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>PATHOLOGY CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Test Name</td>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Test Date</td>");
                rpt.Append("<td  colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Test Cost</td>");


                rpt.Append("</tr >");

                for (int i = 0; i < dtpathology.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtpathology.Rows[i]["TestName"]);
                    rpt.AppendFormat("<td colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtpathology.Rows[i]["TestDate"]);
                    rpt.AppendFormat("<td  colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtpathology.Rows[i]["Cost"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td    colspan='2' style='width: 8%;border-bottom: 1px solid black;  border-top: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttpathology.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");

            }


            DataSet xray = thepatientbill.xraybill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtxray = xray.Tables[0];
            DataTable dttxray = xray.Tables[1];
            if (dtxray.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>X-RAY CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Test Name</td>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Test Date</td>");
                rpt.Append("<td  colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Test Cost</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtxray.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtxray.Rows[i]["TestName"]);
                    rpt.AppendFormat("<td colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtxray.Rows[i]["TestDate"]);
                    rpt.AppendFormat("<td  colspan='2' style='width: 8%; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtxray.Rows[i]["TestCost"]);
                    rpt.Append("</tr >");
                }
                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td   colspan='2' style='width: 8%;border-bottom: 1px solid black; border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttxray.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");
            }



            DataSet usg = thepatientbill.USGBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtusg = usg.Tables[0];
            DataTable dttusg = usg.Tables[1];
            if (dtusg.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>USG CHARGES DETAILS</td>");
                rpt.Append("</tr >");


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Test Name</td>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:center'>Test Date</td>");
                rpt.Append("<td  colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Test Cost</td>");


                rpt.Append("</tr >");

                for (int i = 0; i < dtusg.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtusg.Rows[i]["Name"]);
                    rpt.AppendFormat("<td colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtusg.Rows[i]["TestDate"]);
                    rpt.AppendFormat("<td  colspan='2' style='width: 8%;font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtusg.Rows[i]["Charges"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td  colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#F6B2B2; font-size:small; text-align:right'>{0}.00</td>", dttusg.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");
            }
            rpt.Append("</table >");

            rpt.Append("<table width='100%' cellspacing=0 style='border-bottom:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            DataSet others = thepatientbill.OthersBill(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dtothers = others.Tables[0];
            DataTable dttothers = others.Tables[1];
            if (dtothers.Rows.Count > 0)
            {
                rpt.Append("<tr style='height:15px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black;text-align:center'><br/></td>");
                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#B6BBB4;font-size:small; text-align:center'>OTHERS CHARGES DETAILS</td>");
                rpt.Append("</tr >");

                rpt.Append("</tr >");

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Date</td>");
                rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:left'>Other Item</td>");
                rpt.Append("<td  colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D; font-size:small; text-align:right'>Cost</td>");

                rpt.Append("</tr >");

                for (int i = 0; i < dtothers.Rows.Count; i++)
                {
                    rpt.Append("<tr style='height:20px'>");
                    rpt.AppendFormat("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtothers.Rows[i]["IssueDate"]);
                    rpt.AppendFormat("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small; text-align:left'>{0}</td>", dtothers.Rows[i]["OthersDetails"]);
                    rpt.AppendFormat("<td  colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:right'>{0}</td>", dtothers.Rows[i]["Amount"]);
                    rpt.Append("</tr >");
                }

                rpt.Append("<tr style='height:25px'>");
                rpt.Append("<td colspan='4' style='width: 8%; border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#4FBECF; font-size:small; text-align:center'>Total : -</td>");
                rpt.AppendFormat("<td  colspan='2' style='width: 8%;  border-top: 1px solid black;font-family:Verdana;font-weight:bold;background-color:#4FBECF; font-size:small; text-align:right'>{0}.00</td>", dttothers.Rows[0]["TotalBill"]);
                rpt.Append("</tr >");
            }

            DataSet total = thepatientbill.TotalBillDtls(dt.Rows[0]["LedgerId"].ToString(), cocode);
            DataTable dttotal = total.Tables[0];
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#496EF6; font-size:small; text-align:center'>Grand Total : -</td>");
            rpt.AppendFormat("<td  colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#496EF6; font-size:small; text-align:right'>{0}.00</td>", dttotal.Rows[0]["Total"]);
            rpt.Append("</tr >");

            rpt.Append("</table >");

        }

        ltrReport.Visible = true;

    }


    protected void Button4_Click(object sender, System.EventArgs e)
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

    public void PDF(string pdf)
    {
        string filename = "DischargeBillDetails" + pdf;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".pdf");
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
        GetReport();
        ltrReport.Text = rpt.ToString();
        PDF(txtreg.Text);
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