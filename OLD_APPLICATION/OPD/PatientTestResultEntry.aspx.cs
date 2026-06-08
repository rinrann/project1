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

public partial class OPD_PatientTestResultEntry : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAdmission thepd = new PatientAdmission(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Discharge thedischarge = new Discharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PatientAppointment thepatientAppo = new PatientAppointment(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    static string userId1 = "";
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION REQUISITION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
        Page.Title = "Patient Test Result Entry";
        if (!IsPostBack)
        {
            txtdateSrch.Text = DateTime.Now.ToString("yyyy-MM-dd");
            GridFill();
        }
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            Label lblreqno = (Label)GridView1.Rows[index].FindControl("lblreqno");
            Session["Reqno"] = lblreqno.Text;
            lblmsg.Text = "";
            GridView2.DataSource = thereq.GetTestResult(Session["CoCode"].ToString().Trim(), lblreqno.Text);
            GridView2.DataBind();
        }
    }


    protected void Button11_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    public void GridFill()
    {
        GridView1.DataSource = thereq.GridFillforTest(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtdateSrch.Text, "DIG", txtnameSrch.Text, txtphSrch.Text);
        GridView1.DataBind();
    }
    protected void CmdSave_Click(object sender, EventArgs e)
    {
        int c = 0;
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            Label lblRequisitionID = (Label)GridView2.Rows[i].FindControl("lblRequisitionID");
            Label lblRowId = (Label)GridView2.Rows[i].FindControl("lblRowId");
            Label lbltestcode = (Label)GridView2.Rows[i].FindControl("lbltestcode");
            TextBox txtResult = (TextBox)GridView2.Rows[i].FindControl("txtResult");
            if (txtResult.Text.Trim() != "")
            {
                thereq.UpdateResult(Session["CoCode"].ToString().Trim(), lblRequisitionID.Text, lblRowId.Text, lbltestcode.Text, txtResult.Text);
                c = c + 1;
            }
        }
        if (c > 1)
            lblmsg.Text = "Updated Succesfully!";
        else lblmsg.Text = "";
    }
    protected void CmdShow_Click(object sender, EventArgs e)
    {
        DataSet ds = thereq.GetTestReportsResult(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), Session["Reqno"].ToString());

        DataTable dt1 = ds.Tables[0];
        //DataTable dt2 = ds.Tables[1];


        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        rpt.Append("<table width='100%' cellspacing=0 border=0 style='font-family:verdana;'>");
        rpt.Append("<tr>");
        rpt.Append("<td width='30%'><img src='../Images/logo.jpg'  style=''/></td>");
        rpt.Append("<td   width='40%'  style='height:18px;font-family:Arial; font-size:medium; text-align:center'><b>TEST RESULT</b></td>");
        rpt.Append("<td width='30%' style='text-align:right'>Print Date : " + date + "</td>");
        rpt.Append("</tr>");

        rpt.Append("</table><br/>");

        rpt.Append("<table width='100%' cellspacing=0 style='border-top:1px solid gray;border-bottom:1px solid gray;border-left:1px solid gray;border-right:1px solid gray; font-family:verdana;'>");
        rpt.Append("<tr>");
        rpt.Append("<td style='padding-left:5px' >Patient Name : </td>");
        rpt.Append("<td>" + dt1.Rows[0]["PName"].ToString() + " </td>");
        rpt.Append("<td>Collected : </td>");
        rpt.Append("<td>" + dt1.Rows[0]["Collected"].ToString() + " </td>");
        rpt.Append("<tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='padding-left:5px'>Age/Gender : </td>");
        rpt.Append("<td> " + dt1.Rows[0]["Age"].ToString() + "</td>");
        rpt.Append("<td>Received : </td>");
        rpt.Append("<td> " + dt1.Rows[0]["Received"].ToString() + "</td>");
        rpt.Append("<tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='padding-left:5px'>UHID : </td>");
        rpt.Append("<td> " + dt1.Rows[0]["UHID"].ToString() + "</td>");
        rpt.Append("<td>Reported : </td>");
        rpt.Append("<td>" + dt1.Rows[0]["Reported"].ToString() + " </td>");
        rpt.Append("<tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='padding-left:5px'>Mobile No : </td>");
        rpt.Append("<td> " + dt1.Rows[0]["MobNo"].ToString() + "</td>");
        rpt.Append("<td>Status : </td>");
        rpt.Append("<td> " + dt1.Rows[0]["RepStatus"].ToString() + "</td>");
        rpt.Append("<tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='padding-left:5px'>Ref. Doctor : </td>");
        rpt.Append("<td>" + dt1.Rows[0]["RefDocName"].ToString() + " </td>");
        rpt.Append("<td>Visit ID : </td>");
        rpt.Append("<td>" + dt1.Rows[0]["VisitId"].ToString() + " </td>");
        rpt.Append("<tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='padding-left:5px'>Client Code : </td>");
        rpt.Append("<td>" + dt1.Rows[0]["ClientCd"].ToString() + " </td>");
        rpt.Append("<td>Barcode No : </td>");
        rpt.Append("<td>  </td>");
        rpt.Append("<tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='padding-left:5px'>Patient ID Proof : </td>");
        rpt.Append("<td>  </td>");
        rpt.Append("<tr>");

        rpt.Append("</table><br/>");


        rpt.Append("<table width='100%' cellspacing=0 border=0 style='font-family:verdana;border-top:1px solid gray;border-bottom:1px solid gray;border-left:1px solid gray;border-right:1px solid gray;'>");

        rpt.Append("<tr>");
        rpt.Append("<td colspan='5' style='text-align:center;height:30px'><b>" + dt1.Rows[0]["DepName"].ToString() + "</b></td>");
        rpt.Append("<tr>");

        rpt.Append("<tr>");
        rpt.Append("<td style='border-top:1px solid gray;border-bottom:1px solid gray;width:35%;padding-left:5px'><b>Test Name</b></td>");
        rpt.Append("<td style='border-top:1px solid gray;border-bottom:1px solid gray;width:15%'><b>Result</b></td>");
        rpt.Append("<td style='border-top:1px solid gray;border-bottom:1px solid gray;width:15%'><b>Unit</b></td>");
        rpt.Append("<td style='border-top:1px solid gray;border-bottom:1px solid gray;width:15%'><b>Bio. Ref. Interval</b></td>");
        rpt.Append("<td style='border-top:1px solid gray;border-bottom:1px solid gray;width:20%'><b>Method</b></td>");
        rpt.Append("<tr>");

        string OldGrp = "";
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            if (OldGrp != dt1.Rows[i]["TestGrpName"].ToString())
            {
                rpt.Append("<tr>");
                rpt.Append("<td style='padding-left:5px'><b>" + dt1.Rows[i]["TestGrpName"].ToString() + "</b></td>");
                rpt.Append("</tr>");
            }

            rpt.Append("<tr>");
            rpt.Append("<td style='padding-left:10px'>" + dt1.Rows[i]["TestName"].ToString() + "<br/><span style='font-size:x-small'><I>" + dt1.Rows[i]["TestDetails"].ToString() + "</I></span></td>");
            rpt.Append("<td>" + dt1.Rows[i]["Result"].ToString() + "</td>");
            rpt.Append("<td>" + dt1.Rows[i]["Unit"].ToString() + "</td>");
            rpt.Append("<td>" + dt1.Rows[i]["BioRefInterval"].ToString() + "</td>");
            rpt.Append("<td>" + dt1.Rows[i]["Method"].ToString() + "</td>");
            rpt.Append("<tr>");

            OldGrp = dt1.Rows[i]["TestGrpName"].ToString();
        }
        rpt.Append("</table>");

        ltrReport.Text = rpt.ToString();
    }
}