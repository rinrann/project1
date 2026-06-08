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

public partial class IPD_AdmissionSlip : System.Web.UI.Page
{

    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAdmission thepd = new PatientAdmission(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "ADMISSION SLIP", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            RadioButtonList1.SelectedValue = "With Header";
        }

        Page.Title = "Admission Slip";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        GetReport();
    }

    public void GetReport()
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
    }

    public void Report_Header()
    {
        rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='4' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='/Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
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
        DataTable dt = thepd.GetPatientDtls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox1.Text);
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> PATIENT DETAILS  </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Registration No</td>");
        rpt.AppendFormat("<td style=' border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PatientReg"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Patient's Name</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["patient_name"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Guadian Name</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;  font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["guardian_name"]);

        rpt.Append("</tr >");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Age</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["age"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Address</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["vill_city"]);
        rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:left'>Phone No</td>");
        rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["PhNo1"]);
        rpt.Append("</tr >");
        rpt.Append("</table >");

        rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana;font-weight:bold;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small; text-align:left'>Date of Admission</td>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px;font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-right: 1px solid black;text-align:left'>{0}</td>", dt.Rows[0]["ADate"]);
        rpt.Append("<td style='width: 5%;Height:70px;font-family:Verdana;font-weight:bold;border-bottom: 1px solid black;border-right: 1px solid black; font-size:small; text-align:left'>Time of Admission</td>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px; font-family:Verdana; font-size:small;border-bottom: 1px solid black;border-right: 1px solid black;text-align:left'>{0}</td>", dt.Rows[0]["FromTime"]);
        rpt.Append("<td style='width: 5%;Height:70px; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:small; text-align:left'>Bed No</td>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px;border-bottom: 1px solid black; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["BedNoText"]);
        rpt.Append("</tr >");


        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;Height:70px;border-right: 1px solid black; font-family:Verdana;font-weight:bold;font-size:small; text-align:left'>Doctor's Name :</td>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px; border-right: 1px solid black;font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["doc_name"]);
        rpt.Append("<td style='width: 5%;Height:70px; border-right: 1px solid black;font-family:Verdana;font-weight:bold; font-size:small; text-align:left'>Diagnosis :</td>");
        rpt.AppendFormat("<td style='width: 5%;Height:70px; font-family:Verdana; font-size:small;text-align:left'>{0}</td>", dt.Rows[0]["DiagnosisName"]);
        rpt.Append("<td style='width: 5%;Height:70px;font-family:Verdana;font-weight:bold; font-size:small; text-align:left'></td>");
        rpt.Append("<td style='width: 5%;Height:70px;  font-family:Verdana; font-size:small;text-align:left'></td>");

        rpt.Append("</tr >");
        rpt.Append("</table>");

        ltrReport.Visible = true;

    }

   
}