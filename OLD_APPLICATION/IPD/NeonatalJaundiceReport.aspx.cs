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

public partial class IPD_NeonatalJaundiceReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Eclamsia12 theJaundice = new Eclamsia12(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Neonatal Jaundice";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "NEONATAL JAUNDICE", checkAccessType.ViewAction) == false)
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

         DataTable dt = theJaundice.GetPatientDtls(TextBox1.Text, Session["CoCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>  রোগীর বিশেষ কয়েকটি আশঙ্কাজনক পরিস্থিতি ও তার ঝুঁকি সম্পর্কে স্বীকার পত্র    </td>");
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
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["HusbandName"]);
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
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> {0}</td>", dt.Rows[0]["DiagnosisName"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>রোগীর সমস্যা বা ঝুঁকির কারণ সমূহ: </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>বেশী জন্ডিস, খিঁচুনি,জীবনী শক্তি কম, লিভারের অবস্থা খারাপ, ফুসফুস কমজোরী, হার্ট দূর্বল, অন্যান্য অঙ্গ ভাল কাজ করছে না, খাওয়ানোর সমস্যা, যখন তখন বাচ্চার অবস্থা খারাপ হওয়ার প্রনণতা</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>ওই সমস্ত রোগীর যে সমস্ত বিপত্তি ঘটতে পারে :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>ইনফেকসন বা সেপশিস হতে পারে, শ্বাসকষ্ট হতে পারে, ব্রেনে রক্তপাত হতে পারে, হঠাৎ হৃদযন্র বিকল হতে পারে, পেট ফুলে যেতে পারে, যখন তখন বাচ্চার অবস্থা খারাপ হতে পারে, লিভার অকেজো হতে পারে, এমন কি মারা ও যেতে পারে, যে কোন মুহূর্তে অন্যত্র নিয়ে যেতে হতে পারে</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'>এই প্রতিষ্ঠানের পক্ষ থেকে রোগীর এই সমস্ত সমস্যা ও আশঙ্কার কথা জানলাম । এই প্রতিষ্ঠানের পক্ষ থেকে আমাদের বারবার কোন সরকারী মেডিকেল কলেজ কিংবা অন্য কোনও স্বাস্থ্য প্রতিষ্ঠানের নিয়ে যাবার কথা বলা হয়েছে , আমরাও এই প্রতিষ্ঠান যে সমস্ত পরামর্শ দিয়েছে তারসঙ্গে একমত । কিন্তু এই মূহুর্তে রোগীকে অন্যত্র নিয়ে যাবার মতো পরিস্থিতি আমাদের নেই , তাই রোগীর জীবন হানী সহ সমস্ত ঝুঁকির কথা মাথায় রেখেই আমরা আপদকালীন চিকিৎ্সার জন্য রোগীকে এই প্রতিষ্ঠানে ভর্তি করলাম , এখানে রোগীর প্রয়োজনীয় যাবতীয় চিকিত্সা করার জন্য অনুমতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'>আমরা যেহেতু সুস্থ মনে আমাদের আত্মীয় পরিজনদের সঙ্গে আলোচনা করে রোগীর চিকিত্সা চালানোর অনুমতি দিচ্ছি সেক্ষেত্রে চিকিত্সা চলাকালীন কিংবা এই প্রতিষ্ঠান থেকে ছাড়া পাওয়ার পর রোগীর অন্য কোন জটিল সম্যসা হলে কিংবা জীবন সংশয় হলে আমরা এই প্রতিষ্ঠানকে দায়ী করব না ।</td>");
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
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", dt.Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            ltrReport.Visible = true;
        }

    }


    public void GetHearder_Detail1()
    {
        ltrReport.Text = "";
        DataTable dt = theJaundice.GetPatientDtls(TextBox1.Text, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> Some of the critical situation of the patient about the risks and accept the Letter  </td>");
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
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["HusbandName"]);
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
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> {0}</td>", dt.Rows[0]["DiagnosisName"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold; text-align:left'>Problems or risk factors of the patient  : </td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'> Than jaundice, convulsions, biographies energy less severe liver condition, deficient lungs, heart weak, does not work well in other organs, feeding problems, when the child's condition is worse prananata </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>"); rpt.Append("<u>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium;font-weight:bold;text-align:left'>Hazard that may occur in the patient  :</td>"); rpt.Append("</u>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:left'>Sepasisa inaphekasana or may be, may be shortness of breath, bleeding may brene, hrdayanra can crash suddenly, the stomach may be swollen, when the child could be worse, could be liver failure, and even death can be anywhere at any time may be taken</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> The organization learned from the patient about these issues and concerns . This organization has repeatedly us from any government medical college or any other health organizations have been asked to take , we suggest that these institutions have agreed along . But now the situation is like taking away patients who do not have , so we are keeping in mind the patient's life is at risk , including hostile cikitsara emergency admissions for patients who have made this institution , the patient gave permission for the necessary treatment .  </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style=' font-family:Verdana;font-size:small; text-align:justify'> We think it's healthy to talk with our kindred allowed to treat the patient during treatment, or in this case, the organization would be without the complicated error occurred when the patient or any other life-threatening if not, we'll blame the institution. </td>");
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
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='font-family:Verdana; font-size:small; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:27px'>");
            rpt.Append("<td style='font-family:Verdana;  font-size:small; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='font-family:Verdana;  font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:small; text-align:center'> {0} </td>", dt.Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");

            ltrReport.Visible = true;
        }
    }

    public void PDF()
    {
        string filename = "NeonatalJaundice" + TextBox1.Text;
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