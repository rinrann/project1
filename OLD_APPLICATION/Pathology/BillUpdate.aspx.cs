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

public partial class Pathology_BillUpdate : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_PatientRequisition thereq = new PH_PatientRequisition(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_TestPopupMultiple thedia = new PH_TestPopupMultiple(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BILL UPDATE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "BILL UPDATE", checkAccessType.InsertAction) == false)
        {
            //Button1.Enabled = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
        Page.Title = "Bill Update";
        if (!IsPostBack)
        {
            txtInvDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            GridFill();

        }
    }

    public void GridFill()
    {
        DataTable dt = thereq.GridFillBillDetls(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtRegNo.Text.Trim(), txtPtName.Text.Trim(), txtInvDate.Text.Trim());
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl_paymode = (DropDownList)e.Row.FindControl("ddl_paymode");
            Label lblpaymode = (Label)e.Row.FindControl("lblpaymode");
            
            string usr_status = thereq.userStatus(Session["CoCode"].ToString().Trim(), Session["userName"].ToString().Trim());

            ddl_paymode.SelectedValue = lblpaymode.Text.Trim();
            
            //if (usr_status == "0")
            //{
            //    ddl_paymode.Enabled = false;
            //    GridView1.Columns[8].Visible = false;
            //}
            //else
            //{
            //    ddl_paymode.Enabled = true;
            //    GridView1.Columns[8].Visible = true;
            //}
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Save")
        {
            GridViewRow rowSelect = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = rowSelect.RowIndex;
            DropDownList ddl_paymode = (DropDownList)GridView1.Rows[index].FindControl("ddl_paymode");
            Label lblVchNo = (Label)GridView1.Rows[index].FindControl("lblVchNo");


            if (thereq.UpdateBillDetails(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), lblVchNo.Text.Trim(), ddl_paymode.SelectedValue) == true)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Saves Successfully')", true);
                GridFill();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridFill();
    }
}