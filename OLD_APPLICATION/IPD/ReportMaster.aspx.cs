using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class IPD_ReportMaster : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    ReportMasterClass theHelper = new  ReportMasterClass(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPORT MASTER", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPORT MASTER", checkAccessType.InsertAction) == false)
        {
            btnSubmit.Enabled = false;
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPORT MASTER", checkAccessType.DeleteAction) == false)
        {
            GridView1.Columns[6].Visible = false;
        }

        if (!IsPostBack)
        {
            Panel2.Visible = false;
            DropDownList1.SelectedValue = "1";
            GridFill();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "1")
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
        }
        else
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
        }
    }

    public void GridFill()
    {
        GridView1.DataSource = theHelper.GetReportMaster(Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }

    public void ResetAllFields()
    {
        btnSubmit.Text = "Submit";
        DropDownList1.SelectedIndex = 0; DropDownList2.SelectedIndex = 0;
        txtBengali.Text = ""; txtEnglish.Text = ""; txtReportName.Text = "";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPORT MASTER", checkAccessType.InsertAction) == false)
        {
            btnSubmit.Enabled = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string reportName = "";
        string reportContext = "";
        if (DropDownList1.SelectedValue == "1")
        {
            reportName = txtReportName.Text; 
            reportContext = txtBengali.Text;
        }
        else
        {
            reportName = txtReportNameEnglish.Text;
            reportContext = txtEnglish.Text;
        }
        if (btnSubmit.Text == "Submit")
        {
            theHelper.Insert_Update_Delete_ReportMaster(1, null, DropDownList1.SelectedValue, DropDownList2.SelectedValue, reportName, reportContext, Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
        }
        else
        {
            theHelper.Insert_Update_Delete_ReportMaster(2, HiddenField1.Value, DropDownList1.SelectedValue, DropDownList2.SelectedValue, reportName, reportContext, Session["CoCode"].ToString().Trim());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
        }
        GridFill();
        ResetAllFields();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ResetAllFields();

        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblID = (Label)GridView1.Rows[index].FindControl("lblID"); 
            HiddenField1.Value = lblID.Text;

            Label lblLanguage = (Label)GridView1.Rows[index].FindControl("lblLanguage");
            DropDownList1.SelectedValue = lblLanguage.Text;

            Label lblFormType = (Label)GridView1.Rows[index].FindControl("lblFormType");
            DropDownList2.SelectedValue = lblFormType.Text;

            Label lblFormName = (Label)GridView1.Rows[index].FindControl("lblFormName");
            Label lblFormContext = (Label)GridView1.Rows[index].FindControl("lblFormContext");

            if (lblLanguage.Text == "1")
            {
                Panel1.Visible = true; Panel2.Visible = false;
                txtReportName.Text = lblFormName.Text;
                txtBengali.Text = lblFormContext.Text;
            }
            else
            {
                Panel1.Visible = false; Panel2.Visible = true;
                txtReportNameEnglish.Text = lblFormName.Text;
                txtEnglish.Text = lblFormContext.Text;
            }           
            btnSubmit.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPORT MASTER", checkAccessType.UpdateAction) == false)
            {
                btnSubmit.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblID");
        if (theHelper.Insert_Update_Delete_ReportMaster(3, lblID.Text, null, null, null, null, Session["CoCode"].ToString().Trim()) == true)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);

        GridFill();
        ResetAllFields();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "REPORT MASTER", checkAccessType.DeleteAction) == false)
            {
                e.Row.Cells[6].Visible = false;
            }
        }
    }
}