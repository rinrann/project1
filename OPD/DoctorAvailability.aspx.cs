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
using System.Web.Services;
using System.Globalization;

public partial class OPD_DoctorAvailability : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    DoctorAvailablity thehelper = new DoctorAvailablity(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    System.Text.StringBuilder rpt = new System.Text.StringBuilder();
    String Date = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Doctor's Availability";
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
         if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DOCTOR AVAILABILITY", checkAccessType.ViewAction) == false)
         {
             Response.Redirect("../AccessDenied.aspx");
         }

         if (!IsPostBack)
         {
             dropDownfill();
             txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
             txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
         }
    }

    public void dropDownfill()
    {
        ddldiscpline.Items.Clear();
        ddldiscpline.DataSource = thehelper.GetDiscpline(Session["CoCode"].ToString().Trim());
        ddldiscpline.DataTextField = "TypeName";
        ddldiscpline.DataValueField = "DocTypeId";
        ddldiscpline.DataBind();
        ddldiscpline.Items.Insert(0, new ListItem("--Select--", ""));
    }

    protected void ddldiscpline_SelectedIndexChanged(object sender, EventArgs e)
    {
        string discpline = ddldiscpline.SelectedValue.ToString();
        ddlDoc.Items.Clear();
        ddlDoc.DataSource = thehelper.GetDoctors(discpline, Session["CoCode"].ToString().Trim());
        ddlDoc.DataTextField = "Name";
        ddlDoc.DataValueField = "Id";
        ddlDoc.DataBind();
        ddlDoc.Items.Insert(0, new ListItem("--Select--", ""));

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        //QuackGridFill();
    }

    private void GridFill()
    {
        string discpline = ddldiscpline.SelectedValue.ToString();
        string doctor = ddlDoc.SelectedValue.ToString();
        string date1, date2;
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();

        if (txtFromDate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", dtf);
            date1 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date1 = "null";
        }
        if (txtTodate.Text != "")
        {
            DateTime testdate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", dtf);
            date2 = testdate.ToString("yyyy-MM-dd");
        }
        else
        {
            date2 = "null";
        }

        GridView1.DataSource = thehelper.GetDoctorAvailabilityDtls(Session["CoCode"].ToString().Trim(), discpline, doctor,date1,date2);
        GridView1.DataBind();
    }
}