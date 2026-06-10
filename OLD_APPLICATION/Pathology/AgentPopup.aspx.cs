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
 

public partial class Pathology_AgentPopup : System.Web.UI.Page
{
    PH_AgentPopup thereagent = new PH_AgentPopup(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        GridFill();
        if (Session["userName"] == null)
        {
            Response.Redirect("../LoginPage.aspx");
        }
    }
    public void GridFill()
    {
         GridView_popup.DataSource = thereagent.GetGrid(txtname.Text,txtdate.Text);
        GridView_popup.DataBind();
    }
    protected void GridView_popup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lblrname = (Label)GridView_popup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrname");
        HiddenField1.Value = lblrname.Text;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_popup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_popup.PageIndex = e.NewPageIndex;
        GridFill();
    }
}