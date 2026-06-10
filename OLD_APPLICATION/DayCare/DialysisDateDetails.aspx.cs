using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class DayCare_DialysisDateDetails : System.Web.UI.Page
{
    DC_DialysisPopup thedia = new DC_DialysisPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Date Wise Dialysis";
        Label2.Visible = false;

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (TextBox2.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Please Select Registration No');", true);
            GridView2.Visible = false;
        }
        else
        {
            GridFill();
        }
    }

    private void GridFill()
    {

        DataTable dt = thedia.gridviewfill(TextBox2.Text);
        //DataTable dt = thedia.gridviewfill(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), TextBox2.Text);
        //GridView2.DataSource = dt;
        //GridView2.DataBind();
        if (dt.Rows.Count > 0)
        {
            GridView2.Visible = true;
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        else
        {
            //Label2.Visible = true;
            //Label2.Text = "There Is No data To Found";
            //Label2.ForeColor = System.Drawing.Color.Red;
            GridView2.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('There Is No data To Found');", true);
        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("PatientListSearch.aspx");
        
    }
    

}