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
using System.Collections.Generic;
using System.IO; 


public partial class Assignment_PatientOperationDetails : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientOperationDetailsClass thoperationObject = new PatientOperationDetailsClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "OT Related Report";

        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "OT RELATED REPORT", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            DropDownFill();
           
        }
    }


    public void DropDownFill()
    {

        this.ddlOTType.Items.Clear();
        this.ddlOTType.DataSource = thoperationObject.OperationType();
        this.ddlOTType.DataTextField = "OperationTypeName";
        this.ddlOTType.DataValueField = "OperationTypeID";
        this.ddlOTType.DataBind();
        this.ddlOTType.Items.Insert(0, new ListItem("--Select--", "0"));

        this.ddlOTName.Items.Insert(0, new ListItem("--Select--", "0"));

    }

    //public void Report_Header()
    //{
    //    rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
    //    rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
    //    rpt.Append("<td rowspan='3' width='2%' style='font-family:Verdana; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='/Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
    //    rpt.Append("<td   width='20%'  style='height:20px;font-family:Verdana; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
    //    rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Verdana; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='/Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
    //    rpt.Append("</tr>");

    //    rpt.Append("<tr  style='height:20px;'>");
    //    rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
    //    rpt.Append("</tr>");

    //    rpt.Append("<tr  style='height:10px;'>");
    //    rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Verdana; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

    //    rpt.Append("</tr>");
    //    rpt.Append("</table>");
    //}

    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Verdana; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../photo/" + Session["Logopath"].ToString().Trim() + "'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Verdana; font-size:x-large; font-weight:bold;text-align:center'><u>" + Session["CoName"].ToString().Trim() + "</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Verdana; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../photo/" + Session["Logopath2"].ToString().Trim() + "'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Verdana; font-size:small; font-weight:bold;text-align:center'>(Regn. No : " + Session["HosRegnNo"].ToString().Trim() + ")</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Verdana; font-size:medium; font-weight:bold;text-align:center'>" + Session["ADDR"].ToString().Trim() + "</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    protected void ddlOTType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlOTName.Items.Clear();
        this.ddlOTName.DataSource = thoperationObject.OperationDetails(ddlOTType.SelectedValue);
        this.ddlOTName.DataTextField = "OperationName";
        this.ddlOTName.DataValueField = "OperationID";
        this.ddlOTName.DataBind();
        this.ddlOTName.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Report_Header();
        GetHearder_Detail();
    }

    public void GetHearder_Detail()
    {
        int i;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date1, date2;
        if (txtFromDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date1 = "null";
        }

        if (txtTodate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date2 = "null";
        }

        DataTable dt = thoperationObject.GetOperationInfo(date1, date2, ddlOTType.SelectedValue, ddlOTName.SelectedValue, Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Sl.No.</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>RegNo</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Address</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>PhoneNo</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Adm.Dt</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>OT Name</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>OT Date</td>");
        rpt.Append("<td style='width: 5%;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Doctor</td>");
        rpt.Append("</tr >");

        for (i = 0; i < dt.Rows.Count; i++)
        {
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", i + 1);
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PatientReg"]);
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["patient_name"]);
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["vill_city"]);
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["PhNo1"]);
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["AdmissionDate"]);
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["OperationName"]);
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["OTDate"]);
            rpt.AppendFormat("<td style='width: 7%;font-family:Verdana; font-size:small;text-align:center'>{0}</td>", dt.Rows[i]["doc_name"]);
            rpt.Append("</tr >");

        }
        rpt.Append("</table'>");
        ltrReport.Text = rpt.ToString();

    }
}