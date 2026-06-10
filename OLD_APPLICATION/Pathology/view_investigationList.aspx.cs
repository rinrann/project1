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

public partial class Pathology_view_investigationList : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestMaster thetest = new PH_TestMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
            bind_report();
    }
    public void bind_report()
    {
        DataTable dt,dtGrpCost;//= thetest.getInvestigationList(Session["CoCode"].ToString().Trim());
        DataTable dtGrp = thetest.getInvestigationGroupList(Session["CoCode"].ToString().Trim());

        rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:Courier New;font-size:small;'>");
        rpt.Append("<tr cellpadding:'0'>");
        rpt.AppendFormat("<td rowspan='5' style='width: 35%; font-family:Courier New; font-size:x-Medium; font-weight:bold;text-align:left'> </td>");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "Investigation List");
        rpt.AppendFormat("<td style='width: 35%; font-family:Courier New; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
        rpt.Append("</tr>");

         



        rpt.Append("<table width='100%' cellspacing=0    style='font-family:Courier New;font-size:small;'>");

       

        string old_dep = "";
        string new_dep = "";
        string old_grp = "";
        string new_grp = "";

        for (int i = 0; i < dtGrp.Rows.Count; i++)
        {
            new_dep = dtGrp.Rows[i]["DepartmentID"].ToString().Trim();
            new_grp = dtGrp.Rows[i]["GroupCd"].ToString().Trim();

            if (new_dep != old_dep)
            {
                if (i > 0)
                {
                    rpt.Append("<tr><td colspan=7><br/> </td></tr>");
                }
                rpt.Append("<tr>");
                rpt.Append("<td style='font-weight:bold' colspan=7><u>" + dtGrp.Rows[i]["deptnm"].ToString().Trim() + "</u></td>");
                rpt.Append("</tr>");


            }
            if (old_grp != new_grp)
            {
                if (i > 0 && new_dep == old_dep)
                {
                    rpt.Append("<tr><td colspan=7><br/></td></tr>");
                }
                rpt.Append("<tr>");
                rpt.Append("<td style='font-weight:bold' colspan=7>" + dtGrp.Rows[i]["grpnm"].ToString().Trim() + "</td>");
                rpt.Append("</tr>");
            }

            dt = thetest.getInvestigationList(Session["CoCode"].ToString().Trim(), new_grp);
            dtGrpCost = thetest.getInvestigationGrpCost(Session["CoCode"].ToString().Trim(), new_grp);

            if (dt.Rows.Count > 0)
            {
                rpt.Append("<tr>");
                rpt.Append("<td style='' colspan=7>");
                rpt.Append("<table width='100%' cellspacing=0    style='font-family:Courier New;font-size:small;'>");
                rpt.Append("<tr>");
                rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:20%'> </td>");
                if(Convert.ToDecimal(dtGrpCost.Rows[0]["cost"])>0)
                {
                    rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:6%;'>Charges</td>");
                }
                if (Convert.ToDecimal(dtGrpCost.Rows[0]["SingleChrg"]) > 0)
                {
                    rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:6%;'>Single</td>");
                }
                if (Convert.ToDecimal(dtGrpCost.Rows[0]["TwinsChrg"]) > 0)
                {
                    rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:6%;'>Twins</td>");
                }
                if (Convert.ToDecimal(dtGrpCost.Rows[0]["LabChrg"]) > 0)
                {
                    rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:6%;'>Lab Cost</td>");
                }
                if (Convert.ToDecimal(dtGrpCost.Rows[0]["OtCharge"]) > 0)
                {
                    rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:6%;'>OT Charges</td>");
                }
                if (Convert.ToDecimal(dtGrpCost.Rows[0]["MedicinesChrg"]) > 0)
                {
                    rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:6%;'>Medicines</td>");
                }
                if (Convert.ToDecimal(dtGrpCost.Rows[0]["BiopsyChrg"]) > 0)
                {
                    rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:6%;'>Biopsy Chrgs</td>");
                }
                if (Convert.ToDecimal(dtGrpCost.Rows[0]["IVFLAbChrg"]) > 0)
                {
                    rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:6%;'>IVF/<br/>Andro lAb</td>");
                }
                rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:8%;'>Bio. Ref. Interval</td>");
                rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:8%;'>Unit</td>");
                rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:8%;'>Method</td>");
                rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:8%;'>Consultant 1</td>");
                rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:8%;'>Consultant 2</td>");
                rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;border-top:solid 1px silver;width:8%;'>Consultant 3</td>");
           
                //rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;width:8%;'>Consultant</td>");
                //rpt.Append("<td style='text-align:center;border-bottom:solid 1px silver;width:8%;'>Company</td>");
                rpt.Append("</tr>");
                for (int n = 0; n < dt.Rows.Count; n++)
                {
                    rpt.Append("<tr>");
                    rpt.Append("<td style='border:solid 1px silver; '>" + dt.Rows[n]["TestName"].ToString().Trim() + "</td>");
                    if (Convert.ToDecimal(dtGrpCost.Rows[0]["cost"]) > 0)
                    {
                        rpt.Append("<td style='border:solid 1px silver; text-align:right'>" + dt.Rows[n]["cost"].ToString().Trim() + "</td>");
                    }
                    if (Convert.ToDecimal(dtGrpCost.Rows[0]["SingleChrg"]) > 0)
                    {
                        rpt.Append("<td style='border:solid 1px silver; text-align:right'>" + dt.Rows[n]["SingleChrg"].ToString().Trim() + "</td>");
                    }
                    if (Convert.ToDecimal(dtGrpCost.Rows[0]["TwinsChrg"]) > 0)
                    {
                        rpt.Append("<td style='border:solid 1px silver; text-align:right'>" + dt.Rows[n]["TwinsChrg"].ToString().Trim() + "</td>");
                    }
                    if (Convert.ToDecimal(dtGrpCost.Rows[0]["LabChrg"]) > 0)
                    {
                        rpt.Append("<td style='border:solid 1px silver; text-align:right'>" + dt.Rows[n]["LabChrg"].ToString().Trim() + "</td>");
                    }
                    if (Convert.ToDecimal(dtGrpCost.Rows[0]["OtCharge"]) > 0)
                    {
                        rpt.Append("<td style='border:solid 1px silver; text-align:right'>" + dt.Rows[n]["OtCharge"].ToString().Trim() + "</td>");
                    }
                    if (Convert.ToDecimal(dtGrpCost.Rows[0]["MedicinesChrg"]) > 0)
                    {
                        rpt.Append("<td style='border:solid 1px silver; text-align:right'>" + dt.Rows[n]["MedicinesChrg"].ToString().Trim() + "</td>");
                    }
                    if (Convert.ToDecimal(dtGrpCost.Rows[0]["BiopsyChrg"]) > 0)
                    {
                        rpt.Append("<td style='border:solid 1px silver; text-align:right'>" + dt.Rows[n]["BiopsyChrg"].ToString().Trim() + "</td>");
                    }
                    if (Convert.ToDecimal(dtGrpCost.Rows[0]["IVFLAbChrg"]) > 0)
                    {
                        rpt.Append("<td style='border:solid 1px silver; text-align:right'>" + dt.Rows[n]["IVFLAbChrg"].ToString().Trim() + "</td>");
                    }
                    rpt.Append("<td style='border:solid 1px silver; text-align:left;padding-left:5px'>" + dt.Rows[n]["NormalRange"].ToString().Trim() + "</td>");
                    rpt.Append("<td style='border:solid 1px silver; text-align:left;padding-left:5px'>" + dt.Rows[n]["Unit"].ToString().Trim() + "</td>");
                    rpt.Append("<td style='border:solid 1px silver; text-align:left;padding-left:5px'>" + dt.Rows[n]["Method"].ToString().Trim() + "</td>");
                    rpt.Append("<td style='border:solid 1px silver; text-align:left;padding-left:5px'>" + dt.Rows[n]["consullt_name1"].ToString().Trim() + "</td>");
                    rpt.Append("<td style='border:solid 1px silver; text-align:left;padding-left:5px'>" + dt.Rows[n]["consullt_name2"].ToString().Trim() + "</td>");
                    rpt.Append("<td style='border:solid 1px silver; text-align:left;padding-left:5px'>" + dt.Rows[n]["consullt_name3"].ToString().Trim() + "</td>");

                    //rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["ConsultantChrg"].ToString().Trim() + "</td>");
                    //rpt.Append("<td style='text-align:right'>" + dt.Rows[i]["companychrg"].ToString().Trim() + "</td>");
                    rpt.Append("</tr>");
                }
                rpt.Append("</table>");
                rpt.Append("</td>");
                rpt.Append("</tr>");
            } 

            old_dep = new_dep;
            old_grp = new_grp;
        }

        rpt.Append("</table>");
        Literal1.Text = rpt.ToString();

    }

    protected void cmd_excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AppendHeader("content-disposition", "attachment; filename=InvestigationList.xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        Literal1.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }
}