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

public partial class Rep_InvestGrpwisecollection : System.Web.UI.Page
{
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    PH_Report objreport = new PH_Report(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "INVESTIGATION GROUP WISE COLLECTION", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            txtfromdt.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txttodt.Text = DateTime.Now.ToString("yyyy-MM-dd");
            trGrp.Visible = true;
        }
    }
    protected void lnktab1_Click(object sender, EventArgs e)
    {
        trGrp.Visible = true; 
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HomePage.aspx");
    }
    protected void btnproceed_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        
        string grp = InvGroup.GetSelectedCodetild();
        DataSet ds = new DataSet();
        ds = objreport.GetCollectionRegister(txtfromdt.Text, txttodt.Text, grp);
        Session["ds"] = ds;
        Response.Redirect("View_InvestGrpwisecollection.aspx");

    }
}