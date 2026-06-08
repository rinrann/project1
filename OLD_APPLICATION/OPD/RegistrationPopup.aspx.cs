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

 
public partial class Pathology_RegistrationPopup : System.Web.UI.Page
{
    DoseMaster thereg = new DoseMaster(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
        string path = HttpContext.Current.Request.Url.Query;
        string[] arr = path.Split('?');
        string type;
        if (arr.Length > 1 && Request.QueryString["type"] != "")
        {
            type = Request.QueryString["type"].ToString();
            hdntype.Value = type;
        }
        else { type = ""; }
        ViewState["type"] = type;
        GridFill(type);
    }
    public void GridFill(string PopupType)
    {
        GridView1.DataSource = thereg.GridPopup(Session["CoCode"].ToString().Trim(), txtreg.Text.Trim(), txtname.Text.Trim(), txtph.Text.Trim(), txtaddress.Text.Trim(), PopupType);
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridFill(ViewState["type"].ToString().Trim());
    }
   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblregno = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblregno");
        HiddenField1.Value = lblregno.Text;
        Label lblappno = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblappno");
        HiddenField3.Value = lblappno.Text;
        Label lblName = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblName");
        Label lblladd = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblladd");
        HiddenField2.Value = lblName.Text + "#" + lblladd.Text;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill(ViewState["type"].ToString().Trim());
    }
}