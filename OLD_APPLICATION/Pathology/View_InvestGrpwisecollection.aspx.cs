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


public partial class Pathology_View_InvestGrpwisecollection : System.Web.UI.Page
{
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bind_report();
    }
    public void bind_report()
    {
        DataSet ds = (DataSet)HttpContext.Current.Session["ds"];

        for (int i = 0; i < ds.Tables.Count; i++)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
            rpt.Append("<tr cellpadding:'0'>");
            rpt.AppendFormat("<td rowspan='5' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
            rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "");
            rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
            rpt.Append("</tr>");

            
            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 8%; font-family:Verdana;font-weight:bold; text-align:Center;padding-bottom:10px'><br/> <u>Investment Group wise Collection Register</u></td>");
            rpt.Append("</tr'>");
            rpt.Append("</table>");

            rpt.Append("<table cellspacing=0 cellpadding=0 border=1 bordercolor=silver style='font-family:verdana;font-size:small'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width:8%'><strong>Date</strong></td>");
            rpt.Append("<td style='width:5%'><strong>Slno</strong></td>");
            rpt.Append("<td style='width:12%'><strong>Patient ID</strong></td>");
            rpt.Append("<td style='width:15%'><strong>Patient Name</strong></td>");
            rpt.Append("<td style='width:12%'><strong>Requisition No</strong></td>");
            rpt.Append("<td style='width:10%'><strong>Invoice No</strong></td>");
            rpt.Append("<td style='width:12%'><strong>Doctor Name</strong></td>");
            rpt.Append("<td style='width:15%'><strong>Service Name</strong></td>");
            rpt.Append("<td style='width:10%;text-align:right'><strong>Fees Collected</strong></td>");
            //rpt.Append("<td style='width:6%;text-align:right'>By Cash</td>");
            //rpt.Append("<td style='width:6%;text-align:right'>By Bank</td>");
            //rpt.Append("<td style='width:6%;text-align:right'>Refund</td>");
            //rpt.Append("<td style='width:6%;text-align:right'>Net Collection</td>");
            rpt.Append("</tr>");

            for (int a = 0; a < ds.Tables[i].Rows.Count; a++)
            {
                if (ds.Tables[i].Rows[a]["dates"].ToString() == "")
                {
                    rpt.Append("<tr>");                    
                    if (ds.Tables[i].Rows[a]["PatientName"].ToString() == "Grand Total")
                    {
                        rpt.Append("<td style='font-weight:bold;' colspan='8'>" + ds.Tables[i].Rows[a]["PatientName"].ToString() + "</td>");
                        rpt.Append("<td style='font-weight:bold;text-align:right'>" + ds.Tables[i].Rows[a]["FeesCollected"].ToString() + "</td>");
                        //rpt.Append("<td style='font-weight:bold;text-align:right'>" + ds.Tables[i].Rows[a]["ByCash"].ToString() + "</td>");
                        //rpt.Append("<td style='font-weight:bold;text-align:right'>" + ds.Tables[i].Rows[a]["ByBank"].ToString() + "</td>");
                        //rpt.Append("<td style='font-weight:bold;text-align:right'>" + ds.Tables[i].Rows[a]["Refund"].ToString() + "</td>");
                        //rpt.Append("<td style='font-weight:bold;text-align:right'>" + ds.Tables[i].Rows[a]["NetCollec"].ToString() + "</td>");
                    }
                    else rpt.Append("<td style='font-weight:bold;' colspan=9>" + ds.Tables[i].Rows[a]["PatientName"].ToString() + "</td>");
                    rpt.Append("</tr>");
                }
                else
                {
                    rpt.Append("<tr>");
                    rpt.Append("<td>" + ds.Tables[i].Rows[a]["Dates"].ToString() + "</td>");
                    rpt.Append("<td>" + ds.Tables[i].Rows[a]["SlNo"].ToString() + "</td>");
                    rpt.Append("<td>" + ds.Tables[i].Rows[a]["PatientId"].ToString() + "</td>");
                    rpt.Append("<td>" + ds.Tables[i].Rows[a]["PatientName"].ToString() + "</td>");
                    rpt.Append("<td>" + ds.Tables[i].Rows[a]["ReqNo"].ToString() + "</td>");
                    rpt.Append("<td>" + ds.Tables[i].Rows[a]["BillNo"].ToString() + "</td>");
                    rpt.Append("<td>" + ds.Tables[i].Rows[a]["Doctor"].ToString() + "</td>");
                    rpt.Append("<td>" + ds.Tables[i].Rows[a]["TestName"].ToString() + "</td>");
                    rpt.Append("<td style='text-align:right'>" + ds.Tables[i].Rows[a]["FeesCollected"].ToString() + "</td>");
                    //rpt.Append("<td style='text-align:right'>" + ds.Tables[i].Rows[a]["ByCash"].ToString() + "</td>");
                    //rpt.Append("<td style='text-align:right'>" + ds.Tables[i].Rows[a]["ByBank"].ToString() + "</td>");
                    //rpt.Append("<td style='text-align:right'>" + ds.Tables[i].Rows[a]["Refund"].ToString() + "</td>");
                    //rpt.Append("<td style='text-align:right'>" + ds.Tables[i].Rows[a]["NetCollec"].ToString() + "</td>");
                    rpt.Append("</tr>");
                }
            }
            rpt.Append("</table><div style='page-break-after: always;'></div>");
        }
        ltrReport.Text = rpt.ToString();

    }

    protected void cmd_excel_Click(object sender, EventArgs e)
    {
        DataSet ds = (DataSet)HttpContext.Current.Session["ds"];
        for (int i = 0; i < ds.Tables.Count; i++)
        {
            ds.Tables[i].TableName = ds.Tables[i].Rows[0]["PatientName"].ToString();
        }
        using (XLWorkbook wb = new XLWorkbook())
        {
            foreach (DataTable dt in ds.Tables)
            { 
                wb.Worksheets.Add(dt);
            }
            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=InvGrpwiseCollection.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}