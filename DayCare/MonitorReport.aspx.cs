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

 
public partial class DayCare_MonitorReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    DC_MonitorReport thebill = new DC_MonitorReport(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MONITORING REPORT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        Page.Title = "Monitoring Report";
        if (!IsPostBack)
        {
            RadioButtonList1.SelectedValue = "With Header";
            btnBack.Visible = false; btnPDF.Visible = false; cmdPrint.Visible = false;
            Panel2.Visible = false;
            RadioButtonList2.SelectedValue = "Current Report";
        }
    }

    public void GetReport()
    {
        //Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }
    public void GetReport_Duplicate()
    {
        //Report_Header();
        GetHearder_Detail_Duplicate();
        ltrReport.Text = rpt.ToString();
    }

    public void GetHearder_Detail_Duplicate()
    {
        DataTable dt = thebill.GetPatientDetails(TextBox1.Text.Trim(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        //DataTable custTable1 = thebill.GetAmount(txtreg.Text);
        //DataTable refund_discount = thebill.Get_Refund_Discount(txtreg.Text);


        if (dt.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No :</td>");
            rpt.AppendFormat("<td style='width: 7%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);

            rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Patient's Name :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);


            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Address :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);

            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; text-align:left'>Phone No :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);


            rpt.Append("</tr>");
            rpt.Append("</table>");

            //For Item.............................................................

            DataTable dtfees = thebill.GetPatientDetails_Duplicate(TextBox1.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
            if (dtfees.Rows.Count > 0)
            {
                rpt.Append("<br/>");
                rpt.Append("<table cellspacing='0'>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td colspan='3' style='width: 5%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;border-right: 1px solid black;background-color:#9B9C8D;font-weight:bold; font-size:small; text-align:center'>* Dialysis Bill Slip *</td>");
                rpt.Append("</tr>");

                rpt.Append("<tr  style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; text-align:left'>Item</td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black; border-bottom: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:left'>Cost</td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-bottom: 1px solid black;font-weight:bold; text-align:left'>Item</td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black; border-bottom: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:left'>Cost</td>");

                rpt.Append("</tr>");



                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Doctor's Fees :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dtfees.Rows[0]["DoctorFees"]);
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Bed Rent :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["Charges"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Dialysis :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dtfees.Rows[0]["DialysisCharge"]);
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Medicines :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dtfees.Rows[0]["Medicine"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Disposals :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dtfees.Rows[0]["DispsableCharge"]);
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Others :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dtfees.Rows[0]["OtherCharge"]);
                rpt.Append("</tr>");

                double total = Convert.ToDouble(dtfees.Rows[0]["DoctorFees"]) + Convert.ToDouble(dt.Rows[0]["Charges"]) + Convert.ToDouble(dtfees.Rows[0]["DialysisCharge"]) + Convert.ToDouble(dtfees.Rows[0]["Medicine"]) + Convert.ToDouble(dtfees.Rows[0]["DispsableCharge"]) + Convert.ToDouble(dtfees.Rows[0]["OtherCharge"]);
                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black; font-size:small; text-align:left'></td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;font-size:small; text-align:left'></td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana; border-bottom: 1px solid black;font-size:small;font-weight:bold; text-align:center'>Total :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-bottom: 1px solid black;border-left: 1px solid black;font-size:small;text-align:left'>{0}</td>", total);

                rpt.Append("</tr>");




                rpt.Append("<tr  style='height:30px'>");

                rpt.Append("<td style='width: 5%; font-family:Verdana; border-bottom: 1px solid black;border-left: 1px solid black;background-color:beige;border-right: 1px solid black;font-size:small;font-weight:bold; text-align:left'>Advanced Date :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; border-bottom: 1px solid black;border-right: 1px solid black;font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["AppDate1"]);
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:center'>Advanced  :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-right: 1px solid black;text-align:left'>{0}</td>", dt.Rows[0]["AdvAmount"]);



                rpt.Append("</tr>");

                double due = total - Convert.ToDouble(dt.Rows[0]["AdvAmount"]);
                rpt.Append("<tr  style='height:30px'>");

                rpt.Append("<td style='width: 5%; font-family:Verdana; border-bottom: 1px solid black; border-left: 1px solid black;font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; border-bottom: 1px solid black;font-size:small;text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small; border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:center'>Due Amount  :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-bottom: 1px solid black;border-right: 1px solid black;text-align:left'>{0}.00</td>", due);

                rpt.Append("</tr>");
                rpt.Append("</table>");

                // End for   Items
                rpt.Append("<br/>");
                rpt.Append("<br/>");



                rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
                rpt.Append("<table>");

                rpt.Append("<tr>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>______________________________________</td>");
                rpt.Append("</tr'>");

                rpt.Append("<tr>");
                rpt.Append("<td style='width: 5%; font-family:Times New Roman; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>Cashier</td>");

                rpt.Append("</tr'>");
                rpt.Append("</table>");

            }
            rpt.Append("<hr>");

        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Please Select Another Patient...";

        }
    }

  
 
    public void Report_Header()
    {

        rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='4' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "GFC Hospital");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "REG NO : NH-315/G-70/2013");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "Ph :(03225)244400/244643  M:9434419825");
        rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_Detail()
    {
        DataTable dt = thebill.GetPatientDetails(txtreg.Text.Trim(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()); //ds.Tables[0];
        for (int i=0;i< dt.Rows.Count;i++)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='6' style='width: 35%;border-bottom: 1px solid black;background-color:#9B9C8D; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>PATIENT'S DETAILS</td>");
            rpt.Append("</tr>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 7%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No :</td>");
            rpt.AppendFormat("<td style='width: 7%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["PatientReg"]);

            rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Patient's Name :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["patient_name"]);

            rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;border-left: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Age :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["age"]);


            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Sex :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["SexName"]);

            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; text-align:left'>Start Time :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["StartTime"]);             

            rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;border-left: 1px solid black;font-weight:bold;font-size:small; text-align:left'>End Time :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[i]["EndTime"]); 

            rpt.Append("</tr>");
            rpt.Append("</table>");

            //For Dialysis  ....................................................................  

            rpt.Append("<br/>");
            rpt.Append("<table width='100%'  cellspacing='0'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='8' style='width: 35%;border-left: 1px solid black;border-right: 1px solid black;border-top: 1px solid black;background-color:#9B9C8D; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>DIALYSIS DETAILS</td>");
            rpt.Append("</tr>");
          
            rpt.Append("<tr  style='height:30px'>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Dialysis</td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black; border-bottom: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:center'>BP</td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-bottom: 1px solid black;font-weight:bold; text-align:center'>weight</td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:center'>HB (%)</td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; text-align:center'>UREA</td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black; border-bottom: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:center'>Creatinine</td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-bottom: 1px solid black;font-weight:bold; text-align:center'>NA+</td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black; border-bottom: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:center'>K+</td>");
            rpt.Append("</tr>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;background-color:beige;border-left: 1px solid black;font-weight:bold; text-align:center'>Pre</td>");
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PreBP"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PreWeight"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PreHb"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["Preurea"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["Precritimine"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PreNA"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-right: 1px solid black;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PreK"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Post</td>");
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PostBP"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PostWeight"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PostHb"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["Posturea"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["postcreatinine"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PostNA"]);
            rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PostNA"]);
            rpt.Append("</tr>");
            rpt.Append("</table>");

            // End for   dialysis  ..........................................................................
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'  cellspacing='0'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td colspan='6' style='width: 35%;border-left: 1px solid black;border-right: 1px solid black;border-top: 1px solid black;background-color:#9B9C8D; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>DETAILS REPORT</td>");
            rpt.Append("</tr>");

            rpt.Append("<tr  style='height:30px'>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-left: 1px solid black;font-weight:bold; text-align:center'>Time</td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:center'>BP</td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;font-weight:bold; text-align:center'>Pulse</td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:center'>Blood Flow</td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-left: 1px solid black;font-weight:bold; text-align:center'>UF Goal</td>");
            rpt.Append("<td style='width: 25%; font-family:Verdana;border-right: 1px solid black; border-left: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:center'>Comment</td>");
            rpt.Append("</tr>");

            rpt.Append("<tr  style='height:30px'>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black;text-align:center'>{0}</td>", dt.Rows[i]["InterTime1"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["InterBP1"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterPulse1"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["InterBlood1"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterUFGoal1"]);
            rpt.AppendFormat("<td style='width: 25%; font-family:Verdana;border-right: 1px solid black; border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["Comment1"]);
            rpt.Append("</tr>");

            rpt.Append("<tr  style='height:30px'>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black;text-align:center'>{0}</td>", dt.Rows[i]["InterTime2"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["InterBP2"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterPulse2"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["InterBlood2"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterUFGoal2"]); ;
            rpt.AppendFormat("<td style='width: 25%; font-family:Verdana;border-right: 1px solid black; border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["Comment2"]); ;
            rpt.Append("</tr>");

            rpt.Append("<tr  style='height:30px'>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterTime3"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["InterBP3"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterPulse3"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["InterBlood3"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterUFGoal3"]);
            rpt.AppendFormat("<td style='width: 25%; font-family:Verdana;border-right: 1px solid black; border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["Comment3"]);
            rpt.Append("</tr>");

            rpt.Append("<tr  style='height:30px'>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterTime4"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["InterBP4"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterPulse4"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["InterBlood4"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterUFGoal4"]);
            rpt.AppendFormat("<td style='width: 25%; font-family:Verdana;border-right: 1px solid black; border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["Comment4"]);
            rpt.Append("</tr>");

            rpt.Append("<tr  style='height:30px'>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterTime5"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["InterBP5"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterPulse5"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["InterBlood5"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterUFGoal5"]);
            rpt.AppendFormat("<td style='width: 25%; font-family:Verdana;border-right: 1px solid black; border-left: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["Comment5"]);
            rpt.Append("</tr>");

            rpt.Append("<tr  style='height:30px'>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black;border-bottom: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterTime6"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-left: 1px solid black;border-bottom: 1px solid black;border-top: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["InterBP6"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-bottom: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterPulse6"]);
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-top: 1px solid black;font-size:small; border-bottom: 1px solid black;text-align:center'>{0}</td>", dt.Rows[i]["InterBlood6"]); ;
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-left: 1px solid black;border-bottom: 1px solid black; text-align:center'>{0}</td>", dt.Rows[i]["InterUFGoal6"]);
            rpt.AppendFormat("<td style='width: 25%; font-family:Verdana;border-right: 1px solid black; border-left: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["Comment6"]);
            rpt.Append("</tr>");
            rpt.Append("</table>");

            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=padding: 0;font-family:verdana;'>");
            rpt.Append("<tr  style='height:30px'>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:medium; text-align:left'>TOTAL UF :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:medium; text-align:left'>TECHNICIAN NAME :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;font-size:small; text-align:left'></td>");
            rpt.Append("</tr>");
            rpt.Append("<tr  style='height:30px'>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:medium;text-align:left'>COMPLICATION :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;font-size:small; text-align:left'></td>");
            rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:medium; text-align:left'>DOCTOR'S NAME :</td>");
            rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;font-size:small; text-align:left'></td>");
           
            rpt.Append("</tr>");
            rpt.Append("</table>");
            rpt.Append("<hr>");

            rpt.Append("<br/><br/><br/><br/>");

        }
       
    }


    public void PDF(string pdf)
    {
        string filename = "MonitoringReport" + pdf;
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
 
  
    protected void Button4_Click1(object sender, System.EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "Current Report")
        {
            TextBox1.Text = "";
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
    }
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        txtreg.Text = "";
        if (RadioButtonList1.SelectedValue == "With Header")
        {
            Report_Header();
            GetReport_Duplicate();


        }
        else
        {
            GetReport_Duplicate();

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


    protected void btnPDF_Click(object sender, System.EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "Current Report" || txtreg.Text != "")
        {
            GetReport();
            ltrReport.Text = rpt.ToString();
            //PDF(txtreg.Text);


        }
        else if (RadioButtonList1.SelectedValue == "Duplicate Report" || TextBox1.Text != "")
        {
            GetReport_Duplicate();
            ltrReport.Text = rpt.ToString();
            //PDF(TextBox2.Text);


        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select Proper Report !');", true);
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        if (RadioButtonList2.SelectedValue == "Current Report")
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
        }
        else
        {
            Panel2.Visible = true;
            Panel1.Visible = false;
        }
    }
}