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

public partial class DayCare_RegPopup : System.Web.UI.Page
{
    DC_RegistrationPopup thereg = new DC_RegistrationPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        Page.Title = "Registration Popup";
        GridFill();
    }
    public void GridFill()
    {
        GridView1.DataSource = thereg.GridRegistrationpopup(txtreg.Text.Trim(), txtname.Text.Trim(), txtph.Text.Trim(), txtaddress.Text.Trim());
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label AppNo = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("AppNo");
        HiddenField1.Value = AppNo.Text;
        Label lblName = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblName");
        Label lblladd = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblladd");
        HiddenField2.Value = lblName.Text + "#" + lblladd.Text;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
}