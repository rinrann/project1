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
 
public partial class DayCare_DialysisReportCard : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    DC_BillSlip thedreport = new DC_BillSlip(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSIS REPORT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        Page.Title = "Dialysis Report";
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
        ltrReport.Text = "";
        DataTable dt = thedreport.GetPatientDetails(txtreg.Text.Trim(), Session["CoCode"].ToString().Trim()); //ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
           
                rpt.Append("<br/>");
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

                rpt.Append("<tr style='height:40px'>");
                rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige;font-size:medium; text-align:Center'>  PATIENT'S  DETAILS  </td>");
                rpt.Append("</tr'>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No :</td>");
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);

                rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Patient's Name :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);


                rpt.Append("</tr >");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 8%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Address :</td>");
                rpt.AppendFormat("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);

                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; text-align:left'>Phone No :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);


                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 8%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Diagnosis :</td>");
                rpt.AppendFormat("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>HEMO-DIALYSIS</td>");

                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; text-align:left'>Doctor Name :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'></td>");
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 8%; font-family:Verdana;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Previously  Dialysis done from this Centre  :</td>");
                rpt.AppendFormat("<td style='width: 5%; font-family:Verdana;border-right: 1px solid black; font-size:small;text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Shift Name :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ShiftName"]);

                rpt.Append("</tr>");

                rpt.Append("</table>");

                //For Parameter.............................................................

                rpt.Append("<br/>");
                rpt.Append("<table cellspacing='0'>");
                rpt.Append("<tr style='height:40px'>");
                rpt.Append("<td colspan='4' style='width: 8%;border-left: 1px solid black;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige;font-size:medium; text-align:Center'> DIALYSIS  DETAILS  </td>");
                rpt.Append("</tr'>");
                rpt.Append("<tr  style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Parameter</td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;background-color:beige;border-left: 1px solid black;border-bottom: 1px solid black;font-weight:bold; text-align:center'>Pre Dialysis</td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black; border-left: 1px solid black; border-bottom: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:center'>Post Dialysis</td>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black; border-bottom: 1px solid black;border-top: 1px solid black;background-color:beige;font-size:small;font-weight:bold; text-align:center'>Comment</td>");
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>HB :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["PreHb"]);
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["PostHb"]);
                rpt.Append("<td  rowspan='6' style='width: 7%;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'></td>");
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>BP :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["PreBP"]);
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["PostBP"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Urea :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["Preurea"]);
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["Posturea"]);
                rpt.Append("</tr>");


                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Creatinine :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["Precritimine"]);
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["postcreatinine"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>NA+  :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["PreNA"]);
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["PostNA"]);
                rpt.Append("</tr>");

                rpt.Append("<tr style='height:30px'>");
                rpt.Append("<td style='width: 7%; font-family:Verdana;border-left: 1px solid black;border-bottom: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>K+  :</td>");
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["PreK"]);
                rpt.AppendFormat("<td style='width: 7%;border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana;border-bottom: 1px solid black;font-size:small;text-align:center'>{0}</td>", dt.Rows[0]["PostK"]);
                rpt.Append("</tr>");
                rpt.Append("</table>");

                // End for   Parameter
                rpt.Append("<br/>");
                rpt.Append("<table cellspacing='0'>");

                rpt.Append("<tr  style='height:30px'>");
                rpt.Append("<td style='width: 8%; font-family:Verdana; font-size:small;background-color:beige;border-top: 1px solid black;border-left: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:left'>Next Date  :</td>");
                rpt.AppendFormat("<td style='width: 5%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-right: 1px solid black;text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-top: 1px solid black;border-right: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Shift :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-right: 1px solid black;border-top: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ShiftName"]);

                rpt.Append("<td style='width: 5%; font-family:Verdana;border-top: 1px solid black;background-color:beige;border-right: 1px solid black;font-size:small;font-weight:bold; text-align:left'>Adv. Payment :</td>");
                rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-top: 1px solid black;border-right: 1px solid black;font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["AdvAmount"]);
                rpt.Append("</tr>");

                rpt.Append("<tr  style='height:30px'>");
                rpt.Append("<td style='width: 8%; font-family:Verdana; font-size:small;background-color:beige;border-top: 1px solid black;border-left: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:left'>Advice on Charge  :</td>");
                rpt.AppendFormat("<td colspan='5' rowspan='3' style='width: 7%; font-family:Verdana; font-size:small;border-top: 1px solid black;border-bottom: 1px solid black;border-right: 1px solid black;text-align:left'></td>");
                rpt.Append("</tr>");

                rpt.Append("<tr  style='height:30px'>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;background-color:beige;border-left: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:left'></td>");
                rpt.Append("</tr>");

                rpt.Append("<tr  style='height:30px'>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;background-color:beige;border-left: 1px solid black;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:left'></td>");
                rpt.Append("</tr>");




                rpt.Append("<tr  style='height:30px'>");
                rpt.Append("<td  style='width: 8%; font-family:Verdana; font-size:small;background-color:beige;border-bottom: 1px solid black;border-left: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:left'>Next Date & Time of Reporting :</td>");
                rpt.AppendFormat("<td colspan='5' style='width: 7%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-right: 1px solid black;text-align:left'></td>");
                rpt.Append("</tr>");


                rpt.Append("</table>");


                rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
                rpt.Append("<table>");

                rpt.Append("<tr>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>______________________________________</td>");
                rpt.Append("</tr'>");

                rpt.Append("<tr>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
                rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>Signature With Date</td>");

                rpt.Append("</tr'>");
                rpt.Append("</table>");

                rpt.Append("<hr>");
                lblError.ForeColor = System.Drawing.Color.White;
                lblError.Text = " ";
            }

        
            
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Please Complete full Transaction...";

        }
        
    }

    public void GetHearder_Detail_Duplicate()
    {
        DataTable dt = thedreport.GetPatientDetails_Duplicate(TextBox1.Text.Trim(), Session["CoCode"].ToString().Trim());
        //DataTable custTable1 = thedreport.GetAmount(txtreg.Text);
        //DataTable refund_discount = thedreport.Get_Refund_Discount(txtreg.Text);


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

            DataTable dtfees = thedreport.GetDuplicateFees(TextBox1.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
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
    public void PDF()
    {
        string filename = "DialysisReport" + txtreg.Text;
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

    protected void btnPDF_Click(object sender, EventArgs e)
    {

        GetReport(); 
        ltrReport.Text = rpt.ToString();
        PDF();
       
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
        if (RadioButtonList2.SelectedValue == "Duplicate Report")
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
    }

    public void PDF(string pdf)
    {
        string filename = "BillSlip" + pdf;
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
    protected void btnPDF_Click1(object sender, System.EventArgs e)
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