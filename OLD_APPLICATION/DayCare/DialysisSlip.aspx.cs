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
  
public partial class DayCare_DialysisSlip : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DC_DialysisSlip thediaslip = new DC_DialysisSlip(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DIALYSIS SLIP", checkAccessType.ViewAction) == false)
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
        Page.Title = "Dialysis Slip";
    }

    public void GetReport()
    {
        //Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }
    public void GetReport_Duplicate()
    {
        //Report_Header();
        GetHearder_Detail_Duplicate();
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
         DataTable dt = thediaslip.GetPatientDetails(txtreg.Text.Trim(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()); //ds.Tables[0];
         DataTable dt1 = thediaslip.GetNoofDia(txtreg.Text.Trim(), Session["CoCode"].ToString().Trim());
         if (dt.Rows.Count > 0)
         {

             rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
             rpt.Append("<tr style='height:30px'>");
             rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No :</td>");
             rpt.AppendFormat("<td colspan='2' style='width: 7%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
             rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Patient's Name :</td>");
             rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
             rpt.Append("</tr >");

             rpt.Append("<tr style='height:30px'>");
             rpt.Append("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Address :</td>");
             rpt.AppendFormat("<td  colspan='2' style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; text-align:left'>Phone No :</td>");
             rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
             rpt.Append("</tr>");

             rpt.Append("<tr  style='height:30px'>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:left'>Advanced  :</td>");
             rpt.AppendFormat("<td  colspan='2' style='width: 7%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-right: 1px solid black;text-align:left'>{0}</td>", dt.Rows[0]["Amount"]);
             rpt.Append("<td style='width: 5%; font-family:Verdana; border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-size:small;font-weight:bold; text-align:left'>Date :</td>");
             rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
             rpt.Append("</tr>");

           
                 rpt.Append("<tr style='height:30px'>");
                 rpt.Append("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Dialyser No :</td>");
                 if (dt1.Rows.Count > 0)
                 {
                     rpt.AppendFormat("<td colspan='2' style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>{0}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;({1})</td>", dt1.Rows[0]["DialyserNo"], dt1.Rows[0]["DiaNo"]);
                 }
                 else
                 {
                     rpt.AppendFormat("<td colspan='2' style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'></td>");
                 }
                 rpt.Append("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Shift :</td>");
                 rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;text-align:left'>{0}</td>", dt.Rows[0]["ShiftName"]);
                 rpt.Append("</tr>");
          
             rpt.Append("</table>");

             rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
             rpt.Append("<table>");

             rpt.Append("<tr>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>______________________________________</td>");
             rpt.Append("</tr'>");

             rpt.Append("<tr>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>Signature with Date</td>");

             rpt.Append("</tr'>");
             rpt.Append("</table>");
             rpt.Append("<hr>");
         }
         else
         {
             lblError.ForeColor = System.Drawing.Color.Red;
             lblError.Text = "Please Select Another Patient...";

         }
     }


     public void GetHearder_Detail_Duplicate()
     {
         DataTable dt = thediaslip.GetPatientDetailsDuplicate(TextBox1.Text.Trim(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim()); //ds.Tables[0];
         DataTable dt1 = thediaslip.GetNoofDia(TextBox1.Text.Trim(), Session["CoCode"].ToString().Trim());
         if (dt.Rows.Count > 0)
         {

             rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
             rpt.Append("<tr style='height:30px'>");
             rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No :</td>");
             rpt.AppendFormat("<td colspan='2' style='width: 7%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
             rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Patient's Name :</td>");
             rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
             rpt.Append("</tr >");

             rpt.Append("<tr style='height:30px'>");
             rpt.Append("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;background-color:beige;font-weight:bold; font-size:small; text-align:left'>Address :</td>");
             rpt.AppendFormat("<td  colspan='2' style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; text-align:left'>Phone No :</td>");
             rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
             rpt.Append("</tr>");

             rpt.Append("<tr  style='height:30px'>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold; text-align:left'>Advanced  :</td>");
             rpt.AppendFormat("<td  colspan='2' style='width: 7%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-right: 1px solid black;text-align:left'>{0}</td>", dt.Rows[0]["Amount"]);
             rpt.Append("<td style='width: 5%; font-family:Verdana; border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-size:small;font-weight:bold; text-align:left'>Date :</td>");
             rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; border-bottom: 1px solid black;font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
             rpt.Append("</tr>");

             rpt.Append("<tr style='height:30px'>");
             rpt.Append("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Dialyser No :</td>");
             rpt.AppendFormat("<td colspan='2' style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small;text-align:left'>{0}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;({1})</td>", dt1.Rows[0]["DialyserNo"], dt1.Rows[0]["DiaNo"]);
             rpt.Append("<td style='width: 5%; font-family:Verdana;border-bottom: 1px solid black;background-color:beige;border-right: 1px solid black;font-weight:bold; font-size:small; text-align:left'>Shift :</td>");
             rpt.AppendFormat("<td style='width: 7%; font-family:Verdana; font-size:small;border-bottom: 1px solid black;text-align:left'>{0}</td>", dt.Rows[0]["ShiftName"]);
             rpt.Append("</tr>");

             rpt.Append("</table>");

             rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
             rpt.Append("<table>");

             rpt.Append("<tr>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>______________________________________</td>");
             rpt.Append("</tr'>");

             rpt.Append("<tr>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
             rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>Signature with Date</td>");

             rpt.Append("</tr'>");
             rpt.Append("</table>");
             rpt.Append("<hr>");
         }
         else
         {
             lblError.ForeColor = System.Drawing.Color.Red;
             lblError.Text = "Please Select Another Patient...";

         }
     }

     protected void Button4_Click(object sender, EventArgs e)
     {
         if (RadioButtonList2.SelectedValue == "Current Report")
         {
             TextBox1.Text = "";
             if (RadioButtonList1.SelectedValue == "With Header")
             {
                 Report_Header();
                 GetReport();


             }
             else
             {
                 GetReport();

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
     protected void Button2_Click(object sender, System.EventArgs e)
     {
         if (RadioButtonList2.SelectedValue == "Duplicate Report")
         {
             txtreg.Text = "";
             if (RadioButtonList1.SelectedValue == "With Header")
             {
                 Report_Header();
                 GetReport_Duplicate();


             }
             else
             {
                 GetReport_Duplicate();

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


     public void PDF(string pdf)
     {
         string filename = "BillSlip" + pdf;
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
         if (RadioButtonList1.SelectedValue == "Current Report" || txtreg.Text != "")
         {
             GetReport();
             ltrReport.Text = rpt.ToString();
             //PDF(txtreg.Text);


         }
         else if (RadioButtonList1.SelectedValue == "Duplicate Report" || TextBox1.Text != "")
         {
             GetReport_Duplicate();
             ltrReport.Text = rpt.ToString();
             //PDF(TextBox2.Text);


         }
         else
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select Proper Report !');", true);
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
}