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



public partial class IPD_BirthCertificate : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DischargeCertificate theabortion = new DischargeCertificate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BirthCertificate thepd = new BirthCertificate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    Discharge thepatientbill = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Birth Certificate";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BIRTH CERTIFICATE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        string kjfs = mydiv.ClientID;
        if (!IsPostBack)
        {
            RadioButtonList1.SelectedValue = "With Header";
            btnBack.Visible = false; 
            btnPDF.Visible = false; cmdPrint.Visible = false;
            Panel2.Visible = false;
            RadioButtonList2.SelectedValue = "Current Report";

            DataSet docTab = thepatientbill.GetDoctors(Session["CoCode"].ToString().Trim());
            //docTab.Tables[0].Columns.Add("SelDoc", typeof(string));
            //docTab.Tables[0].Columns["SelDoc"].DefaultValue = "false";
            GridView1.DataSource = docTab.Tables[0];
            GridView1.DataBind();
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
        DataTable dt = thepd.GetPatientDtls(reg, Session["CoCode"].ToString().Trim());
        DataTable dt1 = thepd.GetChildDtls(reg, Session["CoCode"].ToString().Trim());
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            Report_Header();

            rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> জাতক পত্র  </td>");
            rpt.Append("</tr'>");
            rpt.Append("</table>");
            rpt.Append("<br/>"); rpt.Append("<br/>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> মায়ের বিশদ বিবরণ  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>নিবন্ধ সংখ্যা</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>মায়ের নাম </td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>স্বামীর নাম</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["HusbandName"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>বয়স</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0} yrs.</td>", dt.Rows[0]["age"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> মায়ের ঠিকানা বিবরণ </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'> গ্রাম / শহর </td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>পোস্ট</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["po"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;font-weight:bold;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>থানা</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ps"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>জেলা</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DistrictName"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> শিশু বিশদ বিবরণ  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'> ভর্তি তারিখ </td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-weight:bold;font-family:Verdana;background-color:beige; font-size:small; text-align:left'>প্রসবের মোড</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt1.Rows[i]["DeliveryMode"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'> লিঙ্গ </td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt1.Rows[i]["SexName"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'> ওজন </td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0} কেজি</td>", dt1.Rows[i]["Weight"]);
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>জন্ম তারিখ</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt1.Rows[i]["deldt"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-weight:bold;font-family:Verdana;background-color:beige; font-size:small; text-align:left'>জন্ম টাইম </td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt1.Rows[i]["DeliveryTime"]);
            rpt.Append("</tr >");
            rpt.Append("</table >");



            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Dr. T. K. KARMAKAR</td>");
            rpt.Append("</tr >");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>MBBS , DGO, MD, DNB, MNAMS, FICMCH, FICOG, MBA, Ph.D</td>");
            rpt.Append("</tr >");
            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Regn No. WBMC 46599</td>");
            rpt.Append("</tr >");
            rpt.Append("</table>");
            ltrReport.Visible = true;
        }

    }


    public void GetHearder_Detail1(string reg1)
    {
        ltrReport.Text = "";
        DataTable dt = thepd.GetPatientDtls(reg1, Session["CoCode"].ToString().Trim());
        DataTable dt1 = thepd.GetChildDtls(reg1, Session["CoCode"].ToString().Trim());
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            Report_Header();

            rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> BIRTH CERTIFICATE  </td>");
            rpt.Append("</tr'>");
            rpt.Append("</table>");
            rpt.Append("<br/>");


            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> MOTHER'S DETAILS  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>REG. NO.</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>MOTHER'S NAME</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>HUSBAND NAME</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["HusbandName"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>AGE</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0} yrs.</td>", dt.Rows[0]["age"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> MOTHER'S ADDRESS DETAILS  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>VILLAGE / CITY</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>POST</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["po"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>POLICE STATION</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ps"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>DISTRICT</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DistrictName"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");
            rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> CHILD'S DETAILS  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;font-weight:bold;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>DATE OF ADMISSION</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>MODE OF DELIVERY</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt1.Rows[i]["DeliveryMode"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-weight:bold;font-family:Verdana;background-color:beige; font-size:small; text-align:left'>SEX</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt1.Rows[i]["SexName"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>WEIGHT</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0} KG</td>", dt1.Rows[i]["Weight"]);
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>BIRTH DATE</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt1.Rows[i]["deldt"]);
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>BIRTH TIME</td>");
            rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt1.Rows[i]["DeliveryTime"]);
            rpt.Append("</tr >");
            DataTable dtDoctor = theabortion.GetDoctorDetailsDtls(reg1, Session["CoCode"].ToString().Trim());
            if (dtDoctor.Rows.Count > 0)
            {
                rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />");
                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                rpt.Append("<tr style='height:0px'>");
                //rpt.AppendFormat("<td style='font-family:Verdana;text-align:left'><div style='font-size:small;height:0px;'>{0}</div> <br/><div style='font-size:x-small;height:0px;'>{1}</div> <br/><div style='font-size:x-small;height:0px;'>{2}</div> </td>", dtDoctor.Rows[0][0], dtDoctor.Rows[0][1], dtDoctor.Rows[0][2]);
                if (GridView1.Items.Count > 0)
                {
                    for (int c = 0; c < GridView1.Items.Count; c++)
                    {
                        CheckBox chk = (CheckBox)GridView1.Items[c].FindControl("chkSelect");
                        if (chk.Checked == true)
                        {

                            Label lblname = (Label)GridView1.Items[c].FindControl("lblName");
                            Label lblQuali = (Label)GridView1.Items[c].FindControl("lblQuali");
                            Label lblReg = (Label)GridView1.Items[c].FindControl("lblReg");
                            if (c % 4 != 0 && c != 0)
                            {
                                rpt.AppendFormat("</td>");
                                rpt.AppendFormat("<td style='font-family:Verdana;text-align:left'><div style='font-size:small;height:0px;'>{0}</div> <br/><div style='font-size:x-small;height:0px;'>{1}</div> <br/><div style='font-size:x-small;height:0px;'>{2}</div> </td>", lblname.Text, lblQuali.Text, lblReg.Text);
                            }
                            else
                            {
                                rpt.AppendFormat("</tr>");
                                rpt.Append("</table >");
                                rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />"); rpt.Append("<br />");
                                rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                                rpt.Append("<tr style='height:0px'>");
                                rpt.AppendFormat("<td style='font-family:Verdana;text-align:left'><div style='font-size:small;height:0px;'>{0}</div> <br/><div style='font-size:x-small;height:0px;'>{1}</div> <br/><div style='font-size:x-small;height:0px;'>{2}</div> </td>", lblname.Text, lblQuali.Text, lblReg.Text);
                            }
                        }
                    }
                }
                rpt.Append("</tr >");
                rpt.Append("</table >");



                //rpt.Append("<br/>");
                //rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                //rpt.Append("<tr style='height:30px'>");
                //rpt.Append("<td style='width: 8%;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Dr. T. K. KARMAKAR</td>");
                //rpt.Append("</tr >");
                //rpt.Append("<tr style='height:30px'>");
                //rpt.Append("<td style='width: 8%;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>MBBS , DGO, MD, DNB, MNAMS, FICMCH, FICOG, MBA, Ph.D</td>");
                //rpt.Append("</tr >");
                //rpt.Append("<tr style='height:30px'>");
                //rpt.Append("<td style='width: 8%;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Regn No. WBMC 46599</td>");
                //rpt.Append("</tr >");
                //rpt.Append("</table>");

                //rpt.Append("<br/>");
                //rpt.Append("<table width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
                //rpt.Append("<tr style='height:30px'>");
                //rpt.Append("<td style='width: 8%;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Dr. T. K. KARMAKAR</td>");
                //rpt.Append("</tr >");
                //rpt.Append("<tr style='height:30px'>");
                //rpt.Append("<td style='width: 8%;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>MBBS , DGO, MD, DNB, MNAMS, FICMCH, FICOG, MBA, Ph.D</td>");
                //rpt.Append("</tr >");
                //rpt.Append("<tr style='height:30px'>");
                //rpt.Append("<td style='width: 8%;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Regn No. WBMC 46599</td>");
                //rpt.Append("</tr >");
                //rpt.Append("</table>");
                ltrReport.Visible = true;
            }

        }
    }

    public void PDF(string pdf)
    {
        string filename = "BirthCertificate" +pdf;
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        txtreg.Text = "";

        if (RadioButtonList1.SelectedValue == "With Header")
        {
            //   Button1.Enabled = false;
            //Report_Header();
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
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void Button4_Click1(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        if (RadioButtonList1.SelectedValue == "With Header")
        {
           //Report_Header();
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
}