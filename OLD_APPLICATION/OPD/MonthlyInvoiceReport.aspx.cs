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
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;


public partial class OPD_MonthlyInvoiceReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientHistory theabortion = new PatientHistory(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MONTHLY INVOICE REPORT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            string firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
            string lastdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            txtfromdt.Text = firstdayofmonth;// Session["YearCode"].ToString().Substring(0, 4) + "-04-01";
            txttodt.Text = lastdayofmonth;
        }
    }

    protected void btnGenRpt_Click(object sender, EventArgs e)
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();
    }


    public void Report_Header()
    {

        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date = DateTime.Now.ToString("dd/MM/yyyy");

        hd.Visible = true;


    }

    public void GetHearder_Detail()
    {
        DataTable dtinvestigation = theabortion.GetInvoiceReportDetails(Session["CoCode"].ToString(), txtfromdt.Text.Trim(), txttodt.Text.Trim());
        
        
        rpt.Append("<table width='100%' cellspacing=0 border=1 bordercolor=silver style=' padding: 0;font-family:verdana; font-size:x-small'>");
        rpt.Append("<tr>");
        rpt.Append("<td width='4%'><strong>SRL</strong></td>");
        rpt.Append("<td width='8%'><strong>DATE</strong></td>");
        rpt.Append("<td width='10%'><strong>INVOICE NUMBER</strong></td>");
        rpt.Append("<td width='12%'><strong>PATIENT REG NO </strong></td>");
        rpt.Append("<td width='15%'><strong>PATIENT NAME</strong></td>");
        rpt.Append("<td width='11%'><strong>REQUISITION NO </strong></td>");
        rpt.Append("<td width='10%'><strong>SERVICE NAME</strong></td>");
        rpt.Append("<td width='7%'><strong>SERVICE COST</strong></td>");
        rpt.Append("<td width='5%'><strong>DISCOUNT AMT</strong></td>");
        rpt.Append("<td width='8%'><strong>PAYABLE AMT</strong></td>");
        rpt.Append("<td width='10%'><strong>COLLECTED AMT</strong></td>");
        rpt.Append("</tr>");


        int i = 0;
        int srl = 1;
        decimal totalamt = 0;
        decimal totdiscamt = 0;
        decimal totpayableamt = 0;
        decimal totadvamt = 0;
        decimal totdueamt = 0;
        decimal totrefundamt = 0;

        string recptnocurr = "";
        string recptnonext = "";
        string reqcurr = "";
        string reqprev = "";
        string reqnext = "";
        string olddate = "";
        string newdate = "";
        string currsrv = "";
        string prevsrv = "";


        for (i = 0; i < dtinvestigation.Rows.Count; i++)
        {
            reqcurr = dtinvestigation.Rows[i]["ReqNo"].ToString();
            recptnocurr = dtinvestigation.Rows[i]["ReceiptNo"].ToString();
            if (i + 1 < dtinvestigation.Rows.Count)
            {
                reqnext = dtinvestigation.Rows[i + 1]["ReqNo"].ToString();
                recptnonext = dtinvestigation.Rows[i + 1]["ReceiptNo"].ToString();
            }
            else
            {
                reqnext = "";
                recptnonext = ""; 
            }

            rpt.Append("<tr>");
            rpt.Append("<td>" + srl.ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["PayDt"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["ReceiptNo"].ToString() + "</td>");
            //rpt.Append("<td>" + dtinvestigation.Rows[i]["PayTime"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["RegNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["PatientName"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["ReqNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["ServiceName"].ToString() + "</td>");
            rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["TotalAmount"].ToString() + "</td>");
            totalamt = totalamt + Convert.ToDecimal(dtinvestigation.Rows[i]["TotalAmount"].ToString());
            if (reqcurr != reqnext)
            {
                rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["DiscountAmount"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["PayableAmount"].ToString() + "</td>");
                totdiscamt = totdiscamt + Convert.ToDecimal(dtinvestigation.Rows[i]["DiscountAmount"].ToString());
                totpayableamt = totpayableamt + Convert.ToDecimal(dtinvestigation.Rows[i]["PayableAmount"].ToString());
            }
            else
            {
                rpt.Append("<td style='text-align:right'></td>");
                rpt.Append("<td style='text-align:right'></td>");
            }
            if (recptnocurr != recptnonext)
            {
                rpt.Append("<td style='text-align:right'>" + dtinvestigation.Rows[i]["AdvanceAmount"].ToString() + "</td>");
                //rpt.Append("<td style='text-align:left'>" + dtinvestigation.Rows[i]["paymode"].ToString() + "</td>");
                totadvamt = totadvamt + Convert.ToDecimal(dtinvestigation.Rows[i]["AdvanceAmount"].ToString());
            }
            else
            {
                rpt.Append("<td style='text-align:right'></td>");
                //rpt.Append("<td style='text-align:left'></td>");
            }
            rpt.Append("</tr>");
            srl = srl + 1;
            //docold = docnew;
            //reqold = reqnew;
            //olddate = newdate;
            prevsrv = currsrv;
            reqprev = reqcurr;
        }
        rpt.Append("<tr>");
        rpt.Append("<td colspan=7 style='font-weight:bold;'>Total :</td>");
        rpt.Append("<td style='font-weight:bold;text-align:right'>" + totalamt.ToString() + "</td>");
        rpt.Append("<td style='font-weight:bold;text-align:right'>" + totdiscamt.ToString() + "</td>");
        rpt.Append("<td style='font-weight:bold;text-align:right'>" + totpayableamt.ToString() + "</td>");
        rpt.Append("<td style='font-weight:bold;text-align:right'>" + totadvamt.ToString() + "</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HomePage.aspx");
    }

    protected void btn_excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AppendHeader("content-disposition", "attachment; filename=DocWiseMISReport.xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        ltrReport.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }
}