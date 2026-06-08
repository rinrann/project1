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

public partial class IPD_LigationReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Eclamsia12 theligation = new Eclamsia12(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "LIGATION CONSENT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        Page.Title = "Ligation Consent";
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
        DataTable dt = theligation.GetPatientDtls(txtreg.Text, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>  বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধের জন্য আবেদন   </td>");
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
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>মহাশয়/মহাশয়া, </td>");
            rpt.Append("</tr >");
            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:justify'>আমি বিবাহিত এবং আমার স্বামী বেঁচে আছেন । আমার বয়স <b> {0} বছর </b> , আমার স্বামীর বয়স _________বছর । বাড়িতে আমাদের _________টি পুত্র সন্তান ________ টি কন্যাসন্তান রয়েছে । সব থেকে কনিষ্ঠ সন্তানের বর্তমান বয়স________ বছর । তাই বন্ধ্যাত্বকরণ তথা বাচ্চা বন্ধের প্রযোজনীয় ব্যবস্থা করার জন্য আপনার কাছে প্রার্থনা করছি । প্রসঙ্গক্রমে আপনাকে জানাই :</td>", dt.Rows[0]["age"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(১)</b> এই বন্ধাত্বকরনের সিদ্ধান্তটি আমার নিজের।আমি স্বতঃস্ফূর্তভাবে এই সিদ্ধান্ত নিয়েছি। এই ব্যাপারে আমাকে কেউ চাপ দেয়নি বা বাধ্য করেনি । </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(২)</b> বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধের অন্যান্য প্রক্রিয়া সম্বন্ধে আমি অবগত। অন্যান্য প্রক্রিয়া সম্বন্ধে আমাকে ভালভাবে বোঝানো হয়েছে । </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(৩)</b> বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধ করতে হলে যে যে মানসিক ও শারীরিক সুস্থতার প্রয়োজন তা আমাকে ভালভাবে বোঝানো হয়েছে । ওই সব শোনার পর আমার মনে হয়েছে, এই মুহুর্তে আমি বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধ করানোর পক্ষেই উপযুক্ত । </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(৪)</b> বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধের উদ্দেশ্য আমি ভালভাবে জানি।এও জানি এই অপারেশনের পর আমি কোনদিন বাচ্চাধারণ করতে পারব না ।  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Vrinda;font-size:small; text-align:justify'><b>(৫)</b> আমি জানি এই অপারেশন পুরোপুরি ত্রুটিমুক্ত নয়। এই বন্ধ্যাত্বকরন বা বাচ্চাবন্ধের অপারেশনের পরও কোনও কোনও ক্ষেত্রে বিশেষ কারণে আবার গর্ভবতী হয়ে যেতে পারি। ডাক্তার বাবুর কাছ থেকে জেনেছি যে, এই ব্যতিক্রমি বা বিরল ঘটনার কথা চিকিৎসা বিজ্ঞানে উল্লেখ আছে । তাই তার জন্য আমি, আমার স্বামী, আমার অন্যান্য আত্মীয় কিম্ব্বা পরিচিতরা সংশ্লিষ্ট ডাক্তারবাবু বা এই প্রতিষ্ঠানকে দায়ী করব না বা করবেন না। এই অপারেশনের পর আমার নিয়মিত ঋতুস্রাবের কোনও অনিয়ম ব্যাঘাত ঘটলে আমি পনেরো দিনের মধ্যে এই প্রতিষ্ঠানে বা সংশ্লিষ্ট ডাক্তারবাবুকে জানাব। </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Vrinda;font-size:small; text-align:justify'><b>(৬)</b> এর আগে আমার স্বামী বাচ্চাবন্ধের অপারেশন করাননি। </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Vrinda;font-size:small; text-align:justify'><b>(৭)</b> আমি জানি এই অপারেশনে অনেক ঝুঁকি আছে এমন কি প্রাণহানি পর্যন্ত হতে পারে ।  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(৮)</b> এই প্রতিষ্ঠানের ডাক্তার বাবুর নির্দেশ মতো অপারেশনের পর নিয়মিত চেক্-আপ করতে বা দেখাতে আসব । যদি না আসতে পারি তাহলে কোনও কিছু অঘটন বা অসুবিধা হলে তার জন্য এই প্রতিষ্ঠান বা ডাক্তারবাবু দায়ী থাকবেন না।  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(৯)</b> এই বন্ধ্যাত্বকরণ বা বাচ্চা বন্ধের অপারেশনের জন্য সংশ্লিষ্ট ডাক্তারবাবু আমাকে তাঁর সুবিধা মতো যে কোনও ধরণের অজ্ঞান করতে এবং ওষুধ প্রদান ও প্রয়োগ করতে পারেন , সে ক্ষেত্রে আমার কোনও আপত্তি থাকবে না ।  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(১০)</b> আমাকে পুরোপুরি জানানো হয়েছে -- আমাকে যে সমস্ত ওষুধপত্র প্রয়োগ করা হবে বা পরে ব্যবহারের জন্য নির্দেশ দেওয়া হবে , সেগুলি স্বপ্লমেয়াদী বা দীর্ঘমেয়াদী প্রতিক্রিয়া থাকতে পারে, কোনও কোনও ক্ষেত্রে এই বিরূপ প্রতিক্রিয়া মারাত্বক হতে পারে --কিন্তু এক জন ডাক্তারবাবু এর জন্য কোনও মতেই দায়ী হতে পারে না । আশাকরি আমার এই আবেদনের প্রেক্ষিতে আপনার সুবিধে মতো সময়ে আমার বন্ধ্যাত্বকরণের তথা বাচ্চা বন্ধের অপারেশানের জন্য প্রয়োজনীয় পদ্ধতিগত ব্যবস্থা গ্রহণ করবেন। </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style=' font-family:Verdana;font-size:small; text-align:right'>ইতি--------- {0}</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:27px'>");
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
        }
        ltrReport.Visible = true;

    }


    public void GetHearder_Detail1()
    {
        ltrReport.Text = "";
        DataTable dt = theligation.GetPatientDtls(txtreg.Text, Session["CoCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>  LIGATION FORM   </td>");
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
            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:medium; text-align:left'>Dear Sir / Madam, </td>");
            rpt.Append("</tr >");
            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='font-family:Verdana;font-size:small; text-align:justify'>I am married and my husband alive.<b> {0}  years </b> of my age, my husband _________ years of age. ________ Son of _________ of us have daughters at home. ________ Years from the current age of the youngest child. So to make the necessary arrangements to stop children of bandhyatbakarana pray. By the way you are:</td>", dt.Rows[0]["age"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'> <b>(1)</b>  This bandhatbakaranera my own decision., I decided spontaneously to this. This has forced me to or not having one. </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(2)</b>   Bandhyatbakarana or child I knew about the process of closing the other. Other process has meant to me, as well. </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(3)</b>   Bandhyatbakarana to stop or if the child requires that the mental and physical health, it has meant to me, as well. After listening to all of that, I thought, this is the moment I stop or baby into bandhyatbakarana appropriate side.</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(4)</b>   I well know that the purpose of closing bandhyatbakarana or child. Baccadharana also know that someday I will be able to do this after the operation.  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(5)</b>   I know that this operation is not quite correct . This bandhyatbakarana or have any special reason baccabandhera after the operation can become pregnant again . Babu, learned from doctors that are mentioned in this exceptional or rare cases of medical science . So for me, my husband , my other relatives daktarababu or institution concerned kimbba contacts will not be liable or not . After this operation I have my regular period of fifteen days of the occurrence of an irregularity, interruption or the institution concerned shall daktarababuke . </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(6)</b>   Prior to my husband karanani baccabandhera operations. </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(7)</b>   I know there are many risks to this operation may even be killed.  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(8)</b>   This bandhyatbakarana or closing operations associated daktarababu child like me any sort of advantage to his senses and medicine as directed pradaei babu business operation after doctors regularly check - will show up or not. If you do not have to come if something strange or difficult for him not to be responsible for this institution or daktarababu.</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(9)</b>   This bandhyatbakarana or closing operations associated daktarababu child like me any sort of advantage to his senses and to apply the medicine and can also, in that case, I do not have any objections.  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.Append("<td style='font-family:Verdana;font-size:small; text-align:justify'><b>(10)</b>   I have been fully informed - to me that would be applied to all medicines should be given to the use of , or after , the long-term effects they may have sbaplameyadi or , in some cases, these adverse reactions may be serious - but no way for one person to be responsible daktarababu does not . Hopefully, in the context of this application, you can make me cum like a child at the time of closing my bandhyatbakaranera aparesanera necessary procedural steps to take .</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style=' font-family:Verdana;font-size:small; text-align:right'>Finish--------- {0}</td>", dt.Rows[0]["patient_name"]);
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
        }
        ltrReport.Visible = true;
    }

    public void PDF()
    {
        string filename = "LigationConsent" + txtreg.Text;
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