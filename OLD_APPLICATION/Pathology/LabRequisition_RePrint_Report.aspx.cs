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

public partial class Pathology_LabRequisition_RePrint_Report : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Pathology_reprint_Class _obj = new  Pathology_reprint_Class(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString()); 

    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPRINT LAB SLIP", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            GridFill();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblRequisitionNo = (Label)GridView1.Rows[index].FindControl("lblRequisitionNo");

            GetReport(lblRequisitionNo.Text);
            Button7.Visible = true;
            Button6.Visible = true;
        }
    }

    public void GetReport(string reqno)
    {
        Report_Header();
        GetHearder_Detail(reqno);
        ltrReport.Text = rpt.ToString();
    }

    public void Report_Header()
    {
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Arial;'>");
        rpt.Append("<tr cellpadding:'0'  style='height:30px;'>");
        rpt.Append("<td rowspan='3' width='2%' style='font-family:Arial; font-size:x-large; font-weight:bold;padding-left:100px;text-align:left'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("<td   width='20%'  style='height:20px;font-family:Arial; font-size:x-large; font-weight:bold;text-align:center'><u>GFC HOSPITAL</u></td>");
        rpt.Append("<td rowspan='3'  width='2%' style='height:20px;font-family:Arial; font-size:x-large;padding-right:100px; font-weight:bold;text-align:right'><img src='../Images/gfclogo.png'  style='height:77px;width:75px;'/></td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:20px;'>");
        rpt.AppendFormat("<td   width='20%'  style='height:10px;font-family:Arial; font-size:small; font-weight:bold;text-align:center'>(Regn. No : NH-315/G-70/2013)</td>");
        rpt.Append("</tr>");

        rpt.Append("<tr  style='height:10px;'>");
        rpt.AppendFormat("<td  width='70%'  style='height:15px;font-family:Arial; font-size:medium; font-weight:bold;text-align:center'>Kushpata, Ghatal, Paschim Medinipur, WB, 721212</td>");

        rpt.Append("</tr>");
        rpt.Append("</table>");
    }

    public void GetHearder_Detail(string reqno)
    {
        DataTable dt = thereq.GetRequisitionForReport(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), reqno); //ds.Tables[0]; 
        if (dt.Rows.Count>0)
        {
            rpt.Append("<br/>");
          
            rpt.Append("<table width='100%' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:Arial;'>");
            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Arial; font-size:small;text-align:center'><u><b>Patient:</b></u> {0}, ( {1} ), {2}, <u><b>of</b></u> {3}, <u><b>Ph No:</b></u>{4}</td>", dt.Rows[0]["PatientName"], dt.Rows[0]["RegistrationNo"], dt.Rows[0]["Age"], dt.Rows[0]["Address"], dt.Rows[0]["Ph1"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Arial; font-size:small;text-align:center'><u><b>Requisition No :</b></u> {0}, <u><b>Test Date :</b></u> {1},  <u><b>Delivery Date :</b></u></td>", dt.Rows[0]["RequisitionNo"], dt.Rows[0]["TDate"], dt.Rows[0]["delDate"]);
            rpt.Append("</tr >");

            rpt.Append("<tr style='height:20px'>");
            rpt.AppendFormat("<td style='height:20px; font-family:Arial; font-size:small;text-align:center'><u><b>Advance Amount :</b></u> {0}, <u><b>Refer By :</b></u> {1}</td>", dt.Rows[0]["AdAmt"], dt.Rows[0]["ReferalName"]);
            rpt.Append("</tr >");
            rpt.Append("</table>");




            rpt.Append("<br/>");
            rpt.Append("<center>");
            rpt.Append("<table width='500px;' cellspacing=0 border=0 style='border:1px solid black;border-spacing: 0; padding: 0;font-family:verdana;'>");

            rpt.Append("<tr style='height:40px'>");
            rpt.Append("<td colspan='6' style='width: 8%;border-bottom: 1px solid black; font-family:Verdana;font-weight:bold;background-color:#9B9C8D;font-size:medium; text-align:Center'> TEST DETAILS  </td>");
            rpt.Append("</tr'>");

            DataTable dt1 = thereq.GetTestDtls(Session["CoCode"].ToString().Trim(), reqno);

            rpt.Append("<tr style='height:30px'>");
            rpt.Append("<td style='width:250px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Name</td>");
            rpt.Append("<td style='width:250px;border-bottom: 1px solid black;font-family:Verdana;font-weight:bold;background-color:beige; font-size:x-small; text-align:center'>Test Cost</td>");
            double total = 0.0; 
            rpt.Append("</tr >");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                rpt.AppendFormat("<td style='width: 250px;border-bottom: 1px solid black;border-right: 1px solid black; font-family:Verdana; font-size:x-small;text-align:center'>{0}</td>", dt1.Rows[i]["TestName"]);
                rpt.AppendFormat("<td style='width: 250px;border-bottom: 1px solid black;font-family:Verdana; font-size:x-small;text-align:center'> {0}</td>", dt1.Rows[i]["Cost"]);
                rpt.Append("</tr >");
                total = total + Convert.ToDouble(dt1.Rows[i]["cost"]);
            }

            rpt.Append("<tr style='height:30px;background-color:#FF9999;'>");
            rpt.Append("<td style='width:250px;border-right: 1px solid black; font-family:Verdana;font-weight:bold; font-size:x-small; text-align:center'>Total Cost</td>");
            rpt.AppendFormat("<td style='width: 250px;font-family:Verdana; font-size:x-small;text-align:center'>Rs. {0}</td>", total);
            rpt.Append("</tr>");
            rpt.Append("</table>");

            rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>"); rpt.Append("<br/>");
            rpt.Append("<table>");

            rpt.Append("<tr>");
            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'>______________________________________</td>");
            rpt.Append("</tr'>");

            rpt.Append("<tr>");
            rpt.Append("<td style='width: 5%; font-family:Times New Roman; font-size:small;font-weight:bold; text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:left'></td>");
            rpt.Append("<td style='width: 5%; font-family:Verdana; font-size:small;font-weight:bold; text-align:center'> Signature </td>");

            rpt.Append("</tr'>");
            rpt.Append("</table>");
            rpt.Append("</center>");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No row to Display.. !');", true);
        }         
        ltrReport.Visible = true;

    }

    private void GridFill()
    {
        GridView1.DataSource = _obj.GridFillDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(),txtName.Text, txtCo.Text, txtAddress.Text, txtPhNo.Text);
        GridView1.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridFill();
    }
}