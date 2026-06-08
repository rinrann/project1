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

public partial class OPD_DayOff : System.Web.UI.Page
{
    DayOff thechemicallist = new  DayOff(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Day Off";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        GridFill();
        if (!IsPostBack)
        {
            DropDownFill();
            Tab1Func();
        }


    }


    private void DropDownFill()
    {
        this.DropDownList1.DataSource = thechemicallist.DropDownDoctor();
        this.DropDownList1.DataTextField = "doc_name";
        this.DropDownList1.DataValueField = "doc_id";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    private void ResetAllFields()
    {
        TextBox1.Text = ""; TextBox3.Text = "";
        DropDownList1.SelectedIndex = 0;
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

            Label lblDayoffDay1 = (Label)GridView1.Rows[index].FindControl("lblDayoffDay1");
            TextBox1.Text = lblDayoffDay1.Text;

            Label lblDayoffDay2 = (Label)GridView1.Rows[index].FindControl("lblDayoffDay2");
            TextBox3.Text = lblDayoffDay2.Text;

            Label lbldocid = (Label)GridView1.Rows[index].FindControl("lbldocid");
            DropDownList1.SelectedValue = lbldocid.Text;

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
        DateTime testdate1 = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", dtf);

        if (Button1.Text == "Submit")
        {

            if (thechemicallist.InsertDayOff(DropDownList1.SelectedValue, testdate.ToString(), testdate1.ToString(), TextBox2.Text) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Inserted  Successfully !');", true);
                ResetAllFields();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Inserted Data !');", true);
            }


        }
        else
        {

            if (thechemicallist.UpdateDayOff(HiddenField1.Value, DropDownList1.SelectedValue, testdate.ToString(), testdate1.ToString(), TextBox2.Text) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated  Successfully !');", true);
                ResetAllFields();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }
            Button1.Text = "Submit";
        }

        GridFill();
       
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