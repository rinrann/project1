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


public partial class View_StockValuation : System.Web.UI.Page
{

    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    DataTable dt = new DataTable();

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
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");

        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>{0}</td>", "REG NO : NH-315/G-70/2013");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>&nbsp;</td>");
        //rpt.Append("</tr>");

        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>{0}</td>", "KUSHPATA,GHATAL,MIDNAPUR(W),WB,721212");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>&nbsp;</td>");
        //rpt.Append("</tr>");

        //rpt.Append("<tr>");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>{0}</td>", "Ph :(03225)244400/244643  M:9434419825");
        //rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; text-align:center'>&nbsp;</td>");
        //rpt.Append("</tr>");

        rpt.Append("<tr style='height:20px'>");
        if (Session["itemwise"] == "1")
            rpt.Append("<td style='width: 8%; font-family:Courier New;font-weight:bold; text-align:Center;padding-bottom:10px'><br/> <u>Stock Valuation</u><br/>Item Wise</td>");
        else rpt.Append("<td style='width: 8%; font-family:Courier New;font-weight:bold; text-align:Center;padding-bottom:10px'><br/> <u>Stock Valuation</u><br/>Item & Batch Wise</td>"); ;
        rpt.Append("</tr'>");
        rpt.Append("</table>");

      
        rpt.Append("<table width='100%' cellspacing=0 cellpadding=0 style=' font-family:Courier New; font-size:small'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 8%; font-family:Courier New; font-size:small; font-weight:bold'>Item Code</td>");
        rpt.Append("<td style='width: 20%; font-family:Courier New; font-size:small; font-weight:bold'>Item name</td>");
        if (Session["itemwise"].ToString() == "2")
            rpt.Append("<td style='width: 8%; font-family:Courier New; font-size:small; font-weight:bold'>Batch No</td>");
        rpt.Append("<td style='width: 5%; font-family:Courier New; font-size:small; font-weight:bold'>Unit</td>");
        rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>Opn Stock</td>");
        if (Session["withvalue"] == "1")
            rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>Opn Value</td>");
        rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>Cls Qty</td>");
        if (Session["withvalue"] == "1")
            rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>Current Value</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.Append("<td colspan='8' style='font-family:Courier New; font-size:small; font-weight:bold; border-top: 1px solid #000000'>&nbsp;</td>");
        rpt.Append("</tr>");

        string grpcd_old="";
        string grpcd_new="";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            grpcd_new=dt.Rows[i]["IGroup"].ToString().Trim();
            if (grpcd_old != grpcd_new)
            {
                rpt.Append("<tr>");
                rpt.Append("<td style='font-weight:bold'>" + dt.Rows[i]["IGroup"].ToString() + " - " + dt.Rows[i]["GrpName"].ToString() + "</td>");
                rpt.Append("</tr>");
            }
             
            
                rpt.Append("<tr>");
                rpt.Append("<td style='padding-left:10px'>" + dt.Rows[i]["icode"].ToString() + "</td>");
                rpt.Append("<td>" + dt.Rows[i]["iname"].ToString() + "</td>");
                if (Session["itemwise"].ToString() == "2")
                    rpt.Append("<td>" + dt.Rows[i]["batchno"].ToString() + "</td>");
                rpt.Append("<td>" + dt.Rows[i]["UnitName"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["IStock"].ToString() + "</td>");
                if (Session["withvalue"].ToString() == "1")
                    rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["IepVal"].ToString() + "</td>");
                rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["CurStock"].ToString() + "</td>");
                if (Session["withvalue"].ToString() == "1")
                    rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["CurValue"].ToString() + "</td>");
                rpt.Append("</tr>");
            
            grpcd_old=grpcd_new;
        }
        rpt.Append("<tr>");
        rpt.Append("<td colspan='8' style='font-family:Courier New; font-size:small; font-weight:bold; border-top: 1px solid #000000'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
        ltrReport.Text = rpt.ToString();
    }


    public string SuppressZero(string Value)
    {
        if (Value == "0.00")
        {
            Value = "";
        }
        return Value;
    }




    protected void cmd_excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AppendHeader("content-disposition", "attachment; filename=StockValuation.xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        ltrReport.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }
}