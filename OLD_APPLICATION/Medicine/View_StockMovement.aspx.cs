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


public partial class View_StockMovement : System.Web.UI.Page
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
        rpt.Append("<td style='width: 8%; font-family:Courier New;font-weight:bold; text-align:Center;padding-bottom:10px'><br/> <u>Stock Movement Register</u></td>");
        rpt.Append("</tr'>");
        rpt.Append("</table>");

        Hearder_Detail();

        for (var i = 0; i < dt.Rows.Count; i++)
        {

            if (Convert.IsDBNull(dt.Rows[i]["CPROC04"]) == false)
            {
                if (Convert.ToString(OldGrpCode).Trim(' ') != Convert.ToString(dt.Rows[i]["CPROC04"]).Trim(' '))
                {
                    PGroup_Group();
                }
                OldGrpCode = dt.Rows[i]["CPROC04"].ToString().Trim();
                OldGrpName = dt.Rows[i]["CPROC06"].ToString().Trim();
            }
            

            if (Convert.IsDBNull(dt.Rows[i]["CPROC03"]) == false)
            {
                if (Convert.ToString(OldItemCode).Trim(' ') != Convert.ToString(dt.Rows[i]["CPROC03"]).Trim(' '))
                {
                    Item_Group();
                }

                OldItemCode = dt.Rows[i]["CPROC03"].ToString().Trim();
            }
            Report_Details();
            if (Convert.IsDBNull(dt.Rows[i]["CPROC03"]) == false)
            {
                if (i < dt.Rows.Count - 1)
                {
                    if (Convert.ToString(dt.Rows[i]["CPROC03"]).Trim(' ') != Convert.ToString(dt.Rows[i + 1]["CPROC03"]).Trim(' '))
                    {
                        //If ItemQty1 <> "0.00" Or ItemQty2 <> "0.00" Then
                        Closing_Total();
                        //oldCloseTotal = dt.Rows[i]("CPROC03")
                    }
                }
                else
                {
                    Closing_Total();
                }
            }
        }

        ltrReport.Text = rpt.ToString();

    }
    public void Report_Details()
    {
        rpt.Append("<table width='100%'>");
        rpt.Append("<tr>");
        if (Convert.IsDBNull(dt.Rows[i]["dproc01"]))
        {
            rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:small'>{0}</td>", dt.Rows[i]["docdt"].ToString());
        }
        if (Convert.IsDBNull(dt.Rows[i]["CPROC01"]))
        {
            rpt.Append("<td style='width: 15%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 15%; font-family:Courier New; font-size:small'>{0}</td>", dt.Rows[i]["CPROC01"]);
        }
        if (Convert.IsDBNull(dt.Rows[i]["CPROC08"]))
        {
            rpt.Append("<td style='width: 35%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:small'>{0}</td>", SuppressZero(dt.Rows[i]["CPROC08"].ToString()));
        }
        if (Convert.IsDBNull(dt.Rows[i]["NPROC01"]))
        {
            rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:small; text-align:right'>{0}</td>", SuppressZero(dt.Rows[i]["NPROC01"].ToString()));
            ItemQty1 = Convert.ToString( Convert.ToDecimal(ItemQty1) + Convert.ToDecimal(dt.Rows[i]["NPROC01"]));
        }
        if (Convert.IsDBNull(dt.Rows[i]["NPROC03"]))
        {
            rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:small; text-align:right'>{0}</td>", SuppressZero(dt.Rows[i]["NPROC03"].ToString()));
            ItemQty2 =Convert.ToString( Convert.ToDecimal(ItemQty2) + Convert.ToDecimal(dt.Rows[i]["NPROC03"]));
        }
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public string SuppressZero(string Value)
    {
        if (Value == "0.00")
        {
            Value = "";
        }
        return Value;
    }

    public void Closing_Total()
    {
        rpt.Append("<table width='100%'>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td colspan='4' style='width: 60%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>{0}</td>", "&nbsp");
        rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>{0}</td>", SuppressZero(ItemQty1));
        rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>{0}</td>", SuppressZero(ItemQty2));

        rpt.Append("</tr></table>");
        rpt.Append("<table width='100%'><tr>");
        rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold'>" + Session["to"].ToString() + "</td>");
        rpt.Append("<td style='width: 30%; font-family:Courier New; font-size:small;font-weight:bold; text-align:right'>Closing Stock&nbsp;&nbsp;</td>");
        if (Convert.IsDBNull(dt.Rows[i]["NProc06"]))
        {
            rpt.Append("<td style='width: 15%; font-family:Courier New; font-size:small'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 15%; font-family:Courier New; font-size:small;font-weight:bold; text-align:right'>{0}</td>", SuppressZero(dt.Rows[i]["NProc06"].ToString()));
        }
        if (Convert.IsDBNull(dt.Rows[i]["CProc10"]))
        {
            rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New;font-weight:bold; font-size:small'>{0}</td>", dt.Rows[i]["CProc10"]);
        }
        rpt.AppendFormat("<td colspan='2' style='width: 15%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>{0}</td>", "&nbsp");
        rpt.Append("</tr>");
        rpt.Append("</table>");
        ItemQty1 = "0.00";
        ItemQty2 = "0.00";
    }

    public void Item_Group()
    {
        rpt.Append("<table width='50%'>");
        rpt.Append("<tr>");
        if (Convert.IsDBNull(dt.Rows[i]["CPROC03"]))
        {
            rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold'>{0}</td>", dt.Rows[i]["CPROC03"]);
        }
        if (Convert.IsDBNull(dt.Rows[i]["CPROC09"]))
        {
            rpt.Append("<td style='width: 30%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 30%; font-family:Courier New; font-size:small; font-weight:bold'>{0}</td>", dt.Rows[i]["CPROC09"]);
        }

        rpt.Append("</tr>");
        rpt.Append("</table>");
        rpt.Append("<table width='100%'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold'>" + Session["from"] + "</td>");
        rpt.Append("<td style='width: 30%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>Opening Stock</td>");
        //rpt.Append("<td style='width: 15%; font-family:Courier New; font-size:small; font-weight:bold'></td>")
        if (Convert.IsDBNull(dt.Rows[i]["NProc05"]))
        {
            rpt.Append("<td style='width: 15%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 15%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>{0}</td>", dt.Rows[i]["NProc05"]);
        }
        if (Convert.IsDBNull(dt.Rows[i]["CProc10"]))
        {
            rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold'>{0}</td>", dt.Rows[i]["CProc10"]);
        }
        rpt.Append("<td colspan='2' style='width: 15%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }


    public void PGroup_Group()
    {
        rpt.Append("<table width='50%'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 15%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        if (Convert.IsDBNull(dt.Rows[i]["CPROC06"]))
        {
            rpt.Append("<td style='width: 35%; font-family:Courier New; font-size:small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:small; font-weight:bold'>{0}</td>", dt.Rows[i]["CPROC04"] + " - " + dt.Rows[i]["CPROC06"]);
        }
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }


    public void Hearder_Detail()
    {
        rpt.Append("<table width='100%'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 100%; font-family:Courier New; font-size:small; font-weight:bold; border-bottom: 1px solid #000000'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
        rpt.Append("<table width='100%'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold'>DOC. DATE</td>");
        rpt.Append("<td style='width: 15%; font-family:Courier New; font-size:small; font-weight:bold'>DOCUMENT NO.</td>");
        rpt.Append("<td style='width: 35%; font-family:Courier New; font-size:small; font-weight:bold'>BOOK NAME</td>");
        rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>RECEIPT</td>");
        rpt.Append("<td style='width: 10%; font-family:Courier New; font-size:small; font-weight:bold; text-align:right'>ISSUE</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
        rpt.Append("<table width='100%'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 100%; font-family:Courier New; font-size:small; font-weight:bold; border-top: 1px solid #000000'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }


    protected void cmd_excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AppendHeader("content-disposition", "attachment; filename=StockMovement.xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        ltrReport.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }
    protected void cmdBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Rep_StockMovement.aspx");
    }
}