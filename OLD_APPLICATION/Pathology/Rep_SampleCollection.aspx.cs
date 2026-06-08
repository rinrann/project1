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
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;

public partial class Rep_SampleCollection : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_Report objreport = new PH_Report(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "Sample wise Collection Register", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            ddlindi.DataSource = objreport.getsampleddl();
            ddlindi.DataValueField = "scode";
            ddlindi.DataTextField = "sname";    
            ddlindi.DataBind();

            if (rbl.SelectedValue == "Y")
                tr_indi.Visible = false;
            else tr_indi.Visible = true;
        }
    }
   
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HomePage.aspx");
    }
    protected void btnproceed_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        DateTime from = DateTime.ParseExact(txtfromdt.Text, "dd/MM/yyyy", dtf);
        DateTime to = DateTime.ParseExact(txttodt.Text, "dd/MM/yyyy", dtf);
        string sample = ddlindi.SelectedValue;
        Session["dt"] = objreport.GetSamplecollection(from, to, sample, rbl.SelectedValue);
        Session["from"] = txtfromdt.Text;
        Session["to"] = txttodt.Text;
        Response.Redirect("View_SampleCollection.aspx");
    }
    protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl.SelectedValue == "Y")
            tr_indi.Visible = false;
        else tr_indi.Visible = true;
    }
}