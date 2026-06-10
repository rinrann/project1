using System;
using System.Data;
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
 
public partial class IPD_OperationConsentReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Eclamsia12 thepd = new Eclamsia12(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["userName"] == null)
        //{
        //    Response.Redirect("../LoginPage.aspx");
        //}
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OPERATION CONSENT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }


        if (!IsPostBack)
        {

            RadioButtonList1.SelectedValue = "With Header";
            btnBack.Visible = false; btnPDF.Visible = false; cmdPrint.Visible = false;

        }
        Page.Title = "Operation Content";
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
         DataTable dt = thepd.GetPatientDtls(txtreg.Text, Session["CoCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>          অপারেশন,অজ্ঞান করার পদ্ধতি,বিশেষ চিকিৎসা বা প্রক্রিয়ার সম্মতি পত্র   </td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> PATIENT DETAILS  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Reg. No</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Patient's Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Age</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["age"]);


            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Guadian Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["HusbandName"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Address</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Phone No</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Bed No</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["BedNoText"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Admission Date</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Admission Time</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["AdmissionTime"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Admitted By</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Relation</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["relation"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Doctor's Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["doc_name"]);
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
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'> আমি / আমার___________________চিকিৎসা, অপারেশন পদ্ধতিগুলি সংক্রান্ত আমার সমস্ত প্রশ্নের যথাযথ এবং সন্তোষজনক উত্তর এই প্রতিষ্ঠানের সংশ্লিষ্ট চিকিৎসকের কাছ থেকে পেয়ে আমার উপসর্গ গুলির যথাযথ চিকিৎসা/অপারেশন/রোগ নিরুপণ সংক্রান্ত পদ্ধতিগুলি সমাপনের নির্দেশ দিলাম । উক্ত চিকিৎসা অপারেশন, অজ্ঞান পদ্ধতি ও প্রক্রিয়াগুলি এবং সম্ভাব্য চিকিৎসা পদ্ধতিগুলির সঙ্গে সংশ্লিষ্ট ঝুঁকি , ঝামেলা জটিলতা ও পরিণামগুলি সম্পর্কে আমি অবহিত হয়েছি এবং জেনেছি এই অপারেশন বা চিকিৎসা চলাকালীন এমন কিছু অদৃষ্টপূর্ব অবস্থা ঘটতে পারে যাতে পূর্ব-নির্দ্দিষ্ট প্রক্রিয়া ছাড়াও অন্যান্য বা মূল পদ্ধতির পরিবর্জনের প্রয়োজন হতে পারে । উক্ত চিকিৎসক বা শল্যচিকিৎসক বা সহকর্মীগণকে যেন তাঁদের পেশাগত বিবেচনার বলে প্রয়োজনীয় চিকিৎসা ও শল্যপদ্ধতি গ্রহণ করেন , সে বিষয়ে আমার সম্মতি ও অনুরোধ রইল । যে কোনও চিকিৎসা বা শল্যচিকিৎসার ক্ষেত্রে প্রচুর রক্তক্ষরণ ,বীজদূষণ , হৃৎপিণ্ড অচলতা ইত্যাদি যে সমস্ত ঝুঁকি থাকে , সেগুলি এক্ষেত্রেও এসে যেতে পারে তাও আমাকে জানানো হয়েছে এবং আমার শারীরিক অসুস্থতা পুরোপুরি নিরাময়ের কোনও গ্যারেন্টি বা প্রতিশ্রুতি আমাকে দেওয়া হয়নি । তাই আমার চিকিৎসা চলাকালীন কোনও জটিলতা হলে আমি অযথা ডাক্তারবাবু ও তাঁর সহকর্মীগণকে কিম্বা নার্সিং হোম/হাসপাতালকে দায়ী করব না ।</td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");



            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>আমি সম্মতি দিচ্ছি যে, এই পরিষেবায় প্রয়োজনীয় ও উপযুক্ত যে কোনও ওষুধের সাহায্যে অজ্ঞানতা প্রয়োগ করা যেতে পারে । আমি মনে করি যে, অজ্ঞানতা প্রয়োগের ফলে জটিলতা দেখা দিতে পারে এবং অজ্ঞান পদ্ধতি ও অপারেশন চলাকালীন মস্তিষ্কের এবং দাঁতের ক্ষতি হতে পারে । </td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:justify'>বিচ্ছিন্ন শারীরিক কোনও অঙ্গ বা যন্ত্রাংশ যাতে নার্সিং হোম/হাসপাতাল কর্তৃপক্ষ তাঁদের প্রচলিত পদ্ধতি অনুসারে অপসরণ করতে পারেন, সে সম্পর্কে আমি সম্মতি দিলাম । </td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>ল্যাপারোস্কোপি বা হিস্টোরোস্কোপি বা দূরবীন শল্যচিকিৎসা বা মাইক্রো সার্জারি চলা কালীন পদ্ধতিগত জটিলতা দেখা দিলে --প্রথাগত ভাবে শল্যবিদ্যা অনুসরণ করার অর্থাৎ পেট কেটে শল্য চিকিৎসা করার অনুমতি দিলাম । ওই পদ্ধতিতে শল্যচিকিৎসার ক্ষেত্রে হৃৎপিণ্ড অচলাবস্থার মতো মারাত্বক জটিলতা দেখা দিতে পারে তাও জানলাম ।</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>আমি OT-তে থাকাকালীন চিকিৎসকের প্রয়োজনে বা নির্দেশে অন্যান্য পর্যবেক্ষকদের OT-তে প্রবেশ করার অনুমতি দেওয়ার পাশাপাশি শিক্ষা কিম্বা গবেষণার উদ্দেশ্যে অপারেশনের প্রক্রিয়া এবং আমার শরীর/দেহাংশের আলোকচিত্র গ্রহণ এবং আমার পরিচয় গোপন রেখে ওই সমস্ত আলোকচিত্র তৎক্ষণাৎ কিম্বা ভবিষ্যতে সম্প্রসারণ করার সম্মতি দিলাম । </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>কোনও জটিলতার ক্ষেত্রে অন্য কোনও ডাক্তারবাবুর সাহায্য নেওয়া হতে পারে এবং ওই ডা:বাবুর চিকিৎসায় আমার সম্মতি থাকল। জরুরি পরিস্থিতির খাতিরে,সংশ্লিষ্ট বিশেষ দরকারি সাজসজ্জাযুক্ত রোগী স্থানান্তরের ব্যবস্থা করতে পারেন । ওই ধরনের পরিস্থিতির উদ্ভব হলে আমরা প্রতিষ্ঠান কিম্বা ডাক্তারবাবুকে দায়ী করব না । প্রয়োজনীয় মেডিক্যাল, প্যাথলজিক্যাল, রেডিওলজিক্যাল, সাইটোলজিক্যাল এবং অন্যান্য অনুরূপ রোগ নির্ণয় পদ্ধতির রির্পোটের ভিত্তিতেই চিকিৎসা ও শল্য প্রযুক্তি কৌশল রচনা করেছেন সংশ্লিষ্ট ডাক্তারবাবু পরবর্তীকালে উক্ত রির্পোট জনিত চিকিৎসা ভ্রমের জন্য ডাক্তারবাবু কোনও মতেই দায়বদ্ধ থাকবেন না। আমাকে যে সমস্ত ওষুধপত্র প্রয়োগ করা হবে বা পরে ব্যবহারের জন্য নির্দেশ দেওয়া হবে, সেগুলি স্বপ্লমেয়াদী বা দীর্ঘমেয়াদী প্রতিক্রিয়া থাকতে পারে, কোনও কোনও ক্ষেত্রে এই বিরূপ প্রতিক্রিয়া মারাত্বক হতে পারে --কিন্তু আমরা জেনেছি, একজন ডাক্তারবাবু এর জন্য কোনও মতেই দায়ী থাকতে পারে না । কারোর প্ররোচনা ছাডা আমরা স্বেচ্ছায় ও সুস্থ মনে সমস্থ কিছুর মর্ম বুঝে এই অঙ্গীকার পত্রে সই করলাম। </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> স্বাক্ষর/টিপ </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> পুরো  নাম  </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> ঠিকানা </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> সম্পর্ক  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> নিজে </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", dt.Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");


            ltrReport.Visible = true;
        }

    }


    public void GetHearder_Detail1()
    {
         ltrReport.Text = "";
         DataTable dt = thepd.GetPatientDtls(txtreg.Text, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'> Operation, the method of the senses, in particular medical consent forms and procedures  </td>");
            rpt.Append("</tr'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> PATIENT DETAILS  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Reg. No</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Patient's Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Age</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["age"]);


            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Guadian Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["HusbandName"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Address</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Phone No</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Bed No</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["BedNoText"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Admission Date</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Admission Time</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["AdmissionTime"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Admitted By</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Relation</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["relation"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:left'>Doctor's Name</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:left'>{0}</td>", dt.Rows[0]["doc_name"]);
            rpt.Append("</tr >");
            rpt.Append("</table >");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;font-weight:bold; font-size:medium; text-align:left'> Reason of Admission :</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> {0}</td>", dt.Rows[0]["DiagnosisName"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:left'> </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>I / my _____________ treatment , appropriate and satisfactory answers to all of my questions regarding the method of operation of this organization to handle my symptoms associated with getting appropriate medical care from a doctor / operations / methods having the disease given point of termination .The medical operations , procedures and processes of the unconscious and the potential risks associated with medical procedures , complications and consequences of the trouble I 've been informed and know some of these operations or unforeseen conditions that may occur during treatment so that the East - in particular the needs of the other , or the procedure to be paribarjanera could . Professional discretion of the physician or surgeon or the necessary medical care they need and salyapaddhati sahakarmiganake took , she proceeded to ask me about the willingness and .In the case of any medical or surgery , bleeding , sepsis , immobility , etc. that are at risk of heart , they also informed me that the case may be , and have been completely cured of my physical illness, no guarantees or promises were given to me . During my treatment without any complications so I daktarababu and his sahakarmiganake or nursing home / hospital will not be responsible .</td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");



            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>I would agree that it is necessary and appropriate that these services can be applied to any drug of unconsciousness. I think that, implementation of ignorance and unconscious procedures and operations as a result of complications during a brain and can lead to loss of teeth. </td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:justify'>Isolated body parts so that no organ or nursing home / hospital authorities can withdraw them in accordance with conventional methods, she stuck with me and I agreed.</td>");

            rpt.Append("</tr >");
            rpt.Append("</table>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>Lyaparoskopi or historoskopi or telescope or micro surgery During surgery systemic complications arise - the traditional way to follow surgery to allow the surgical cut made ​​in the stomach. Stagnating in the heart of the serious complications of the surgery can also do.</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>While I was on OT-OT-observers on medical necessity or the other educational or research purposes as well as to allow access to the process of operation and my body / part of the body, taking the photograph and the photograph of me revealing the future to expand or accepted immediately.</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-size:medium; text-align:justify'>In case of any problems , and the other can be taken to help daktarababura Dr. Babu treatment began my consent . For emergency situations , associated particularly useful to transfer the patient to the decor . If such a situation that we will not be responsible institutions or daktarababuke . Medically necessary , pyathalajikyala , rediolajikyala , saitolajikyala and other similar methods of diagnosis based on the medical and surgical technology techniques rirpotera Related daktarababu subsequently wrote an account of the rirpota induced daktarababu not be responsible for medical oversight . That would be me after all the medicines should be given to the use of , or the long-term effects they may have sbaplameyadi , in some cases, these adverse reactions may be serious - but as we know , no one stays for a daktarababu can not be held liable . Provoking someone to quit voluntarily and healthy mind , we all understand the meaning of these things, I signed the letter of commitment . </td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Signature </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Full Name  </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Address </td>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Relation  </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> Self </td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>..................</td>");
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana;  font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["guardian_name"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.AppendFormat("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:medium; text-align:center'> {0} </td>", dt.Rows[0]["relation"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            ltrReport.Visible = true;
        }

    }

    public void PDF()
    {
        string filename = "OperationContent" + txtreg.Text;
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