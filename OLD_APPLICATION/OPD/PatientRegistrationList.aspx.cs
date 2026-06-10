using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using ClosedXML.Excel;
using System.Threading;


public partial class OPD_PatientRegistrationList : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_Report objreport = new PH_Report(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "PATIENT REGISTRATION LIST", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            //bind_report();
            //txtFrmDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //txtToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    protected void btnGenRpt_Click(object sender, EventArgs e)
    {
        bind_report();
        ltrReport.Text = rpt.ToString();
    }

    public void bind_report()
    {
        string fromdate = txtFrmDate.Text;
        string todate = txtToDate.Text;
        DataTable dtDtls = objreport.GetRegList(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), fromdate, todate);
        rpt.Append("<table width='100%' cellspacing=0 border=1 bordercolor=silver style=' padding: 0;font-family:verdana; font-size:x-small'>");

        rpt.Append("<tr>");
        rpt.Append("<td style='width:5%;'><strong>SRL</strong></td>");
        rpt.Append("<td style='width:15%;'><strong>REG NO</strong></td>");
        rpt.Append("<td style='width:25%;'><strong>PATIENT NAME</strong></td>");
        rpt.Append("<td style='width:10%;'><strong>PHONE NO</strong></td>");
        rpt.Append("<td style='width:10%;'><strong>REG DATE</strong></td>");
        rpt.Append("<td style='width:10%;'><strong>BIRTH DATE</strong></td>");
        rpt.Append("<td style='width:25%;'><strong>ADDRESS</strong></td>");
        rpt.Append("</tr>");

        int srl = 1;
        int i = 0;
        for (i = 0; i < dtDtls.Rows.Count; i++)
        {
            rpt.Append("<tr>");
            rpt.Append("<td valign='top'>" + srl.ToString() + "</td>");
            rpt.Append("<td valign='top'>" + dtDtls.Rows[i]["PatientRegNo"].ToString() + "</td>");
            rpt.Append("<td valign='top'>" + dtDtls.Rows[i]["PName"].ToString() + "</td>");
            rpt.Append("<td valign='top'>" + dtDtls.Rows[i]["PhNo1"].ToString() + "</td>");
            rpt.Append("<td valign='top'>" + dtDtls.Rows[i]["RegDate"].ToString() + "</td>");
            rpt.Append("<td valign='top'>" + dtDtls.Rows[i]["BirthDate"].ToString() + "</td>");
            string address = dtDtls.Rows[i]["Address"].ToString();
            if (dtDtls.Rows[i]["DistrictName"].ToString() != "")
            {
                address = address + ", " + dtDtls.Rows[i]["DistrictName"].ToString();
            }
            if (dtDtls.Rows[i]["StateName"].ToString() != "")
            {
                address=address+", "+ dtDtls.Rows[i]["StateName"].ToString();
            }
            if (dtDtls.Rows[i]["CountryName"].ToString() != "")
            {
                address=address+", "+ dtDtls.Rows[i]["CountryName"].ToString();
            }
            rpt.Append("<td valign='top'>" + address + "</td>");
            rpt.Append("</tr>");

            srl = srl + 1;
        }
        rpt.Append("</table>");
    }

    protected void btn_excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AppendHeader("content-disposition", "attachment; filename=RegistrationList.xls");
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
}