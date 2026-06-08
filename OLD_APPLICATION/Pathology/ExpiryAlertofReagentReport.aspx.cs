using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Collections;
using System.Drawing;
 
public partial class Pathology_ExpiryAlertofReagentReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    PH_ExpiryAlertofReagentReport thedreport = new PH_ExpiryAlertofReagentReport(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "TEST DONE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        Page.Title = "Expiry Alert of Reagent";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        GetReport();
    }


    public void GetReport()
    {
        Report_Header();
        GetHearder_Detail();
        ltrReport.Text = rpt.ToString();



    }
    public void Report_Header()
    {
        rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;background-color:#9B9C8D;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='4' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
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
        DataTable dt = thedreport.Gettestdtls(Session["CoCode"].ToString().Trim()); //ds.Tables[0];
       
        DateTime dt1 = DateTime.Now.AddMonths(1);
      
            rpt.Append("<br/>");
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='4' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige;font-size:medium; text-align:Center'> EXPIRY REPORT OF REAGENT  </td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width: 8%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Reagent Name</td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;border-right: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>Expiry Date</td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana;background-color:beige;border-bottom: 1px solid black;font-weight:bold;font-size:x-small; text-align:center'>Approximate Day's of Use</td>");
            rpt.Append("</tr >");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime exdate =Convert.ToDateTime(dt.Rows[i]["ExpiryDate"]);
                TimeSpan t=exdate-dt1;
                double noofdays=t.TotalDays;
                if (noofdays <= 31)
                {
                    rpt.Append("<tr >");
                    rpt.AppendFormat("<td style='width: 5%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[i]["ReagentName"]);
                    rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black;border-right: 1px solid black; font-size:x-small;text-align:center'>{0}</td>", dt.Rows[i]["ExDate"]);
                    rpt.AppendFormat("<td style='width: 7%; font-family:Verdana;border-bottom: 1px solid black; font-size:x-small;text-align:center'></td>");
                    rpt.Append("</tr >");
                }
            }

            rpt.Append("<hr>");

        }
      
    }
