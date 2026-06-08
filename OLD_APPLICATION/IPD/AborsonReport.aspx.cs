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


public partial class IPD_AborsonReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AborsonReport theabortion = new AborsonReport(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "MTP Consent";
        //if (Session["userName"] == null)
        //{
        //    Response.Redirect("../LoginPage.aspx");
        //}
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MTP CONSENT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
           
            RadioButtonList1.SelectedValue = "With Header";
            btnBack.Visible = false; btnPDF.Visible = false; cmdPrint.Visible = false;
        
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "With Header")
        {
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
          DataTable dt = theabortion.GetPatientDtls(txtreg.Text, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>  গর্ভপাতের সম্মতি পত্র    </td>");
        rpt.Append("</tr'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> রোগীর বিবরণ </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>নিবন্ধ সংখ্যা</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>রোগীর নাম</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>বয়স</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["age"]);


        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>গার্ডিয়ান নাম</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ঠিকানা</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ফোন নং</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>বিছানা সংখ্যা</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["BedNoText"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ভর্তি তারিখ</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ভর্তি টাইম</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["FromTime"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>যিনি ভর্তি করছেন</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>সম্পর্ক</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["relation"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>ডাক্তার বাবুর নাম</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["doc_name"]);
        rpt.Append("</tr >");
        rpt.Append("</table >");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;font-weight:bold; font-size:medium; text-align:left'> ভর্তির কারণ :</td>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> {0}</td>", dt.Rows[0]["Diagnosis"]);
        rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> </td>");
        rpt.Append("</tr >");
        rpt.Append("</table>");

        rpt.Append("<table width='100%'>");
        rpt.Append("<tr style='height:25px'>");
        rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>আমার গর্ভে যে সন্তান আছে তা আমি জানি এবং ডাক্তারবাবুও আমাকে পরীক্ষা করে সে কথা আমাকে জানালেন । কিন্তু আমি আমার____________ কারণে আমার এই গর্ভস্থ সন্তান নষ্ট করে দিতে চাই । </td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:25px'>");
        rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>ডাক্তারবাবু গর্ভপাত করানোর পদ্ধতি ও তার সম্ভাব্য সমস্ত জটিলতা সম্বন্ধে আমাকে জানিয়েছেন । এমন কি কোনও কোনও ক্ষেত্রে প্রছুর রক্তস্রাব ,পেটের অন্ত্রে ক্ষতি বা জীবাণু সংক্রামনের মতো যাতে জীবণ সংশয় পর্যন্ত হতে পারে বা জরায়ূ-কে আপারেশনের মাধ্যমে বাদ দিতে পর্যন্ত হতে পারে । এসব ভালভাবে জেনে এই গর্ভপাতে অনুমতি দিলাম । আরও জানলাম যে , চিকিৎসা বিজ্ঞানের তথ্যানুযায়ী গর্ভপাতর ফলে ভবিষ্যতে আবার বাচ্চা না-আসার সম্ভাবনা সহ সূদূর প্রসারী আরও অনেক কুফল আছে । এসবের জন্য ডাক্তারবাবু দায়ী থাকবেন না।</td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:25px'>");
        rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>এই সম্মতি পত্রে স্বেচ্ছায়, সমস্ত কথার মানে বুঝে সই করলাম । </td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:25px'>");
        rpt.AppendFormat("<td style=' font-family:Verdana;font-size:medium; text-align:right'>ইতি--------- {0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");




        rpt.Append("<table width='100%'>");
        rpt.Append("<tr style='height:35px'>");
        rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
        rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
        rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
        rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:27px'>");
        rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
        rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> নিজে </td>");
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:27px'>");
        rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
        rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["guardian_name"]);
        rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", dt.Rows[0]["relation"]);
        rpt.Append("</tr >");
        rpt.Append("</table>");

        ltrReport.Visible = true;

    }


    public void GetHearder_Detail1()
    {
        ltrReport.Text = "";
        DataTable dt = theabortion.GetPatientDtls(txtreg.Text, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>  MTP CONSENT   </td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's Details </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Patient's Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Patient's Age</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["age"]);


            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Guadian's Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Address</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Contact No</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Bed No</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["BedNoText"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Admission Date</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Admission Time</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["FromTime"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Admitted By</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Relation</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["relation"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Doctor's Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["doc_name"]);
            rpt.Append("</tr >");
            rpt.Append("</table >");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;font-weight:bold; font-size:medium; text-align:left'> Reasons for admission:</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> {0}</td>", dt.Rows[0]["Diagnosis"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>I know that there are children in my womb and told me he would check me daktarababuo.But I lost my ____________ to the fetus due to me like this.</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>About the abortion procedure and its potential complications daktarababu told me . Prachura even bleeding in some cases , loss of bowel or gut microbe that lives in sankramanera There may be confusion or jarayu - There may be removed through a aparesanera . We well know that these allow the abortion . Also learned that , according to medical science, abortion, the baby is due in the future - including the possibility of reaching sudura have many consequences . Shall not be liable for these daktarababu .</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:justify'>About the abortion procedure and its potential complications daktarababu told me. Even at this Consent Form, signed by all mean I understand.</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style=' font-family:Verdana;font-size:medium; text-align:right'>Finish--------- {0}</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", dt.Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
        }
        ltrReport.Visible = true;
   }

    public void PDF()
    {
        string filename = "MTPConsent" + txtreg.Text;
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