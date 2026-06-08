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
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;  


public partial class IPD_DischargeCertificateProvesional : System.Web.UI.Page
{

    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DischargeCertificate theabortion = new DischargeCertificate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Provesonal Discharge Certificate";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PROV DISCHARGE CERTIFICATE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            RadioButtonList1.SelectedValue = "With Header";
            btnBack.Visible = false; btnPDF.Visible = false; cmdPrint.Visible = false;
            // Button1.Enabled = false;
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "With Header")
        {
            //   Button1.Enabled = false;
            Report_Header();
          
            
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1();
            }
            else
            {
                GetHearder_Detail();
            }
        }
        else
        {
           
            //  Button1.Enabled = true;
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1();
            }
            else
            {
                GetHearder_Detail();
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

    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }

    public void GetReport1()
    {
        Report_Header();
        GetHearder_Detail1();
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
        DataTable dt = theabortion.GetPatientDtls(TextBox1.Text, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>প্রভেসনাল ডিসচার্জ সার্টিফিকেট  </td>");
        rpt.Append("</tr'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'>রোগীর বিবরণ </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>নিবন্ধ সংখ্যা :</td>");
        rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
        rpt.Append("<td style='width:5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>রোগীর নাম :</td>");
        rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>রোগীর বয়স :</td>");
        rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0} yrs.</td>", dt.Rows[0]["age"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অভিভাবক নাম :</td>");
        rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>রোগীর ঠিকানা :</td>");
        rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>যোগাযোগ নম্বর :</td>");
        rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
        rpt.Append("</tr >");


        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ডাকঘর :</td>");
        rpt.AppendFormat("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["po"]);
        rpt.Append("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>থানা :</td>");
        rpt.AppendFormat("<td style='width:8%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ps"]);
        rpt.Append("<td style='width:5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>জেলা :</td>");
        rpt.AppendFormat("<td style='width: 8%;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DistrictName"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");


        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ভর্তি তারিখ :</td>");
        rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
        rpt.Append("<td style='width:5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ভর্তি টাইম :</td>");
        rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["FromTime"]);
        rpt.Append("</tr >");

        DataTable dtdischarge = theabortion.DischargeDtks(TextBox1.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        if (dtdischarge.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>নিষ্কাশন তারিখ :</td>");
            rpt.AppendFormat("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtdischarge.Rows[0]["disdate"]);
            rpt.Append("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>নিষ্কাশন টাইম :</td>");
            rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtdischarge.Rows[0]["DischargeTime"]);
            rpt.Append("</tr >");
        }

        rpt.Append("</table >");

        DataTable dtot = theabortion.GetOperation(TextBox1.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        string opname = "", opdate = "", opremarks = "";
        for (int i = 0; i < dtot.Rows.Count; i++)
        {
            if (opname == "")
                opname = dtot.Rows[i]["OperationName"].ToString();
            else
                opname = opname + " , " + dtot.Rows[i]["OperationName"].ToString();

            if (opdate == "")
                opdate = dtot.Rows[i]["otdate"].ToString();
            else
                opdate = opdate + " , " + dtot.Rows[i]["otdate"].ToString();


            if (opremarks == "")
                opremarks = dtot.Rows[i]["Remarks"].ToString();
            else
                opremarks = opremarks + " , " + dtot.Rows[i]["Remarks"].ToString();
        }

        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ক্লিনিকাল ডায়াগনসিস  :</td>");
        rpt.AppendFormat("<td style='width: 15%; padding-left:30px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DiagnosisName"]);
        rpt.Append("</tr >");

        if (dt.Rows[0]["TreatementNote"].ToString() != "")
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ট্রীটমেন্ট  নোট   :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["TreatementNote"]);
            rpt.Append("</tr >");
        }

        if (dtot.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অপারেশন নাম  :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", opname);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অপারেশন তারিখ   :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", opdate);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>অপারেশন লক্ষ্য   :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", opremarks);
            rpt.Append("</tr >");
        }


        if (dtdischarge.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>পরিস্থিতি নিষ্কাশন :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtdischarge.Rows[0]["Remarks"]);
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


    public void GetHearder_Detail1()
    {
        ltrReport.Text = "";
        DataTable dt = theabortion.GetPatientDtls(TextBox1.Text, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>PROVESONAL DISCHARGE CERTIFICATE  </td>");
        rpt.Append("</tr'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's Details </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Reg. No :</td>");
        rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Name :</td>");
        rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Age :</td>");
        rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0} yrs.</td>", dt.Rows[0]["age"]);

        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>C/O :</td>");
        rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Address :</td>");
        rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Mob No :</td>");
        rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
        rpt.Append("</tr >");


        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Post :</td>");
        rpt.AppendFormat("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["po"]);
        rpt.Append("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Police Station :</td>");
        rpt.AppendFormat("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ps"]);
        rpt.Append("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>District :</td>");
        rpt.AppendFormat("<td style='width: 8%;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DistrictName"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");


        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Admission Date :</td>");
        rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Admission Time :</td>");
        rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["FromTime"]);
        rpt.Append("</tr >");

        DataTable dtdischarge = theabortion.DischargeDtks(TextBox1.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        if (dtdischarge.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Discharge Date :</td>");
            rpt.AppendFormat("<td style='width: 8%; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtdischarge.Rows[0]["disdate"]);
            rpt.Append("<td style='width: 5%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Discharge Time :</td>");
            rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtdischarge.Rows[0]["DischargeTime"]);
            rpt.Append("</tr >");
        }

        rpt.Append("</table >");

        DataTable dtot = theabortion.GetOperation(TextBox1.Text, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        string opname = "", opdate = "", opremarks = "";
        for (int i = 0; i < dtot.Rows.Count; i++)
        {
            if (opname == "")
                opname = dtot.Rows[i]["OperationName"].ToString();
            else
                opname = opname + " , " + dtot.Rows[i]["OperationName"].ToString();

            if (opdate == "")
                opdate = dtot.Rows[i]["otdate"].ToString();
            else
                opdate = opdate + " , " + dtot.Rows[i]["otdate"].ToString();


            if (opremarks == "")
                opremarks = dtot.Rows[i]["Remarks"].ToString();
            else
                opremarks = opremarks + " , " + dtot.Rows[i]["Remarks"].ToString();
        }

        rpt.Append("<br />");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Clinical Diagnosis   :</td>");
        rpt.AppendFormat("<td style='width: 15%; padding-left:30px;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DiagnosisName"]);
        rpt.Append("</tr >");

        if (dt.Rows[0]["TreatementNote"].ToString() != "")
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Treatement Note  :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["TreatementNote"]);
            rpt.Append("</tr >");
        }

        if (dtot.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Operation Name   :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", opname);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Operation Date   :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", opdate);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width:3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Operation Note   :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", opremarks);
            rpt.Append("</tr >");
        }


        if (dtdischarge.Rows.Count > 0)
        {
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 3%; border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Discharge Condition :</td>");
            rpt.AppendFormat("<td style='width: 15%;padding-left:30px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtdischarge.Rows[0]["Remarks"]);
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

    public void PDF()
    {
        string filename = "ProvesonalDischargeCertificate" + TextBox1.Text;
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
        if (DropDownList1.SelectedIndex == 2)
        {
            GetHearder_Detail1();
        }
        else
        {
            GetHearder_Detail();
        }

        ltrReport.Text = rpt.ToString();
        PDF();
    }
}