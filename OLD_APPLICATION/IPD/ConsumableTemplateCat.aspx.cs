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

public partial class IPD_ConsumableTemplateCat : System.Web.UI.Page
{

    ConsumableTemplate theshift = new ConsumableTemplate(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
          if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
        }
        Page.Title = "Consumable Template Category";


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string id;
        if (TextBox4.Value != "")
        {
            id = TextBox4.Value;
        }
        else
        {
            id = "0";
        }
        if (Button1.Text == "Submit")
        {
            //dibynedu
            if (theshift.CategoryFunction(1, id, TextBox1.Text) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            }
            else
            {
                // dibynedu
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
            }
        }
        else
        {
            //dibyendu
            if (theshift.CategoryFunction(2, id, TextBox1.Text) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);

                Button1.Text = "Submit";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
        }

        GridFill();
        ResetAllFields();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    private void GridFill()
    {


        GridView1.SelectedIndex = -1;
        GridView1.DataSource = theshift.GridFill();
        GridView1.DataBind();
    }
    private void ResetAllFields()
    {
        TextBox1.Text = "";
        TextBox4.Value = "";
        Button1.Text = "Submit";
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
            Label TemplateCategoryId = (Label)GridView1.Rows[index].FindControl("TemplateCategoryId");
            TextBox4.Value = TemplateCategoryId.Text;

            Label CategoryName = (Label)GridView1.Rows[index].FindControl("CategoryName");
            TextBox1.Text = CategoryName.Text;

            Tab1Func();
            Button1.Text = "Update";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label TemplateCategoryId = (Label)GridView1.Rows[e.RowIndex].FindControl("TemplateCategoryId");
        if (theshift.CategoryFunction(3, TemplateCategoryId.Text, TextBox1.Text) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deleted Successfully !');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Deleted Data !');", true);
        }
        GridFill();
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