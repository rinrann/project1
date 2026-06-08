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
 

public partial class IPD_AmbulanceCharge : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    AmbulanceCharge thereagentEntry = new AmbulanceCharge(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    String Date = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Ambulance Charge";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "AMBULANCE SERVICE", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "AMBULANCE SERVICE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }

        if (!IsPostBack)
        {
            Tab1Func();
        }

      }

    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime testdate = DateTime.ParseExact(TextBox5.Text, "dd/MM/yyyy", dtf);
         if (Button1.Text == "Submit")
        {

            if (thereagentEntry.InsertAmbulance(TextBox23.Text, TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, testdate.ToString(), Session["CoCode"].ToString(), Session["userName"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Insered Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Insered Data !');", true);
            }
        }
        else
        {

            if (thereagentEntry.UpdateAmbulance(HiddenField1.Value, TextBox23.Text, TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, testdate.ToString(), Session["CoCode"].ToString(), Session["userName"].ToString().Trim()) == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Updated Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Updated Data !');", true);
            }

        }
        GridFill();
        ResetAllFields();
    }

    private void GridFill()
    {

        GridView1.DataSource = thereagentEntry.GridFill(TextBox23.Text, Session["CoCode"].ToString());
        GridView1.DataBind();
      }

    public void ResetAllFields()
    {
        HiddenField1.Value = "";
        TextBox1.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; TextBox4.Text = ""; TextBox5.Text = "";
        TextBox23.Text = ""; TextBox24.Text = ""; TextBox25.Text = ""; TextBox26.Text = "";
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "AMBULANCE SERVICE", checkAccessType.InsertAction) == false)
        {
            Button1.Enabled = false;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            ResetAllFields();
            int index = Convert.ToInt32(e.CommandArgument);
            lblError.Text = "";
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            HiddenField1.Value = lblid.Text;

            Label lblRegno = (Label)GridView1.Rows[index].FindControl("lblRegno");
            TextBox23.Text = lblRegno.Text;


            Label lbllname = (Label)GridView1.Rows[index].FindControl("lbllname");
            TextBox24.Text = lbllname.Text;


            Label lbladate = (Label)GridView1.Rows[index].FindControl("lbladate");
            TextBox26.Text = lbladate.Text;


            Label lblbedno = (Label)GridView1.Rows[index].FindControl("lblbedno");
            TextBox25.Text = lblbedno.Text;


            Label lblserivcecat = (Label)GridView1.Rows[index].FindControl("lblserivcecat");
            TextBox1.Text = lblserivcecat.Text;


            Label lblservice = (Label)GridView1.Rows[index].FindControl("lblservice");
            TextBox2.Text = lblservice.Text;

            Label lbllblissuedate = (Label)GridView1.Rows[index].FindControl("lbllblissuedate");
            TextBox3.Text = lbllblissuedate.Text;


            Label lblquantity = (Label)GridView1.Rows[index].FindControl("lblquantity");
            TextBox4.Text = lblquantity.Text;

            Label lbladvicedby = (Label)GridView1.Rows[index].FindControl("lbladvicedby");
            TextBox5.Text = lbladvicedby.Text;
            Tab1Func();
            Button1.Text = "Update";
            if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "AMBULANCE SERVICE", checkAccessType.UpdateAction) == false)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dt= thereagentEntry.PatientDtls(TextBox23.Text);
       TextBox24.Text = dt.Rows[0]["patient_name"].ToString();
       TextBox25.Text = dt.Rows[0]["BedNoText"].ToString();
       TextBox26.Text = dt.Rows[0]["adate"].ToString();
       GridFill();
      
    }
    protected void Button4_Click(object sender, EventArgs e)
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
        MainView.ActiveViewIndex = 1;
    }

    public void Tab1Func()
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
}