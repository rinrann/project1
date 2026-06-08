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
using System.Globalization;
 
public partial class Pathology_ReagentRefund : System.Web.UI.Page
{
    PH_ReagentRefund thereagentrefund = new PH_ReagentRefund(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        GridridFill();
        Page.Title = "Reagent Refund";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            Tab1Func();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

      
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

        DateTime testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf);
        if (Button1.Text == "Submit")
        {
            thereagentrefund.InsertReagentRefund(txtid.Text, testdate.ToString(), txtpurchaseqty.Text, txtprice.Text, Session["userId"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted Successfully !');", true);
        }
        else
        {
            thereagentrefund.UpdateRefund(TextBox4.Value,txtid.Text, testdate.ToString(), txtpurchaseqty.Text, txtprice.Text );
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
        }
        GridridFill();
        ResetAllFields();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    public void ResetAllFields()
    {
        txtdate.Text = ""; txtid.Text = ""; txtprice.Text = ""; txtpurchaseqty.Text = ""; Button1.Text = "Submit";
    }

    private void GridridFill()
    {
         GridView1.DataSource = thereagentrefund.GridFill();
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridridFill();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            Label lblcode = (Label)GridView1.Rows[index].FindControl("lblcode");
            TextBox4.Value = lblcode.Text;

            Label lblpurid = (Label)GridView1.Rows[index].FindControl("lblpurid");
            txtid.Text = lblpurid.Text;

            Label lbldate = (Label)GridView1.Rows[index].FindControl("lbldate");
            txtdate.Text = lbldate.Text;

            Label lblqty = (Label)GridView1.Rows[index].FindControl("lblqty");
            txtpurchaseqty.Text = lblqty.Text;

            Label lbltestper = (Label)GridView1.Rows[index].FindControl("lbltestper");
            txtprice.Text = lbltestper.Text;
            Tab1Func();
            Button1.Text = "Update";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         Label lblcode = (Label)GridView1.Rows[e.RowIndex].FindControl("lblcode");
        thereagentrefund.DeleteRefund(lblcode.Text);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        GridridFill();
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