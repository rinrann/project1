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

public partial class IPD_SickCertificate : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Eclamsia12 theabortion = new Eclamsia12(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Sick Certificate";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "SICK CERTIFICATE", checkAccessType.ViewAction) == false)
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
        DataTable dt = theabortion.GetPatientDtls(TextBox11.Text, Session["CoCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:Center'> <u> TO WHOM IT MAY CONCERN  </u> </td>");
            rpt.Append("</tr'>");


            rpt.Append("<table>");
            rpt.Append("<tr style='height:35px'>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Certified that   <b> {0} </b>, Aged <b>{1} yrs </b>H/F,<b> W/O</b> or <b>D/O</b> or <b>S/O  {2} </b> of Vill: {3}, PO & PS: {4} ; {5}  had undergone <b> {6} </b> on <b> {7} </b> at<b> GFC</b>, Kushpata, Ghatal, Paschim Medinipur</td>", dt.Rows[0]["patient_name"], dt.Rows[0]["age"], dt.Rows[0]["guardian_name"], dt.Rows[0]["vill_city"], dt.Rows[0]["ps"], dt.Rows[0]["DistrictName"], dt.Rows[0]["DiagnosisName"], dt.Rows[0]["ADate"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Mr/Mrs <b>{0}</b> is now under my treatment at <b>GFC</b>, Kushpata, Ghatal, Paschim Medinipur.</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Arial;font-size:small; text-align:justify'>He / She needs rest & restricted activity untill directed.</td>");
            rpt.Append("</tr >");

            rpt.Append("</table>");

            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:medium;font-weight:bold; text-align:left'> Dr. T K Karmakar </td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:medium;font-weight:bold; text-align:right'> Date: {0}</td>", Date);
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:small; font-weight:bold;text-align:left'> MBBS,DGO,MD,DNB,MNAMS,FICMCH	, MBA, FICOG </td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:medium; text-align:right'></td>");
            rpt.Append("</tr>");


            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:small;font-weight:bold; text-align:left'>Reg: 46599, W.B.</td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:medium;font-weight:bold; text-align:right'> </td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:small;font-weight:bold; text-align:left'> MEDICAL DIRECTOR </td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:medium; font-weight:bold;text-align:left'> GFC </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        ltrReport.Visible = true;

    }


    public void GetHearder_Detail1()
    {
        ltrReport.Text = "";
        DataTable dt = theabortion.GetPatientDtls(TextBox11.Text, Session["CoCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:Center'> <u> TO WHOM IT MAY CONCERN  </u> </td>");
            rpt.Append("</tr'>");


            rpt.Append("<table>");
            rpt.Append("<tr style='height:35px'>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Certified that   <b> {0} </b>, Aged<b> {1} yrs </b>H/F,<b> W/O</b> or <b>D/O</b> or <b>S/O  {2} </b> of Vill: {3}, PO & PS: {4} ; {5}  had undergone <b> {6} </b> on <b> {7} </b> at <b>GFC</b>, Kushpata, Ghatal, Paschim Medinipur</td>", dt.Rows[0]["patient_name"], dt.Rows[0]["age"], dt.Rows[0]["guardian_name"], dt.Rows[0]["vill_city"], dt.Rows[0]["ps"], dt.Rows[0]["DistrictName"], dt.Rows[0]["DiagnosisName"], dt.Rows[0]["ADate"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Mr/Mrs <b>{0}</b> is now under my treatment at <b>GFC</b>, Kushpata, Ghatal, Paschim Medinipur.</td>", dt.Rows[0]["patient_name"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:35px'>");
            rpt.Append("<td style='font-family:Arial;font-size:small; text-align:justify'>He / She needs rest & restricted activity untill directed.</td>");
            rpt.Append("</tr >");

            rpt.Append("</table>");

            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:medium;font-weight:bold; text-align:left'> Dr. T K Karmakar </td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:medium;font-weight:bold; text-align:right'> Date: {0}</td>", Date);
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:small; font-weight:bold;text-align:left'> MBBS,DGO,MD,DNB,MNAMS,FICMCH	, MBA, FICOG </td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:medium; text-align:right'></td>");
            rpt.Append("</tr>");


            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:small;font-weight:bold; text-align:left'>Reg: 46599, W.B.</td>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:medium;font-weight:bold; text-align:right'> </td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:small;font-weight:bold; text-align:left'> MEDICAL DIRECTOR </td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:medium; font-weight:bold;text-align:left'> GFC </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");
        }
        ltrReport.Visible = true;


    }


    public void PDF()
    {
        string filename = "SickCertificate" + TextBox11.Text;
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