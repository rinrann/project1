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
 
public partial class Master_ServiceCategory : System.Web.UI.Page
{
    ServiceCategory theHelper = new ServiceCategory(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Service Template Name";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            GridFill();
            Tab1Func();
            txtServiceCatId.Text = theHelper.GetServiceID().ToString();
        }
    }
    private void GridFill()
    {
         GridView1.DataSource = theHelper.GetAllService();
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        txtServiceCatId.Text = "";
        txtServiceCatName.Text = "";
        txtServiceCharge.Text = "";
        Button1.Text = "Submit";
        txtServiceCatId.Text = theHelper.GetServiceID().ToString();
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
            GridView1.Rows[index].BackColor = Color.GreenYellow;

            Label lblServiceCategoryID = (Label)GridView1.Rows[index].FindControl("ServiceCategoryID");
            txtServiceCatId.Text = lblServiceCategoryID.Text;
            HiddenField1.Value = lblServiceCategoryID.Text;

            Label lblServiceCategoryName = (Label)GridView1.Rows[index].FindControl("ServiceCategoryName");
            txtServiceCatName.Text = lblServiceCategoryName.Text;

            Label ServiceCharge = (Label)GridView1.Rows[index].FindControl("ServiceCharge");
            txtServiceCharge.Text = ServiceCharge.Text;
            Tab1Func();
            Button1.Text = "Update";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblServiceCategoryID = (Label)GridView1.Rows[e.RowIndex].FindControl("ServiceCategoryID");
        theHelper.DeleteServiceCategory(Convert.ToInt32(lblServiceCategoryID.Text));
        lblError.ForeColor = System.Drawing.Color.Green;
        lblError.Text = "Deleted Successfully";
        GridFill();
        ResetAllFields();
        txtServiceCatId.Text = theHelper.GetServiceID().ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
         if (Button1.Text == "Submit")
        {
            theHelper.InsertServiceCategory(txtServiceCatName.Text, txtServiceCharge.Text, Session["userName"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
        }
        else
        {
            theHelper.UpdateServiceCategory(HiddenField1.Value,txtServiceCharge.Text, txtServiceCatName.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
            Button1.Text = "Submit";
        }

        GridView1.DataSource = theHelper.GetAllService();
        GridView1.DataBind();
        ResetAllFields();
        txtServiceCatId.Text = theHelper.GetServiceID().ToString();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
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
}