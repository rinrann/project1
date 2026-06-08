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

public partial class Account_DocRegistrationPopup : System.Web.UI.Page
{

    DC_DocRegistrationPopup theDocHelper = new DC_DocRegistrationPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        Page.Title = "Registration Popup";
        if (!IsPostBack)
        {
            DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
            DropDownList1.Items.Insert(1, new ListItem("Doctor", "D"));
            DropDownList1.Items.Insert(2, new ListItem("Referrer ", "Q"));
            DropDownList1.SelectedValue = "D";
        }
        GridFill();
    }

    public void GridFill()
    {
        GridView1.DataSource = theDocHelper.GridPopup(txtname.Text.Trim(), DropDownList1.SelectedValue);
        GridView1.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblid = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid");
        HiddenField1.Value = lblid.Text;
        Label lblName = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblName");
        Label lbladd = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbladd");
        HiddenField2.Value = lblName.Text + "#" + lbladd.Text + "#" + DropDownList1.SelectedValue;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
}