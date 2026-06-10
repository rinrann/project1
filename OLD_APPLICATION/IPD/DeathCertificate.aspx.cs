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

public partial class IPD_DeathCertificate : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DeathCertificate theabortion = new DeathCertificate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Death Certificate";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DEATH CERTIFICATE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            RadioButtonList1.SelectedValue = "With Header";
            btnBack.Visible = false; btnPDF.Visible = false; cmdPrint.Visible = false;
            Panel2.Visible = false;
            RadioButtonList2.SelectedValue = "Current Report";
        }
    }

    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Arial;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Arial; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Arial; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Arial; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Arial; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Arial; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_Detail(string reg)
    {
        ltrReport.Text = "";
        DataTable dt = theabortion.GetPatientDtls(reg,Session["CoCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:Center'> <u> CERTIFICATE  OF DEATH  </u> </td>");
            rpt.Append("</tr'>");


            rpt.Append("<table>");
            rpt.Append("<tr style='height:35px'>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Certified that   <b> {0} </b>, Aged {1} yrs H/F,<b> W/O</b> or <b>D/O</b> or <b>S/O  {2} </b> of Vill: {3}, PO & PS: {4} ; {5}  has been carefully examined by me  and declared  his dead due to <b> {6} </b> as result of quadriplegia with pneumonia at 12.00 PM on 11st day of May, 2012 at <b>GFC</b>, Kushpata, Ghatal Paschim Medinipur, West Bengal, India, PIN 72121.</td>", dt.Rows[0]["patient_name"], dt.Rows[0]["age"], dt.Rows[0]["guardian_name"], dt.Rows[0]["vill_city"], dt.Rows[0]["ps"], dt.Rows[0]["DistrictName"], dt.Rows[0]["DiagnosisName"]);
            rpt.Append("</tr >");

            rpt.Append("</table>");





            rpt.Append("<br/>"); rpt.Append("<br/>");
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
            rpt.Append("</table>");


            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:small;font-weight:bold; text-align:left'> MEDICAL DIRECTOR </td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:medium; font-weight:bold;text-align:left'> GFC </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");

            ltrReport.Visible = true;
        }

    }


    public void GetHearder_Detail1(string reg1)
    {
       ltrReport.Text = "";
       DataTable dt = theabortion.GetPatientDtls(reg1, Session["CoCode"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%; font-family:Arial;font-weight:bold; font-size:larger; text-align:Center'> <u> CERTIFICATE  OF DEATH  </u> </td>");
            rpt.Append("</tr'>");


            rpt.Append("<table>");
            rpt.Append("<tr style='height:35px'>");
            rpt.AppendFormat("<td style='font-family:Arial;font-size:small; text-align:justify'>Certified that   <b> {0} </b>, Aged {1} yrs H/F,<b> W/O</b> or <b>D/O</b> or <b>S/O  {2} </b> of Vill: {3}, PO & PS: {4} ; {5}  has been carefully examined by me  and declared  his dead due to <b> {6} </b> as result of quadriplegia with pneumonia at 12.00 PM on 11st day of May, 2012 at <b>GFC</b>, Kushpata, Ghatal Paschim Medinipur, West Bengal, India, PIN 72121.</td>", dt.Rows[0]["patient_name"], dt.Rows[0]["age"], dt.Rows[0]["guardian_name"], dt.Rows[0]["vill_city"], dt.Rows[0]["ps"], dt.Rows[0]["DistrictName"], dt.Rows[0]["DiagnosisName"]);
            rpt.Append("</tr >");

            rpt.Append("</table>");

   



            rpt.Append("<br/>"); rpt.Append("<br/>");
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
            rpt.Append("</table>");


            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:small;font-weight:bold; text-align:left'> MEDICAL DIRECTOR </td>");
            rpt.Append("</tr>");

            rpt.Append("<tr>");
            rpt.Append("<td style='font-family:Arial;font-size:medium; font-weight:bold;text-align:left'> GFC </td>");
            rpt.Append("</tr>");
            rpt.Append("</table>");

            ltrReport.Visible = true;
        }


    }
    public void PDF(string pdf)
    {
        string filename = "DeathCertificate" +pdf;
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

   
    protected void Button4_Click1(object sender, System.EventArgs e)
    {
        TextBox1.Text = "";
        if (RadioButtonList1.SelectedValue == "With Header")
        {
            //   Button1.Enabled = false;
            Report_Header();
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1(TextBox11.Text);
                ltrReport.Text = rpt.ToString();
            }
            else
            {
                GetHearder_Detail(TextBox11.Text);
                ltrReport.Text = rpt.ToString();
            }
        }
        else
        {
            if (DropDownList1.SelectedIndex == 2)
            {
                GetHearder_Detail1(TextBox11.Text);
                ltrReport.Text = rpt.ToString();
            }
            else
            {
                GetHearder_Detail(TextBox11.Text);
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
    protected void RadioButtonList2_SelectedIndexChanged(object sender, System.EventArgs e)
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
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        TextBox11.Text = "";

        if (RadioButtonList1.SelectedValue == "With Header")
        {
            //   Button1.Enabled = false;
            Report_Header();
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
    protected void btnPDF_Click(object sender, System.EventArgs e)
    {
        if (TextBox1.Text == "")
        {
            if (DropDownList1.SelectedIndex == 2)
            {

                GetHearder_Detail1(TextBox11.Text);
            }
            else
            {
                GetHearder_Detail(TextBox11.Text);

            }

            ltrReport.Text = rpt.ToString();
            PDF(TextBox11.Text);
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
}