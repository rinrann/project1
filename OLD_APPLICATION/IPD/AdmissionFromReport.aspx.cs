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

 
public partial class IPD_AdmissionFromReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AdmissionFromReport thepd = new AdmissionFromReport(ConfigurationSettings.AppSettings["ConnectionString"].ToString()); 
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADMISSION FORM", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        Page.Title = "Admission Form";
        if(!IsPostBack)
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
        rpt.AppendFormat("<td rowspan='4' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='/Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
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
          DataTable dt = thepd.GetPatientDtls(TextBox1.Text, Session["CoCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>  ভর্তি ফর্ম   </td>");
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
            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;font-weight:bold; font-size:medium; text-align:left'> ভর্তির কারণ :</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> {0}</td>", dt.Rows[0]["DiagnosisName"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'> আমরা সমস্ত কিছু জেনে বুঝে আমাদের রোগী কে এই প্রতিষ্ঠানে ভর্তি করছি। আমরা এই প্রতিষ্ঠানে সমস্ত অসুবিধা, সুবিধা ও চিকিৎসার সীমাবদ্ধতা সম্বন্ধে আমরা খুব ভালো ভাবে জেনেছি।আমাদের আত্মীয়বর্গ ও পরিজনের সংগে দীর্ঘ আলোচনার পরিপ্রেক্ষিতে সম্মিলিত ভাবে আমাদের রোগী ভর্তির এই সিদ্ধান্ত নিয়েছি। আমাদের এখানে রোগী ভর্তি করতে কেউ বাধ্য করেনি । </td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>কিছু কিছু ওষুধের পার্শ্ব প্রতিক্রিয়ায় রোগী খারাপ হয়ে যাওয়া বা মারা যাওয়ার মতো দূর্ঘটনা ঘটে যেতে পারে-যখন কারো কিছু করার থাকে না। এবং এর আগে থেকে কোন প্রতিষেধক নেওয়া যায় না। </td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana; font-size:medium; text-align:justify'>আমাদের রোগী যদি খুব খারাপ অবস্থা কোন কারনে হয়ে যায় এবং এই প্রতিষ্ঠানের চিকিৎসার সীমাবদ্ধতার বাইরে চলে যায় তখন রোগীর চিকিৎসার স্বার্থে আমরা আমাদের রোগীকে অন্যত্র নিয়ে যাবার প্রতিশ্রুতি দিচ্ছি।</td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা রোগীর চিকিৎসার জন্যে জীবনদায়ী ওষুধ, রক্ত এবং যখন যেমন দরকার তার ভিত্তিতে সেই সমস্ত জিনিষ সরবরাহ করে এই প্রতিষ্ঠান কে সবসময় সাহায্য ও সহযোগিতা করব। </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:medium; text-align:justify'>আমরা এই প্রতিষ্ঠানের কর্মকর্তা এবং সংশ্লিষ্ট ডাঃ বাবুর সংগে দীর্ঘ আলোচনা সাপেক্ষে সব কথা শুনে ভালোভাবে বুঝে এবং আমাদের সমস্ত জিজ্ঞাসার সন্তোষজনক উত্তর পেয়ে এখানে রোগী ভর্তি করলাম। </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            
            rpt.Append("<br/>"); rpt.Append("<br/>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='  font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style=' font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style=' font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style=' font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", dt.Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:medium; text-align:justify'>-----------------------------------------------------------------------------------------------------------------------</td>");
            rpt.Append("</tr >");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:justify'>আমরা রোগী কে সুস্থ অবস্থায় নিজ দায়িত্বে এখান থেকে নিয়ে যাচ্ছি। রোগীর সমস্ত চিকিৎসার নথি ও পরীক্ষার কাগজপত্র বুঝে পেলাম। ডাঃ বাবুকে নিয়মিত দেখাব ও তাৎক্ষনিক খুব জরুরী সমস্যা হলে সরকারী হাসপাতালে বা অন্যত্র যোগাযোগ করব। </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table >");
        }
        ltrReport.Visible = true;

    }


    public void GetHearder_Detail1()
    {
        ltrReport.Text = "";
        DataTable dt = thepd.GetPatientDtls(TextBox1.Text, Session["CoCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> ADMISSION FORM  </td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> PATIENT DETAILS  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Reg. No</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Patient's Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Age</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["age"]);


            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Guadian Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Address</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Phone No</td>");
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
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;font-weight:bold; font-size:medium; text-align:left'> Reason of Admission :</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> {0}</td>", dt.Rows[0]["Diagnosis"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> We have all of our patients who have knowingly been admitted to this institution. We all difficulties of this institution, we have a very good way and I learned about the benefits and limitations of treatment. Terms of long discussions with our kinsfolk and family together to make this decision for admission to our patients. None of the patients has forced us to refill. </td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'>Some patients fail to respond to drug side died in the accident or may be - when somebody has to do something. And cannot be taken before any antidote. </td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:justify'> Is due to the very bad condition of our patients, and this goes beyond the limitations of treatment, the patient's medical interests of the company, we promise to give our patients the transplant. </td>");

            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> We are life-saving drugs for the treatment of the patient, such as blood, and the need to provide the basis for all the things in this organization will always help and cooperation. </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'>We discuss the organization's officers and subject to long associated with Dr. Babu heard and understood well the patients admitted to our getting satisfactory answers to all questions asked. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana; font-size:small; text-align:center'> Signature / thumb </td>");
            rpt.Append("<td style=' font-family:Verdana; font-size:small; text-align:center'> Full Name  </td>");
            rpt.Append("<td style=' font-family:Verdana; font-size:small; text-align:center'> Address </td>");
            rpt.Append("<td style=' font-family:Verdana; font-size:small; text-align:center'> Relationship  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style=' font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style=' font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style=' font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style=' font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style=' font-family:Verdana; font-size:small; text-align:center'> {0} </td>", dt.Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana;  font-size:medium; text-align:justify'>-----------------------------------------------------------------------------------------------------------------------</td>");
            rpt.Append("</tr >");
            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style=' font-family:Verdana; font-size:small; text-align:justify'> We are going to take from the patient's own account a healthy condition. We found the patient understand all the medical documents and test papers. Dr. babuke very important issue both regular show and will contact government hospital or elsewhere. </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:32px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:small; text-align:center'> Signature / thumb </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:small; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:small; text-align:center'> Address </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:small; text-align:center'> Relationship  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table >");
        }
        ltrReport.Visible = true;

    }

    public void PDF()
    {
        string filename = "AdmissionForm" + TextBox1.Text;
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