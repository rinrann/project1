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


public partial class View_StockLedger : System.Web.UI.Page
{
    string CompCode = "";
    string yearcode = "";
    string Username = "";
    string compname = "";
    string address = "";


    string OldItemCode = "";
    string OldGrpCode = "";
    string OldGrpName = "";
    string OldCostCode = "";
    string OldSubGrpCode = "";
    string OldSubGrpName = "";
    string oldCloseTotal = "";
    string OldItem = "";

    decimal SubTotRepQuty = 0;
    decimal SubTotRepVal = 0;
    decimal SubTotIsuQuty = 0;
    decimal SubTotIsuVal = 0;

    decimal RecQtyTotal = 0.0M;
    decimal RecValTotal = 0.0M;

    string curstr = "";
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    DataTable dt = new DataTable();
    int i = 0;



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //bind_report();
            bind_reportNew();
        }
    }

    public void bind_reportNew()
    {
        dt = (DataTable)HttpContext.Current.Session["dt"];


        rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:Courier New;font-size:small;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='5' style='width: 35%; font-family:Courier New; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr style='height:20px'>");
        rpt.Append("<td style='width: 8%; font-family:Courier New;font-weight:bold; text-align:Center;padding-bottom:10px'><br/> <u>Stock Ledger</u></td>");
        rpt.Append("</tr'>");
        rpt.Append("</table>");
        //Header
        
        rpt.Append("<table width='100%'  border='0'>");
        rpt.Append("<tr>");
        rpt.Append("<td rowspan='2' style='width: 15%; font-family:Verdana; font-size:x-small; font-weight:bold;text-align:center;border-top:1px solid;border-bottom:1px solid;'>Medicine Name</td>");
        rpt.Append("<td rowspan='2' style='width: 8%; font-family:Verdana; font-size:x-small; font-weight:bold;text-align:center;border-top:1px solid;border-bottom:1px solid;'>Batch No.</td>");
        rpt.Append("<td rowspan='2' style='width: 8%; font-family:Verdana; font-size:x-small; font-weight:bold;text-align:center;border-top:1px solid;border-bottom:1px solid;'>Opening.</td>");
        rpt.Append("<td rowspan='2' style='width: 8%; font-family:Verdana; font-size:x-small; font-weight:bold;text-align:center;border-top:1px solid;border-bottom:1px solid;'>Doc No</td>");
        rpt.Append("<td rowspan='2' style='width: 8%; font-family:Verdana; font-size:x-small; font-weight:bold;text-align:center;border-top:1px solid;border-bottom:1px solid;'>Doc Date</td>");
        rpt.Append("<td colspan='2' style='width: 10%; font-family:Verdana; font-size:x-small; font-weight:bold;text-align:center;border-top:1px solid;'>R E C E I P T");
        rpt.Append("<td colspan='2' style='width: 10%; font-family:Verdana; font-size:x-small; font-weight:bold;text-align:center;border-top:1px solid;'>I S S U E");
        rpt.Append("<td rowspan='2' colspan='2' style='width: 12%; font-family:Verdana; font-size:x-small; font-weight:bold;text-align:center;border-top:1px solid;border-bottom:1px solid;'>Unit1 Closing</td>");
        rpt.Append("<td rowspan='2' colspan='2' style='width: 12%; font-family:Verdana; font-size:x-small; font-weight:bold;text-align:center;border-top:1px solid;border-bottom:1px solid;'>Unit2 Closing</td>");
        rpt.Append("</tr>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:center;border-bottom:1px solid;'>Qty</td>");
        rpt.Append("<td style='width: 8%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:center;border-bottom:1px solid;'>Value</td>");
        rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:center;border-bottom:1px solid;'>Qty</td>");
        rpt.Append("<td style='width: 8%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:center;border-bottom:1px solid;'>Value</td>");
        //rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:center;border-bottom:1px solid;'>Qty</td>");
        //rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:center;border-bottom:1px solid;'></td>");
        //rpt.Append("<td style='width: 7%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:center;border-bottom:1px solid;'>Qty</td>");
        //rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:center;border-bottom:1px solid;'></td>");
        rpt.Append("</tr>");


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            /*if (Convert.IsDBNull(dt.Rows[i]["CPROC04"]) == false)
            {
                if (Convert.ToString(OldGrpCode).Trim() != Convert.ToString(dt.Rows[i]["CPROC04"]).Trim())
                {
                    rpt.Append("<tr>");
                    if (Convert.IsDBNull(dt.Rows[i]["CPROC04"]))
                    {
                        rpt.AppendFormat("<td colspan='10' style='font-family:Verdana; font-size:x-small; font-weight:bold'>&nbsp;</td>");
                    }
                    else
                    {
                        rpt.AppendFormat("<td colspan='10' style='font-family:Verdana; font-size:x-small; font-weight:bold; text-align:left'>{0}</td>", dt.Rows[i]["CPROC06"]);
                    }
                    rpt.Append("</tr>");
                }
                OldGrpCode = dt.Rows[i]["CPROC04"].ToString().Trim();
            }*/
            //if (Convert.ToString(OldItemCode).Trim() != Convert.ToString(dt.Rows[i]["CPROC03"]).Trim())
            //{
            //    rpt.Append("<tr>");
            //    rpt.AppendFormat("<td style='font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:left'>{0}</td>", Convert.ToString(dt.Rows[i]["CPROC03"]).Trim() + "--" + dt.Rows[i]["CPROC09"]);
            //    rpt.AppendFormat("<td style='font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:left'>{0}</td>", Convert.ToString(dt.Rows[i]["BatchNo"]).Trim());
            //    rpt.AppendFormat("<td colspan='8' style='font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:left'>{0}</td>", "");
            //    rpt.Append("</tr>");
                
            //}
            //OldItemCode = dt.Rows[i]["CPROC03"].ToString().Trim();

            rpt.Append("<tr>");
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:left'>{0}</td>", Convert.ToString(dt.Rows[i]["CPROC03"]).Trim() + "--" + dt.Rows[i]["CPROC09"]);
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:left'>{0}</td>", Convert.ToString(dt.Rows[i]["BatchNo"]).Trim());
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:right;'>{0}</td>", Convert.ToString(dt.Rows[i]["NPROC05"]).Trim());
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:left;'>{0}</td>", Convert.ToString(dt.Rows[i]["CProc01"]).Trim());
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:center;'>{0}</td>", Convert.ToString(dt.Rows[i]["DProc01"]).Trim());
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:right;'>{0}</td>", Convert.ToString(dt.Rows[i]["NPROC01"]).Trim());
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:right;'>{0}</td>", Convert.ToString(dt.Rows[i]["NPROC02"]).Trim());
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:right;'>{0}</td>", Convert.ToString(dt.Rows[i]["NPROC03"]).Trim());
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:right;'>{0}</td>", Convert.ToString(dt.Rows[i]["NPROC04"]).Trim());
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:right;'>{0}</td>", Convert.ToString(dt.Rows[i]["NPROC06"]).Trim());
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:left;'>{0}</td>", " "+Convert.ToString(dt.Rows[i]["UnitName1Closing"]).Trim());
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:right;'>{0}</td>", Convert.ToString(dt.Rows[i]["unit2closing"]).Trim());
            rpt.AppendFormat("<td style='font-family:Verdana; font-size:x-small; text-align:left;'>{0}</td>", " " + Convert.ToString(dt.Rows[i]["UnitName2Closing"]).Trim());
            rpt.Append("</tr>");
            
        }
        rpt.Append("</table>");
        ltrReport.Text = rpt.ToString();
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
        rpt.Append("<td style='width: 8%; font-family:Courier New;font-weight:bold; text-align:Center;padding-bottom:10px'><br/> <u>Stock Ledger</u></td>");
        rpt.Append("</tr'>");
        rpt.Append("</table>");


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == 0)
            {
                // Report_Header()
                Hearder_Detail();
            }
            if (Convert.IsDBNull(dt.Rows[i]["CPROC07"]) == false)
            {
                //If SubTotRepVal <> "0.00" Then
                //    Closing_Total()
                //End If
                if (Convert.ToString(OldGrpCode).Trim(' ') != Convert.ToString(dt.Rows[i]["CPROC07"]).Trim(' '))
                {
                    PGroup_Group(i);
                }
                OldGrpCode = dt.Rows[i]["CPROC07"].ToString().Trim();
            }
            //If IsDBNull(dt.Rows[i]("CPROC08")) = False Then
            //    If Trim(OldSubGrpCode) <> Trim(dt.Rows[i]("CPROC08")) Then
            //        PSGroup_Group()
            //    End If
            //    OldSubGrpCode = dt.Rows[i]("CPROC08")
            //End If
            if (Convert.IsDBNull(dt.Rows[i]["CPROC03"]) == false)
            {
                if (Convert.ToString(OldItemCode).Trim(' ') != Convert.ToString(dt.Rows[i]["CPROC03"]).Trim(' '))
                {
                    Item_Group(i);
                }
                OldItemCode = dt.Rows[i]["CPROC03"].ToString().Trim();
            }
            rpt.Append("<table width='100%'  border='0'>");
            rpt.Append("<tr>");
            if (Convert.IsDBNull(dt.Rows[i]["DocDt"])) // Date
            {
                rpt.Append("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
            }
            else
            {
                rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small'>{0}</td>", dt.Rows[i]["DocDt"]); //Doc Date
            }
            if (Convert.IsDBNull(dt.Rows[i]["CProc01"])) // DocNo
            {
                rpt.Append("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
            }
            else
            {
                rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small'>{0}</td>", dt.Rows[i]["CProc01"]);
            }
            /*if (Convert.IsDBNull(dt.Rows[i]["CPROC08"])) // Book Name
            {
                rpt.Append("<td style='width: 20%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
            }
            else
            {
                rpt.AppendFormat("<td style='width: 18%; font-family:Verdana; font-size:xx-small'>{0}</td>", dt.Rows[i]["CPROC08"]);
            }*/
            if (Convert.IsDBNull(dt.Rows[i]["NProc01"])) // Reciept Qty
            {
                rpt.Append("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
            }
            else
            {
                rpt.AppendFormat("<td style='width: 9%; font-family:Verdana; font-size:xx-small; text-align:right'>{0}</td>", SuppressZero(dt.Rows[i]["NPROC01"].ToString()));
                SubTotRepQuty = (Convert.ToDecimal(SubTotRepQuty) + Convert.ToDecimal(dt.Rows[i]["NProc01"]));
            }
            if (Convert.IsDBNull(dt.Rows[i]["NProc02"])) // Reciept Value
            {
                rpt.Append("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
            }
            else
            {
                rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; text-align:right'>{0}</td>", SuppressZero(dt.Rows[i]["NPROC02"].ToString()));
                SubTotRepVal =( Convert.ToDecimal(SubTotRepVal) + Convert.ToDecimal(dt.Rows[i]["NProc02"]));
            }
            if (Convert.IsDBNull(dt.Rows[i]["NProc03"])) // Issue Qty
            {
                rpt.Append("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
            }
            else
            {
                rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; text-align:right'>{0}</td>", SuppressZero(dt.Rows[i]["NPROC03"].ToString()));
                SubTotIsuQuty = (Convert.ToDecimal(SubTotIsuQuty) + Convert.ToDecimal(dt.Rows[i]["NProc03"]));
            }
            if (Convert.IsDBNull(dt.Rows[i]["NProc04"])) // Issue Value
            {
                rpt.Append("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
            }
            else
            {
                rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; text-align:right'>{0}</td>", SuppressZero(dt.Rows[i]["NPROC04"].ToString()));
                SubTotIsuVal = (Convert.ToDecimal(SubTotIsuVal) + Convert.ToDecimal(dt.Rows[i]["NProc04"]));
            }
            rpt.Append("</tr>");
            rpt.Append("</table>");
            if (Convert.IsDBNull(dt.Rows[i]["CPROC03"]) == false)
            {
                if (i < dt.Rows.Count - 1)
                {
                    if (Convert.ToString(dt.Rows[i]["CPROC03"]).Trim(' ') != Convert.ToString(dt.Rows[i + 1]["CPROC03"]).Trim(' '))
                    {
                        itemTotal(i);
                        Closing_Total(i);
                        oldCloseTotal = dt.Rows[i]["CPROC03"].ToString();
                    }
                }
                else
                {
                    itemTotal(i);
                    Closing_Total(i);
                }
            }
        }


         rpt.Append("<table width='100%'   border='0'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 100%; font-family:Verdana; font-size:x-small; font-weight:bold; border-top: 1px solid #000000'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");


        ltrReport.Text = rpt.ToString();

    }

    public void Item_Group(int i)
    {
        rpt.Append("<table width='61%'  border='0'>");
        rpt.Append("<tr>");

        rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold;'>{0}</td>", ((Convert.IsDBNull(dt.Rows[i]["CPROC03"]) == true) ? "&nbsp;" : dt.Rows[i]["CPROC03"]) + " -- " + ((Convert.IsDBNull(dt.Rows[i]["CPROC09"]) == true) ? "&nbsp;" : dt.Rows[i]["CPROC09"]));
        rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold;'>{0}</td>", ((Convert.IsDBNull(dt.Rows[i]["BatchNo"]) == true) ? "&nbsp;" : dt.Rows[i]["BatchNo"]));

        if (Convert.IsDBNull(dt.Rows[i]["CPROC10"]))
        {
            rpt.Append("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:right'>{0}</td>", dt.Rows[i]["CPROC10"]);
        }
        rpt.Append("</tr>");
        rpt.Append("</table>");
        //-----------------------------------------
        rpt.Append("<table width='56%'  border='0'>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 13%; font-family:Verdana; font-size:xx-small; font-weight:bold'>{0}</td>", "");
        rpt.AppendFormat("<td style='width: 18%; font-family:Verdana; font-size:xx-small; font-weight:bold'>{0}</td>", "&nbsp;");
        rpt.AppendFormat("<td style='width: 15%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:left'>{0}</td>", "Opening Stock ");
        rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:right'>{0}</td>", (Convert.IsDBNull(dt.Rows[i]["NPROC05"]) ? "&nbsp;" : SuppressZero(dt.Rows[i]["NPROC05"].ToString())));
        rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:left'>{0}</td>", (Convert.IsDBNull(dt.Rows[i]["CPROC10"]) ? "&nbsp;" : "&nbsp;&nbsp;" + dt.Rows[i]["CPROC10"]));
        rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:right'>{0}</td>", (Convert.IsDBNull(dt.Rows[i]["NPROC07"]) ? "&nbsp;" : SuppressZero(dt.Rows[i]["NPROC07"].ToString())));

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }


    public void PGroup_Group(int i)
    {
        rpt.Append("<table width='90%' border='0'>");
        rpt.Append("<tr>");
        if (Convert.IsDBNull(dt.Rows[i]["CPROC05"]))
        {
            rpt.Append("<td style='width: 50%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td><td style='width: 40%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:left'>{0}</td>", dt.Rows[i]["CPROC05"] + " -- " + dt.Rows[i]["CPROC06"]);
        }
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void PSGroup_Group(int i)
    {
        rpt.Append("<table width='100%' border='0'>");
        rpt.Append("<tr>");
        if (Convert.IsDBNull(dt.Rows[i]["CPROC04"]))
        {
            rpt.Append("<td style='width: 50%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 5%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td><td style='width: 50%; font-family:Verdana; font-size:xx-small; font-weight:bold'>{0}</td>", dt.Rows[i]["CPROC04"] + " -- " + dt.Rows[i]["CPROC07"]);
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

    public void Hearder_Detail()
    {
        rpt.Append("<table width='100%'  border='0'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 80%; font-family:Verdana; font-size:x-small; font-weight:bold; border-bottom: 1px solid #000000'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
        rpt.Append("<table width='100%'  border='0'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 10%; font-family:Verdana; font-size:x-small; font-weight:bold'>Doc Date</td>");
        rpt.Append("<td style='width: 20%; font-family:Verdana; font-size:x-small; font-weight:bold'>Batch No.</td>");
        rpt.Append("<td style='width: 10%; font-family:Verdana; font-size:x-small; font-weight:bold'>Doc No</td>");
        //rpt.Append("<td style='width: 20%; font-family:Verdana; font-size:x-small; font-weight:bold'>Book Name</td>");
        rpt.Append("<td style='width: 20%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:center'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;R E C E I P T");
        rpt.Append("<table width='100%'  border='0'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 40%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:right'>Qty</td>");
        rpt.Append("<td style='width: 60%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:right'>Value</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
        rpt.Append("</td>");
        rpt.Append("<td style='width: 20%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:center'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;I S S U E");
        rpt.Append("<table width='100%'  border='0'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 40%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:right'>Qty</td>");
        rpt.Append("<td style='width: 60%; font-family:Verdana; font-size:x-small; font-weight:bold; text-align:right'>Value</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
        rpt.Append("</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
        rpt.Append("<table width='100%'  border='0'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='width: 80%; font-family:Verdana; font-size:x-small; font-weight:bold; border-top: 1px solid #000000'>&nbsp;</td>");
        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void itemTotal(int i)
    {
        rpt.Append("<table width='100%'  border='0'>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 15%; font-family:Verdana; font-size:xx-small; font-weight:bold'>{0}</td>", "&nbsp;");

        rpt.Append("<td style='width: 32%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:center;'>Total</td>");

        if (Convert.ToDecimal(SubTotRepQuty) == 0)
        {
            rpt.Append("<td style='width: 14%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 14%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:right'>{0}</td>", SubTotRepQuty.ToString());
        }


        if (Convert.ToDecimal(SubTotRepVal) == 0)
        {
            rpt.Append("<td style='width: 13%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 13%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:right'>{0}</td>", SubTotRepVal.ToString());
        }

        if (Convert.ToDecimal(SubTotIsuQuty) == 0)
        {
            rpt.Append("<td style='width: 13%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 13%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:right'>{0}</td>", SubTotIsuQuty.ToString());
        }


        if (Convert.ToDecimal(SubTotIsuVal) == 0)
        {
            rpt.Append("<td style='width: 15%; font-family:Verdana; font-size:xx-small; font-weight:bold'>&nbsp;</td>");
        }
        else
        {
            rpt.AppendFormat("<td style='width: 15%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:right'>{0}</td>", SubTotIsuVal.ToString());
        }

        rpt.Append("</tr>");
        rpt.Append("</table>");

        SubTotRepQuty = 0;
        SubTotRepVal = 0;
        SubTotIsuQuty = 0;
        SubTotIsuVal = 0;
    }


    public void Closing_Total(int i)
    {
        rpt.Append("<table width='56%'  border='0'>");
        rpt.Append("<tr>");
        rpt.AppendFormat("<td style='width: 13%; font-family:Verdana; font-size:xx-small; font-weight:bold'>{0}</td>", Session["to"].ToString());
        rpt.AppendFormat("<td style='width: 18%; font-family:Verdana; font-size:xx-small; font-weight:bold'>{0}</td>", "&nbsp;");
        rpt.AppendFormat("<td style='width: 15%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:left'>{0}</td>", "Closing Stock ");
        rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:right'>{0}</td>", (Convert.IsDBNull(dt.Rows[i]["NPROC06"]) ? "&nbsp;" : SuppressZero(dt.Rows[i]["NPROC06"].ToString())));
        rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:left'>{0}</td>", (Convert.IsDBNull(dt.Rows[i]["CPROC10"]) ? "&nbsp;" : "&nbsp;&nbsp;" + dt.Rows[i]["CPROC10"]));
        rpt.AppendFormat("<td style='width: 10%; font-family:Verdana; font-size:xx-small; font-weight:bold; text-align:right'>{0}</td>", (Convert.IsDBNull(dt.Rows[i]["NPROC08"]) ? "&nbsp;" : SuppressZero(dt.Rows[i]["NPROC08"].ToString())));
    }


    protected void cmd_excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AppendHeader("content-disposition", "attachment; filename=StockLedger.xls");
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
        Response.Redirect("Rep_StockLedger.aspx");
    }
}