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


public partial class IPD_TotalIncome : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    BHTReportClass theBHTReportClass = new BHTReportClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "TOTAL TRUNSACTION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        Page.Title = "Total Income & Expenduture";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");

        }
        if (!IsPostBack)
            GetReport();
    }

    public void GetReport()
    {
        Report_Header();
        string cocode = Session["CoCode"].ToString().Trim();
        string yearcode = Session["YearCode"].ToString().Trim();
        string from, to;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

        if (TextBox1.Text == "")
            from = "";
        else
            from = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf).ToString();

        if (TextBox2.Text == "")
            to = "";
        else
            to = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", dtf).ToString();

        DataSet ds = theBHTReportClass.GetTotalIncomeExpenditure(cocode, yearcode, from, to, TextBox3.Text);
        DataSet ds2 = theBHTReportClass.GetDailyExpenditure(cocode, yearcode, from, to);

        GetHearder_Detail(ds);
        GetDailyDetails(ds2);
        GetDueDetails(ds);
        GetIncomeDetails(ds);
        GetDiscountDetails(ds);
        GetRefundDetails(ds);
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

    public void GetHearder_Detail(DataSet ds)
    {
        ltrReport.Text = "";
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='8' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>     Total Transaction Details     </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr >");
        rpt.Append("<td colspan=3 style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Payment</td>");
        rpt.Append("<td rowspan=2 width='10%' style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Due</td>");
        rpt.Append("<td rowspan=2 width='15%' style='border-right: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Discount</td>");
        rpt.Append("<td rowspan=2 width='15%' style='border-right: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Refund</td>");
        rpt.Append("<td rowspan=2 width='15%' style='border-right: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Payment to Doctor</td>");
        rpt.Append("<td rowspan=2 width='20%' style=' font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Nt Income</td>");
        rpt.Append("</tr >");
        rpt.Append("<tr >");
        rpt.Append("<td width='10%' style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Cash</td>");
        rpt.Append("<td width='10%' style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Card</td>");
        rpt.Append("<td width='10%' style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Total</td>");
        rpt.Append("</tr >");
        rpt.Append("<tr style='height:40px' >");
        rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["cash_Income"]);
        rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["card_Income"]);
        rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["Income"]);
        rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["Due"]);
        rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["Discount"]);
        rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["Refund"]);
        rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["docpaid"]);
        rpt.AppendFormat("<td style='border-top: 1px solid black;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[0].Rows[0]["net_income"]);
        rpt.Append("</tr >");

        rpt.Append("</table >");
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='8' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>     Total Cash Details     </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr >");
        rpt.Append("<td width='25%' style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Payment by Patient</td>");
        rpt.Append("<td  width='25%' style='border-right: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Refund in cash</td>");
        rpt.Append("<td  width='25%' style='border-right: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Payment to Doctor in cash</td>");
        rpt.Append("<td  width='25%' style=' font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Nt available Cash</td>");
        rpt.Append("</tr >");
        rpt.Append("<tr style='height:40px' >");
        rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[6].Rows[0]["Income"]);
        rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[6].Rows[0]["Refund"]);
        rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[6].Rows[0]["docpaid"]);
        rpt.AppendFormat("<td style='border-top: 1px solid black;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", ds.Tables[6].Rows[0]["net_income"]);
        rpt.Append("</tr >");

        rpt.Append("</table >");
        ltrReport.Text = rpt.ToString();
        ltrReport.Visible = true;
    }
    public void GetDailyDetails(DataSet ds)
    {
        int i;
        DataTable dt0 = ds.Tables[0];
        DataTable dt1 = ds.Tables[1];
        DataTable dt2 = ds.Tables[2];
        DataTable dt3 = ds.Tables[3];
        rpt.Clear();
        ltrDaily.Text = "";
        Report_Header();
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:auto'>");
        rpt.Append("<td colspan='11' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>Daily Transaction Report From " + dt3.Rows[0]["Fromdt"] + " To " + dt3.Rows[0]["Todt"] + "</td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr >");
        rpt.Append("<td rowspan=2 width='10%' style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Date</td>");
        rpt.Append("<td colspan=2 width='20%' style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Payment by Patient</td>");
        rpt.Append("<td rowspan=2 width='10%' style='border-right: 1px solid black;font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Due</td>");
        rpt.Append("<td rowspan=2 width='10%' style='border-right: 1px solid black;font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Discount</td>");
        rpt.Append("<td colspan=2 width='20%' style='border-bottom: 1px solid black;border-right: 1px solid black;font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Refund</td>");
        rpt.Append("<td colspan=2 width='20%' style='border-bottom: 1px solid black;border-right: 1px solid black;font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Payment to Doctor</td>");
        rpt.Append("<td rowspan=2 width='10%' style=' border-right: 1px solid black;font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Nt available Cash</td>");
        rpt.Append("<td rowspan=2 width='10%' style='font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Nt Income</td>");
        rpt.Append("</tr >");
        rpt.Append("<tr >");
        rpt.Append("<td width='10%' style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Cash</td>");
        rpt.Append("<td width='10%' style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Card</td>");
        rpt.Append("<td width='10%' style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Cash</td>");
        rpt.Append("<td width='10%' style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Card</td>");
        rpt.Append("<td width='10%' style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Cash</td>");
        rpt.Append("<td width='10%' style='border-bottom: 1px solid black;border-right: 1px solid black; font-family:Arial;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Cheque</td>");
        for (i = 0; i < dt2.Rows.Count; i++)
        {
            rpt.Append("<tr style='height:auto' >");
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Arial; font-size:x-small;text-align:center'>{0}</td>", dt2.Rows[i]["dailydatestr"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Arial; font-size:x-small;text-align:right'>{0}</td>", dt2.Rows[i]["Income"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Arial; font-size:x-small;text-align:right'>{0}</td>", dt2.Rows[i]["Income_other"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Arial; font-size:x-small;text-align:right'>{0}</td>", dt2.Rows[i]["Due"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Arial; font-size:x-small;text-align:right'>{0}</td>", dt2.Rows[i]["Discount"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Arial; font-size:x-small;text-align:right'>{0}</td>", dt2.Rows[i]["Refund"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Arial; font-size:x-small;text-align:right'>{0}</td>", dt2.Rows[i]["Refund_other"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Arial; font-size:x-small;text-align:right'>{0}</td>", dt2.Rows[i]["docpaid"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Arial; font-size:x-small;text-align:right'>{0}</td>", dt2.Rows[i]["docpaid_other"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Arial; font-size:x-small;text-align:right'>{0}</td>", dt2.Rows[i]["net_cash_income"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;font-family:Arial; font-size:x-small;text-align:right'>{0}</td>", dt2.Rows[i]["net_income"]);
            rpt.Append("</tr >");
        }
        rpt.Append("</table >");
        ltrDaily.Text = rpt.ToString();
        ltrDaily.Visible = true;
        //ltrReport.Visible = false;
    }
    public void GetDueDetails(DataSet ds)
    {
        ltrDue.Text = "";
        rpt = new System.Text.StringBuilder();
        Report_Header();
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>     Total Due Details     </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr >");
        rpt.Append("<td  style='width: 6%;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Date</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Reg. No.</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Name</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Address</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Phone No.</td>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Due Amt</td>");
        rpt.Append("</tr >");
        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
        {
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td   style='width:6%; border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[1].Rows[i]["Date"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[1].Rows[i]["PatientReg"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[1].Rows[i]["patient_name"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[1].Rows[i]["vill_City"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[1].Rows[i]["PhNo1"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[1].Rows[i]["Debit"]);
            rpt.Append("</tr >");
        }
        rpt.Append("</table >");
        ltrDue.Text = rpt.ToString();
        ltrDue.Visible = true;
    }


    public void GetIncomeDetails(DataSet ds)
    {
        GridView1.DataSource = ds.Tables[5];
        GridView1.DataBind();

        ltrIncome.Text = "";
        rpt = new System.Text.StringBuilder();
        Report_Header();
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='7' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>     Total Payment Details     </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr >");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Date</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Reg. No.</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Name</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Address</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Phone No.</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Reason</td>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Income</td>");
        rpt.Append("</tr >");
        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
        {
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[2].Rows[i]["Date"]);
            //rpt.AppendFormat("<td id='getpopup'  style='cursor:pointer;border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana;color:Blue; font-size:small;text-align:center'>{0}</td>", ds.Tables[2].Rows[i]["PatientReg"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-size:x-small;text-align:center'>{0}</td>", ds.Tables[2].Rows[i]["PatientReg"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[2].Rows[i]["patient_name"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[2].Rows[i]["vill_City"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[2].Rows[i]["PhNo1"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[2].Rows[i]["Reason"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[2].Rows[i]["Credit"]);
            rpt.Append("</tr >");
        }
        rpt.Append("</table >");
        ltrIncome.Text = rpt.ToString();
        ltrIncome.Visible = true;
    }


    public void GetDiscountDetails(DataSet ds)
    {
        ltrDiscount.Text = "";
        rpt = new System.Text.StringBuilder();
        Report_Header();
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>     Total Discount Details     </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr >");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Date</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Reg. No.</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Name</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Address</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Phone No.</td>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Discount</td>");
        rpt.Append("</tr >");
        for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
        {
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[3].Rows[i]["Date"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[3].Rows[i]["PatientReg"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[3].Rows[i]["patient_name"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[3].Rows[i]["vill_City"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[3].Rows[i]["PhNo1"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[3].Rows[i]["Credit"]);
            rpt.Append("</tr >");
        }
        rpt.Append("</table >");

        ltrDiscount.Text = rpt.ToString();
        ltrDiscount.Visible = true;
    }


    public void GetRefundDetails(DataSet ds)
    {
        ltrRefund.Text = "";
        rpt = new System.Text.StringBuilder();
        Report_Header();
        rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:larger; text-align:Center'>     Total Refund Details     </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr >");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Date</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Reg. No.</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Name</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Address</td>");
        rpt.Append("<td style='border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Phone No.</td>");
        rpt.Append("<td style='font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Refund</td>");
        rpt.Append("</tr >");
        for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
        {
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[4].Rows[i]["Date"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[4].Rows[i]["PatientReg"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[4].Rows[i]["patient_name"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[4].Rows[i]["vill_City"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[4].Rows[i]["PhNo1"]);
            rpt.AppendFormat("<td style='border-top: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", ds.Tables[4].Rows[i]["Credit"]);
            rpt.Append("</tr >");
        }
        rpt.Append("</table >");

        ltrRefund.Text = rpt.ToString();
        ltrRefund.Visible = true;
    }
    public void PDF()
    {
        string filename = "IncomeDetails";
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
        //GetHearder_Detail();
        //ltrReport.Text = rpt.ToString();
        //PDF();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetReport();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PatientReg")
        {
            string id = (e.CommandArgument).ToString();
            Session.Add("LedgerId", id);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "ShowDialog();", true);
        }
    }
}