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

public partial class IPD_CommissionReport : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ReferPatientList theHelper = new ReferPatientList(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTORS COMISSION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            RadioButtonList1.SelectedValue = "With Header";
            DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
            DropDownList1.Items.Insert(1, new ListItem("Doctor", "D"));
            DropDownList1.Items.Insert(2, new ListItem("Quack ", "Q"));
            DropDownList1.SelectedValue = "D";
            //cmdBack.visible = false;
            //this.cmdPrint.visible = false;
            PopulateDoc("D");
        }
        Page.Title = "Comission Details";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {


        GetReport();

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        PopulateDoc(DropDownList1.SelectedValue);

    }

    private void PopulateDoc(string type)
    {
        DropDownList2.Items.Clear();


        if (type == "D")
        {
            this.DropDownList2.DataSource = theHelper.getDoc("D", Session["CoCode"].ToString().Trim());
            this.DropDownList2.DataTextField = "doc_name";
            this.DropDownList2.DataValueField = "doc_id";
            this.DropDownList2.DataBind();
            this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            if (type == "Q")
            {
                this.DropDownList2.DataSource = theHelper.getDoc("Q", Session["CoCode"].ToString().Trim());
                this.DropDownList2.DataTextField = "QuackName";
                this.DropDownList2.DataValueField = "QuackId";
                this.DropDownList2.DataBind();
                this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                DropDownList2.Items.Clear();
            }

        }

    }

    public void GetReport()
    {
        if (DropDownList1.SelectedValue != "0" && DropDownList2.SelectedValue != "0")
        {
            if (RadioButtonList1.SelectedValue == "With Header")
            {
                Report_Header();
                GetHearder_Detail();
            }
            else
            {
                GetHearder_Detail();
            }
            ltrReport.Text = rpt.ToString();
        }
        else
        {
            ltrReport.Text = "";
        }
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
        DataTable dt = theHelper.GetPatientComissionDtls(DropDownList1.SelectedValue.ToString(), DropDownList2.SelectedValue.ToString(), Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim());
        rpt.Append("<br/>");

        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

        rpt.Append("<tr style='height:40px'>");
        rpt.Append("<td colspan='7' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> PATIENT DETAILS  </td>");
        rpt.Append("</tr'>");

        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Registration No</td>");
        rpt.Append("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Patient's Name</td>");
        rpt.Append("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Guardian Name</td>");
        rpt.Append("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Admission Date</td>");
        /*rpt.Append("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Under Doctor</td>");*/
       rpt.Append("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Amount</td>");
         /*rpt.Append("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:center'>Discharge Date</td>");*/
        rpt.Append("</tr >");
       double totamt = 0.00;
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                rpt.Append("<tr style='height:30px'>");
                rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>{0}</td>", dt.Rows[i]["PatientReg"]);
                rpt.AppendFormat("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>{0}</td>", dt.Rows[i]["patient_name"]);
                rpt.AppendFormat("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>{0}</td>", dt.Rows[i]["guardian_name"]);
                rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:center'>{0}</td>", dt.Rows[i]["ADate"]);
                /*rpt.AppendFormat("<td style='width: 20%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:left'>{0}</td>", dt.Rows[i]["doctor"]);*/
                rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:right'>{0}</td>", dt.Rows[i]["Debit"]);
                /*rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:center'>{0}</td>", discdt);*/
                rpt.Append("</tr >");
                totamt = totamt + double.Parse(dt.Rows[i]["Debit"].ToString());
            }
        }
        rpt.Append("<tr style='height:30px'>");
        rpt.Append("<td colspan='4' style='width: 90%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:small; text-align:right'>Total Amount</td>");
        rpt.AppendFormat("<td style='width: 10%;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;background-color:beige; font-size:small; text-align:right'>{0}</td>", String.Format("{0:0.##}",totamt));
        rpt.Append("</tr >");
        rpt.Append("</table>");
        ltrReport.Visible = true;
    }
}