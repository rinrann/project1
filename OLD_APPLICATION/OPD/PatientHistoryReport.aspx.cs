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
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;

public partial class OPD_PatientHistoryReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientHistory theabortion = new PatientHistory(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "History Report";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT HISTORY", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Report_Header();
        GetHearder_Detail1();
        //if (DropDownList1.SelectedIndex == 2)
        //{
        //    GetHearder_Detail1();
        //}
        //else
        //{
        //    GetHearder_Detail();
        //}
        ltrReport.Text = rpt.ToString();
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
        //rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
        //rpt.Append("<tr cellpadding:'0'>");
        //rpt.AppendFormat("<td rowspan='4' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "GFC Hospital");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        //rpt.Append("</tr>");
        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "REG NO : NH-315/G-70/2013");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        //rpt.Append("</tr>");
        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        //rpt.Append("</tr>");
        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>{0}</td>", "Ph :(03225)244400/244643  M:9434419825");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        //rpt.Append("</tr>");
        //rpt.Append("</table>");
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date = DateTime.Now.ToString("dd/MM/yyyy");

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
        rpt.Append("<td   width='40%'  style='height:18px;font-family:Arial; font-size:medium; text-align:center'></td>");
        rpt.Append("<td width='30%' style='text-align:right'></td>");
        rpt.Append("</tr>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.Append("<td width='30%'></td>");
        rpt.Append("<td   width='40%'  style='height:20px;font-family:Arial; font-size:medium; text-align:center'><b><u>HISTORY REPORT</u></b></td>");
        rpt.Append("<td width='30%' style='text-align:right;'>Print Date : " + date + "</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }
    
    public void GetHearder_Detail()
    {
        ltrReport.Text = "";
        DataTable dt = theabortion.GetPatientDtls(Session["CoCode"].ToString(), TextBox2.Text);
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
        DataTable dt = theabortion.GetPatientDtls(Session["CoCode"].ToString(), TextBox2.Text.Trim());
        
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
        //rpt.Append("<tr style='height:40px'>");
        //rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>HISTORY REPORT</td>");
        //rpt.Append("</tr'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='4' style='width: 100%;border-left: 1px solid black;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's Details </td>");
        rpt.Append("</tr'>");
        
        rpt.Append("<tr>");
        rpt.Append("<td style='border-left: 1px solid black;width: 20%;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Name</td>");
        rpt.AppendFormat("<td colspan='3' style='border-right: 1px solid black;width: 80%;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", ": " + dt.Rows[0]["patient_name"].ToString().ToUpper());
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='border-left: 1px solid black;width: 20%;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Registration No</td>");
        rpt.AppendFormat("<td style='width: 40%;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", ": " + dt.Rows[0]["PatientReg"].ToString().ToUpper());
        rpt.Append("<td style='width: 20%;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Date</td>");
        rpt.AppendFormat("<td style='border-right: 1px solid black;width: 40%;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", ": " + dt.Rows[0]["RegistrationDate"].ToString().ToUpper());
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='border-left: 1px solid black;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Gender</td>");
        rpt.AppendFormat("<td  style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", ": " + dt.Rows[0]["SexName"].ToString());
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Age</td>");
        rpt.AppendFormat("<td  style='border-right: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", ": " + dt.Rows[0]["age"].ToString() +" yrs");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='border-left: 1px solid black;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Guadian's Name</td>");
        rpt.AppendFormat("<td  style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", ": " + dt.Rows[0]["guardian_name"].ToString());
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Spouce Name</td>");
        rpt.AppendFormat("<td  style='border-right: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", ": " + dt.Rows[0]["SpouseName"].ToString());
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='border-left: 1px solid black;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Address</td>");
        rpt.AppendFormat("<td  style='font-family:Verdana; font-size:small;text-align:left'>{0}</td>", ": " + dt.Rows[0]["vill_city"].ToString());
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Contact No</td>");
        rpt.AppendFormat("<td  style='border-right: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", ": " + dt.Rows[0]["PhNo1"].ToString());
        rpt.Append("</tr>");

        //rpt.Append("</tr'>");
        //rpt.Append("<tr style='height:40px'>");
        //rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's History </td>");
        //rpt.Append("</tr'>");
        rpt.Append("</table>");
        


        //DataTable dthistory = theabortion.GetHistory(TextBox2.Text);
        //if (dthistory.Rows.Count > 0)
        //{
        //    rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        //    rpt.Append("<tr style='height:30px'>");
        //    rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Date</td>");
        //    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Mens</td>");
        //    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Operation Details</td>");
        //    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Special</td>");
        //    rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Others</td>");
        //    rpt.Append("</tr >");
        //    for (int i = 0; i < dthistory.Rows.Count; i++)
        //    {
        //        rpt.Append("<tr style='height:30px'>");
        //        rpt.AppendFormat("<td colspan='2' style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dthistory.Rows[i]["Date1"]);
        //        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dthistory.Rows[i]["Mens"]);
        //        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dthistory.Rows[i]["OperationDtls"]);
        //        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dthistory.Rows[i]["Special"]);
        //        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dthistory.Rows[i]["Others"]);
        //        rpt.Append("</tr >");
        //    }
        //    rpt.Append("</table>");
        //}

      




        

        //DataTable dtvaccination = theabortion.GetVaccine(TextBox2.Text);
        //if (dtvaccination.Rows.Count > 0)
        //{
        //    rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        //    rpt.Append("</tr'>");
        //    rpt.Append("<tr style='height:40px'>");
        //    rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's Vaccination </td>");
        //    rpt.Append("</tr'>");

        //    rpt.Append("<tr style='height:30px'>");
        //    rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Vaccine Name</td>");
        //    rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Vaccine Date</td>");
        //    rpt.Append("<td colspan='2' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Comment</td>");
        //    rpt.Append("</tr >");
        //    for (int i = 0; i < dtvaccination.Rows.Count; i++)
        //    {
        //        rpt.Append("<tr style='height:30px'>");
        //        rpt.AppendFormat("<td colspan='2'  style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtvaccination.Rows[i]["Name"]);
        //        rpt.AppendFormat("<td colspan='2'  style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtvaccination.Rows[i]["Date1"]);
        //        rpt.AppendFormat("<td colspan='2'  style='width: 5%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtvaccination.Rows[i]["Comment"]);
        //        rpt.Append("</tr >");
        //    }
        //    rpt.Append("</table>");
        //}













        DataTable dtinvestigation = theabortion.GetInvestigation(Session["CoCode"].ToString(), TextBox2.Text.Trim());
        if (dtinvestigation.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-left: 1px solid black;border-right: 1px solid black;border-top: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's Investigation </td>");
            rpt.Append("</tr'>");

            
            string reqNoOld = "", reqNonew = "";
            Decimal totAmt = 0;
            int i = 0;
            for (i = 0; i < dtinvestigation.Rows.Count; i++)
            {
                reqNonew = dtinvestigation.Rows[i]["RequisitionNo"].ToString();
                if (reqNonew != reqNoOld && reqNoOld != "")
                {
                    rpt.Append("<tr>");
                    rpt.AppendFormat("<td colspan='2' style='border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "");
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left;font-weight:bold;'>{0}</td>", "Total Amount");
                    rpt.AppendFormat("<td style='border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:right;font-weight:bold;'>{0}</td>", totAmt.ToString());
                    rpt.Append("</tr >");

                    rpt.Append("<tr>");
                    rpt.AppendFormat("<td colspan='2' style='border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "");
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left;font-weight:bold;'>{0}</td>", "Discount Amount");
                    rpt.AppendFormat("<td style='border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:right;font-weight:bold;'>{0}</td>", dtinvestigation.Rows[i]["DiscountAmt"].ToString());
                    rpt.Append("</tr >");

                    rpt.Append("<tr >");
                    rpt.AppendFormat("<td colspan='2' style='border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "");
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left;font-weight:bold;'>{0}</td>", "Paid Amount");
                    rpt.AppendFormat("<td style='border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:right;font-weight:bold;'>{0}</td>", dtinvestigation.Rows[i]["PaidAmt"].ToString());
                    rpt.Append("</tr >");

                    rpt.Append("<tr >");
                    rpt.AppendFormat("<td colspan='2' style='border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "");
                    rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left;font-weight:bold;'>{0}</td>", "Due Amount");
                    rpt.AppendFormat("<td style='border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:right;font-weight:bold;'>{0}</td>", dtinvestigation.Rows[i]["DueAmt"].ToString());
                    rpt.Append("</tr >");
                    totAmt = 0;

                }
                if (reqNonew != reqNoOld)
                {
                    rpt.Append("<tr>");
                    rpt.Append("<td colspan='2' style='border-left: 1px solid black;border-top: 1px solid black;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Requisition No : " + reqNonew.ToUpper() + "</td>");
                    rpt.Append("<td colspan='2' style='border-right: 1px solid black;border-top: 1px solid black;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Date : " + dtinvestigation.Rows[i]["ReqDate"].ToString() + "  " + dtinvestigation.Rows[i]["Time"].ToString() + "</td>");
                    rpt.Append("</tr>");
                    rpt.Append("<tr>");
                    rpt.Append("<td colspan='4' style='border-left: 1px solid black;border-right: 1px solid black;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Ref By : " + dtinvestigation.Rows[i]["ReferalName"].ToString() + "</td>");
                    rpt.Append("</tr>");
                    rpt.Append("<tr style='height:30px'>");
                    rpt.Append("<td style='width: 20%;border-left: 1px solid black;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Service Group</td>");
                    rpt.Append("<td style='width: 30%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Service Name</td>");
                    rpt.Append("<td style='width: 30%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Performing Doc</td>");
                    rpt.Append("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>service Cost</td>");
                    rpt.Append("</tr >");
                }
                
                rpt.Append("<tr>");
                rpt.AppendFormat("<td style='border-left: 1px solid black;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtinvestigation.Rows[i]["ProfileName"].ToString());
                rpt.AppendFormat("<td style='border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtinvestigation.Rows[i]["TestName"].ToString());
                rpt.AppendFormat("<td style='border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dtinvestigation.Rows[i]["consultant"].ToString());
                rpt.AppendFormat("<td style='border-right: 1px solid black;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:right'>{0}</td>", dtinvestigation.Rows[i]["Cost"].ToString());
                rpt.Append("</tr >");

                totAmt = totAmt + Convert.ToDecimal(dtinvestigation.Rows[i]["Cost"]);
                
                reqNoOld = reqNonew;
            }
            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='2' style='border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "");
            rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left;font-weight:bold;'>{0}</td>", "Total Amount");
            rpt.AppendFormat("<td style='border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:right;font-weight:bold;'>{0}</td>", totAmt.ToString());
            rpt.Append("</tr >");


            rpt.Append("<tr>");
            rpt.AppendFormat("<td colspan='2' style='border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "");
            rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left;font-weight:bold;'>{0}</td>", "Discount Amount");
            rpt.AppendFormat("<td style='border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:right;font-weight:bold;'>{0}</td>", dtinvestigation.Rows[i-1]["DiscountAmt"].ToString());
            rpt.Append("</tr >");

            rpt.Append("<tr >");
            rpt.AppendFormat("<td colspan='2' style='border-left: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "");
            rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left;font-weight:bold;'>{0}</td>", "Paid Amount");
            rpt.AppendFormat("<td style='border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:right;font-weight:bold;'>{0}</td>", dtinvestigation.Rows[i - 1]["PaidAmt"].ToString());
            rpt.Append("</tr >");

            rpt.Append("<tr >");
            rpt.AppendFormat("<td colspan='2' style='border-left: 1px solid black;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", "");
            rpt.AppendFormat("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left;font-weight:bold;'>{0}</td>", "Due Amount");
            rpt.AppendFormat("<td style='border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:right;font-weight:bold;'>{0}</td>", dtinvestigation.Rows[i - 1]["DueAmt"].ToString());
            rpt.Append("</tr >");
            rpt.Append("</table>");
        }
        










        


        DataTable dtclinical = theabortion.GetClinicalFinding(TextBox2.Text);
        if (dtclinical.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='11' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's Clinical Finding </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Date</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Complain</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Weight</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>BP</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>P</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>E</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Chest</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>PA</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>PV</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>FH8</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;  font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Others</td>");
            rpt.Append("</tr >");
            for (int i = 0; i < dtclinical.Rows.Count; i++)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtclinical.Rows[0]["Date1"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtclinical.Rows[0]["ComplainName"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtclinical.Rows[0]["Weight"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtclinical.Rows[0]["BP"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtclinical.Rows[0]["P"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtclinical.Rows[0]["E"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtclinical.Rows[0]["Chest"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtclinical.Rows[0]["PA"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtclinical.Rows[0]["PV"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtclinical.Rows[0]["FH8"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;  font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtclinical.Rows[0]["Others"]);
                rpt.Append("</tr >");
            }
            rpt.Append("</table>");

        }

        










        


        DataTable dtprescription = theabortion.GetPrescriptiondtls(TextBox2.Text);
        if (dtprescription.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's Prescription </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Prescription ID</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Date</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Group Name</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Sub Group Name</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Medicine Name</td>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Dose</td>");
            rpt.Append("</tr >");
            for (int i = 0; i < dtprescription.Rows.Count; i++)
            {
                rpt.Append("<tr style='height:30px'>");
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtprescription.Rows[i]["PrescriptionId"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtprescription.Rows[i]["Date1"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtprescription.Rows[i]["MedicineGroupName"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtprescription.Rows[i]["SubGrName"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtprescription.Rows[i]["MedicineName"]);
                rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dtprescription.Rows[i]["Dose"]);
                rpt.Append("</tr >");
            }
            rpt.Append("</table >");
        }
        

        DataTable dtnote = theabortion.GetHospitalNote(TextBox2.Text);
        
        if (dtnote.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's Hospital Note </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td  colspan='6' style='width: 8%;border-bottom: 1px solid black;  font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Note Details</td>");
            rpt.Append("</tr >");
            rpt.Append("<tr style='height:30px'>");
            rpt.AppendFormat("<td  colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dtnote.Rows[0]["NoteDtls"]);
            rpt.Append("</tr >");
            rpt.Append("</table >");
        }
        else
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.AppendFormat("<td  colspan='6' style='width: 8%;border-left: 1px solid black;border-right: 1px solid black;border-bottom: 1px solid black; font-family:Verdana; font-size:small; text-align:center'>{0}</td>", "");
            rpt.Append("</tr >");
            rpt.Append("</table >");
        }
        
        



        ltrReport.Text = rpt.ToString();
        ltrReport.Visible = true;


    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchPatientName(string prefixText, int count)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select distinct PName + '~' + PatientRegNo +'~'+PhNo1+'~'+ ReferedBy as Name from OPD_PatientRegistration where compcode=@Compcode and PName like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Parameters.AddWithValue("@Compcode", compcode);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchPatientbyPhoneNo(string prefixText, int count)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select distinct PhNo1+'~'+PName + '~' + PatientRegNo +'~'+ ReferedBy as Name from OPD_PatientRegistration where compcode=@Compcode and PhNo1 like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Parameters.AddWithValue("@Compcode", compcode);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
}