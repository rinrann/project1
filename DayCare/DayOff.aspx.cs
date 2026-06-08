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

public partial class DayCare_DayOff : System.Web.UI.Page
{
    DC_DayOff thechemicallist = new DC_DayOff(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Day Off";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            Tab1Func();
            GridFill();
        }


    }
    private void ResetAllFields()
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        HiddenField1.Value = "";
        Button1.Text = "Submit";

    }
    private void GridFill()
    {
        GridView1.DataSource = thechemicallist.GridDayoff();
        GridView1.DataBind();
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
            lblError.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblID = (Label)GridView1.Rows[index].FindControl("lblID");
            HiddenField1.Value = lblID.Text;

            Label lblDayoffDay = (Label)GridView1.Rows[index].FindControl("lblDayoffDay");
            TextBox1.Text = lblDayoffDay.Text;

            Label lblDayoffReason = (Label)GridView1.Rows[index].FindControl("lblDayoffReason");
            TextBox2.Text = lblDayoffReason.Text;
            Tab1Func();
            Button1.Text = "Update";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblID");
        if (thechemicallist.DeleteDayOff(lblID.Text) == true)
        {
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Deleted Successfully";
        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Error Deleted Data";
        }
        GridFill();
        ResetAllFields();
    }

 

    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", dtf);

        if (Button1.Text == "Submit")
        {

            if (thechemicallist.InsertDayOff(testdate.ToString(), TextBox2.Text) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
                ResetAllFields();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error Insered Data !');", true);
            }


        }
        else
        {

            if (thechemicallist.UpdateDayOff(HiddenField1.Value, testdate.ToString(), TextBox2.Text) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
                GridView1.SelectedIndex = -1;
                ResetAllFields();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error Updated Data !');", true);
            }
            Button1.Text = "Submit";
        }

        GridFill();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ResetAllFields();
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1Func();
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex =1;
    }
    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
}