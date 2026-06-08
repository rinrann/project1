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

public partial class Pathology_HospitalsDetailsReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    HospitalInfoClass theabortion = new HospitalInfoClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
  

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Bill Report";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "HOSPITAL INFO REPORT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        DropDownList1.SelectedIndex = 1;
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
            if (DropDownList1.SelectedIndex == 1)
            {
                GetHearder_DetailEng();
            }

        }
        else
        {
            if (DropDownList1.SelectedIndex == 1)
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
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Verdana; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../photo/" + Session["Logopath"].ToString().Trim() + "'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Verdana; font-size:x-large; font-weight:bold;text-align:center'><u>" + Session["CoName"].ToString().Trim() + "</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Verdana; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../photo/" + Session["Logopath2"].ToString().Trim() + "'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>(Regn. No : " + Session["HosRegnNo"].ToString().Trim() + ")</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Verdana; font-size:medium; font-weight:bold;text-align:center'>" + Session["ADDR"].ToString().Trim() + "</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_DetailEng()
    {
        ltrReport.Text = "";
        DataTable dt = theabortion.GridFill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        if (dt.Rows.Count > 0)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='100%' style='width: 5%; font-family:Verdana;font-weight:bold;font-size:medium; text-align:Center'> <b> Hospital Details </b>   </td>");
            rpt.Append("</tr'>");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Name of the Institution :</td>");
            rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["InstitutionName"]);
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Category :</td>");
            rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["Catagory"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Lisence No :</td>");
            rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["lisenceno"]);
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Valid Upto :</td>");
            rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["validity"]);
            rpt.Append("</tr >");



            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td  colspan='4' style='width: 5%; border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;font-size:medium; text-align:Center'> Address Details :</td>");
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Address :</td>");
            rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["Address"]);
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Email :</td>");
            rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["Email"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Phone No 1 :</td>");
            rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["Ph1"]);
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Phone No 2 :</td>");
            rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["Ph2"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Fax No :</td>");
            rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["Fax"]);
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> WebSite :</td>");
            rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["website"]);
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td  colspan='4' style='width: 5%; border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;font-size:medium; text-align:Center'> Bed Details :</td>");
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> ICU :</td>");
            rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["icu"]);
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Dialysis :</td>");
            rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["dialysis"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> NICU :</td>");
            rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["nicu"]);
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> General Ward :</td>");
            rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["generalward"]);
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Cabin :</td>");
            rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["cabin"]);
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Delux :</td>");
            rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["delux"]);
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> HDU :</td>");
            rpt.AppendFormat("<td style='width:8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["hdu"]);
            rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:Center'> Duplex :</td>");
            rpt.AppendFormat("<td style='width: 8%;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:Center'>{0}</td>", dt.Rows[0]["Duplex"]);
            rpt.Append("</tr >");


            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td  colspan='2' style='width: 5%; padding-right:50px; border-right: 1px solid black; border-bottom: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:right'> Total Bed :</td>");
            rpt.AppendFormat("<td  colspan='2' style='width:8%; padding-left:50px; border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["TotalBed"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td  style='width: 5%;border-right: 1px solid black; border-bottom: 1px solid black; font-family:Verdana;font-weight:bold; font-size:small; text-align:center'> RMO :</td>");
            rpt.AppendFormat("<td  colspan='3' style='width:8%; padding-left:40px; border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["Rmo"]);
            rpt.Append("</tr >");

            rpt.Append("</table>");
        }


    }

}