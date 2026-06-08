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
using System.Web.Security;

public partial class IPD_OTPopup : System.Web.UI.Page
{
    OperationDetails theHelper = new OperationDetails(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    OTPopup thereg = new OTPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }

        if (!IsPostBack)
        {
            DropDownFill(); GridFill();
            Tab1Func();
        }
    }

    public void DropDownFill()
    {

        ddlOTTYpe.Items.Clear();
        this.ddlOTTYpe.DataSource = theHelper.DropdownID(Session["CoCode"].ToString().Trim());
        this.ddlOTTYpe.DataTextField = "OperationTypeName";
        this.ddlOTTYpe.DataValueField = "OperationTypeID";
        this.ddlOTTYpe.DataBind();
        this.ddlOTTYpe.Items.Insert(0, new ListItem("--Select--", "0"));


        DropDownList1.Items.Clear();
        this.DropDownList1.DataSource = thereg.DropDownOTType(Session["CoCode"].ToString().Trim());
        this.DropDownList1.DataTextField = "OperationTypeName";
        this.DropDownList1.DataValueField = "OperationTypeID";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));

        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void GridFill()
    {
        GridView1.DataSource = thereg.GridPopup(DropDownList1.SelectedValue, DropDownList2.SelectedValue, Session["CoCode"].ToString().Trim());
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblOTTYPEID = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblOTTYPEID");
        Label lblottypename = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblottypename");
        HiddenField1.Value = lblOTTYPEID.Text + "#" + lblottypename.Text;
        Label lblotid = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblotid");
        Label lblotname = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblotname");
        HiddenField2.Value = lblotid.Text + "#" + lblotname.Text;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList2.Items.Clear();
        this.DropDownList2.DataSource = thereg.DropdownOTName(DropDownList1.SelectedValue,Session["CoCode"].ToString().Trim());
        this.DropDownList2.DataTextField = "OperationName";
        this.DropDownList2.DataValueField = "OperationID";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string OperationId= theHelper.GetOperationID(Session["CoCode"].ToString().Trim()).ToString();

        if (btnSubmit.Text == "Submit")
        {

            theHelper.InsertOperationDetails(OperationId,txtOperationName.Text, ddlOTTYpe.SelectedValue, txtCost.Text, txtSummary.Text, txtDuration.Text, Session["userName"].ToString(), Session["CoCode"].ToString(), "0");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            theHelper.UpdateOperationDetails(HiddenField1.Value, txtOperationName.Text, ddlOTTYpe.SelectedValue, txtCost.Text, txtSummary.Text, txtDuration.Text, Session["CoCode"].ToString(),"0");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            btnSubmit.Text = "Submit";
        }

        ResetAllFields();
        GridFill();
    }

    private void ResetAllFields()
    {
        txtOperationName.Text = "";
        ddlOTTYpe.SelectedIndex = -1;
        txtCost.Text = "";
        txtSummary.Text = "";
        txtDuration.Text = "";
        btnSubmit.Text = "Submit";
    }
}