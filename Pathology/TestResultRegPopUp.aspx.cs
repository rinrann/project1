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

 
public partial class Pathology_TestResultRegPopUp : System.Web.UI.Page
{
    PH_TestResultRegPopUp thereg = new PH_TestResultRegPopUp(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        string path = HttpContext.Current.Request.Url.Query;
        string[] arr = path.Split('?');
        string testID;
        if (arr.Length > 1 && Request.QueryString["TestID"] != "")
        {
            testID = Request.QueryString["TestID"].ToString();
            hdntype.Value = testID;
        }
        else { testID = ""; }
        ViewState["TestID"] = testID;
        GridFill(testID);
    }
    public void GridFill(string testid)
    {

        GridView1.DataSource = thereg.GridPopup(Session["CoCode"].ToString().Trim(), Session["YearCode"].ToString().Trim(), txtreg.Text, txtname.Text, txtph.Text, txtaddress.Text, testid);
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill(ViewState["TestID"].ToString());
    }

   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        Label lblregno = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblregno");
        HiddenField1.Value = lblregno.Text;
        Label lblreqno = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblreqno");
        HiddenField2.Value = lblreqno.Text;
       
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill(ViewState["TestID"].ToString());
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}