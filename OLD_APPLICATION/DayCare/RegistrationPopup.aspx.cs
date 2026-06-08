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
using System.Globalization;
 
public partial class Pathology_RegistrationPopup : System.Web.UI.Page
{
    DC_RegistrationPopup thereg = new DC_RegistrationPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["userName"] == null)
        //{
        //    Response.Redirect("../LoginPage.aspx");
        //}
        Page.Title = "Registration Popup";

     //   ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "GetDatetime();", true);
        if (!IsPostBack)
        {
          
            GridFill();
            txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    public void GridFill()
    {
        string  testdate = "";
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        if (txtdate.Text != "")
            testdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", dtf).ToString();
        else
            testdate = "";


        GridView1.DataSource = thereg.RegistrationPopup(Session["CoCode"].ToString().Trim(), testdate, txtname.Text.Trim(), txtph.Text.Trim(), txtaddress.Text.Trim());
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill();
    }
   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblregno = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblregno");
        HiddenField1.Value = lblregno.Text;
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