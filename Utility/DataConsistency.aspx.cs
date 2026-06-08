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
using System.Globalization;

public partial class Master_DataConsistency : System.Web.UI.Page
{
    DataConsistency theHelper = new DataConsistency(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    Main Objmain = new Main(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Data Consistency";
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DATA CONSISTENCY", checkAccessType.ViewAction) == false)
        {
            Response.Redirect("../AccessDenied.aspx");
        }

        if (Objmain.checkaccss(Session["CoCode"].ToString(), Session["UserRoleID"].ToString().Trim(), "DATA CONSISTENCY", checkAccessType.InsertAction) == false)
        {
            btnSubmit.Enabled = false;
        }
        if (!IsPostBack)
        {
            
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtf = new DateTimeFormatInfo();
        string dt = DateTime.Now.ToString("yyyy-MM-dd");
        theHelper.process(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), dt, Session["userName"].ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Process Successfully Completed!');", true);
    }
}