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

public partial class Pathology_AdmissionReference : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AdmissionReferenceClass theabortion = new AdmissionReferenceClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Admission Reference Certificate";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADMISSION REFERENCE CERTIFICATE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        DropDownList1.SelectedIndex = 0;
        DropDownList1.Enabled = false;

        if (!IsPostBack)
        {

            RadioButtonList1.SelectedValue = "With Header";
            btnBack.Visible = false; btnPDF.Visible = false; cmdPrint.Visible = false;

        }

    }
    protected void Button4_Click(object sender, System.EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "With Header")
        {
            Report_Header();
            if (DropDownList1.SelectedIndex == 0)
            {
                GetHearder_DetailEng();
            }

        }
        else
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                GetHearder_DetailEng();
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

    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Verdana; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Verdana; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Verdana; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Verdana; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_DetailEng()
    {


        ltrReport.Text = "";
        DataSet ds = theabortion.AdmissionReferCertificate(txtreg.Text, Session["CoCode"].ToString().Trim());

        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='100%' style='width: 5%; font-family:Verdana;font-weight:bold;font-size:medium; text-align:Center'> <b>Admission Reference Certificate</b></td>");
            rpt.Append("</tr'>");
            rpt.Append("</table>");
        }

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black; border-spacing: 0; padding: 0;font-family:verdana;'>");

        if (ds.Tables[0].Rows.Count > 0)
        {

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; padding-left:20px; font-size:Medium; text-align:Center'><b> Name </b> :{0},{1} yrs, {2}, <b> C/O </b> :{3} </td>", ds.Tables[0].Rows[0]["patient_name"], ds.Tables[0].Rows[0]["age"], ds.Tables[0].Rows[0]["SexName"], ds.Tables[0].Rows[0]["guardian_name"]);
            rpt.Append("</tr>");

            rpt.Append("<tr style='height:25px'>");
            rpt.AppendFormat("<td style='width: 8%; font-family:Verdana; padding-left:20px; font-size:Medium; text-align:Center ; text-transform: uppercase;'> <b> Address </b> :{0},{1},{2},{3}, <b> Mobile No </b> :{4} </td>", ds.Tables[0].Rows[0]["vill_city"], ds.Tables[0].Rows[0]["po"], ds.Tables[0].Rows[0]["PS"], ds.Tables[0].Rows[0]["DistrictName"], ds.Tables[0].Rows[0]["PhNo1"]);
            rpt.Append("</tr>");


        }

        rpt.Append("</table>");

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black; border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:25px'>");
        rpt.AppendFormat("<td colspan='6' style='width: 8%; border-bottom: 1px solid black; font-family:Verdana; padding-left:20px; font-size:Medium; text-align:Center'><b>Gravida</b> : &nbsp&nbsp&nbsp&nbsp,&nbsp&nbsp<b>Pavity</b> : &nbsp&nbsp&nbsp&nbsp,&nbsp&nbsp<b>Abortion</b> : &nbsp&nbsp&nbsp&nbsp,&nbsp&nbsp<b>Living</b> : &nbsp&nbsp&nbsp&nbsp</td>");
        rpt.Append("</tr>");


        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;  border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana;font-weight:bold;font-size:small; text-align:Center'>Clinical Diagnosis :</td>");
        rpt.AppendFormat("<td style='width: 8%; padding-left:20px; border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", ds.Tables[0].Rows[0]["DiagnosisName"]);
        rpt.Append("<td style='width: 5%; border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'>Dr. Name :</td>");
        rpt.AppendFormat("<td style='width: 8%; padding-left:20px; border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", ds.Tables[0].Rows[0]["doc_name"]);
        rpt.Append("</tr >");

       

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;  border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana;font-weight:bold;font-size:small; text-align:Center'> Cause Of Admission :</td>");
        rpt.AppendFormat("<td style='width: 8%; border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'></td>");
        rpt.Append("<td style='width: 5%; border-bottom: 1px solid black; border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'>Date & Time :</td>");
        rpt.AppendFormat("<td style='width: 8%; border-bottom: 1px solid black; font-family:Verdana;padding-left:20px;  font-size:small;text-align:left'>{0},{1}</td>", ds.Tables[0].Rows[0]["ADate"], ds.Tables[0].Rows[0]["FromTime"]);
        rpt.Append("</tr >");


        rpt.Append("</table>");


        rpt.Append("<br />"); rpt.Append("<br />");
        rpt.Append("<br />"); rpt.Append("<br />");
        rpt.Append("<table   width='100%' cellspacing=0 border=0 style='border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:10px'>");
        rpt.AppendFormat("<td  colspan='2' style='font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>................ </td>");
        rpt.Append("</tr >");
        rpt.Append("<tr style='height:10px'>");
        rpt.AppendFormat("<td style='font-family:Verdana;font-weight:bold;font-size:small; text-align:right'>Signature</td>");
        rpt.Append("</tr >");
        rpt.Append("</table>");

    }
}