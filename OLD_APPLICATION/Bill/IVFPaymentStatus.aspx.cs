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

public partial class Bill_IVFPaymentStatus : System.Web.UI.Page
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
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "MONTHLY MIS REPORT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            string firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
            string lastdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            txtfromdt.Text = firstdayofmonth;// Session["YearCode"].ToString().Substring(0, 4) + "-04-01";
            txttodt.Text = firstdayofmonth; //lastdayofmonth;
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
        DataTable dtinvestigation = theabortion.GetIVFPaymentDetls(Session["CoCode"].ToString(), txtfromdt.Text.Trim(), txttodt.Text.Trim());

        rpt.Append("<table width='200%' cellspacing=0 border=1 bordercolor=silver style=' padding: 0;font-family:verdana; font-size:x-small'>");
        rpt.Append("<tr>");
        rpt.Append("<td rowspan='2' align='center'><strong>SRL</strong></td>");
        rpt.Append("<td rowspan='2' align='center'><strong>PATIENT NAME</strong></td>");
        rpt.Append("<td rowspan='2' align='center'><strong>PATIENT REG NO </strong></td>");
        rpt.Append("<td rowspan='2' align='center'><strong>PROCEDURE NAME</strong></td>");
        rpt.Append("<td rowspan='2' align='center'><strong>CONSULTANT DOCTOR </strong></td>");
        rpt.Append("<td rowspan='2' align='center'><strong>TOTAL CHARGES</strong></td>");
        rpt.Append("<td rowspan='2' align='center'><strong>DISCOUNT AMT</strong></td>");
        rpt.Append("<td colspan='4' align='center'><strong>PHASE 1</strong></td>");
        rpt.Append("<td colspan='4' align='center'><strong>PHASE 2</strong></td>");
        rpt.Append("<td colspan='4' align='center'><strong>PHASE 3</strong></td>");
        rpt.Append("<td colspan='4' align='center'><strong>PHASE 4</strong></td>");
        rpt.Append("<td colspan='4' align='center'><strong>PHASE 5</strong></td>");
        rpt.Append("<td colspan='4' align='center'><strong>PHASE 6</strong></td>");
        rpt.Append("<td  rowspan='2' align='center'><strong>REFUND AMT</strong></td>");
        rpt.Append("<td  rowspan='2' align='center'><strong>DUE AMT</strong></td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.Append("<td align='center'><strong>INV/RECPT NO</strong></td>");
        rpt.Append("<td align='center'><strong>DATE</strong></td>");
        rpt.Append("<td align='center'><strong>AMOUNT</strong></td>");
        rpt.Append("<td align='center'><strong>MODE</strong></td>");
        rpt.Append("<td align='center'><strong>INV/RECPT NO</strong></td>");
        rpt.Append("<td align='center'><strong>DATE</strong></td>");
        rpt.Append("<td align='center'><strong>AMOUNT</strong></td>");
        rpt.Append("<td align='center'><strong>MODE</strong></td>");
        rpt.Append("<td align='center'><strong>INV/RECPT NO</strong></td>");
        rpt.Append("<td align='center'><strong>DATE</strong></td>");
        rpt.Append("<td align='center'><strong>AMOUNT</strong></td>");
        rpt.Append("<td align='center'><strong>MODE</strong></td>");
        rpt.Append("<td align='center'><strong>INV/RECPT NO</strong></td>");
        rpt.Append("<td align='center'><strong>DATE</strong></td>");
        rpt.Append("<td align='center'><strong>AMOUNT</strong></td>");
        rpt.Append("<td align='center'><strong>MODE</strong></td>");
        rpt.Append("<td align='center'><strong>INV/RECPT NO</strong></td>");
        rpt.Append("<td align='center'><strong>DATE</strong></td>");
        rpt.Append("<td align='center'><strong>AMOUNT</strong></td>");
        rpt.Append("<td align='center'><strong>MODE</strong></td>");
        rpt.Append("<td align='center'><strong>INV/RECPT NO</strong></td>");
        rpt.Append("<td align='center'><strong>DATE</strong></td>");
        rpt.Append("<td align='center'><strong>AMOUNT</strong></td>");
        rpt.Append("<td align='center'><strong>MODE</strong></td>");
        rpt.Append("</tr>");
        int srl = 1;
        for (int i = 0; i < dtinvestigation.Rows.Count; i++)
        {
            rpt.Append("<tr>");
            rpt.Append("<td>" + srl.ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["PatientName"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["RegNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["ServiceName"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["ConsultantName"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["TotalCharges"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["DiscountAmount"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase1InvNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase1Date"].ToString() + "</td>");
            rpt.Append("<td align='right'>" + dtinvestigation.Rows[i]["Phase1Amount"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase1Mode"].ToString() + "</td>");

            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase2InvNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase2Date"].ToString() + "</td>");
            rpt.Append("<td align='right'>" + dtinvestigation.Rows[i]["Phase2Amount"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase2Mode"].ToString() + "</td>");

            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase3InvNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase3Date"].ToString() + "</td>");
            rpt.Append("<td align='right'>" + dtinvestigation.Rows[i]["Phase3Amount"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase3Mode"].ToString() + "</td>");

            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase4InvNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase4Date"].ToString() + "</td>");
            rpt.Append("<td align='right'>" + dtinvestigation.Rows[i]["Phase4Amount"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase4Mode"].ToString() + "</td>");

            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase5InvNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase5Date"].ToString() + "</td>");
            rpt.Append("<td align='right'>" + dtinvestigation.Rows[i]["Phase5Amount"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase5Mode"].ToString() + "</td>");

            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase6InvNo"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase6Date"].ToString() + "</td>");
            rpt.Append("<td align='right'>" + dtinvestigation.Rows[i]["Phase6Amount"].ToString() + "</td>");
            rpt.Append("<td>" + dtinvestigation.Rows[i]["Phase6Mode"].ToString() + "</td>");
            rpt.Append("<td align='right'>" + dtinvestigation.Rows[i]["RefundAmount"].ToString() + "</td>");
            rpt.Append("<td align='right'>" + dtinvestigation.Rows[i]["DueAmount"].ToString() + "</td>");
            rpt.Append("</tr>");
        }
        rpt.Append("</table>");
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
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HomePage.aspx");
    }
    protected void ddlopt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlopt.SelectedValue == "PN")
        {
            ddlOperator.Items.Clear();
            ddlOperator.Items.Insert(0, new ListItem("Equal", "E"));

            txtSearchText.Visible = true;
            txtfromdt.Visible = false;
            lblmid.Visible = false;
            txttodt.Visible = false;
        }
        else if (ddlopt.SelectedValue == "RN")
        {
            ddlOperator.Items.Clear();
            ddlOperator.Items.Insert(0, new ListItem("Equal", "E"));

            txtSearchText.Visible = true;
            txtfromdt.Visible = false;
            lblmid.Visible = false;
            txttodt.Visible = false;
        }
        else if (ddlopt.SelectedValue == "PH")
        {
            ddlOperator.Items.Clear();
            ddlOperator.Items.Insert(0, new ListItem("Equal", "E"));

            txtSearchText.Visible = true;
            txtfromdt.Visible = false;
            lblmid.Visible = false;
            txttodt.Visible = false;
        }
        else if (ddlopt.SelectedValue == "PD")
        {
            ddlOperator.Items.Clear();
            ddlOperator.Items.Insert(0, new ListItem("Equal", "E"));
            ddlOperator.Items.Insert(1, new ListItem("Greater Than", "G"));
            ddlOperator.Items.Insert(2, new ListItem("Less Than", "L"));
            ddlOperator.Items.Insert(3, new ListItem("Greater Than Equal", "GE"));
            ddlOperator.Items.Insert(4, new ListItem("Less Than Equal", "LE"));
            ddlOperator.Items.Insert(5, new ListItem("Between", "B"));

            txtSearchText.Visible = false;
            txtfromdt.Visible = true;
            lblmid.Visible = false;
            txttodt.Visible = false;
        }
        else if (ddlopt.SelectedValue == "RD")
        {
            ddlOperator.Items.Clear();
            ddlOperator.Items.Insert(0, new ListItem("Equal", "E"));
            ddlOperator.Items.Insert(1, new ListItem("Greater Than", "G"));
            ddlOperator.Items.Insert(2, new ListItem("Less Than", "L"));
            ddlOperator.Items.Insert(3, new ListItem("Greater Than Equal", "GE"));
            ddlOperator.Items.Insert(4, new ListItem("Less Than Equal", "LE"));
            ddlOperator.Items.Insert(5, new ListItem("Between", "B"));

            txtSearchText.Visible = false;
            txtfromdt.Visible = true;
            lblmid.Visible = false;
            txttodt.Visible = false;
        }
    }
    protected void ddlOperator_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOperator.SelectedValue == "B")
        {
            lblmid.Visible = true;
            txttodt.Visible = true;
        }
        else
        {
            lblmid.Visible = false;
            txttodt.Visible = false;
        }
    }
}