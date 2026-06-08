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


public partial class View_SampleCollection : System.Web.UI.Page
{
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    string CompCode = "";
    string yearcode = "";
    string Username = "";
    string compname = "";
    string address = "";
    string costcode1 = "";
    string ls_sql = string.Empty;
    string costcenter = "";
    string OldItemCode = "";
    string OldItemName = "";
    string OldGrpCode = "";
    string OldGrpName = "";
    string OldCostCode = "";
    string OldCCName = "";
    string OldSubGrpCode = "";
    string OldSubGrpName = "";

    string ItemQty1 = "0.00";
    string ItemQty2 = "0.00";

    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    int i = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bind_report();
    }
    public void bind_report()
    {
        dt = (DataTable)HttpContext.Current.Session["dt"];

        rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:Courier New;font-size:small;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='5' style='width: 35%; font-family:Courier New; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "GFC Hospital");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>{0}</td>", "REG NO : NH-315/G-70/2013");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>{0}</td>", "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>{0}</td>", "Ph :(03225)244400/244643  M:9434419825");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr style='height:20px'>");
        rpt.Append("<td style='width: 8%; font-family:Courier New;font-weight:bold; text-align:Center;padding-bottom:10px'><br/> <u>Sample Collection Register</u></td>");
        rpt.Append("</tr'>");
        rpt.Append("</table>");


        rpt.Append("<table width='100%' cellspacing=0 border=1 bordercolor=silver  style='font-family:Courier New;font-size:small;'>");

        rpt.Append("</tr>");
        rpt.Append("<td style='width:2%;'>Srl</td>");
        rpt.Append("<td style='width:5%;'>Collect Dates</td>");
        rpt.Append("<td style='width:8%;'>Patient Id</td>");
        rpt.Append("<td style='width:10%;'>Patient Name</td>");
        rpt.Append("<td style='width:10%;'>Doctor</td>");
        rpt.Append("<td style='width:10%;'>Agency Name</td>");
        rpt.Append("<td style='width:10%;'>Investigation</td>");
        rpt.Append("<td style='width:5%;'>Delivery Date</td>");
        rpt.Append("<td style='width:5%;text-align:right'>Fees Collected</td>");
        rpt.Append("<td style='width:5%;text-align:right'>By Cash</td>");
        rpt.Append("<td style='width:5%;text-align:right'>By Bank</td>");
        rpt.Append("<td style='width:5%;text-align:right'>Net Collection</td>");
        rpt.Append("</tr>");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["ibold"].ToString() == "1")
            {
                rpt.Append("</tr>"); 
                rpt.Append("<td style='font-weight:bold;' colspan=8>" + dt.Rows[i]["PatientName"].ToString() + "</td>");
                rpt.Append("<td style='font-weight:bold;text-align:right'>" + dt.Rows[i]["FeesCollected"].ToString() + "</td>");
                rpt.Append("<td style='font-weight:bold;text-align:right'>" + dt.Rows[i]["ByCash"].ToString() + "</td>");
                rpt.Append("<td style='font-weight:bold;text-align:right'>" + dt.Rows[i]["ByBank"].ToString() + "</td>");
                rpt.Append("<td style='font-weight:bold;text-align:right'>" + dt.Rows[i]["NetCollec"].ToString() + "</td>");
                rpt.Append("</tr>");
            }
            else
            {
                rpt.Append("</tr>");
                rpt.Append("<td>" + dt.Rows[i]["SlNo"].ToString() + "</td>");
                rpt.Append("<td>" + dt.Rows[i]["CollecDates"].ToString() + "</td>");
                rpt.Append("<td>" + dt.Rows[i]["PatientId"].ToString() + "</td>");
                rpt.Append("<td>" + dt.Rows[i]["PatientName"].ToString() + "</td>");
                rpt.Append("<td>" + dt.Rows[i]["Doctor"].ToString() + "</td>");
                rpt.Append("<td>" + dt.Rows[i]["AgencyName"].ToString() + "</td>");
                rpt.Append("<td>" + dt.Rows[i]["Investigation"].ToString() + "</td>");
                rpt.Append("<td>" + dt.Rows[i]["DeliveryDate"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["FeesCollected"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["ByCash"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["ByBank"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["NetCollec"].ToString() + "</td>");
                rpt.Append("</tr>");
            }
        }
        rpt.Append("</table>");
        ltrReport.Text = rpt.ToString();
    }
 


    protected void cmd_excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AppendHeader("content-disposition", "attachment; filename=SampleCollection.xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        ltrReport.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }
}