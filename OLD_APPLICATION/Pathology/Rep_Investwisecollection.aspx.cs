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

public partial class Pathology_Rep_Investwisecollection : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_Report objreport = new PH_Report(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestMaster thetest = new PH_TestMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION WISE COLLECTION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            txtfromdt.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txttodt.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DropDownFill();            
        }
    }

    private void DropDownFill()
    {
        this.ddlgroup.DataSource = thetest.DropdownTestGroup(Session["CoCode"].ToString().Trim());
        this.ddlgroup.DataTextField = "ProfileName";
        this.ddlgroup.DataValueField = "ProfileCode";
        this.ddlgroup.DataBind();
        this.ddlgroup.Items.Insert(0, new ListItem("--Select--", "0"));

    }


    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HomePage.aspx");
    }

    private void SetContextKey()
    {

        AjaxControlToolkit.AutoCompleteExtender modal = (AjaxControlToolkit.AutoCompleteExtender)txtServiceName.FindControl("AutoCompleteExtender1");
        modal.ContextKey = ddlgroup.SelectedValue.ToString().Trim();// Any constant value
    }

    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetContextKey();
    }


    protected void btnGenRpt_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DataSet ds = new DataSet();
        ds = objreport.GetServiceCollectionRegister(txtfromdt.Text, txttodt.Text, ddlgroup.SelectedValue.Trim(),txtSrvId.Text.Trim());
        Session["ds"] = ds;
        //Response.Redirect("View_Investwisecollection.aspx");
        bind_report(ds);
    }


    public void bind_report(DataSet ds)
    {
       

        for (int i = 0; i < ds.Tables.Count; i++)
        {
            rpt.Append("<table width='100%' cellspacing=0 border=0 style=' border-spacing: 0; padding: 0;font-family:verdana;font-size:small;'>");
            rpt.Append("<tr cellpadding:'0'>");
            rpt.AppendFormat("<td rowspan='5' style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:left'><img src='../Images/logo.jpg'  style='height:87px;width:205px;border-style:inherit;border-color:#D3CDD4'/></td>");
            rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:large; font-weight:bold;text-align:center'>{0}</td>", "");
            rpt.AppendFormat("<td style='width: 35%; font-family:Verdana; font-size:x-Medium; font-weight:bold;text-align:center'>&nbsp;</td>");
            rpt.Append("</tr>");



            rpt.Append("<tr style='height:20px'>");
            rpt.Append("<td style='width: 8%; font-family:Verdana;font-weight:bold; text-align:Center;padding-bottom:10px'><br/> <u>Investment wise Collection Register</u></td>");
            rpt.Append("</tr'>");
            rpt.Append("</table>");

            rpt.Append("<table width='100%' cellspacing=0 cellpadding=0 border=1 bordercolor=silver style='font-family:verdana;font-size:small'>");
            rpt.Append("<tr>");
            rpt.Append("<td style='width:8%'><strong>Date</strong></td>");
            rpt.Append("<td style='width:4%'><strong>Slno</strong></td>");
            rpt.Append("<td style='width:15%'><strong>Patient ID</strong></td>");
            rpt.Append("<td style='width:15%'><strong>Patient Name</strong></td>");
            rpt.Append("<td style='width:15%'><strong>Requisition No</strong></td>");
            rpt.Append("<td style='width:10%'><strong>Invoice No</strong></td>");
            rpt.Append("<td style='width:13%'><strong>Doctor Name</strong></td>");
            rpt.Append("<td style='width:10%;text-align:right;padding-right:10px;'><strong>Amount</strong></td>");
            rpt.Append("<td style='width:10%'><strong>Status</strong></td>");
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
                    if (ds.Tables[i].Rows[a]["BillNo"].ToString() == "ZZZZZZ")
                    {
                        rpt.Append("<td style='font-weight:bold;' colspan='7'>" + ds.Tables[i].Rows[a]["PatientName"].ToString() + "</td>");
                        rpt.Append("<td style='font-weight:bold;text-align:right'>" + ds.Tables[i].Rows[a]["FeesCollected"].ToString() + "</td>");
                        //rpt.Append("<td style='font-weight:bold;text-align:right'>" + ds.Tables[i].Rows[a]["ByCash"].ToString() + "</td>");
                        //rpt.Append("<td style='font-weight:bold;text-align:right'>" + ds.Tables[i].Rows[a]["ByBank"].ToString() + "</td>");
                        //rpt.Append("<td style='font-weight:bold;text-align:right'>" + ds.Tables[i].Rows[a]["Refund"].ToString() + "</td>");
                        rpt.Append("<td style='font-weight:bold;text-align:right'></td>");
                    }
                    else rpt.Append("<td style='font-weight:bold;' colspan='9'>" + ds.Tables[i].Rows[a]["PatientName"].ToString() + "</td>");
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
                    rpt.Append("<td style='text-align:right;padding-right:10px;'>" + ds.Tables[i].Rows[a]["FeesCollected"].ToString() + "</td>");
                    rpt.Append("<td style='text-align:left'>" + ds.Tables[i].Rows[a]["BillStatus"].ToString() + "</td>");
                    //rpt.Append("<td style='text-align:right'>" + ds.Tables[i].Rows[a]["ByBank"].ToString() + "</td>");
                    //rpt.Append("<td style='text-align:right'>" + ds.Tables[i].Rows[a]["Refund"].ToString() + "</td>");
                    //rpt.Append("<td style='text-align:right'>" + ds.Tables[i].Rows[a]["NetCollec"].ToString() + "</td>");
                    rpt.Append("</tr>");
                }
            }
            rpt.Append("</table><div style='page-break-after: always;'></div>");
        }
        ltrReport.Text = rpt.ToString();
        hd.Visible=true;
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] SearchServiceName(string prefixText, string srvgrp)
    {
        string compcode = string.Empty;
        string yearcode = string.Empty;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                compcode = HttpContext.Current.Session["CoCode"].ToString();
                yearcode = HttpContext.Current.Session["YearCode"].ToString();
                cmd.CommandText = "select top 100 TestId + '~' + TestName as Name from ph_testmaster where compcode=@Compcode and GroupCode=@GroupCode and TestName like '%'+@SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Parameters.AddWithValue("@GroupCode", srvgrp);
                cmd.Parameters.AddWithValue("@Compcode", compcode);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Name"].ToString());
                    }
                }
                conn.Close();
                return customers.ToArray();
            }
        }
    }

    
}