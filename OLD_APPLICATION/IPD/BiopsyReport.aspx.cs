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

public partial class IPD_BiopsyReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DischargeCertificate theabortion = new DischargeCertificate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Biopsy Requisition";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BIOPSY REQUISITION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            RadioButtonList1.SelectedValue = "With Header";
            btnBack.Visible = false; btnPDF.Visible = false; cmdPrint.Visible = false;
            Panel2.Visible = false;
            RadioButtonList2.SelectedValue = "Current Report";
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


    public void GetHearder_Detail(string reg)
    {
        ltrReport.Text = "";
        DataTable dt = theabortion.GetPatientDtls(reg, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>BIOPSY REQUISITION  </td>");
        rpt.Append("</tr'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'>রোগীর বিবরণ </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>নিবন্ধ সংখ্যা :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>রোগীর নাম :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>রোগীর বয়স :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0} yrs.</td>", dt.Rows[0]["age"]);

        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অভিভাবক নাম :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>রোগীর ঠিকানা :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>যোগাযোগ নম্বর :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
        rpt.Append("</tr >");


        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ডাকঘর :</td>");
        rpt.AppendFormat("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["po"]);
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>থানা :</td>");
        rpt.AppendFormat("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ps"]);
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>জেলা :</td>");
        rpt.AppendFormat("<td style='width: 5%;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DistrictName"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");


        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ভর্তি তারিখ :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ভর্তি টাইম :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["FromTime"]);
        rpt.Append("</tr >");



        rpt.Append("</table >");

        DataTable dtot = theabortion.GetOperation(reg, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ক্লিনিকাল ডায়াগনসিস  :</td>");
        rpt.AppendFormat("<td style='width: 15%; padding-left:30px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DiagnosisName"]);
        rpt.Append("</tr >");
        if (dtot.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অপারেশন নাম   :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtot.Rows[0]["OperationName"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অপারেশন তারিখ   :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtot.Rows[0]["otdate"]);
            rpt.Append("</tr >");

            //rpt.Append("<tr style='height:30px'>");
            //rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অপারেশন লক্ষ্য   :</td>");
            //rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtot.Rows[0]["Remarks"]);
            //rpt.Append("</tr >");

         
        }

     

        rpt.Append("</table >");


        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Exam. Required   :</td>");
        rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "H/P Examination");
        rpt.Append("</tr >");


        string tissue = "";
        DataTable dtBiopsyTissue = theabortion.BiopsyTissue(reg, Session["CoCode"].ToString().Trim());
        for (int i = 0; i < dtBiopsyTissue.Rows.Count; i++)
        {
            if (tissue == "")
            {
                tissue = dtBiopsyTissue.Rows[i]["TypeOfTissue"].ToString();
            }
            else
            {
                tissue = tissue + " , " + dtBiopsyTissue.Rows[i]["TypeOfTissue"].ToString();
            }
        }
        if (dtBiopsyTissue.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Tissue Taken From :</td>");
            rpt.AppendFormat("<td colspan='3' style='width: 5%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", tissue);
            rpt.Append("</tr >");
        }
        rpt.Append("</table >");

        rpt.Append("<br />"); rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>ডঃ টি কে কর্মকার</td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>MBBS,DGO,MD,DNB,MNAMS,FICMCH,FICOG,MBA,Ph.D</td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Regn. WNMC 46599</td>");
        rpt.Append("</tr >");
        rpt.Append("</table >");
        ltrReport.Visible = true;


    }


    public void GetHearder_Detail1(string reg1)
    {
        ltrReport.Text = "";
        DataTable dt = theabortion.GetPatientDtls(reg1, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> BIOPSY REQUISITION  </td>");
        rpt.Append("</tr'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's Details </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Patient's Name :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Patient's Age :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0} yrs.</td>", dt.Rows[0]["age"]);

        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Guadian's Name :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Address :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Contact No :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
        rpt.Append("</tr >");


        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Post :</td>");
        rpt.AppendFormat("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["po"]);
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Police Station :</td>");
        rpt.AppendFormat("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ps"]);
        rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>District :</td>");
        rpt.AppendFormat("<td style='width: 5%;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DistrictName"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");


        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Admission Date :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Admission Time :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["FromTime"]);
        rpt.Append("</tr >");


        rpt.Append("</table >");

        DataTable dtot = theabortion.GetOperation(reg1, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Clinical Diagnosis   :</td>");
        rpt.AppendFormat("<td style='width: 15%; padding-left:30px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DiagnosisName"]);
        rpt.Append("</tr >");
        if (dtot.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Operation Name   :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtot.Rows[0]["OperationName"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Operation Date   :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtot.Rows[0]["otdate"]);
            rpt.Append("</tr >");

            //rpt.Append("<tr style='height:30px'>");
            //rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Operation Note   :</td>");
            //rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtot.Rows[0]["Remarks"]);
            //rpt.Append("</tr >");


          
        }

       

        rpt.Append("</table >");


        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Exam. Required   :</td>");
        rpt.AppendFormat("<td colspan='3' style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "H/P Examination");
        rpt.Append("</tr >");


        string tissue = "";
        DataTable dtBiopsyTissue = theabortion.BiopsyTissue(reg1, Session["CoCode"].ToString().Trim());
        for (int i = 0; i < dtBiopsyTissue.Rows.Count; i++)
        {
            if (tissue == "")
            {
                tissue = dtBiopsyTissue.Rows[i]["TypeOfTissue"].ToString();
            }
            else
            {
                tissue = tissue + " , " + dtBiopsyTissue.Rows[i]["TypeOfTissue"].ToString();
            }
        }
        if (dtBiopsyTissue.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Tissue Taken From :</td>");
            rpt.AppendFormat("<td colspan='3' style='width: 5%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", tissue);
            rpt.Append("</tr >");
        }
        rpt.Append("</table >");

        rpt.Append("<br />"); rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Dr. T. K. Karmakar</td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>MBBS,DGO,MD,DNB,MNAMS,FICMCH,FICOG,MBA,Ph.D</td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Regn. WNMC 46599</td>");
        rpt.Append("</tr >");
        rpt.Append("</table >");
        ltrReport.Visible = true;


    }

    public void PDF(string pdf)
    {
        string filename = "BiopsyRequisition" +pdf;
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
        TextBox1.Text = "";
        if (RadioButtonList1.SelectedValue == "With Header")
        { 
            Report_Header();
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1(txtreg.Text);
                ltrReport.Text = rpt.ToString();
            }
            else
            {
                GetHearder_Detail(txtreg.Text);
                ltrReport.Text = rpt.ToString();
            }
        }
        else
        {
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1(txtreg.Text);
                ltrReport.Text = rpt.ToString();
            }
            else
            {
                GetHearder_Detail(txtreg.Text);
                ltrReport.Text = rpt.ToString();
            }
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
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        txtreg.Text = "";

        if (RadioButtonList1.SelectedValue == "With Header")
        {
            //   Button1.Enabled = false;
            Report_Header();
            if (DropDownList2.SelectedIndex == 2)
            {
                GetHearder_Detail1(TextBox1.Text);
            }
            else
            {
                GetHearder_Detail(TextBox1.Text);
            }
        }
        else
        {
            if (DropDownList2.SelectedIndex == 2)
            {
                GetHearder_Detail1(TextBox1.Text);
            }
            else
            {
                GetHearder_Detail(TextBox1.Text);
            }
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
        if (TextBox1.Text == "")
        {
            if (DropDownList1.SelectedIndex == 2)
            {

                GetHearder_Detail1(txtreg.Text);
            }
            else
            {
                GetHearder_Detail(txtreg.Text);

            }

            ltrReport.Text = rpt.ToString();
            PDF(txtreg.Text);
        }
        else
        {
            if (DropDownList2.SelectedIndex == 2)
            {
                GetHearder_Detail1(TextBox1.Text);
            }
            else
            {
                GetHearder_Detail(TextBox1.Text);
            }
            ltrReport.Text = rpt.ToString();
            PDF(TextBox1.Text);
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