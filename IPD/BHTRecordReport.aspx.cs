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

public partial class IPD_BHTRecordReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BHTReportClass theBHTReportClass = new BHTReportClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "BHT Record";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BHT REPORT", checkAccessType.ViewAction) == false)
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
            GetHearder_Detail();
        }
        else
        {
            GetHearder_Detail(); 
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

    public void Report_Header()
    {
        rpt.Append("<br/>");
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
        DataTable dt = theBHTReportClass.GetPatientDtls(TextBox1.Text, Session["CoCode"].ToString().Trim());
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>  BHT Record Details    </td>");
        rpt.Append("</tr'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> Patient's Details </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Reg.No :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Name :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Age :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["age"]);


        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>C/O :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Address :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Mo.No :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Bed No :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["BedNoText"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Admission Date :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Admission Time :</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["FromTime"]);
        rpt.Append("</tr >");
        rpt.Append("</table >");

        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr >");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Date</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Time</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>BP</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Bleeding</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Chest</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Doppler</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>PA</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>PV</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Pulse</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>SPO2</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Sunction</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Temp</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Urin</td>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Others</td>");
        rpt.Append("</tr >");

        DataTable dt1 = theBHTReportClass.GetBHTDetails(TextBox1.Text, Session["CoCode"].ToString().Trim());
         for (int i = 0; i < dt1.Rows.Count; i++)
         {
             rpt.Append("<tr >");
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["Date"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["Time"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["BP"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["Bleeding"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["Chest"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["Doppler"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["PA"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["PV"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["Pulse"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["SPO2"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["Sunction"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["Temp"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["Urin"]);
             rpt.AppendFormat("<td style='border-top: 1px solid black;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt1.Rows[i]["Others"]);
             rpt.Append("</tr >");
         }
        rpt.Append("</table >");
        ltrReport.Visible = true;

    }


   
    public void PDF()
    {
        string filename = "MTPConsent" + TextBox1.Text;
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
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
        PDF();
    }
}